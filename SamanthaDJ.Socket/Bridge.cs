using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SamanthaDJ.Socket {

    public class Bridge {
        private readonly IPAddress _tcpAddress;
        private readonly int _tcpPort;
        private readonly string _httpPrefix; // para WebSocket via HttpListener
        private TcpListener _tcpListener;
        private HttpListener _httpListener;
        private readonly List<TcpClient> _tcpClients = new List<TcpClient>();
        private readonly List<System.Net.WebSockets.WebSocket> _webSockets = new List<System.Net.WebSockets.WebSocket>();
        private CancellationTokenSource _cts;

        #region Public Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler EventHandleTcpClient;

        #endregion Public Events

        public Bridge(string tcpAddress = "127.0.0.1", int tcpPort = 5004, string httpPrefix = "http://localhost:5003/") {
            _tcpAddress = IPAddress.Parse(tcpAddress);
            _tcpPort = tcpPort;
            _httpPrefix = httpPrefix;
        }

        public void Start() {
            _cts = new CancellationTokenSource();
            StartTcpListener(_cts.Token);
            StartWebSocketListener(_cts.Token);
        }

        public void Stop() {
            try { _cts?.Cancel(); } catch { }
            try {
                lock (_tcpClients) {
                    foreach (var c in _tcpClients) {
                        try { c.Close(); } catch { }
                    }
                    _tcpClients.Clear();
                }
            } catch { }

            try {
                lock (_webSockets) {
                    foreach (var ws in _webSockets) {
                        try { ws.Abort(); } catch { }
                    }
                    _webSockets.Clear();
                }
            } catch { }

            try { _tcpListener?.Stop(); } catch { }
            try { _httpListener?.Stop(); } catch { }
        }

        private void StartTcpListener(CancellationToken ct) {
            _tcpListener = new TcpListener(_tcpAddress, _tcpPort);
            _tcpListener.Start();
            Task.Run(async () => {
                while (!ct.IsCancellationRequested) {
                    TcpClient client = null;
                    try {
                        client = await _tcpListener.AcceptTcpClientAsync();
                    } catch {
                        break;
                    }
                    lock (_tcpClients) { _tcpClients.Add(client); }
                    _ = Task.Run(() => HandleTcpClientAsync(client, ct), ct);
                }
            }, ct);
        }

        private async Task HandleTcpClientAsync(TcpClient client, CancellationToken ct) {
            var stream = client.GetStream();
            var buffer = new byte[1024];
            try {
                // Opcional: leer datos entrantes si el cliente envía comandos
                while (!ct.IsCancellationRequested && client.Connected) {
                    if (stream.DataAvailable) {
                        int read = await stream.ReadAsync(buffer, 0, buffer.Length, ct);
                        if (read == 0) break;
                        // Ignorar contenido o podrías implementar comandos
                    } else {
                        await Task.Delay(200, ct);
                    }
                }
            } catch { }
            finally {
                try { client.Close(); } catch { }
                lock (_tcpClients) { _tcpClients.Remove(client); }
            }
        }

        private void StartWebSocketListener(CancellationToken ct) {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(_httpPrefix);
            try { _httpListener.Start(); } catch {
                // Si falla (permiso), caller debe manejarlo
                return;
            }

            Task.Run(async () => {
                while (!ct.IsCancellationRequested && _httpListener.IsListening) {
                    HttpListenerContext ctx = null;
                    try {
                        ctx = await _httpListener.GetContextAsync();
                    } catch {
                        break;
                    }

                    if (ctx.Request.IsWebSocketRequest) {
                        HttpListenerWebSocketContext wsContext = null;
                        try {
                            wsContext = await ctx.AcceptWebSocketAsync(null);
                            var ws = wsContext.WebSocket;
                            lock (_webSockets) { _webSockets.Add(ws); }
                            _ = Task.Run(() => HandleWebSocketAsync(ws, ct), ct);
                        } catch {
                            try { ctx.Response.StatusCode = 500; ctx.Response.Close(); } catch { }
                        }
                    } else {
                        // Petición no WebSocket — responder simple
                        ctx.Response.StatusCode = 400;
                        using (var sw = new StreamWriter(ctx.Response.OutputStream)) {
                            sw.Write("WebSocket endpoint");
                        }
                        ctx.Response.Close();
                    }
                }
            }, ct);
        }

        private async Task HandleWebSocketAsync(System.Net.WebSockets.WebSocket ws, CancellationToken ct) {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            try {
                while (ws.State == WebSocketState.Open && !ct.IsCancellationRequested) {
                    var result = await ws.ReceiveAsync(buffer, ct);
                    if (result.MessageType == WebSocketMessageType.Close) break;

                    string cadenaUtf8 = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    EventHandleTcpClient(cadenaUtf8, null);
                }
            } catch { }
            finally {
                try { ws.Abort(); } catch { }
                lock (_webSockets) { _webSockets.Remove(ws); }
            }
        }

        public void Broadcast(string eventName, string payloadJson) {
            var msg = $"{{\"EventBridge\":\"{eventName}\",\"Data\":{payloadJson}}}";

            // Enviar a clientes TCP
            byte[] tcpBytes = Encoding.UTF8.GetBytes(msg + "\n");
            lock (_tcpClients) {
                foreach (var client in _tcpClients.ToArray()) {
                    try {
                        if (client?.Connected == true) {
                            var stream = client.GetStream();
                            stream.Write(tcpBytes, 0, tcpBytes.Length);
                            stream.Flush();
                        } else {
                            client.Close();
                            _tcpClients.Remove(client);
                        }
                    } catch {
                        try { client.Close(); } catch { }
                        _tcpClients.Remove(client);
                    }
                }
            }

            // Enviar a WebSocket clients
            var wsBytes = Encoding.UTF8.GetBytes(msg);
            ArraySegment<byte> wsSegment = new ArraySegment<byte>(wsBytes);
            lock (_webSockets) {
                foreach (var ws in _webSockets.ToArray()) {
                    try {
                        if (ws != null && ws.State == WebSocketState.Open) {
                            ws.SendAsync(wsSegment, WebSocketMessageType.Text, true, CancellationToken.None).Wait();
                        } else {
                            try { ws.Abort(); } catch { }
                            _webSockets.Remove(ws);
                        }
                    } catch {
                        try { ws.Abort(); } catch { }
                        _webSockets.Remove(ws);
                    }
                }
            }
        }
    }
}