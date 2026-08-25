using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Core.Model {
    
    public class DeducedWordEntity {
        public int DeducedWordId { get; set; }
        public string EvaluatedWord { get; set; }
        public string DeducedWord { get; set; }
        public int Position { get; set; }
        /// <summary>
        ///     Letter x Letter proximity
        /// </summary>
        public float Proximity1L { get; set; }
        /// <summary>
        ///     Two letter proximity
        /// </summary>
        public float Proximity2L { get; set; }
        /// <summary>
        ///     Three letter proximity
        /// </summary>
        public float Proximity3L { get; set; }

        /// <summary>
        ///     Four letter proximity
        /// </summary>
        public float Proximity4L { get; set; }

        public float ProximityLength { get; set; }
        /// <summary>
        ///     Average proximity
        /// </summary>
        public float ProximityAverage {  get; set; }
        public string RecognizedWordCode { get; set; }
        public int WordClassId { get; set; }
        public string WordClass { get; set; }
        public string VoiceProcessing { get; set; }
        public string VoiceSolution { get; set; }
        public string VoiceCancel { get; set; }
        public string VoiceFail { get; set; }
        public bool InstructionFound { get; set; }

        public DeducedWordEntity() {
            DeducedWordId = 0;
            EvaluatedWord = string.Empty;
            DeducedWord = string.Empty;
            Position = 0;
            Proximity1L = 0.0f;
            Proximity2L = 0.0f;
            Proximity3L = 0.0f;
            Proximity4L = 0.0f;
            ProximityLength = 0.0f;
            ProximityAverage = 0.0f;
            RecognizedWordCode = string.Empty;
            WordClassId = 0;
            WordClass = string.Empty;
            VoiceProcessing = string.Empty;
            VoiceSolution = string.Empty;
            VoiceCancel = string.Empty;
            VoiceFail = string.Empty;
            InstructionFound = false;
        }
    }
}
