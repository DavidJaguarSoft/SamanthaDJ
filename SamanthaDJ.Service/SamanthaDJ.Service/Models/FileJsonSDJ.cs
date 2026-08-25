using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaDJ.ServiceWPF.Models {
    
    public class FileJsonSDJ {
        public CredentialJSon Credential { get; set; }
        public CultureInfoJSon Culture { get; set; }
        public SamanthaVoiceJson SamanthaVoice { get; set; }
        public RecognitionFactorJSon RecognitionFactor { get; set; }
        public ArduinoJSon Arduino { get; set; }
        public LogJSon Log { get; set; }
        public OtherJSon Other { get; set; }
    }

    public class CredentialJSon {
        public string Username { get; set; }
        public string Token { get; set; }
    }

    public class CultureInfoJSon {
        public string CultureInfo { get; set; }
        public string UICultureInfo { get; set; }
        public string CultureSpeech { get; set; }
    }

    public class SamanthaVoiceJson {
        public string SpeechSynthesizerVoice { get; set; }
        public string SpeechSynthesizerVolume { get; set; }
        public string SpeechSynthesizerRate { get; set; }
        public string SamanthaListenigTime { get; set; }
    }

    public class RecognitionFactorJSon {
        public string GrammarLoadMode { get; set; }
        public string TickSamanthaListens { get; set; }
        public string TicksSamAskWhatInstruccion { get; set; }
        public string SpeechRecognizedConfidenceConfidence { get; set; }
        public string TimerInterval { get; set; }
    }

    public class ArduinoJSon {
        public string Enable { get; set; }
        public string Port { get; set; }
        public string BaudRate { get; set; }
    }

    public class LogJSon {
        public string PathLog { get; set; }
        public string GenerateLog { get; set; }
        public string GenerateRunEventLog { get; set; }
        public string GenerateSpeechRecognizedEventLog { get; set; }
    }

    public class OtherJSon {
        public string PropertyStringA { get; set; }
        public string PropertyStringB { get; set; }
        public string PropertyStringC { get; set; }
        public string PropertyStringD { get; set; }
        public string PropertyStringE { get; set; }
        public string PropertyNumericaA { get; set; }
        public string PropertyNumericaB { get; set; }
        public string PropertyNumericaC { get; set; }
        public string PropertyNumericaD { get; set; }
        public string PropertyNumericaE { get; set; }
    }
}
