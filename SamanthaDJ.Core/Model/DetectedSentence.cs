using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Core.Model {

    public class DetectedSentence {
        public string Grammar {  get; set; }
        public string Sentence { get; set; }
        public float Confidence { get; set; }

        public DetectedSentence() {
            Grammar = string.Empty;
            Sentence = string.Empty;
            Confidence = 0.0f;
        }
    }
}
