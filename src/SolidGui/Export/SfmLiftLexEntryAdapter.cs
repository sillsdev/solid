﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Palaso.DictionaryServices.Lift;
using Palaso.DictionaryServices.Model;
using Palaso.Lift;
using Palaso.Lift.Options;
using Palaso.Progress.LogBox;
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
            Antonym,
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
            ScientificName,
            Synonym
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
            { "antonym", Concepts.Antonym },
            { "synonym", Concepts.Synonym },
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

        public void PopulateEntry(IProgress progress)
        {
            var states = new Stack<StateInfo>();
            states.Push(new StateInfo(States.LexEntry, -1, this));
            LexSense currentSense = null;
            string currentPartOfSpeech = "";
            var unicodeEntryName = GetUnicodeValueFromLatin1(SfmLexEntry.Name).Trim();

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
                   progress.WriteError(unicodeEntryName+ ": Could not parse the guid "+existingGuid.Value);
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
                            default:
                                    progress.WriteError(unicodeEntryName+ ": Could not handle the concept '{0}' in the LexEntry state .",liftInfo.LiftConcept );
                                break;
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
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "bibliographic");
                                break;

                            case Concepts.NoteGrammer:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "grammar");
                                break;
                            case Concepts.NoteEncyclopedic:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "encyclopedic");
                                break;
                            case Concepts.NoteGeneral:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "");
                                break;
                            case Concepts.NoteAnthropology:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "anthropology");
                                break;
                            
                            case Concepts.NoteDiscourse:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "discourse");
                                break;
 
                            case Concepts.NotePhonology:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "phonology");
                                break;
                            case Concepts.NoteQuestion:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "question");
                                break;

                            case Concepts.NoteRestriction:  //nb: FLEx 6.03 Sena 3 export uses the 's' here, so that's why I did that
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "restrictions");
                                break;

                            case Concepts.NoteSociolinguistic:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "sociolinguistic");
                                break;

                            case Concepts.NoteSource:
                                AddEntryNote(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "source");
                                break;

                            case Concepts.BorrowedWord:
                                AddEtymology(currentState.LiftLexEntry, /*type*/ "borrowed",  /*source language*/unicodeValue);
                                break;
                            case Concepts.Confer:
                                HandleLexicalRelation(field, currentState, unicodeValue, "confer");
                                break;
                            case Concepts.DateModified:
                                var inModTime = unicodeValue.Trim();
                                DateTime dateValue;
                                if(!DateTime.TryParse(inModTime, out dateValue))
                                {
                                     if (!DateTime.TryParseExact(inModTime, "dd/MMM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                                    {
                                        DateTime.TryParseExact(inModTime, "dd/MMM/yy", CultureInfo.InvariantCulture,
                                                               DateTimeStyles.None, out dateValue);
                                    }
                                }
                                if(dateValue != default(DateTime))
                                {
                                    currentState.LiftLexEntry.CreationTime = currentState.LiftLexEntry.ModificationTime = dateValue.ToUniversalTime();
                                    currentState.LiftLexEntry.ModifiedTimeIsLocked = true;
                                }
                                else
                                {
                                    progress.WriteWarning("Could not parse the date: "+inModTime);
                                }
                                break;
                            case Concepts.HomonymNumber:
                                currentState.LiftLexEntry.OrderInFile = int.Parse(unicodeValue);
                                break;
                            case Concepts.CitationForm:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, LexEntry.WellKnownProperties.Citation);
                                break;
                            case Concepts.Pronunciation:
                                AddPronunciation(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry);
                                break;
                            case Concepts.Variant:
                                AddVariant(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry);
                                break;

                            case Concepts.Etymology:
                                //Note: we don't yet provide a way to indicate the form of the source word, just the language of it
                                AddEtymology(currentState.LiftLexEntry, /*type*/ "borrowed",  /*source*/unicodeValue);
                                break;

                                progress.WriteWarning(unicodeEntryName + ": SOLID cannot yet create real LIFT <etymology> elements, so instead it will create a <field> with type='etymology'");
                                 var op = new OptionRef("proto");
                                op.Value = unicodeValue;
                                currentState.LiftLexEntry.Properties.Add(new KeyValuePair<string, object>("etymology", op));
                                break;
                            case Concepts.EtymologySource:
                                 progress.WriteWarning(unicodeEntryName + ": SOLID cannot yet create real LIFT <etymology-source> elements, so instead it will create a <field> with type='etymology-source'");
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentState.LiftLexEntry, "etymology-source");
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
                            default:
                                progress.WriteError(unicodeEntryName + ": Could not handle the concept '{0}' in the sense state.", liftInfo.LiftConcept);
                                break;

                            case Concepts.Ignore: // dont add to LexEntry
                                break;
                            case Concepts.NoteBibliographic:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "bibliographic");
                                break;

                            case Concepts.NoteGrammer:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "grammar");
                                break;
                            case Concepts.NoteEncyclopedic:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "encyclopedic");
                                break;
                            case Concepts.NoteGeneral:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "");
                                break;
                            case Concepts.NoteAnthropology:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "anthropology");
                                break;

                            case Concepts.NoteDiscourse:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "discourse");
                                break;

                            /* NO, not on senses
                             * case Concepts.NotePhonology:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "phonology");
                                break;
                             */
                            case Concepts.NoteQuestion:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "question");
                                break;

                            case Concepts.NoteRestriction:  //nb: FLEx 6.03 Sena 3 export uses the 's' here, so that's why I did that
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "restrictions");
                                break;

                            case Concepts.NoteSociolinguistic:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "sociolinguistic");
                                break;

                            case Concepts.NoteSource:
                                AddSenseNote(unicodeValue, liftInfo.WritingSystem, currentSense, "source");
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






                            case Concepts.ScientificName:
                                AddMultiTextToPalasoDataObject(unicodeValue, liftInfo.WritingSystem, currentSense, "scientific-name");
                                break;
                            case Concepts.Reversal:
                                AddSenseReversal(unicodeValue, liftInfo.WritingSystem, currentSense, string.Empty/*reversal type*/);
                                break;

                            case Concepts.Antonym:
                                AddRelationToSense(currentSense, unicodeValue, "antonym");//review: exactly spelling?
                                break;
                            case Concepts.Synonym:
                                AddRelationToSense(currentSense, unicodeValue, "synonym");//review: exactly spelling?
                                break;
                            
                            case Concepts.LexicalRelationLexeme:
                                AddRelationToSense(currentSense, unicodeValue, string.Empty);
                                break;
                            
                            case Concepts.Confer:
                                 AddRelationToSense(currentSense, unicodeValue, "confer");
                                break;

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
                            default:
                                progress.WriteError(unicodeEntryName + ": Could not handle the concept '{0}' in the example state.", liftInfo.LiftConcept);
                                break;

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
                        }
                        break;
                }

            }
        }

        private void AddEtymology(LexEntry liftLexEntry, string type, string sourcLanguage)
        {
            liftLexEntry.Etymologies.Add(new LexEtymology(type, sourcLanguage));
        }

        private void AddPronunciation(string unicodeValue, string writingSystem, LexEntry liftLexEntry)
        {
            var phonetic = new LexPhonetic();
            phonetic.SetAlternative(writingSystem, unicodeValue);
            liftLexEntry.Pronunciations.Add(phonetic);
        }

        private void AddEntryNote(string unicodeValue, string writingSystem, LexEntry liftLexEntry, string noteType)
        {
            var note = new LexNote(noteType);
            note.SetAlternative(writingSystem,unicodeValue);
            liftLexEntry.Notes.Add(note);
        }
        private void AddSenseNote(string unicodeValue, string writingSystem, LexSense sense, string noteType)
        {
            var note = new LexNote(noteType);
            note.SetAlternative(writingSystem, unicodeValue);
            sense.Notes.Add(note);
        }
        private void AddSenseReversal(string unicodeValue, string writingSystem, LexSense sense, string reversalType)
        {
            var lexReversal = new LexReversal();
            lexReversal.SetAlternative(writingSystem, unicodeValue);
            if(!string.IsNullOrEmpty(reversalType))
            {
                lexReversal.Type = reversalType;
            }
            sense.Reversals.Add(lexReversal);
        }

        private void AddVariant(string form, string writingSystem, LexEntry liftLexEntry)
        {
            var variant = new LexVariant();
            variant.SetAlternative(writingSystem,form);
            liftLexEntry.Variants.Add(variant);
        }

        private void HandleLexicalRelation(SfmFieldModel field, StateInfo currentState, string unicodeValue, string type)
        {
            string type1 = type;
            Relation relation1;
            string targetID = "";

            //used for things like \an, antonym
            if(!string.IsNullOrEmpty(type1))
            {
                targetID = unicodeValue.Trim();
            }
                // new mdf relation representation
            else if (unicodeValue.Contains("="))
            {
                int equalsSignPosition = unicodeValue.IndexOf('=');
                targetID = unicodeValue.Substring(equalsSignPosition + 1).Trim();
                type1 = unicodeValue.Substring(0, equalsSignPosition).Trim();
            }
            else // old mdf relation representation
            {
                type1 = unicodeValue;
                if (field.Children.Count > 0 && field.Children[0] != null)
                {
                    targetID = field.Children[0].Value;
                }
                else
                {
                    AddSolidNote("Invalid Relation. Could not find targetID for relation:" + type1 + " in " + currentState.LiftLexEntry.LexicalForm.ToString());
                }
            }

            // store in list
            relation1 = new Relation(targetID, type1);
            Relation relation = relation1;
            currentState.LexEntryAdapter.Relations.Add(relation);
        }

        /// <summary>
        /// Review: note that unlike what was attempted for entry, I'm just making a relation to the form, here.
        /// </summary>
        private void AddRelationToSense(LexSense sense, string target, string type)
        {
            sense.AddRelationTarget(type, target);
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

#if unused
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
#endif

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
