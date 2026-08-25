using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

public class UpdateInfo {
    public string version { get; set; }
    public string installerUrl { get; set; }
    public string notes { get; set; }
}

public static class Updater {
    private static readonly HttpClient _http = new HttpClient();

    public static Version GetLocalVersion() {
        var asm = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        return asm.GetName().Version ?? new Version(0, 0, 0, 0);
    }

    public static async Task CheckAndInstallIfAvailableAsync(string metadataUrl, string version, bool autoInstall = true) {
        try {
            // Delay opcional para dejar que el sistema termine de iniciarse
            await Task.Delay(TimeSpan.FromSeconds(5));

            var json = await _http.GetStringAsync(metadataUrl);
            var info = JsonConvert.DeserializeObject<UpdateInfo>(json);
            if (info == null || string.IsNullOrWhiteSpace(info.version) || string.IsNullOrWhiteSpace(info.installerUrl))
                return;

            var remote = new Version(info.version);
            var local = GetLocalVersion();
            if (remote <= local) return;

            // Si no autoInstall, mostrar notificación/preguntar al usuario aquí
            if (!autoInstall) {
                // mostrar notificación en tray: "Actualización disponible"
                return;
            }

            // Descargar installer a temp
            var tmpFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(new Uri(info.installerUrl).LocalPath));
            using (var resp = await _http.GetAsync(info.installerUrl)) {
                resp.EnsureSuccessStatusCode();
                var bytes = await resp.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes(tmpFile, bytes);
            }

            // Ejecutar instalador con elevación; usar flags de tu instalador (Inno Setup example)
            var args = "/VERYSILENT /SUPPRESSMSGBOXES /NORESTART";
            var psi = new ProcessStartInfo(tmpFile, args) {
                UseShellExecute = true,
                Verb = "runas" // fuerza UAC
            };
            Process.Start(psi);

            // Cerrar la app para permitir actualización
            Application.Current.Dispatcher.Invoke(() => {
                try { Application.Current.Shutdown(); } catch { Environment.Exit(0); }
            });
        } catch {
            // log/ignorar errores de red
        }
    }

    //public static async Task DownloadAndRunInstallerAsync(string url) {
    //    var tmp = Path.Combine(Path.GetTempPath(), "SamanthaDJ_Update.exe");
    //    using (var http = new HttpClient()) {
    //        var data = await http.GetByteArrayAsync(url);
    //        await File.WriteAllBytesAsync(tmp, data);
    //    }
    //    var pi = new ProcessStartInfo(tmp, "/VERYSILENT /SUPPRESSMSGBOXES /NORESTART") { UseShellExecute = true, Verb = "runas" };
    //    Process.Start(pi);
    //    Application.Current.Shutdown(); // si necesita cerrarse antes de actualizar
    //}
}