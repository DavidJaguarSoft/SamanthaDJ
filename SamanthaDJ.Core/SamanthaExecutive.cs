 using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using SamanthaX.Core.Model;
using SamanthaX.Core.Service;
using SamanthaX.Core.Utils;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Security;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Grammar = System.Speech.Recognition.Grammar;

namespace SamanthaX.Core {

    public class SamanthaExecutive {

        #region Attributes

        private string _Username = string.Empty;
        private string _Token = string.Empty;

        private string _CultureInfo = string.Empty;
        private string _UICultureInfo = string.Empty;
        private string _CultureSpeech = string.Empty;

        private int _TickSamanthaListens;
        //  Ticks in which Samantha is active and listening for commands after being called
        private int _TicksSamAskWhatInstruccion;
        private double _SpeechRecognizedConfidence;
        private int _TimerInterval;

        private string _GrammarLoadMode = string.Empty;

        private List<NoiseDetection> listNoiseDetection = new List<NoiseDetection>();

        private string _SpeechSynthesizerVoice;
        private int _SpeechSynthesizerVolume;
        private int _SpeechSynthesizerRate;

        private string _PathLog = string.Empty;
        private bool _GenerateLog = false;
        private bool _GenerateInitializeLog = true;
        private bool _GenerateSpeechRecognizedEventLog = true;
        private bool _GenerateSpeechRecognizedAlternateEventLog = false;

        string runMethodFileName = "SamanthaRun";

        #endregion Attributes
      
        #region Internal Flow control Variables
        
        private System.Timers.Timer timerRemaining;
        private System.Timers.Timer timerReconnect;
        private long countRemaining = 0;
        //  How many ticks have passed since Samantha was called.
        private long countTicksPassed = 0;
        private bool samanthaActived = false;

        #endregion Internal Flow control Variables

        #region Inteernal Flow control Objects

        private SpeechRecognitionEngine SRESamantha;
        SpeechSynthesizer synthesizer_ = new SpeechSynthesizer();
        private List<DetectedSentence> sentenceList = new List<DetectedSentence>();
        private SamanthaVoiceEn samanthaVoice;
        private List<RecognizedInstructionEn> recognizedInstructionList;
        private List<GrammarEn> grammarListSX = new List<GrammarEn>();
        private List<RecognizedWordEn> recognizedWordList;
        private Prompt promptSamanthaVoice = new Prompt("Starting");

        #endregion Inteernal Flow control Objects

        #region Propierties

        public string VoiceProcessingDefault { get; set; }
        public string VoiceSolutionDefault { get; set; }
        public string VoiceCancelDefault { get; set; }
        public string VoiceFailDefault { get; set; }

        public bool samanthaVoice_AcceptCommand;
        public bool SamanthaListening { get; set; }

        #endregion Properties

        #region Public Events

        //  Time remaining to since Samantha was called
        public event EventHandler Event_Core_TimeRemaining;

        //  Event to indicate that Samantha is active and listening
        public event EventHandler Event_Core_SamanthaListening;

        //  Speech Recognized event
        public event EventHandler Event_Core_SpeechRecognized;

        //  A speech can be heard, but Samantha is unprepared and not paying attention.
        public event EventHandler Event_Core_ListenWithoutAttention;

        //  An instruction was detected, but we don't know if it exists in the Recognized Instructions Table.
        public event EventHandler Event_Core_InstructionArmed;
        
        //  An instruction was detected and it does exist in the Recognized Instructions Table
        public event EventHandler Event_Core_DetectedInstructions;

        //  Speech is detected, but no known Speech Recognized are detected
        public event EventHandler Event_Core_SpeechDetected;

        //  Event to detect state changes with the audio input (microphone)
        public event EventHandler Event_Core_AudioStateChange;

        //  Event to detect audio signal problems
        public event EventHandler Event_Core_AudioSignalProblem;

        //  No instruction was detected, but Samantha was called and is listening
        public event EventHandler Event_Core_SamanthaUnused;

        #endregion Public Events

        #region Constructors

        public SamanthaExecutive() {
            sentenceList = new List<DetectedSentence>();
            countRemaining = 0;
            synthesizer_.SpeakAsyncCancel(promptSamanthaVoice);

            _SpeechSynthesizerVoice = "Microsoft Helena Desktop";
            _SpeechSynthesizerVolume = 100;
            _SpeechSynthesizerRate = -1;

            _TickSamanthaListens = 35;
            _TicksSamAskWhatInstruccion = 10;
            _SpeechRecognizedConfidence = 0.5;
            _TimerInterval = 100;

            samanthaVoice_AcceptCommand = false;
            SamanthaListening = false;
        }

        #endregion Constructors

        #region Initialize

        public void Initialize(
            string username,
            string token,

            string cultureInfo,
            string uiCultureInfo,
            string cultureSpeech,

            int tickSamanthaListens,
            int ticksSamAskWhatInstruccion,
            double speechRecognizedConfidence,
            
            int timerInterval,

            string grammarLoadMode,

            string speechSynthesizerVoice,
            int speechSynthesizerVolume,
            int speechSynthesizerRate,

            string pathlog,
            bool generateLog,
            bool generateRunEventLog,
            bool generateSpeechRecognizedEventLog
        ) {
            #region Credentials

            _CultureInfo = string.IsNullOrEmpty(cultureInfo) ? "es-ES" : cultureInfo;
            _UICultureInfo = string.IsNullOrEmpty(uiCultureInfo) ? "es-ES" : uiCultureInfo;
            _CultureSpeech = cultureSpeech;

            #endregion Credentials

            #region SpeechSynthesizer

            if (string.IsNullOrEmpty(speechSynthesizerVoice)) {
                _SpeechSynthesizerVoice = "Microsoft Helena Desktop";
            } else {
                _SpeechSynthesizerVoice = speechSynthesizerVoice;
            }

            if (speechSynthesizerVolume < 5 || speechSynthesizerVolume > 100) {
                _SpeechSynthesizerVolume = 95;
            } else {
                _SpeechSynthesizerVolume = speechSynthesizerVolume;
            }

            if (speechSynthesizerRate < 5 || speechSynthesizerRate > 100) {
                _SpeechSynthesizerRate = -1;
            } else {
                _SpeechSynthesizerRate = speechSynthesizerRate;
            }

            #endregion SpeechSynthesizer

            #region TickSamanthaListens

            if (tickSamanthaListens < 20 || tickSamanthaListens > 100) {
                _TickSamanthaListens = 35;
            } else {
                _TickSamanthaListens = tickSamanthaListens;
            }

            #endregion TickSamanthaListens

            #region TicksSamAskWhatInstruccion

            if (ticksSamAskWhatInstruccion < 5 || ticksSamAskWhatInstruccion > 30) {
                _TicksSamAskWhatInstruccion = 10;
            } else {
                _TicksSamAskWhatInstruccion = ticksSamAskWhatInstruccion;
            }

            #endregion TicksSamAskWhatInstruccion

            #region TimerInterval

            if (timerInterval < 50 || timerInterval > 500) {
                _TimerInterval = 100;
            } else {
                _TimerInterval = timerInterval;
            }

            #endregion TimerInterval

            #region Behavior

            _GrammarLoadMode = grammarLoadMode.Trim().ToUpper().Equals("INSTRUCTIONONLY")
                ? "INSTRUCTIONONLY"
                : "ALLGRAMMAR";

            #endregion Behavior

            #region speechRecognizedConfidence

            if (speechRecognizedConfidence < 0.1 || speechRecognizedConfidence > 0.9) {
                _SpeechRecognizedConfidence = 0.5;
            } else {
                _SpeechRecognizedConfidence = speechRecognizedConfidence;
            }

            #endregion speechRecognizedConfidence

            #region Log

            _PathLog = string.IsNullOrEmpty(pathlog) ? "C:\\SamanthaX\\LogFile" : pathlog;
            _GenerateLog = generateLog;
            _GenerateInitializeLog = generateRunEventLog;
            _GenerateSpeechRecognizedEventLog = generateSpeechRecognizedEventLog;

            #endregion Log

            if (_GenerateInitializeLog) {
                WriteLogFile(runMethodFileName, "...........................................................................");
                WriteLogFile(runMethodFileName, $"SamanthaDJ version: {Global.SamanthaDJVersion} initializing..............................");
            }
            Security security = new Security();

            string auxUsername = string.Empty;
            string auxToken = string.Empty;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token)) {
                auxUsername = security.Decrypt(Global.Username);
                auxToken = Global.Token;
            } else {
                #region Validate Credentials

                UserService userHS = new UserService();
                var responseLogin =
                    userHS.GetNameToken<UserStruct>(username, token);
                if (responseLogin.IsSuccess == true) {
                    auxUsername = username;
                    auxToken = token;
                    if (_GenerateInitializeLog)
                        WriteLogFile(runMethodFileName, $"Verified Credentials for {username}. OK");
                } else {
                    auxUsername = security.Decrypt(Global.Username);
                    auxToken = Global.Token;
                    if (_GenerateInitializeLog)
                        WriteLogFile(runMethodFileName, $"Invalid credentials for {username}, the default user will be used");
                }

                #endregion Validate Credentials
            }
            _Username = auxUsername;
            _Token = auxToken;

            if (_GenerateInitializeLog)
                WriteLogFile(runMethodFileName, $"User logged: {_Username} OK");
        }

        #endregion Initialize

        #region Run

        /// <summary>
        /// 
        /// </summary>
        public void Run() {

            try {
                if (_GenerateInitializeLog)
                    WriteLogFile(runMethodFileName, $"Critical configuration begins...");

                #region Timer

                timerRemaining = new System.Timers.Timer();
                timerRemaining.Enabled = true;
                timerRemaining.Elapsed += new System.Timers.ElapsedEventHandler(timerRemaining_Tick);
                timerRemaining.Interval = _TimerInterval;  //Defualt: 100 miliseconds

                #endregion Timer

                #region Get Data

                //  GRAMMAR
                GrammarService grammarS = new GrammarService();
                var responseGrammar = grammarS.GetAllxUser<GrammarStruct>(_Username, _Token);
                if (responseGrammar.IsSuccess) {
                    if (_GenerateInitializeLog)
                        WriteLogFile(runMethodFileName, $"Grammars {responseGrammar.GrammarSt.GrammarList.Count} records loaded. OK");
                } else {
                    throw new Exception(responseGrammar.ErrorMessage);
                }

                //  SAMANTHA VOICE
                SamanthaVoiceService voiceS = new SamanthaVoiceService();
                var responseVoice = voiceS.GetAllxUser<SamanthaVoiceStruct>(_Username, _Token);
                if (responseVoice.IsSuccess) {
                    if (_GenerateInitializeLog)
                        WriteLogFile(runMethodFileName, $"Voice loaded. OK");
                } else {
                    throw new Exception(responseVoice.ErrorMessage);
                }
                samanthaVoice = responseVoice.SamanthaVoiceSt.SamanthaVoice;
                VoiceProcessingDefault = samanthaVoice.VoiceProcessingDefault;
                VoiceSolutionDefault = samanthaVoice.VoiceSolutionDefault;
                VoiceCancelDefault = samanthaVoice.VoiceCancelDefault;
                VoiceFailDefault = samanthaVoice.VoiceFailDefault;

                //  RECOGNIZED INSTRUCTIONS
                RecognizedInstructionService instructionS = new RecognizedInstructionService();
                var responseInstruction = instructionS.GetAllxUser<RecognizedInstructionStruct>(_Username, _Token);
                if (responseInstruction.IsSuccess) {
                    if (_GenerateInitializeLog)
                        WriteLogFile(runMethodFileName, $"Instructions {responseInstruction.RecognizedInstructionSt.RecognizeInstructionList.Count} records loaded. OK");
                } else {
                    throw new Exception(responseInstruction.ErrorMessage);
                }
                recognizedInstructionList = new List<RecognizedInstructionEn>();
                recognizedInstructionList = responseInstruction.RecognizedInstructionSt.RecognizeInstructionList;

                //  RECOGNIZED WORDS
                RecognizedWordService rwS = new RecognizedWordService();
                var responseRW = rwS.GetAllxUser<RecognizedWordStruct>(_Username, _Token);
                if (responseRW.IsSuccess) {
                    if (_GenerateInitializeLog)
                        WriteLogFile(runMethodFileName, $"Recognized Words {responseRW.RecognizedWordSt.RecognizedWordList.Count} records loaded. OK");
                } else {
                    throw new Exception(responseRW.ErrorMessage);
                }
                recognizedWordList = new List<RecognizedWordEn>();
                recognizedWordList = responseRW.RecognizedWordSt.RecognizedWordList;

                #endregion Get Data

                #region Artificial Intelligence Config

                Choices ChoicesIAName = new Choices();
                string[] listWord = responseVoice.SamanthaVoiceSt.SamanthaVoice.AIName.Split(',');
                
                string samanthaNames = string.Empty;
                foreach (string item in listWord) {
                    if (!string.IsNullOrEmpty(item)) {
                        ChoicesIAName.Add(item);
                        samanthaNames += $"{item},";
                    } 
                }
                samanthaNames = samanthaNames.Replace(",", "");
                if(_GenerateInitializeLog) WriteLogFile(runMethodFileName, $"Artificial Intelligence Names: {samanthaNames} OK");

                //  Testing Installed Culture
                //SpeechRecognitionEngine speechRecognitionEngineSX = null;
                foreach (RecognizerInfo recognizerInfo in SpeechRecognitionEngine.InstalledRecognizers()) {
                    //Console.WriteLine($"Name: {recognizerInfo.Name}, Culture: {recognizerInfo.Culture}");
                    if (_GenerateInitializeLog) {
                        WriteLogFile(runMethodFileName, $"Detected Name: {recognizerInfo.Name}, Culture: {recognizerInfo.Culture}, Culture.Name: {recognizerInfo.Culture.Name}");
                    }
                    //speechRecognitionEngineSX = new SpeechRecognitionEngine(recognizerInfo);
                }

                //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-ES");
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(_CultureInfo);
                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, $"Thread.CurrentThread.CurrentCulture: {_CultureInfo} OK");

                //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-ES");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_UICultureInfo);
                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, $"Thread.CurrentThread.CurrentUICulture: {_UICultureInfo} OK");

                
                //SRESamantha = new SpeechRecognitionEngine(new CultureInfo("es-MX"));
                //SRESamantha = new SpeechRecognitionEngine(new CultureInfo("es-ES"));
                SRESamantha = new SpeechRecognitionEngine(new System.Globalization.CultureInfo(_CultureSpeech));
                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, $"SpeechRecognitionEngine CultureInfo => {_CultureSpeech} OK");

                #region Comments

                /*
                 Qué se cambió y por qué:
                    •	Se evita crear SpeechRecognitionEngine directamente con una CultureInfo que puede no existir en el sistema.
                    •	Se busca un RecognizerInfo instalado que coincida con la cultura (primero exacta, luego por idioma) y se usa para construir el motor. Si no hay ninguno, se hace fallback al motor por defecto y se registra el hecho para diagnóstico.
                 Diagnóstico / acciones recomendadas si sigue fallando:
                    •	Ejecuta y revisa el log que ya tienes: el listado de SpeechRecognitionEngine.InstalledRecognizers() (ya lo escribes) debe mostrar un recognizer con Name igual a _CultureSpeech (por ejemplo es-ES). Si no aparece, falta instalar el paquete de idioma de reconocimiento de voz en Windows.
                    •	En Windows: Settings > Time & Language > Speech (o instalar el paquete de idioma y las características de reconocimiento de voz).
                    •	Confirmar que la app corre en Windows (System.Speech no funciona en Linux/macOS).
                    •	Como alternativa temporal, usa new SpeechRecognitionEngine() (sin parámetro) para probar si el motor por defecto funciona.
                 */
                // Reemplazo de la creación directa por CultureInfo:
                /*
                var recognizer = GetBestRecognizer(_CultureSpeech);
                if (recognizer != null) {
                    SRESamantha = new SpeechRecognitionEngine(recognizer);
                    if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, $"SpeechRecognitionEngine created from recognizer {recognizer.Name} ({recognizer.Culture}) OK");
                } else {
                    if (_GenerateRunEventLog) WriteLogFile(runMethodFileName, $"No recognizer for <{_CultureSpeech}< found. Falling back to default engine.");
                    SRESamantha = new SpeechRecognitionEngine(); // fallback al reconocedor por defecto
                }
                */

                #endregion Comments

                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, "SetInputToDefaultAudioDevice OK");
                
                grammarListSX = responseGrammar.GrammarSt.GrammarList;

                foreach (RecognizedInstructionEn iri in recognizedInstructionList) {
                    string[] wordCode = iri.Code.Split('_');
                    GrammarBuilder grammarBuilder = new GrammarBuilder();
                    foreach (string wordCitem in wordCode) {
                        Choices choices = new Choices();
                        string wordProcessed = wordCitem.Trim().Replace(" ", "");
                        choices.Add(wordProcessed);
                        grammarBuilder.Append(choices);
                    }
                    Grammar grammar = new Grammar(grammarBuilder);
                    grammar.Name = iri.Code;
                    grammar.Enabled = true;
                    //
                    SRESamantha.LoadGrammarAsync(grammar);
                }

                //foreach (GrammarEn igrammar in grammarListSX) {
                //    GrammarBuilder grammarBuilder = new GrammarBuilder();
                //    foreach (GrammarBuilderEn igbuilder in igrammar.GrammarBuilderList) {
                //        Choices choices = new Choices();
                //        if (igbuilder.RecognizedWordsList != null) {
                //            foreach (RecognizedWordEn irword in igbuilder.RecognizedWordsList) {
                //                bool updateIt = false;
                //                if (_GrammarLoadMode.Equals("INSTRUCTIONONLY")) {
                //                    foreach (RecognizedInstructionEn iri in recognizedInstructionList) {
                //                        string[] wordCode = iri.Code.Split('_');
                //                        foreach (string wordCitem in wordCode) {
                //                            string wordProcessed = wordCitem.Trim().Replace(" ", "");
                //                            if (wordProcessed.ToUpper().Equals(irword.Code.ToUpper())) {
                //                                updateIt = true;
                //                                break;
                //                            }
                //                        }
                //                    }
                //                } else
                //                    updateIt = true;
                //                if(updateIt) {
                //                    string[] words = irword.RelatedWords.Split(',');
                //                    foreach (string worditem in words) {
                //                        string wordItemProcessed = worditem.Trim().Replace(" ", "");
                //                        choices.Add(wordItemProcessed);
                //                    }
                //                }
                //            }
                //            grammarBuilder.Append(choices);
                //        }
                //    }
                //    if (grammarBuilder.DebugShowPhrases.Length > 0) {
                //        Grammar grammar = new Grammar(grammarBuilder);
                //        grammar.Name = igrammar.Code;
                //        grammar.Enabled = true;
                //        //
                //        SRESamantha.LoadGrammarAsync(grammar);
                //    }
                //}
                
                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, "Load Grammar List OK");

                GrammarBuilder gbIAName = new GrammarBuilder();
                gbIAName.Append(ChoicesIAName);
                Grammar grammarIAName = new Grammar(gbIAName);
                grammarIAName.Name = "IAName";
                grammarIAName.Enabled = true;
                SRESamantha.LoadGrammarAsync(grammarIAName);

                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, "Load Grammar Intelligence Artificial Name OK");

                SRESamantha.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognizedEventFunction);
                SRESamantha.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(SpeechRecognitionRejectedEventFunction);
                SRESamantha.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(SpeechHypothesizedEventFunction);
                SRESamantha.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(SpeechDetectedEventFunction);
                SRESamantha.AudioStateChanged += new EventHandler<AudioStateChangedEventArgs>(AudioStateChangedEventFunction);
                SRESamantha.AudioSignalProblemOccurred += new EventHandler<AudioSignalProblemOccurredEventArgs>(AudioSignalProblemOccurredEventFunction);
                SRESamantha.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(AudioLevelUpdatedEventFunction);

                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, "Events loaded OK");

                SRESamantha.SetInputToDefaultAudioDevice();
                SRESamantha.RecognizeAsync(RecognizeMode.Multiple);

                #endregion Artificial Intelligence Config

                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, "Connect Status: Connedted OK");
                if (_GenerateInitializeLog) WriteLogFile(runMethodFileName, $"Critical configuration end. Everything was configured correctly");

                Thread.Sleep(700);
                timerReconnect = new System.Timers.Timer();
                timerReconnect.Enabled = true;
                timerReconnect.Elapsed += new System.Timers.ElapsedEventHandler(timerReconnect_Tick);
                timerReconnect.Interval = 100;  //Defualt: 100 miliseconds

            } catch (Exception ex) {
                WriteLogFile(runMethodFileName, $"Fail: Samantha DJ did not initialize correctly: {ex.Message}");
                WriteLogFile(runMethodFileName, $"StackTrace: {ex.StackTrace}");
                throw new Exception($"Message: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        #endregion Run

        #region timerRemaining_Tick

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRemaining_Tick(object sender, System.Timers.ElapsedEventArgs e) {
            try {
                if (countRemaining > 0) {
                    countRemaining = countRemaining - 1;
                    countTicksPassed += 1;

                    Event_Core_TimeRemaining(countRemaining, e);
                    if (countTicksPassed == _TicksSamAskWhatInstruccion) {
                        //  Random variable
                        Random _rnd = new Random();
                        string[] aiNames = samanthaVoice.OrderYou.Split(',');
                        string voice = aiNames[_rnd.Next(0, aiNames.Length)];
                        SpeechText(voice);
                        countRemaining = countRemaining + (10/(_TimerInterval/100));
                    }
                } else {
                    if (samanthaActived) {
                        samanthaActived = false;
                        Event_Core_SamanthaUnused(null, null);
                    }
                    countTicksPassed = 0;
                    //  Time Over
                    if (SamanthaListening == false) {
                        samanthaVoice_AcceptCommand = false;
                    }
                    sentenceList = new List<DetectedSentence>();
                }
            } catch(Exception ex) {
                if (_GenerateLog) {
                    WriteLogFile(
                        "EventError",
                        $"timerRemaining_Tick: {ex.Message}"
                    );
                }
            }
        }

        #endregion timerRemaining_Tick

        #region timerReconnect_Tick

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerReconnect_Tick(object sender, System.Timers.ElapsedEventArgs e) {
            try {
                AdminNoiseDetection(null);

                if (SRESamantha.AudioState == AudioState.Stopped) {
                    SRESamantha.SetInputToDefaultAudioDevice();
                    SRESamantha.RecognizeAsync(RecognizeMode.Multiple);
                } else {
                    timerReconnect.Stop();
                }
            } catch (Exception ex) {
                int x= 0;
            }
        }

        #endregion timerReconnect_Tick

        #region SRE EVENT FUNCTIONS

        #region SpeechRecognizedEventFunction

        /// <summary>
        /// 
        /// </summary>
        private void SpeechRecognizedEventFunction(object sender, SpeechRecognizedEventArgs e) {

            if (e.Result.Confidence < 0.52) return;
            
            string strSentence = e.Result.Text.ToUpper();
            try {
                if (SamanthaNameDetected(strSentence)) {
                    samanthaVoice_AcceptCommand = true;
                    samanthaActived = true;
                    //SystemSounds.Hand.Play();
                    //SystemSounds.Exclamation.Play();
                    //SystemSounds.Asterisk.Play();
                    //SpeechText("ding");
                    //Thread.Sleep(300);
                    
                    //synthesizer_.SpeakAsyncCancelAll();
                    countRemaining = _TickSamanthaListens;
                    sentenceList = new List<DetectedSentence>();
                    Event_Core_SamanthaListening(sender, e);
                    Event_Core_SpeechRecognized(strSentence, e);
                    if (_GenerateSpeechRecognizedEventLog)
                        WriteLogFile("SpeechRecognized", $"--{strSentence.ToUpper()} Confidence: {e.Result.Confidence}");
                } else {
                    if (SamanthaListening == true) {
                        samanthaVoice_AcceptCommand = true;
                    }
                }
                if (samanthaVoice_AcceptCommand == true) {

                    if (ThereIsNoise()) {
                        if (e.Result.Confidence < 0.70) {
                            if (_GenerateSpeechRecognizedEventLog) {
                                WriteLogFile("SpeechRecognized", $"Refused: {strSentence} Confidence: {e.Result.Confidence}");
                            }
                            return;
                        }
                    }

                    #region Alternate

                    string alternates = string.Empty;
                    //string alternatesWord = string.Empty;
                    foreach (var alt in e.Result.Alternates) {
                        if (alt.Confidence < 0.1) {
                            continue;
                        }
                        string myWORD = alt.Text;
                        alternates = $"{alternates} {alt.Grammar.Name}:{myWORD}:{alt.Confidence}\n";
                        //alternatesWord = ($"{alternatesWord} {myWORD}").Trim();
                    }
                    //  Log ALTERNATE
                    if (!string.IsNullOrEmpty(alternates) && _GenerateSpeechRecognizedAlternateEventLog) {
                        WriteLogFile("SpeechRecognized", $"ALTERNATES({e.Result.Alternates.Count}): {alternates.Trim()}");
                        //Concatena la palabra y le aumenta una coma al final
                        //strBuildingInstruction = ($"{strBuildingInstruction} {alternatesWord}").Trim();
                    }

                    #endregion Alternate

                    #region

                    //string strSentence_ = strSentence.Replace(" ", "_");
                    //foreach (RecognizedInstructionEn item in recognizedInstructionList) {
                    //    if (strSentence_.Equals(item.Code)) {
                    //        if (e.Result.Confidence < item.Confidence) {
                    //            if (_GenerateSpeechRecognizedEventLog)
                    //                WriteLogFile("SpeechRecognized", $"{e.Result.Grammar.Name}: {strSentence}: {e.Result.Confidence} REFUSED");
                    //            return;
                    //        }
                    //    }
                    //}

                    #endregion

                    if (_GenerateSpeechRecognizedEventLog) {
                        WriteLogFile("SpeechRecognized", $"{e.Result.Grammar.Name}: {strSentence}: {e.Result.Confidence}");
                    }

                    DetectedSentence oSentence = new DetectedSentence();
                    oSentence.Grammar = e.Result.Grammar.Name;
                    oSentence.Sentence = strSentence;
                    oSentence.Confidence = e.Result.Confidence;
                    sentenceList.Add(oSentence);
                    Event_Core_SamanthaListening(sender, e);
                    Event_Core_SpeechRecognized(strSentence, e);

                    GetInstruction(sentenceList);
                } else {
                    Event_Core_ListenWithoutAttention(sender, e);
                }
            } catch (Exception ex) {
                if (_GenerateLog) {
                    WriteLogFile(
                        "EventError",
                        $"SpeechRecognizedEventFunction: {ex.Message}"
                    );
                }
            }
        }

        #endregion SpeechRecognizedEventFunction

        #region SpeechRecognitionRejectedEventFunction

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SpeechRecognitionRejectedEventFunction(object sender, SpeechRecognitionRejectedEventArgs e) {
            try {
                if (_GenerateLog && samanthaVoice_AcceptCommand)
                    WriteLogFile("AudioSignal", $"SpeechRecognitionRejectedEventFunction: {e.Result.ToString()}");
            } catch (Exception ex) {

            }
        }

        #endregion SpeechRecognitionRejectedEventFunction

        #region SpeechHypothesizedEventFunction

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SpeechHypothesizedEventFunction(object sender, SpeechHypothesizedEventArgs e) {
            int i = 0;
            //if (_GenerateSpeechRecognizedEventLog)
            //    WriteLogFile("SpeechRecognized", $"Hypothesized--{e.Result.Text}");
        }

        #endregion SpeechHypothesizedEventFunction

        #region SpeechDetectedEventFunction

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SpeechDetectedEventFunction(object sender, SpeechDetectedEventArgs e) {
            Random random = new Random();
            int dado = random.Next(0, ((SpeechRecognitionEngine)sender).Grammars.Count-1);
            string name = ((SpeechRecognitionEngine)sender).Grammars[dado].Name;
            if (countRemaining > 0) {
                countTicksPassed = _TicksSamAskWhatInstruccion + 1;
            }
            //if (_GenerateLog) WriteLogFile("SpeechDetected", e.AudioPosition.ToString());
            Event_Core_SpeechDetected(name, e);
        }

        #endregion SpeechDetectedEventFunction

        #region AudioStateChangedEventFunction

        /// <summary>
        /// 
        /// </summary>
        void AudioStateChangedEventFunction(object sender, AudioStateChangedEventArgs e) {
            try {
                string result = e.AudioState.ToString();
                if (_GenerateLog && !result.Equals("Speech") && !result.Equals("Silence")) {
                    Event_Core_AudioStateChange(result, e);
                    timerReconnect.Start();
                    if (_GenerateLog && samanthaVoice_AcceptCommand)
                        WriteLogFile("AudioSignal", $"State: {result}");
                }
            } catch (Exception ex) {
                if (_GenerateLog) {
                    WriteLogFile(
                        "EventError",
                        $"AudioStateChangedEventFunction: {ex.Message}"
                    );
                }
            }
        }

        #endregion AudioStateChangedEventFunction

        #region AudioSignalProblemOccurredEventFunction

        /// <summary>
        /// 
        /// </summary>
        void AudioSignalProblemOccurredEventFunction(object sender, AudioSignalProblemOccurredEventArgs e) {
            NoiseDetection noiseDetection = new NoiseDetection();
            noiseDetection.DateRate = DateTime.Now;
            noiseDetection.EventRate = e.AudioSignalProblem;
            AdminNoiseDetection(noiseDetection);
            try {
                string result =
                        $"Audio signal problem Level: {e.AudioLevel}, Position: {e.AudioPosition}, Problem: {e.AudioSignalProblem}," +
                        $" Recognition engine audio position: {e.RecognizerAudioPosition}";
                //if (_GenerateLog) WriteLogFile("AudioSignal", $"Problem: {result}");
                if (_GenerateLog && samanthaVoice_AcceptCommand)
                    WriteLogFile("AudioSignal", $"Problem AudioSignalProblemOccurredEventFunction: {e.AudioSignalProblem}");

                Event_Core_AudioSignalProblem(result, e);
            } catch (Exception ex) {
                //if (_GenerateLog) {
                //    WriteLogFile(
                //        "EventError",
                //        $"AudioSignalProblemOccurredEventFunction: {ex.Message}"
                //    );
                //}
            }
        }

        #endregion AudioSignalProblemOccurredEventFunction

        #region AudioLevelUpdatedEventFunction

        /// <summary>
        /// 
        /// </summary>
        void AudioLevelUpdatedEventFunction(object sender, AudioLevelUpdatedEventArgs e) {
            if (e.AudioLevel < 50) { return; }
            
            try {
                if (_GenerateLog && samanthaVoice_AcceptCommand)
                    WriteLogFile("AudioSignal", $"Problem AudioLevelUpdatedEventFunction: {e.AudioLevel}%");

                //Event_Core_AudioSignalProblem(result, e);
            } catch (Exception ex) {

            }
        }

        #endregion AudioLevelUpdatedEventFunction

        #endregion SRE EVENT FUNCTIONS

        #region METHODS

        #region SamanthaNameDetected

        private bool SamanthaNameDetected(string guessWord) {
            bool yet = false;
            string[] aiNames = samanthaVoice.AIName.Split(',');
            foreach (string item in aiNames) {
                if (guessWord.Trim().ToUpper().Equals(item.Trim().ToUpper())) {
                    yet = true;
                }
            }
            return yet;
        }

        #endregion SamanthaNameDetected

        #region GetInstruction

        public void GetInstruction(List<DetectedSentence> listSentence) {

            if (!(listSentence != null && listSentence.Count > 0)) return;

            List<InstructionEntity> AllInstruction = new List<InstructionEntity>();
            int index = 0;
            foreach (DetectedSentence iSentence in listSentence) {
                string[] listWord = iSentence.Sentence.Trim().Split(' ');
                string builtInstructionCode = string.Empty;
                foreach (GrammarEn igrammar in grammarListSX) {
                    if (igrammar.Code.Equals(iSentence.Grammar.Trim())) {
                        foreach (GrammarBuilderEn igb in igrammar.GrammarBuilderList) {
                            foreach (RecognizedWordEn irw in igb.RecognizedWordsList) {
                                foreach (string iword in listWord) {
                                    string[] words = irw.RelatedWords.Replace(" ", "").Trim().ToUpper().Split(',');
                                    foreach (string word in words) {
                                        if (word.Equals(iword.Trim())) {
                                            string previus = string.IsNullOrEmpty(builtInstructionCode.Trim()) ? "" : "_";
                                            builtInstructionCode = $"{builtInstructionCode}{previus}{irw.Code}";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(builtInstructionCode)) {
                    InstructionEntity oInstructionEntity = new InstructionEntity {
                        InstructionId = index,
                        Grammar = iSentence.Grammar,
                        Sentence = iSentence.Sentence,
                        //Confidence = iSentence.Confidence,
                        InstructionCode = builtInstructionCode,
                        Instruction = string.Empty,
                        Description = string.Empty,
                        VoiceProcessing = string.Empty,
                        VoiceSolution = string.Empty,
                        VoiceCancel = string.Empty,
                        VoiceFail = string.Empty,
                    };
                    AllInstruction.Add(oInstructionEntity);
                    index++;
                }
            }
            foreach (InstructionEntity ientity in AllInstruction) {
                foreach (RecognizedInstructionEn iri in recognizedInstructionList) {
                    if (ientity.InstructionCode.Trim().ToUpper().Equals(iri.Code.ToUpper())) {
                        ientity.InstructionTypeId = iri.InstructionTypeId;
                        ientity.Instruction = iri.Instruction;
                        ientity.Description = iri.Description;
                        //ientity.Confidence = iri.Confidence;
                        ientity.VoiceProcessing = iri.VoiceProcessing;
                        ientity.VoiceSolution = iri.VoiceSolution;
                        ientity.VoiceEnding = iri.VoiceEnding;
                        ientity.VoiceCancel = iri.VoiceCancel;
                        ientity.VoiceFail = iri.VoiceFail;
                        ientity.InstructionFound = true;
                        break;
                    }
                }
            }
            List<InstructionEntity> RecognizedInstructions = new List<InstructionEntity>();
            List<InstructionEntity> UnrecognizedInstructions = new List<InstructionEntity>();
            foreach (InstructionEntity item in AllInstruction) {
                if (item.InstructionFound) {
                    RecognizedInstructions.Add(item);
                } else {
                    UnrecognizedInstructions.Add(item);
                }
            }
            InstructionResponse oInstructionResponse = new InstructionResponse();
            oInstructionResponse.RecognizedInstructionList = RecognizedInstructions;
            oInstructionResponse.UnrecognizedInstructionList = UnrecognizedInstructions;
            string jsonInstructions = Newtonsoft.Json.JsonConvert.SerializeObject(oInstructionResponse);
            Event_Core_InstructionArmed(null, null);

            if (RecognizedInstructions != null && RecognizedInstructions.Count > 0) {
                samanthaActived = false;
                synthesizer_.SpeakAsyncCancel(promptSamanthaVoice);
                countRemaining = 0;
                Event_Core_TimeRemaining(countRemaining, null);
                Event_Core_DetectedInstructions(jsonInstructions, null);
            }
        }

        #endregion GetInstruction

        #region GetTextInstruction

        public void GetTextInstruction(string strSentence) {

            #region Preparation

            //  Words to ignore are added; only those with more than 2 letters are evaluated
            List<RecognizedWordEn> newRecognizedWordList = new List<RecognizedWordEn>(recognizedWordList);
            string[] listWordToIgnore = samanthaVoice.WordsToIgnore.Trim().Split(',');
            int index = 0;
            foreach(string item in listWordToIgnore) {
                if(item.Trim().Length >= 2) {
                    RecognizedWordEn oRW = new RecognizedWordEn {
                        RecognizedWordId = 0,
                        UserId = 0,
                        LanguageId = 0,
                        Code = string.Empty,
                        WordClassId = 0,
                        WordClass = string.Empty,
                        RelatedWords = item.Trim().ToUpper(),
                        Enabled = true,
                    };
                    newRecognizedWordList.Add(oRW);
                    index++;
                }
            }

            List<string> wordsToEvaluate = new List<string>();
            foreach(string item in strSentence.ToUpper().Split(' ')) {
                if(item.Trim().Length >= 2) {
                    wordsToEvaluate.Add(item);
                }
            }

            #endregion Preparation

            #region PHASE 1. Letter x Letter

            List<DeducedWordEntity> listEvaluated = new List<DeducedWordEntity>();
            int positionWord = 0;
            
            foreach (string wordEvaluate in wordsToEvaluate) {
                positionWord += 1;
                foreach (RecognizedWordEn compareIt in newRecognizedWordList) {
                    compareIt.RelatedWords = compareIt.RelatedWords.Trim().ToUpper();
                    int countSuccess = 0;
                    for (int i = 0; i <= wordEvaluate.Length - 1; i++) {
                        for (int j = 0; j <= compareIt.RelatedWords.Length - 1; j++) {
                            if (compareIt.RelatedWords[j].Equals(wordEvaluate[i])) {
                                countSuccess++;
                                break;
                            }
                        }
                    }
                    DeducedWordEntity worde = new DeducedWordEntity {
                        DeducedWordId = index,
                        EvaluatedWord = wordEvaluate,
                        DeducedWord = compareIt.RelatedWords,
                        Position = positionWord,
                        Proximity1L = countSuccess,
                        ProximityAverage = 0,
                        RecognizedWordCode = compareIt.Code.ToUpper(),
                        WordClassId = compareIt.WordClassId,
                        WordClass = compareIt.WordClass,
                    };
                    listEvaluated.Add(worde);
                    index++;
                }
            }
            string jsonEvaluated = Newtonsoft.Json.JsonConvert.SerializeObject(listEvaluated);

            #endregion PHASE 1. Letter x Letter

            //  Order the list
            if(listEvaluated.Count > 0) {

                #region PHASE 2. Second Analysis

                listEvaluated = listEvaluated.OrderBy(x => x.Position).ThenByDescending(x => x.Proximity1L).ToList();
                foreach(DeducedWordEntity item in listEvaluated) {
                    int countSuccess = 0;
                    if(item.EvaluatedWord.Length >= 2) {
                        for(int i = 0; i <= item.EvaluatedWord.Length - 2; i++) {
                            string sectionEvaluated = item.EvaluatedWord.Substring(i,2);
                            for(int j = 0; j <= item.DeducedWord.Length - 2; j++) {
                                string sectionDeduced = item.DeducedWord.Substring(j,2);
                                if(sectionEvaluated.Equals(sectionDeduced)) {
                                    countSuccess++;
                                    break;
                                }
                            }
                        }
                        item.Proximity2L = countSuccess;
                    }
                    float one = ((100 * item.Proximity1L) / item.EvaluatedWord.Length) * (float)1.0;
                    float two = ((100 * item.Proximity2L) / item.EvaluatedWord.Length) * (float)2.0;
                    item.ProximityAverage = (one + two + 0 + 0 + 0) / 5;
                }
                listEvaluated = listEvaluated.OrderBy(x => x.Position).ThenByDescending(x => x.ProximityAverage).ToList();
                jsonEvaluated = Newtonsoft.Json.JsonConvert.SerializeObject(listEvaluated);

                #endregion PHASE 2. Second Analysis

                #region PHASE 3. Third Analysis

                int test = 0;
                foreach(DeducedWordEntity item in listEvaluated) {
                    int countSuccess = 0;
                    test++;
                    if(test == 59) {
                        int i = test;
                    }
                    if(item.EvaluatedWord.Length >= 3){
                        for(int i = 0; i <= item.EvaluatedWord.Length - 3; i++) {
                            string sectionEvaluated = item.EvaluatedWord.Substring(i,3);
                            for(int j = 0; j <= item.DeducedWord.Length - 3; j++) {
                                string sectionDeduced = item.DeducedWord.Substring(j,3);
                                if(sectionEvaluated.Equals(sectionDeduced)) {
                                    countSuccess++;
                                    break;
                                }
                            }
                        }
                    }
                    item.Proximity3L = countSuccess;
                    //
                    float one = ((100 * item.Proximity1L) / item.EvaluatedWord.Length) * (float)1.0;
                    float two = ((100 * item.Proximity2L) / item.EvaluatedWord.Length) * (float)2.0;
                    float three = ((100 * item.Proximity3L) / item.EvaluatedWord.Length) * (float)3.0;
                    item.ProximityAverage = (one + two + three + 0 + 0) / 5;
                }
                listEvaluated = listEvaluated.OrderBy(x => x.Position).ThenByDescending(x => x.ProximityAverage).ToList();
                jsonEvaluated = Newtonsoft.Json.JsonConvert.SerializeObject(listEvaluated);

                #endregion PHASE 3. Third Analysis

                #region PHASE 4. Third Analysis

                foreach(DeducedWordEntity item in listEvaluated) {
                    int countSuccess = 0;
                    if(item.EvaluatedWord.Length >= 4) {
                        for(int i = 0; i <= item.EvaluatedWord.Length - 4; i++) {
                            string sectionEvaluated = item.EvaluatedWord.Substring(i,4);
                            for(int j = 0; j <= item.DeducedWord.Length - 4; j++) {
                                string sectionDeduced = item.DeducedWord.Substring(j,4);
                                if(sectionEvaluated.Equals(sectionDeduced)) {
                                    countSuccess++;
                                    break;
                                }
                            }
                        }
                    }
                    item.Proximity4L = countSuccess;
                    //
                    float one = ((100 * item.Proximity1L) / item.EvaluatedWord.Length) * (float)1.0;
                    float two = ((100 * item.Proximity2L) / item.EvaluatedWord.Length) * (float)2.0;
                    float three = ((100 * item.Proximity3L) / item.EvaluatedWord.Length) * (float)3.0;
                    float four = ((100 * item.Proximity4L) / item.EvaluatedWord.Length) * (float)4.0;
                    item.ProximityAverage = (one + two + three + four + 0) / 5;
                }
                listEvaluated = listEvaluated.OrderBy(x => x.Position).ThenByDescending(x => x.ProximityAverage).ToList();
                jsonEvaluated = Newtonsoft.Json.JsonConvert.SerializeObject(listEvaluated);

                #endregion PHASE 4. Third Analysis

                #region PHASE 5. LENGTH

                foreach(DeducedWordEntity item in listEvaluated) {
                    float percent = (100 * item.EvaluatedWord.Length) / item.DeducedWord.Length;
                    if(percent > 70 && (percent < 160)) {
                        item.ProximityLength = percent;
                    }
                    float one = ((100 * item.Proximity1L) / item.EvaluatedWord.Length) * (float)1.0;
                    float two = ((100 * item.Proximity2L) / item.EvaluatedWord.Length) * (float)2.0;
                    float three = ((100 * item.Proximity3L) / item.EvaluatedWord.Length) * (float)3.0;
                    float four = ((100 * item.Proximity4L) / item.EvaluatedWord.Length) * (float)4.0;
                    item.ProximityAverage = (one + two + three + four + item.ProximityLength) / 5;
                }
                listEvaluated = listEvaluated.OrderBy(x => x.Position).ThenByDescending(x => x.ProximityAverage).ToList();
                jsonEvaluated = Newtonsoft.Json.JsonConvert.SerializeObject(listEvaluated);

                #endregion PHASE 5. LENGTH

                List<DeducedWordEntity> listFiltered = new List<DeducedWordEntity>();
                string newSentence = string.Empty;
                foreach(string evaluated in wordsToEvaluate) {
                    foreach(DeducedWordEntity calculated in listEvaluated) {
                        if(evaluated.Equals(calculated.EvaluatedWord)) {
                            bool mustIgnore = false;
                            foreach(string ignore in listWordToIgnore) {
                                if(ignore.Trim().ToUpper().Equals(calculated.DeducedWord)) {
                                    mustIgnore = true;
                                    break;
                                }
                            }
                            if(mustIgnore == false) {
                                newSentence += $"{calculated.RecognizedWordCode}_";
                                listFiltered.Add(calculated);
                            }
                            break;
                        }
                    }
                }
                newSentence = EliminateSeparator(newSentence);
                //
                List<InstructionEntity> RecognizedInstructions = new List<InstructionEntity>();
                List<InstructionEntity> UnrecognizedInstructions = new List<InstructionEntity>();
                InstructionEntity oInstructionEntity = new InstructionEntity {
                    InstructionId = index,
                    Grammar = string.Empty,
                    Sentence = string.Empty,
                    //Confidence = 0,
                    InstructionCode = newSentence,
                    InstructionFound = false,
                };
                //
                foreach(RecognizedInstructionEn item in recognizedInstructionList) {
                    if(item.Code.Equals(newSentence)) {
                        oInstructionEntity = new InstructionEntity {
                            InstructionId = index,
                            Grammar = string.Empty,
                            Sentence = string.Empty,
                            //Confidence = 0,
                            InstructionCode = item.Code,
                            Instruction = item.Instruction,
                            Description = item.Description,
                            VoiceProcessing = item.VoiceProcessing,
                            VoiceSolution = item.VoiceSolution,
                            VoiceCancel = item.VoiceCancel,
                            VoiceFail = item.VoiceFail,
                            InstructionFound = true,
                        };
                        break;
                    }
                }
                if(oInstructionEntity.InstructionFound) {
                    RecognizedInstructions.Add(oInstructionEntity);
                } else {
                    UnrecognizedInstructions.Add(oInstructionEntity);
                }
                InstructionResponse oInstructionResponse = new InstructionResponse();
                oInstructionResponse.RecognizedInstructionList = RecognizedInstructions;
                oInstructionResponse.UnrecognizedInstructionList = UnrecognizedInstructions;
                jsonEvaluated = Newtonsoft.Json.JsonConvert.SerializeObject(listEvaluated);
                string jsonInstructions = Newtonsoft.Json.JsonConvert.SerializeObject(oInstructionResponse);
                Event_Core_DetectedInstructions(jsonInstructions,null);
            } else {

            }
        }

        #endregion GetTextInstruction

        #region SpeechText

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SpeechText(string textToSpeech) {

            promptSamanthaVoice = new Prompt(textToSpeech);
            //  We create the object (variable) "synthesizer_" of class "SpeechSynthesizer"
            synthesizer_ = new SpeechSynthesizer();

            try {
                synthesizer_.SelectVoice(_SpeechSynthesizerVoice);
                synthesizer_.Volume = _SpeechSynthesizerVolume;
                synthesizer_.Rate = _SpeechSynthesizerRate;
                synthesizer_.SpeakAsync(promptSamanthaVoice);
            } catch (Exception ex) {
                WriteLogFile(
                    "EventError",
                    $"SpeechText: No valid actor for speech was detected: {ex.Message}"
                );
                synthesizer_.SelectVoice("Microsoft Helena Desktop");
                synthesizer_.Volume = 100;
                synthesizer_.Rate = -1;
                synthesizer_.SpeakAsync(ex.Message);
            }
            //synthesizer_.Speak(textToSpeech);
        }

        #endregion SpeechText

        #region SamanthaAreYourHere

        public void SamanthaAreYourHere() {
            samanthaVoice_AcceptCommand = true;
            //  Tiempo de espera para aceptar todos los comandos
            countRemaining = _TickSamanthaListens;

            //  Variable aleatoria
            Random _rnd = new Random();
            String[] samVoiceSplit = this.samanthaVoice.OrderYou.Split(',');
            //  La variable random solo acepta valores que van de cero hasta el numero de elementos de "yesSr"
            SpeechText(samVoiceSplit[_rnd.Next(0, samVoiceSplit.Length)]);
        }

        #endregion SamanthaAreYourHere

        #region ReconectAudio

        public bool ReconectAudio() {
            try {
                SRESamantha.SetInputToDefaultAudioDevice();
                SRESamantha.RecognizeAsync(RecognizeMode.Multiple);
                WriteLogFile("AudioSignal", $"State: Connected");
                return true;
            } catch (Exception ex) {
                WriteLogFile("AudioSignal", $"Error: {ex.Message}");
                return false;
            }
        }

        #endregion ReconectAudio

        #region WriteLogFile

        public void WriteLogFile(string fileName, string strInfo, bool hour = false) {
            bool tryWrite = true;
            int tryCount = 0;
            string nameDirectoryMonth = string.Empty;
            while (tryWrite) {
                try {
                    nameDirectoryMonth = string.IsNullOrEmpty(_PathLog)
                        ? $"SamanthaLog\\{DateTime.Now.ToString("yyyy-MM")}"
                        :$"{_PathLog}\\{DateTime.Now.ToString("yyyy-MM")}";
                    if (!Directory.Exists(nameDirectoryMonth))
                        Directory.CreateDirectory(nameDirectoryMonth);
                    string nameDirectoryDay = DateTime.Now.ToString("yyyy-MM-dd");
                    if (!Directory.Exists($"{nameDirectoryMonth}\\{nameDirectoryDay}"))
                        Directory.CreateDirectory($"{nameDirectoryMonth}\\{nameDirectoryDay}");

                    string hourSt = hour ? "HH" : "";
                    string nameFileComplement = DateTime.Now.ToString($"yyyyMMdd{hourSt}");
                    string sNombreTXTSession = $"{nameDirectoryMonth}\\{nameDirectoryDay}\\{fileName}_{nameFileComplement}.txt";
                    StringBuilder sb = new StringBuilder();

                    string tryCountSt = tryCount > 0 ? $"({tryCount})" : "";
                    sb.AppendLine($"{tryCountSt}{DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.ffff")} {strInfo}");
                    using (StreamWriter archivo = new StreamWriter(sNombreTXTSession, true)) {
                        archivo.Write(sb.ToString());
                    }
                    tryWrite = false;
                } catch (Exception ex) {
                    Console.WriteLine("Write Log File Exception: " + ex.Message);
                    tryCount++;
                    if (tryCount > 2) {
                        tryWrite = false;
                        return;
                    }
                    Thread.Sleep(100);
                }
            }
        }

        #endregion WriteLogFile

        #region EliminateSeparator

        private string EliminateSeparator(string codes) {
            string newCodes = codes;
            string last = codes.Substring(codes.Length-1,1);
            if(last.Equals("_") || last.Equals(",")) {
                newCodes = codes.Substring(0,codes.Length-1);
            }
            return newCodes;
        }

        #endregion EliminateSeparator

        #region AdminNoiseDetection

        private void AdminNoiseDetection(NoiseDetection oND) {
            List<NoiseDetection> listTemp = new List<NoiseDetection>();
            DateTime myNow = DateTime.Now;
            DateTime myNowMinusXsec = myNow.AddMilliseconds(-2000);
            foreach (NoiseDetection item in listNoiseDetection) {
                if (item.DateRate >= myNowMinusXsec) {
                    listTemp.Add(item);
                }
            }
            try {
                if (oND != null) listTemp.Add(oND);
                listNoiseDetection = listTemp;
                //if (_GenerateLog && samanthaVoice_AcceptCommand) {
                //    WriteLogFile("AudioSignal", $"AdminNoiseDetection: {listNoiseDetection.Count}");
                //}
            } catch (Exception ex) {
            }
        }

        private bool ThereIsNoise() {
            bool yet = false;
            foreach (NoiseDetection item in listNoiseDetection) {
                if (item.EventRate == AudioSignalProblem.TooLoud
                    || item.EventRate == AudioSignalProblem.TooNoisy
                    || item.EventRate == AudioSignalProblem.TooSlow
                    || item.EventRate == AudioSignalProblem.TooSoft
                    //|| item.EventRate == AudioSignalProblem.TooFast
                   ) {
                    yet = true;
                    break;
                }
            }
            return yet;
        }

        #endregion AdminNoiseDetection

        #endregion METHODS
    }

    public class NoiseDetection {
        public DateTime DateRate { get; set; }
        public AudioSignalProblem EventRate { get; set; }
    }
}
