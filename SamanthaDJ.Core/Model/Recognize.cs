using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Core.Model {

    public class Recognize {


        #region Properties

        private List<RecognizedInstructionEn> recognizedInstructionList { get; set; }
        public ErrorClass Error { get; }

        #endregion

        #region Constructors

        public Recognize(List<RecognizedInstructionEn> recognizedInstructionList) {
            this.recognizedInstructionList = recognizedInstructionList;
        }

        #endregion Constructors

        #region GetRecognizedInstructions

        /// <summary>
        ///     Determina la naturaleza de cada palabra (tipo de sustantivo): Verbo, Adjetivo, sujeto, pronombre, articulo, pregunta, numerador, etc
        /// </summary>
        /// <param name="strCommand">Lista de palabras (separadas por comas) que se van a clasificar</param>
        public List<RecognizedInstructionEn> GetRecognizedInstructions(
            String strCommandToSplit,
            //List<string> reservedWordList,
            //List<ValidWordEn> validWordList,
            out string result
        ) {
            result = string.Empty;
            //  Obtenemos una lista (sin comas) del parametro "strCommand"
            String[] _wordsCommand = strCommandToSplit.Split(' ');

            //  Eliminamos de la lista los elementos vacios o nulos
            _wordsCommand = _wordsCommand.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            try {
                string verb = string.Empty;
                string substantive = string.Empty;
                string adjective = string.Empty;
                string article = string.Empty;
                string pronoun = string.Empty;
                string numerator = string.Empty;

                //  Variable para determinar si alguna palabra NO fue procesada,
                //  Esto nunca debe ocurrir si la librera de *RecognizedVoice*,
                //  pero si pueden venir de una linea de comandos.
                bool anyWordNotFound = false;

                //  Lista para acumular instrucciones por cada palabra procesada en *_wordsCommand*
                List<RecognizedInstructionEn> instructionBackList = new List<RecognizedInstructionEn>();
                int countWordProcessed = 0;
                int countWordFound = 0;
                //int countWordFoundAndClassified = 0;

                //  Obtenemos la Estructura y Clasificacion de los sustantivos
                foreach (String icommandword in _wordsCommand) {

                    #region Ignore next Words

                    //  Esta varible indicara si detecto una palabra a ignorar
                    bool _coincidence = false;

                    //  Checamos si "_wordCommandEach" coincide con una palabra que deba ser ignorada
                    //foreach (String _word_each in reservedWordList) {
                    //    if (icommandword.ToUpper().Trim().Equals(_word_each.ToUpper().Trim())) {
                    //        _coincidence = true;
                    //        break;
                    //    }
                    //}
                    //  Si se encontro una palabra que coincida con una que deba ignorar entonces la ignoramos y continuamos
                    //  con la siguiente palabra
                    if (_coincidence == true)
                        continue;

                    #endregion Ignore next Words

                    countWordProcessed++;

                    bool wordFound = false;
                    string verb_word = string.Empty;
                    string substantive_word = string.Empty;
                    string adjective_word = string.Empty;
                    string article_word = string.Empty;
                    string pronoun_word = string.Empty;
                    string adverb_word = string.Empty;
                    string preposition_word = string.Empty;
                    string interjection_word = string.Empty;
                    string conjunction_word = string.Empty;
                    string numerator_word = string.Empty;

                    List<RecognizedInstructionEn> listWordClass = null;
                    //foreach (ValidWordEn ivw in validWordList) {
                    //    if (ivw.ValueWord.ToLower().Trim().Equals(icommandword.ToLower().Trim())) {
                    //        switch (ivw.WordClass) {
                    //            case "VERB":
                    //                verb_word = ivw.CodeWord;
                    //                verb = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                //listWordClass = GetInstructionVerb(verb_word);
                    //                break;
                    //            case "SUBSTANTIVE":
                    //                substantive_word = ivw.CodeWord;
                    //                substantive = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                //listWordClass = GetInstructionSubstantive(substantive_word);
                    //                break;
                    //            case "ADJECTIVE":
                    //                adjective_word = ivw.CodeWord;
                    //                adjective = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                //listWordClass = GetInstructionAdjective(adjective_word);
                    //                break;
                    //            case "ARTICLE":
                    //                article_word = string.Empty;
                    //                countWordFoundAndClassified++;
                    //                break;
                    //            case "PRONOUN":
                    //                pronoun_word = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                break;
                    //            case "ADVERB":
                    //                adverb_word = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                break;
                    //            case "PREPOSITION":
                    //                preposition_word = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                break;
                    //            case "INTERJECTION":
                    //                interjection_word = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                break;
                    //            case "CONJUNCTION":
                    //                conjunction_word = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                break;
                    //            case "NUMERATOR":
                    //                numerator_word = ivw.CodeWord;
                    //                numerator = ivw.CodeWord;
                    //                countWordFoundAndClassified++;
                    //                //listWordClass = GetInstructionNumerator(numerator_word);
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //        wordFound = true;
                    //        countWordFound++;
                    //        break;
                    //    }
                    //}
                    if (wordFound && listWordClass != null) {

                        foreach (RecognizedInstructionEn itemNew in listWordClass) {
                            bool isRepeat = false;
                            foreach (RecognizedInstructionEn itemBack in instructionBackList) {
                                if (itemBack.RecognizedInstructionId == itemNew.RecognizedInstructionId) {
                                    isRepeat = true;
                                    break;
                                }
                            }
                            if (!isRepeat) {
                                itemNew.MarkedToAdd = true;
                            }
                        }
                        foreach (RecognizedInstructionEn xxx in listWordClass) {
                            if (xxx.MarkedToAdd)
                                instructionBackList.Add(xxx);
                        }
                    } else {
                        //  Word not found
                        anyWordNotFound = true;
                    }
                }

                if (verb == string.Empty &&
                     substantive == string.Empty &&
                     adjective == string.Empty &&
                     pronoun == string.Empty &&
                     numerator == string.Empty) {
                    // - No se reconocio ninguna palabra en la Base de Datos
                    // - *strCommand* no trajo una cadena spliteable o palabras a procesar
                    result = "NOINSTRUCTION";
                    return null;
                }

                List<RecognizedInstructionEn> instructionFoundList = GetInstructionSpecific(
                    verb,
                    substantive,
                    adjective,
                    Article: string.Empty,
                    pronoun,
                    Adverb: string.Empty,
                    Preposition: string.Empty,
                    Interjection: string.Empty,
                    Conjunction: string.Empty,
                    Numerator: numerator
                );
                if (instructionFoundList != null && instructionFoundList.Count > 0) {
                    if (anyWordNotFound) {
                        //
                        List<RecognizedInstructionEn> tempList = GetValidIntruction(instructionFoundList);
                        if (tempList.Count > 0) {
                            result = "OK";
                            return tempList;
                        } else {
                            if (countWordFound >= 3) {
                                result = "OK";
                                return instructionFoundList;
                            } else {
                                result = "INSTRUCTIONNOTFOUND";
                                return null;
                            }
                        }
                    } else {
                        result = "OK";
                        return instructionFoundList;
                    }
                } else {
                    //  De mi lista de respaldo hay algun objeto que no requiera "Presicion" ?
                    if (instructionBackList.Count > 0) {
                        List<RecognizedInstructionEn> tempList = GetValidIntruction(instructionBackList);
                        if (tempList.Count > 0) {
                            result = "OK";
                            return tempList;
                        } else {
                            result = "INSTRUCTIONNOTFOUND";
                            return null;
                        }
                    } else {
                        result = "INSTRUCTIONNOTFOUND";
                        return null;
                    }
                }

            } catch (Exception ex) {
                Error.Code = "SamanthaChild_Analizer_GetRecognizedInstructions";
                Error.Message = "ERROR Message: " + ex.Message;
                Error.Track = "StackTrace" + ex.StackTrace;

                result = "EXCEPTION";

                return null;
            }
        }

        #endregion GetRecognizedInstructions

        private List<RecognizedInstructionEn> GetValidIntruction(List<RecognizedInstructionEn> pList) {
            List<RecognizedInstructionEn> tempList = new List<RecognizedInstructionEn>();
            foreach (RecognizedInstructionEn item in pList) {
                //if (item.Precision == false) {
                //    tempList.Add(item);
                //}
            }
            return tempList;
        }

        private List<RecognizedInstructionEn> GetInstructionSpecific(
            string verb,
            string Substantive,
            string Adjective,
            string Article,
            string Pronoun,
            string Adverb,
            string Preposition,
            string Interjection,
            string Conjunction,
            string Numerator
        ) {
            List<RecognizedInstructionEn> tempList = new List<RecognizedInstructionEn>();
            //tempList = 
            //    recognizedInstructionList
            //    .Where(item =>
            //        item.Verb == verb &&
            //        item.Substantive == Substantive &&
            //        item.Adjective == Adjective &&
            //        item.Article == Article &&
            //        item.Pronoun == Pronoun &&
            //        item.Adverb == Adverb &&
            //        item.Preposition == Preposition &&
            //        item.Interjection == Interjection &&
            //        item.Conjunction == Conjunction &&
            //        item.Numerator == Numerator
            //    ).ToList();
            //return tempList;
            return null;
        }
        /*
        private List<RecognizedInstructionEn> GetInstructionVerb(string verb) {
            List<RecognizedInstructionEn> tempList = new List<RecognizedInstructionEn>();
            tempList =
                recognizedInstructionList
                .Where(item =>
                    item.Verb == verb).ToList();
            return tempList;
        }

        private List<RecognizedInstructionEn> GetInstructionSubstantive(string substantive
        ) {
            List<RecognizedInstructionEn> tempList = new List<RecognizedInstructionEn>();
            tempList =
                recognizedInstructionList
                .Where(item =>
                    item.Substantive == substantive).ToList();
            return tempList;
        }

        private List<RecognizedInstructionEn> GetInstructionAdjective(string adjective) {
            List<RecognizedInstructionEn> tempList = new List<RecognizedInstructionEn>();
            tempList =
                recognizedInstructionList
                .Where(item =>
                    item.Adjective == adjective).ToList();
            return tempList;
        }

        private List<RecognizedInstructionEn> GetInstructionNumerator(string numerator) {
            List<RecognizedInstructionEn> tempList = new List<RecognizedInstructionEn>();
            tempList =
                recognizedInstructionList
                .Where(item =>
                    item.Numerator == numerator).ToList();
            return tempList;
        }
        */
    }
}