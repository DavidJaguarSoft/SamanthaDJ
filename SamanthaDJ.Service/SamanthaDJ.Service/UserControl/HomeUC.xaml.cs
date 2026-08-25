using Newtonsoft.Json;
using SamanthaDJ.Interface;
using SamanthaDJ.Interface.Model;
using SamanthaDJ.ServiceWPF.Models;
using SamanthaDJ.ServiceWPF.Tools;
using SamanthaDJ.ServiceWPF.ViewModels;
using SamanthaDJ.Socket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WForms = System.Windows.Forms;

namespace SamanthaDJ.ServiceWPF.UserControl {

    /// <summary>
    /// Lógica de interacción para HomeUC.xaml
    /// </summary>
    public partial class HomeUC : System.Windows.Controls.UserControl {

        #region Attributes

        //private readonly WForms.NotifyIcon _notifyIcon;
        //private SocketBridge _socketBridge;
        private Bridge _socketBridge;
        Paragraph _paragraphLog = new Paragraph();
        private string _LogFileName = "Monitor";
        private int _ticksSamanthaListen = 0;

        #endregion Attributes

        #region Variables

        private SerialPort _serialPort = new SerialPort();
        BackgroundWorker _worker;
        System.Windows.Threading.DispatcherTimer timerSamanthaListenig = new System.Windows.Threading.DispatcherTimer();

        #endregion Variables

        #region Events

        public event EventHandler Event_GoParametersConfiguration;
        public event EventHandler Event_WindowMinimize;
        public event EventHandler Event_WindowHide;

        #endregion Events

        #region Constructors

        public HomeUC() {
            InitializeComponent();

            #region BackgroundWorker Configuration

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true; // Habilitar reportes de progreso [1]
            _worker.WorkerSupportsCancellation = true; // Habilitar cancelación [3]

            // Registrar eventos
            _worker.DoWork += Worker_DoWork;
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            #endregion BackgroundWorker Configuration

            this.TextBlockSamanthaListening.Text = "Say Samantha";
            this.TextBlockSamanthaListening.Foreground = System.Windows.Media.Brushes.Blue;
            this.TextBlockSamanthaListening.FontWeight = FontWeights.Normal;
            //_notifyIcon = new WForms.NotifyIcon();
            //InitialiceNotify();
        }

        #endregion Constructors

        #region UserControl Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {

            //this.DataContext = new NotifyViewModel(_notifyIcon);
            bool voideInitialized = false;
            string strSpeechText = string.Empty;
            try {

                #region Samantha Config File

                FileJsonSDJ samanthaFileInfo = new FileJsonSDJ();
                using (StreamReader r = new StreamReader(AppContext.BaseDirectory + "\\SamanthaConfig.json")) {
                    string json = r.ReadToEnd();
                    samanthaFileInfo = JsonConvert.DeserializeObject<FileJsonSDJ>(json);
                }
                this.textBlockUser.Text = samanthaFileInfo.Credential.Username;
                this.lblCulture.Text = samanthaFileInfo.Culture.CultureInfo;
                this.lblUICulture.Text = samanthaFileInfo.Culture.UICultureInfo;
                this.lblCultureSpeech.Text = samanthaFileInfo.Culture.CultureSpeech;

                #region Ticks Samantha Listens

                string ticks = samanthaFileInfo.RecognitionFactor.TickSamanthaListens;
                _ticksSamanthaListen = 0;
                if (int.TryParse(ticks, out _ticksSamanthaListen)) {
                    if (_ticksSamanthaListen < 20 || _ticksSamanthaListen > 100) {
                        _ticksSamanthaListen = 35;
                    }
                } else {
                    _ticksSamanthaListen = 35;
                }
                this.progressBarListening.Visibility = Visibility.Hidden;
                this.progressBarListening.Minimum = 0;
                this.progressBarListening.Maximum = _ticksSamanthaListen;
                this.textBlockProgressBar.Visibility = Visibility.Hidden;

                this.textBlockTickListen.Text = _ticksSamanthaListen.ToString();

                #endregion Ticks Samantha Listens

                this.ButtonReconect.Visibility = Visibility.Hidden;
                this.textVersion.Text = $"Version: {Global.SamanthaDJServiceVersion}";
                this.textAllRightsReserved.Text = $"{Global.AllRighsReserved}";

                #endregion Samantha Config File

                timerSamanthaListenig.Tick += new EventHandler(SamanthaListeningTimer_Tick);
                timerSamanthaListenig.Interval = new TimeSpan(0, Convert.ToInt32(samanthaFileInfo.SamanthaVoice.SamanthaListenigTime), 0);

                #region Samantha Socket

                // Iniciar bridge: TCP tradicional en 127.0.0.1:5004 y WebSocket en http://localhost:5003/
                try {
                    _socketBridge = new Bridge("127.0.0.1", 5004, "http://localhost:5003/");
                    _socketBridge.EventHandleTcpClient += SamanthaSocket_EventHandleTcpClientFunction;
                    //Samantha.EventSamanthaListening += Samantha_EventSamanthaListening;
                    //Samantha.EventSpeechRecognized += Samantha_EventSpeechRecognized;
                    //Samantha.EventListenWithoutAttention += Samantha_EventListenWithoutAttention;
                    //Samantha.EventInstructionArmed += Samantha_EventInstructionArmed;
                    //Samantha.EventDetectedInstructions += Samantha_EventDetectedInstructions;
                    _socketBridge.Start();

                } catch (Exception ex) {
                    string exError = $"An error occurred while configuring the *Socket* service: {ex.Message}";
                    throw new Exception(exError);
                    //Screen Log(error);
                    //Samantha SpeechText("Ocurrió un error al configurar el servicio de Socket");
                }

                #endregion Samantha Socket

                #region Samantha

                if (!Samantha.SamanthaRun) {

                    Samantha.Username = samanthaFileInfo.Credential.Username;
                    Samantha.Token = samanthaFileInfo.Credential.Token;

                    Samantha.CultureInfo = samanthaFileInfo.Culture.CultureInfo;
                    Samantha.UICultureInfo = samanthaFileInfo.Culture.UICultureInfo;
                    Samantha.CultureSpeech = samanthaFileInfo.Culture.CultureSpeech;

                    Samantha.TickSamanthaListens = _ticksSamanthaListen;
                    Samantha.TicksSamAskWhatInstruccion = Convert.ToInt32(samanthaFileInfo.RecognitionFactor.TicksSamAskWhatInstruccion);
                    Samantha.SpeechRecognizedConfidence = Convert.ToDouble(samanthaFileInfo.RecognitionFactor.SpeechRecognizedConfidenceConfidence);

                    Samantha.TimerInterval = Convert.ToInt32(samanthaFileInfo.RecognitionFactor.TimerInterval);

                    Samantha.GrammarLoadMode = samanthaFileInfo.RecognitionFactor.GrammarLoadMode;

                    Samantha.SpeechSynthesizerVoice = samanthaFileInfo.SamanthaVoice.SpeechSynthesizerVoice;
                    Samantha.SpeechSynthesizerVolume = Convert.ToInt32(samanthaFileInfo.SamanthaVoice.SpeechSynthesizerVolume);
                    Samantha.SpeechSynthesizerRate = Convert.ToInt32(samanthaFileInfo.SamanthaVoice.SpeechSynthesizerRate);

                    Samantha.PathLog = samanthaFileInfo.Log.PathLog;
                    Samantha.GenerateParameterLog = Convert.ToBoolean(samanthaFileInfo.Log.GenerateLog);
                    Samantha.GenerateRunEventLog = Convert.ToBoolean(samanthaFileInfo.Log.GenerateRunEventLog);
                    Samantha.GenerateSpeechRecognizedEventLog = Convert.ToBoolean(samanthaFileInfo.Log.GenerateSpeechRecognizedEventLog);

                    Samantha.Initialize();

                    Samantha.EventTimeRemaining += new EventHandler(SC_TimeRemainingFunction);
                    Samantha.EventSamanthaListening += new EventHandler(SC_SamanthaListeningFunction);
                    Samantha.EventSpeechRecognized += new EventHandler(SC_SpeechRecognizedFunction);
                    Samantha.EventListenWithoutAttention += new EventHandler(SC_ListenWithoutAttentionFunction);
                    Samantha.EventInstructionArmed += new EventHandler(SC_InstructionArmedFunction);
                    Samantha.EventDetectedInstructions += new EventHandler(SC_EventDetectedInstructions);
                    Samantha.EventSpeechDetected += new EventHandler(SC_SpeechDetectedFunction);
                    Samantha.EventAudioStateChange += new EventHandler(SC_EventAudioStateChange);
                    Samantha.EventAudioSignalProblem += new EventHandler(SC_EventAudioSignalProblem);
                    Samantha.EventSamanthaUnused += new EventHandler(SC_EventSamanthaUnused);

                    voideInitialized = true;

                    Samantha.Run();

                    Samantha.SamanthaRun = true;
                }

                #endregion Samantha

                ScreenLog("<Samantha en línea y en espera de instrucciones>");
                strSpeechText = "<Samantha en línea y en espera de instrucciones>\n";

                #region Serial Port

                if (samanthaFileInfo.Arduino.Enable.Equals("true")) {
                    try {
                        _serialPort = new SerialPort();
                        _serialPort.PortName = "COM5";
                        _serialPort.BaudRate = 9600;
                        _serialPort.Open();
                    } catch (Exception exSerial) {
                        ScreenLog($"UserControl_Loaded. Error: {exSerial.Message}", ClientRequest.ERROR);
                        //strSpeechText += "Se detectó un problema en el puerto de datos.\n";
                    }
                }

                #endregion Serial Port

            } catch (Exception ex) {
                this.ButtonReconect.Visibility = Visibility.Visible;
                if (voideInitialized) {
                    ScreenLog($"UserControl_Loaded. Error: {ex.Message}", ClientRequest.ERROR);
                    if (ex.Message.Trim().ToUpper().Contains("USER")) {
                        strSpeechText += "Ocurrió un error. Verifique que su Usuario y su toquen sean válidos.\n";
                    } else {
                        strSpeechText +=
                            @"Ocurrió un error. Es probable que no se haya detectado la presencia de un micrófono.
                            Conecte un microfono a su PC y oprima el botón 'Reconectar Micrófono'.";
                    }
                } else {
                    WForms.MessageBox.Show($"An error ocurred: {ex.Message}");
                }
            }
            Samantha.SpeechText(strSpeechText);
        }

        #region Hyperlink_RequestNavigate

        /// <summary>
        ///     Send to Samantha Web's website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) {
            // Obtiene la URI del enlace
            string uri = e.Uri.AbsoluteUri;

            // Inicia el proceso del navegador predeterminado
            Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });

            // Marca el evento como manejado para evitar navegación interna
            e.Handled = true;
        }

        #endregion Hyperlink_RequestNavigate

        private void ButtonReconect_Click(object sender, RoutedEventArgs e) {
            if (Samantha.ReconnectAudio()) {
                this.ButtonReconect.Visibility = Visibility.Hidden;
                ScreenLog("<Samantha en línea y en espera de instrucciones>");
                Samantha.SpeechText("<Samantha en línea y en espera de instrucciones>");
            } else {
                ScreenLog("The device could not be reconnected", ClientRequest.ERROR);
                Samantha.SpeechText("No se pudo reconectar el dispositivo");
                //WForms.MessageBox.Show("The device could not be reconnected");
            }
        }

        private void ButtonLogClear_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(
                "Are you sure you want to delete log information ?",
                "Confirmation",                                   // Título
                MessageBoxButtons.YesNo,                          // Botones
                MessageBoxIcon.Question                           // Icono de pregunta
            );
            if (result == System.Windows.Forms.DialogResult.Yes) {
                this.RichTextLog.Document.Blocks.Clear();
                _paragraphLog = new Paragraph();
                WriteLogFile("<You cleaned the monitor screen>");
            }
        }

        private void ButtonConfiguration_Click(object sender, RoutedEventArgs e) {
            Event_GoParametersConfiguration(sender, e);
        }

        private void ButtonViewLogFiles_Click(object sender, RoutedEventArgs e) {
            //string ruta = @"C:\Temp";
            string yearMonth = DateTime.Now.ToString("yyyy-MM");
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            System.Diagnostics.Process.Start("explorer.exe", $"{Global.PathLogOut}\\{yearMonth}\\{today}");
        }

        private void ButtonRestartService_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(
                "Are you sure you want to restart the Application ?",
                "Confirmation",                                   // Título
                MessageBoxButtons.YesNo,                          // Botones
                MessageBoxIcon.Question                           // Icono de pregunta
            );
            if (result == System.Windows.Forms.DialogResult.Yes) {
                WriteLogFile("<You Restarted the Application>");

                // 1. Iniciar una nueva instancia de la aplicación
                //Process.Start(Application.ResourceAssembly.Location);
                // 2. Cerrar la instancia actual
                //Application.Current.Shutdown();
                
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void SamanthaListeningTimer_Tick(object sender, EventArgs e) {
            timerSamanthaListenig.Stop();
            Samantha.SamanthaListeningStop();
            this.SliderOnlyOneAndSamantha.IsSelectionRangeEnabled = false;
            this.SliderOnlyOneAndSamantha.Value = 0;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e) {
            Event_WindowMinimize(sender, e);
            //this.WindowState = WindowState.Minimized;
            //this.Hide();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        //    Event_WindowMinimize(sender, e);
        //    e.Cancel = true;
        //    //this.Hide();
        //    //this.WindowState = WindowState.Minimized;
        //    //_notifyIcon.Dispose();
        //}

        //private void Window_Closed(object sender, EventArgs e) {
        //    int i = 0;
        //}

        #endregion UserControl Events

        #region BackgroundWorker Events

        private void Worker_DoWork(object sender, DoWorkEventArgs e) {
            for (int i = 0; i <= 100; i += 10) {
                if (_worker.CancellationPending) { e.Cancel = true; return; }
                _worker.ReportProgress(i); // Reportar progreso [1]
                Thread.Sleep(30); // Simular trabajo pesado
            }
        }

        // --- Hilo de UI ---
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            progressBarSpeechDetected.Value = e.ProgressPercentage; // Actualizar barra
            //txtStatus.Text = $"{e.ProgressPercentage}%";
        }

        // --- Hilo de UI ---
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.progressBarSpeechDetected.Value = 0;
            //btnStart.IsEnabled = true;
            //MessageBox.Show("Tarea Finalizada");
        }

        #endregion BackgroundWorker Events

        #region Samantha Event Handlers

        private void SC_TimeRemainingFunction(object sender, EventArgs e) {
            long aux = (long)sender;

            this.Dispatcher.Invoke(() => {
                //this.lblCount.Text = aux.ToString();
                this.progressBarListening.Value = aux;
                this.progressBarListening.Visibility = aux > 0 ? Visibility.Visible : Visibility.Hidden;
                this.textBlockProgressBar.Visibility = aux > 0 ? Visibility.Visible : Visibility.Hidden;
            });
        }

        private async void SC_SamanthaListeningFunction(object sender, EventArgs e) {
            _socketBridge.Broadcast("SamanthaListening", "{\"message\":\"Mi mensaje xxx\"}");

            this.SliderOnlyOneAndSamantha.Value = 1;
            this.SliderOnlyOneAndSamantha.IsSelectionRangeEnabled = true;
            this.TextBlockSamanthaListening.Text = "Samantha is Listening";
            this.TextBlockSamanthaListening.Foreground = System.Windows.Media.Brushes.Red;
            this.TextBlockSamanthaListening.FontWeight = FontWeights.Bold;
            timerSamanthaListenig.Start();
            Samantha.SamanthaListeningStart();
        }

        private async void SC_SpeechRecognizedFunction(object sender, EventArgs e) {
            _socketBridge.Broadcast("SpeechRecognized", "{\"message\":\"wordRecognized\"}");
            string sentence = sender as string;
            //Paragraph paragraphLog = new Paragraph();
            // TEMP
            //ScreenLog(sentence);
        }

        private async void SC_ListenWithoutAttentionFunction(object sender, EventArgs e) {
        }

        private async void SC_InstructionArmedFunction(object sender, EventArgs e) {
            _socketBridge.Broadcast("InstructionArmed", "{\"message\":\"wordRecognized\"}");
        }

        private async void SC_EventDetectedInstructions(object sender, EventArgs e) {
            SamanthaInstructionResponse oInstructions = Newtonsoft.Json.JsonConvert
               .DeserializeObject<SamanthaInstructionResponse>((string)sender);
            if (oInstructions.RecognizedInstructionList != null && oInstructions.RecognizedInstructionList.Count > 0) {
                SamanthaInstruction oSI = oInstructions.RecognizedInstructionList[0];

                ScreenLog(oSI.Instruction);

                switch (oSI.InstructionCode) {
                    case "QUE_HORA_ES":
                        Samantha.SpeechText($"Son las {DateTime.Now.ToString("HH:mm")}");
                        break;
                    case "QUE_DIA_ES_HOY":
                        string sDate = DateTime.Now.ToString("D");
                        sDate = sDate.Replace(",", "");
                        Samantha.SpeechText($"Hoy es {sDate}");
                        break;
                    //case "CUANTO_YA_VENDER":
                    //case "CUANTO_VENDER":
                    //    Samantha.SpeechText($"Llevas vendido 35 pesos");
                    //    break;
                    //case "CUANTO_FUE_ULTIMA_VENTA":
                    //    Samantha.SpeechText($"La última venta fue de 45 pesos hace 18 minutos");
                    //    break;
                    //case "CUANTO_VENDER_AYER":
                    //    Samantha.SpeechText($"Ayer se vendieron 1024 pesos");
                    //    break;
                    default:
                        if (string.IsNullOrEmpty(oSI.VoiceProcessing)) {

                        } else {
                            Samantha.SpeechText(oSI.VoiceProcessing);
                        }

                        _socketBridge.Broadcast("DetectedInstructions", $"{{\"message\":{(string)sender}}}");
                        break;
                }
            }
        }

        private async void SC_SpeechDetectedFunction(object sender, EventArgs e) {
            string data = (string)sender;
            this.Dispatcher.Invoke(() => {
                //this.lblLoud.Text = data;
                if (!_worker.IsBusy) {
                    //btnStart.IsEnabled = false;
                    _worker.RunWorkerAsync(); // Iniciar el hilo [1]
                }
            });
        }

        private async void SC_EventAudioStateChange(object sender, EventArgs e) {
            string data = (string)sender;
            this.Dispatcher.Invoke(() => {
                //this.lblLoud.Text = data;
                if (!_worker.IsBusy) {
                    //btnStart.IsEnabled = false;
                    _worker.RunWorkerAsync(); // Iniciar el hilo [1]
                }
            });
        }

        private async void SC_EventAudioSignalProblem(object sender, EventArgs e) {
        }

        private async void SC_EventSamanthaUnused(object sender, EventArgs e) {
            this.Dispatcher.Invoke(() => {
                // TEMP
                //Samantha.SpeechText("Sin instrucción");
                //Thread.Sleep(1000);
                // TEMP
                //ScreenLog("Samantha se activó, pero no se detectó alguna instrucción");
            });
        }

        private async void SC_EventTroubleInitialize(object sender, EventArgs e) {
            string message = (string)sender;
            ScreenLog($"Something happened: {message}");
        }

        #endregion Samantha Event Handlers

        #region Samantha Event Client Socket

        private void SamanthaSocket_EventHandleTcpClientFunction(object sender, EventArgs e) {
            try {
                BridgeResponse clientRequest = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<BridgeResponse>((string)sender);

                switch (clientRequest.Type) {
                    case "PING":
                        this.Dispatcher.Invoke(() => {
                            ScreenLog($"{clientRequest.Info}");
                            if (!string.IsNullOrEmpty(clientRequest.Detail))
                                ScreenLog($"{clientRequest.Detail}", ClientRequest.PING);
                        });
                        Samantha.SpeechText(clientRequest.Info);
                        break;
                    case "DATA":
                        this.Dispatcher.Invoke(() => {
                            ScreenLog($"{clientRequest.Info}");
                            if (!string.IsNullOrEmpty(clientRequest.Detail))
                                ScreenLog($"{clientRequest.Detail}", ClientRequest.INFO);
                        });
                        if (clientRequest.IsSpeaker.Equals("true")) {
                            Samantha.SpeechText(clientRequest.Info);
                        }
                        break;
                    case "TODO":
                        this.Dispatcher.Invoke(() => {
                            ScreenLog($"Se recibió el código *{clientRequest.Info}*");
                            if(!string.IsNullOrEmpty(clientRequest.Detail))
                                ScreenLog($"{clientRequest.Detail}", ClientRequest.INFO);
                        });
                        _serialPort.Write($"{clientRequest.Info}\"");
                        break;
                    case "ERROR":
                        this.Dispatcher.Invoke(() => {
                            ScreenLog($"{clientRequest.Info}", ClientRequest.ERROR);
                            if (!string.IsNullOrEmpty(clientRequest.Detail))
                                ScreenLog($"{clientRequest.Detail}", ClientRequest.INFO);
                        });
                        if (clientRequest.IsSpeaker.Equals("true")) {
                            Samantha.SpeechText(clientRequest.Info);
                        }
                        break;
                }
            } catch (Exception ex) {
                // Sento to log file
                string msg = ex.Message;
            }
        }

        #endregion Samantha Event Client Socket

        #region Event Mapping Samantha -> Broadcast

        //private void Samantha_EventTimeRemaining(object sender, EventArgs e) {
        //    try {
        //        var aux = (long)sender;
        //        //Broadcast("timeRemaining", $"{{\"remaining\":{aux}}}");
        //    } catch {
        //        //Broadcast("timeRemaining", "{\"remaining\":0}");
        //    }
        //}

        //private void Samantha_EventSamanthaListening(object sender, EventArgs e) {
        //    _socketBridge.Broadcast("SamanthaListening", "{\"message\":\"Mi mensaje xxx\"}");
        //}

        //private void Samantha_EventSpeechRecognized(object sender, EventArgs e) {
        //    _socketBridge.Broadcast("SpeechRecognized", "{\"message\":\"wordRecognized\"}");
        //}

        //private void Samantha_EventListenWithoutAttention(object sender, EventArgs e) {
        //    Broadcast("listenWithoutAttention", "{\"message\":\"wordRecognized\"}");
        //}

        //private void Samantha_EventInstructionArmed(object sender, EventArgs e) {
        //    _socketBridge.Broadcast("InstructionArmed", "{\"message\":\"wordRecognized\"}");
        //}

        //private void Samantha_EventDetectedInstructions(object sender, EventArgs e) {
        //    SamanthaInstructionResponse oInstructions = Newtonsoft.Json.JsonConvert
        //        .DeserializeObject<SamanthaInstructionResponse>((string)sender);
        //    Samantha.SpeechText(oInstructions.RecognizedInstructionList[0].VoiceProcessing);
        //    _socketBridge.Broadcast("DetectedInstructions", $"{{\"message\":{(string)sender}}}");
        //    //Broadcast("detectedInstructions", "{\"message\":\"detected\"}");
        //}

        //private void Samantha_EventSpeechDetected(object sender, EventArgs e) {
        //    Broadcast("speechDetected", "{\"message\":\"wordRecognized\"}");
        //}

        //private void Samantha_EventAudioStateChange(object sender, EventArgs e) {
        //    Broadcast("audioStateChange", "{\"message\":\"Mi mensaje xxx\"}");
        //}

        //private void Samantha_EventAudioSignalProblem(object sender, EventArgs e) {
        //    Broadcast("audioSignalProblem", "{\"message\":\"Mi mensaje xxx\"}");
        //}

        #endregion Event Mapping Samantha -> Broadcast

        #region Methods

        private void ScreenLog(string lineLog, ClientRequest clientRequestType = ClientRequest.DATA) {
            //
            string fullLineLog = $"{DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.fff")} {lineLog}";
            Run run = new Run(fullLineLog);

            switch (clientRequestType) {
                case ClientRequest.ERROR:
                    run.Foreground = System.Windows.Media.Brushes.Red;
                    run.FontSize = 12;
                    run.FontWeight = FontWeights.Bold;
                    break;
                case ClientRequest.INFO:
                    run.FontStyle = FontStyles.Italic;
                    run.Foreground = System.Windows.Media.Brushes.Gray;
                    break;
                default:
                    break;
            }
            this.Dispatcher.Invoke(() => {
            });

            _paragraphLog.Inlines.Add(run);
            _paragraphLog.Inlines.Add(new LineBreak());
            this.RichTextLog.ScrollToEnd();
            this.RichTextLog.Document.Blocks.Add(_paragraphLog);
            WriteLogFile(fullLineLog);
            /*
            _paragraphLog.Inlines.Add(new LineBreak());
            _paragraphLog.Inlines.Add(new Bold(new Run("texto en negrita, ")));
            _paragraphLog.Inlines.Add(new LineBreak());
            _paragraphLog.Inlines.Add(new Italic(new Run("texto en cursiva.")));
            _paragraphLog.Inlines.Add(new LineBreak());
            */
        }

        private void WriteLogFile(string message) {
            Samantha.WriteLogFile(_LogFileName, message);
        }

        #endregion Methods

        private void SliderOnlyOneAndSamantha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (this.SliderOnlyOneAndSamantha.Value == 1) {
                this.SliderOnlyOneAndSamantha.IsSelectionRangeEnabled = true;
                this.TextBlockSamanthaListening.Text = "Samantha is Listening";
                this.TextBlockSamanthaListening.Foreground = System.Windows.Media.Brushes.Red;
                this.TextBlockSamanthaListening.FontWeight = FontWeights.Bold;
                //this.TextBlockSamanthaListening
                timerSamanthaListenig.Start();
                Samantha.SamanthaListeningStart();
            } else {
                this.SliderOnlyOneAndSamantha.IsSelectionRangeEnabled = false;
                this.TextBlockSamanthaListening.Text = "Say Samantha";
                this.TextBlockSamanthaListening.Foreground = System.Windows.Media.Brushes.Blue;
                this.TextBlockSamanthaListening.FontWeight = FontWeights.Normal;
                timerSamanthaListenig.Stop();
                Samantha.SamanthaListeningStop();
            }
        }
    }
}
