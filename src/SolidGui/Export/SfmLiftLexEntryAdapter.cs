using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Palaso.DictionaryServices.Lift;
using Palaso.DictionaryServices.Model;
using Palaso.Lift;
using Palaso.Lift.Options;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Export
{
    public class LiftLexEntryAdapter
    {
        protected LiftDataMapper liftDataMapper { get; private set; }
        public LexEntry LiftLexEntry { get; private set; }

        public List<Relation> Relations { get; private set; }
        public List<LiftLexEntryAdapter> SubEntries { get; private set; }

        public LiftLexEntryAdapter(LiftDataMapper dm)
        {
            liftDataMapper = dm;
            LiftLexEntry = dm.CreateItem();
            Relations = new List<Relation>();
            SubEntries = new List<LiftLexEntryAdapter>();
        }

        public void MakeRelation(string guid, string type)
        {
            LiftLexEntry.AddRelationTarget(type, guid); // string relationName?, string targetID
        }

        
    }

    public class SfmLiftLexEntryAdapter : LiftLexEntryAdapter
    {
        public string SfmID
        {
            get;
            set;
        }

        public int HomonymNumber
        {
            get { return LiftLexEntry.OrderInFile; }
        }

        public enum Concepts
        {
            Ignore,
            LexicalUnit,
            NoteBibliographic,
            NoteEncyclopedic,
            NoteGeneral,
            NoteAnthropology,
            NoteDiscourse,
            NoteGrammer,
            NotePhonology,
            NoteQuestion,
            NoteRestriction,
            Sense,
            NoteSociolinguistic,
            NoteSource,
            BorrowedWord,
            Confer,
            DateModified,
            HomonymNumber,
            CitationForm,
            Illustration,
            Pronunciation,
            Variant,
            Reversal,
            GrammaticalInfo_PS,
            Definition,
            Gloss,
            SemanticDomain,
            ExampleReference,
            ExampleSentence,
            ExampleSentenceTranslation,
            Etymology,
            EtymologySource,
            CustomField,
            LexicalRelationType,
            Comment,
            SubEntry,
            LexicalRelationLexeme,
            ScientificName
        }

        private static readonly Dictionary<string, Concepts> _conceptMap = new Dictionary<string, Concepts>
        {
            { "ignore", Concepts.Ignore },
            { "lexicalUnit", Concepts.LexicalUnit },
            { "noteBibliographic", Concepts.NoteBibliographic },
            { "noteEncyclopedic", Concepts.NoteEncyclopedic },
            { "note", Concepts.NoteGeneral },
            { "noteAnthropology", Concepts.NoteAnthropology },
            { "noteDiscourse", Concepts.NoteDiscourse },
            { "noteGrammar", Concepts.NoteGrammer },
            { "notePhonology", Concepts.NotePhonology },
            { "noteQuestion", Concepts.NoteQuestion },
            { "noteRestriction", Concepts.NoteRestriction },
            { "noteSociolinguistic", Concepts.NoteSociolinguistic },
            { "noteSource", Concepts.NoteSource },
            { "borrowedWord", Concepts.BorrowedWord },
            { "confer", Concepts.Confer },
            { "dateModified", Concepts.DateModified },
            { "homonym", Concepts.HomonymNumber }, // Really homograph number CP 2010-09
            { "citation", Concepts.CitationForm },
            { "illustration", Concepts.Illustration },
            { "pronunciation", Concepts.Pronunciation },
            { "variant", Concepts.Variant },
            { "reversal", Concepts.Reversal },
            { "sense", Concepts.Sense },
            { "grammi", Concepts.GrammaticalInfo_PS },
            { "definition", Concepts.Definition },
            { "gloss", Concepts.Gloss },
            { "semanticDomain", Concepts.SemanticDomain },
            { "example", Concepts.ExampleReference },
            { "exampleSentence", Concepts.ExampleSentence },
            { "exampleSentenceTranslation", Concepts.ExampleSentenceTranslation },
            { "etymology", Concepts.Etymology },
            { "etymologySource", Concepts.EtymologySource },
            { "custom", Concepts.CustomField },
            { "lexicalRelationType", Concepts.LexicalRelationType },
            { "lexicalRelationLexeme", Concepts.LexicalRelationLexeme },
            { "scientificName", Concepts.ScientificName },
            { "comment", Concepts.Comment }, // NOTE added smw 13sep2010
            { "subentry", Concepts.SubEntry } // added

        };

        private enum States
        {
            LexEntry,
            Sense,
            Example
        }

        private enum PartOfSpeechModes
        {
            Unknown,
            PartOfSpeechFirst,
            SenseFirst
        }

        private class LiftInfo
        {
            public string WritingSystem { get; set; }
            public Concepts LiftConcept { get; set; }
        }

        public SfmLiftLexEntryAdapter(LiftDataMapper dm, SfmLexEntry entry, SolidSettings solidSettings) :
            base(dm)
        {
            SfmLexEntry = entry;
            SfmID = entry.Name;
            SolidSettings = solidSettings;
            PartOfSpeechMode = PartOfSpeechModes.Unknown;
        }

        private SfmLexEntry SfmLexEntry { get; set; }

        public string GUID
        {
            get
            {
                return LiftLexEntry.Guid.ToString();
            }
        }

        public void AddSolidNote(string note)
        {
            AddMultiTextToPalasoDataObject("SOLID NOTE: " + note, "en", LiftLexEntry, PalasoDataObject.WellKnownProperties.Note);
        }

        private class StateInfo
        {
            public int Depth;
            public readonly States State;
            public readonly LiftLexEntryAdapter LexEntryAdapter;
            public LexEntry LiftLexEntry { get { return LexEntryAdapter.LiftLexEntry; } }

            public StateInfo(States state, int depth, LiftLexEntryAdapter lexEntryAdapter)
            {
                State = state;
                Depth = depth;
                LexEntryAdapter = lexEntryAdapter;
            }
        }

        public void PopulateEntry()
        {
            var states = new Stack<StateInfo>();
            states.Push(new StateInfo(States.LexEntry, -1, this));
            LexSense currentSense = null;
            string currentPartOfSpeech = "";

            //if the sfm already declares a guid, use that instead of a made up one
            var existingGuid = SfmLexEntry.GetFirstFieldWithMarker("guid");
            if (existingGuid != null)
            {
                try
                {
                    LiftLexEntry.Guid = new Guid(existingGuid.Value);
                }
                catch(Exception)
                {
                    //todo: tell someone that we couldn't parse that guid
                }
            }

            LexExampleSentence currentExample = null;
            foreach (var field in SfmLexEntry.Fields)
            {
                var unicodeValue =GetUnicodeValueFromLatin1(field.Value).Trim();
                var currentState = states.Peek();
                if (field.Depth <= currentState.Depth)
                {
                    if (currentSense != null && currentState.State == States.Sense)
                    {
                        // If there's no grammatical info, and we have a current set, then put that in now.
                        if (!currentSense.Properties.Exists(property => property.Key == LexSense.WellKnownProperties.PartOfSpeech))
                        {
                            currentSense.Properties.Add(new KeyValuePair<string, object>(LexSense.WellKnownProperties.PartOfSpeech, new OptionRef(currentPartOfSpeech)));
                        }
                    }
                    states.Pop();
                    currentState = states.Peek();
                }
                LiftInfo liftInfo = GetLiftInfoForField(field);
                switch (currentState.State)
                {
                    case States.LexEntry:
                        switch (liftInfo.LiftConcept)
                        {
                            case Concepts.Ignore: // dont add to LexEntry
                                break;
                            case Concepts.SubEntry:

                                string lexForm = "";
                                if(String.IsNullOrEmpty(unicodeValue))
                                {
                                    lexForm = states.Peek().LiftLexEntry.LexicalForm.ToString();
                                }
                                else
                                {
                                    lexForm = unicodeValue;
                                }
                                var subEntry = new LiftLexEntryAdapter(liftDataMapper);
                                states.Push(new StateInfo(States.LexEntry, field.Depth, subEntry));
                                currentState = states.Peek();
                                currentState.LiftLexEntry.LexicalForm[liftInfo.WritingSystem] = lexForm;
                                currentState.LiftLexEntry.AddRelationTarget("BaseForm", LiftLexEntry.Guid.ToString());
                                
                                SubEntries.Add(subEntry); 
                                break;
                            case Concepts.LexicalUnit:
                                LiftLexEntry.LexicalForm[liftInfo.WritingSystem] = unicodeValue;
                                break;
                            case Concepts.NoteBibliographic:
                            case Concepts.NoteEncyclopedic:
                            case Concepts.NoteGeneral:
                            case Concepts.NoteAnthropology:
                            case Concepts.NoteDiscourse:
                            case Concepts.NoteGrammer:
                            case Concepts.NotePhonology:
                            case Concepts.NoteQuestion:
                            case Concepts.NoteRestriction:
                            case Concepts.NoteSociolinguistic:
                            case Concepts.NoteSource:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, PalasoDataObject.WellKnownProperties.Note);
                                break;
                            case Concepts.BorrowedWord:
                                // NOTE Palaso does not support lift etymology as a first class element yet, so write it out as a lift trait (OptionRef).
                                var o = new OptionRef("borrowed");
                                o.Value = unicodeValue;
                                currentState.LiftLexEntry.Properties.Add(new KeyValuePair<string, object>("etymology", o));
                                break;
                            case Concepts.Confer:
                                // TODO This is a relation, come back to this when we know how to do relations. CP 2010-09
                                break;
                            case Concepts.DateModified:
                                var inModTime = unicodeValue.Trim();
                                DateTime dateValue;
                                if(!DateTime.TryParse(inModTime, out dateValue))
                                {
                                    DateTime.TryParseExact(inModTime, "dd/MMM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue);
                                }
                                if(dateValue != default(DateTime))
                                {
                                    currentState.LiftLexEntry.CreationTime = currentState.LiftLexEntry.ModificationTime = dateValue.ToUniversalTime();
                                    currentState.LiftLexEntry.ModifiedTimeIsLocked = true;
                                }
                                else
                                {
                                    //todo: when we have a log, tell it that didn't work (but don't report when the date time is just empty)
                                }
                                break;
                            case Concepts.HomonymNumber:
                                currentState.LiftLexEntry.OrderInFile = int.Parse(unicodeValue);
                                break;
                            case Concepts.CitationForm:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, LexEntry.WellKnownProperties.Citation);
                                break;
                            case Concepts.Pronunciation:
                                // NOTE Palaso does not support first class <pronunciation> element yet
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "pronunciation");
                                break;
                            case Concepts.Variant:
                                // NOTE Palaso does not support first class <variant> element yet
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "variant");
                                break;
                            case Concepts.Reversal:
                                // NOTE Palaso does not support first class <reversal> element yet
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "reversal");
                                break;
                            case Concepts.Etymology:
                                // NOTE Palaso does not support first class <etymology> element yet
                                var op = new OptionRef("proto");
                                op.Value = unicodeValue;
                                currentState.LiftLexEntry.Properties.Add(new KeyValuePair<string, object>("etymology", op));
                                break;
                            case Concepts.EtymologySource:
                                // NOTE Palaso does not support etymology-source yet
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "etymology-source");
                                break;
                            case Concepts.ScientificName:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "scientific-name");
                                break;
                            case Concepts.CustomField:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, field.Marker);
                                break;

                            case Concepts.Comment:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, PalasoDataObject.WellKnownProperties.Note);
                                break;

                            case Concepts.LexicalRelationType:
                                HandleLexicalRelation(field, currentState, unicodeValue, "");
                                break;
                                
                            case Concepts.GrammaticalInfo_PS:
                                PartOfSpeechMode = PartOfSpeechModes.PartOfSpeechFirst;
                                currentPartOfSpeech = unicodeValue;
                                currentSense = new LexSense();
                                currentState.LiftLexEntry.Senses.Add(currentSense);
                                states.Push(new StateInfo(States.Sense, field.Depth, currentState.LexEntryAdapter));
                                break;
                            case Concepts.Sense:
                                PartOfSpeechMode = PartOfSpeechModes.SenseFirst;
                                currentSense = new LexSense();
                                currentState.LiftLexEntry.Senses.Add(currentSense);
                                states.Push(new StateInfo(States.Sense, field.Depth, currentState.LexEntryAdapter));
                                break;
                        }
                        break;
                    case States.Sense:
                        switch (liftInfo.LiftConcept)
                        {
                            case Concepts.Ignore: // dont add to LexEntry
                                break;
                            case Concepts.NoteBibliographic:
                            case Concepts.NoteEncyclopedic:
                            case Concepts.NoteGeneral:
                            case Concepts.NoteAnthropology:
                            case Concepts.NoteDiscourse:
                            case Concepts.NoteGrammer:
                            case Concepts.NotePhonology:
                            case Concepts.NoteQuestion:
                            case Concepts.NoteRestriction:
                            case Concepts.NoteSociolinguistic:
                            case Concepts.NoteSource:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentSense, PalasoDataObject.WellKnownProperties.Note);
                                break;
                            case Concepts.Illustration:
                                var illustration = new PictureRef();
                                illustration.Value = unicodeValue;
                                currentSense.Properties.Add(new KeyValuePair<string, object>(LexSense.WellKnownProperties.Picture, illustration));
                                break;
                            case Concepts.GrammaticalInfo_PS:
                                var gi = new OptionRef(unicodeValue); // TODO One we could check the fieldValue against some RangeSet (OptionRefCollection)
                                // var optRefCollection = new OptionRefCollection();
                                currentSense.Properties.Add(new KeyValuePair<string, object>(LexSense.WellKnownProperties.PartOfSpeech, gi));
                                break;
                            case Concepts.Definition:
                                currentSense.Definition[liftInfo.WritingSystem] = unicodeValue;
                                break;
                            case Concepts.SemanticDomain:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentSense, "semantic-domain");
                                break;
                            case Concepts.CustomField:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentSense, field.Marker);
                                break;
                            case Concepts.Comment:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentSense, PalasoDataObject.WellKnownProperties.Note);
                                break;

                            case Concepts.Gloss:
                                currentSense.Gloss[liftInfo.WritingSystem] = unicodeValue;
                                break;

                            case Concepts.Sense:
                                if (PartOfSpeechMode == PartOfSpeechModes.PartOfSpeechFirst)
                                {
                                    currentState.Depth = field.Depth;
                                }
                                break;


                            //TODO: could be a kind of relation, once a mapping allows for one concept to cover multiple markers
//                             case Concepts.Antonym:
                               //TODO: Palaso (in OCt 2010) does not yet support refs coming out of 
                                //senses. So this will end up on the entry.
//                                HandleLexicalRelation(field, currentState, unicodeValue, "antonym");
//                                break;

                            // change state
                            case Concepts.ExampleReference:
                                // NOTE Palaso does not support ExampleReference yet
                                //AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.currentSense, "example-reference");

                                currentExample = new LexExampleSentence();
                                currentSense.ExampleSentences.Add(currentExample);
                                states.Push(new StateInfo(States.Example, field.Depth, currentState.LexEntryAdapter));

                                break;
                        }
                        break;
                    case States.Example:
                        switch (liftInfo.LiftConcept)
                        {
                            case Concepts.Ignore: // dont add to LexEntry
                                break;
                            case Concepts.NoteBibliographic:
                            case Concepts.NoteEncyclopedic:
                            case Concepts.NoteGeneral:
                            case Concepts.NoteAnthropology:
                            case Concepts.NoteDiscourse:
                            case Concepts.NoteGrammer:
                            case Concepts.NotePhonology:
                            case Concepts.NoteQuestion:
                            case Concepts.NoteRestriction:
                            case Concepts.NoteSociolinguistic:
                            case Concepts.NoteSource:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentExample, PalasoDataObject.WellKnownProperties.Note);
                                break;
                            case Concepts.CustomField:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentExample, field.Marker);
                                break;
                            case Concepts.ExampleSentence:
                                currentExample.Sentence[liftInfo.WritingSystem] = unicodeValue;
                                break;
                            case Concepts.ExampleSentenceTranslation:
                                currentExample.Translation[liftInfo.WritingSystem] = unicodeValue;
                                break;
                            case Concepts.Comment:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentExample, PalasoDataObject.WellKnownProperties.Note);
                                break;

                            default:
                                // TODO: log error
                                break;
                        }
                        break;
                }

            }
        }

        private void HandleLexicalRelation(SfmFieldModel field, StateInfo currentState, string unicodeValue, string type)
        {
            Relation relation;
            string targetID = "";

            //used for things like \an, antonym
            if(!string.IsNullOrEmpty(type))
            {
                targetID = unicodeValue.Trim();
            }
            // new mdf relation representation
            else if (unicodeValue.Contains("="))
            {
                int equalsSignPosition = unicodeValue.IndexOf('=');
                targetID = unicodeValue.Substring(equalsSignPosition + 1).Trim();
                type = unicodeValue.Substring(0, equalsSignPosition).Trim();
            }
            else // old mdf relation representation
            {
                type = unicodeValue;
                if (field.Children.Count > 0 && field.Children[0] != null)
                {
                    targetID = field.Children[0].Value;
                }
                else
                {
                    AddSolidNote("Invalid Relation. Could not find targetID for relation:" + type + " in " + currentState.LiftLexEntry.LexicalForm);
                }
            }

            // store in list
            relation = new Relation(targetID, type);
            currentState.LexEntryAdapter.Relations.Add(relation);
        }

        public string GetUnicodeValueFromLatin1(string value)
        {
            string retval;
                retval = string.Empty;
                if (value.Length > 0)
                {
                    Encoding byteEncoding = Encoding.GetEncoding("iso-8859-1");
                    //Encoding byteEncoding = Encoding.Unicode;
                    byte[] valueAsBytes = byteEncoding.GetBytes(value);
                    Encoding stringEncoding = Encoding.UTF8;
                    retval = stringEncoding.GetString(valueAsBytes);
                    if (retval.Length == 0)
                    {
                        retval = "Non Unicode Data Found";
                        // TODO: Need to lock this field of the current record at this point.
                        // The editor must *never* write back to the model (for this field)
                    }
                }

            return retval;
        }

        private PartOfSpeechModes PartOfSpeechMode { get; set; }

        public static void AddMultiTextToPalasoDataObject(string fieldValue, string writingSystem, PalasoDataObject dataObject, string propertyName)
        {
            var mt = new MultiText();
            mt[writingSystem] = fieldValue;
            dataObject.Properties.Add(new KeyValuePair<string, object>(propertyName, mt));
        }

        private static States ConceptState(Concepts concept, States state)
        {
            switch (concept)
            {
                case Concepts.Ignore:
                    return state;
                case Concepts.LexicalUnit:
                    return States.LexEntry;
                case Concepts.NoteBibliographic:
                    return state;
                case Concepts.NoteEncyclopedic:
                    return state;
                case Concepts.NoteGeneral:
                    return state;
                case Concepts.NoteAnthropology:
                    return state;
                case Concepts.NoteDiscourse:
                    return state;
                case Concepts.NoteGrammer:
                    return state;
                case Concepts.NotePhonology:
                    return state;
                case Concepts.NoteQuestion:
                    return state;
                case Concepts.NoteRestriction:
                    return state;
                case Concepts.Sense:
                    return States.Sense;
                case Concepts.NoteSociolinguistic:
                    return state;
                case Concepts.NoteSource:
                    return state;
                case Concepts.BorrowedWord:
                    return States.LexEntry;
                case Concepts.Confer:
                    return States.LexEntry;
                case Concepts.DateModified:
                    return States.LexEntry;
                case Concepts.HomonymNumber:
                    return States.LexEntry;
                case Concepts.CitationForm:
                    return States.LexEntry;
                case Concepts.Illustration:
                    return States.LexEntry;
                case Concepts.Pronunciation:
                    return States.LexEntry;
                case Concepts.Variant:
                    return States.LexEntry;
                case Concepts.Reversal:
                    return States.LexEntry;
                case Concepts.GrammaticalInfo_PS:
                    return States.Sense;
                case Concepts.Definition:
                    return States.Sense;
                case Concepts.Gloss:
                    return States.Sense;
                case Concepts.SemanticDomain:
                    return States.Sense;
                case Concepts.ExampleReference:
                    return States.Example;
                case Concepts.ExampleSentence:
                    return States.Example;
                case Concepts.ExampleSentenceTranslation:
                    return States.Example;
                case Concepts.Etymology:
                    return States.LexEntry;
                case Concepts.EtymologySource:
                    return States.LexEntry;
                case Concepts.ScientificName:
                    return States.Sense;
                case Concepts.CustomField:
                    return state;
                case Concepts.LexicalRelationType:
                    return States.LexEntry;
                case Concepts.LexicalRelationLexeme:
                    return States.LexEntry;
//                case Concepts.Antonym:
//                    return States.Sense;
                case Concepts.SubEntry:
                    return States.LexEntry;
            }
            throw new ApplicationException("boo hoo");
        }

        private SolidSettings SolidSettings { get; set; }

        private LiftInfo GetLiftInfoForField(SfmFieldModel field)
        {
            var result = new LiftInfo();
            var setting = SolidSettings.FindOrCreateMarkerSetting(field.Marker);

            result.LiftConcept = StringToConcept(setting.Mappings[(int)SolidMarkerSetting.MappingType.Lift]);
            result.WritingSystem = setting.WritingSystemRfc4646;

            return result;
        }

        private static Concepts StringToConcept(string s)
        {
            if (s == null)
            {
//SLOW                Console.WriteLine("\t### Ignoring unknown marker: " + s);
                return Concepts.Ignore;
            }
//SLOW            Console.WriteLine("\t" + s);
            return _conceptMap[s];
        }

    }
}
