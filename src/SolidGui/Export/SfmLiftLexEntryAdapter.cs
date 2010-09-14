using System;
using System.Collections.Generic;
using System.Text;
using Palaso.DictionaryServices.Model;
using Palaso.Lift;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Export
{
    public class SfmLiftLexEntryAdapter
    {
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
            LexicalRelationLexeme
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
            { "homonym", Concepts.HomonymNumber },
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

            { "comment", Concepts.Comment }, // NOTE added smw 13sep2010
            { "subentry", Concepts.SubEntry } // added

        };

        private enum States
        {
            LexEntry,
            Sense,
            Example
        }

        private class LiftInfo
        {
            public string WritingSystem { get; set;}
            public Concepts LiftConcept { get; set; }
        }

        public SfmLiftLexEntryAdapter(SfmLexEntry entry, SolidSettings solidSettings)
        {
            SfmEntry = entry;
            SolidSettings = solidSettings;
        }

        private SfmLexEntry SfmEntry { get; set; }

        public void PopulateEntry(LexEntry liftLexEntry)
        {
            var state = States.LexEntry;
            LexSense currentSense = null;
            LexExampleSentence currentExample = null;
            foreach (var field in SfmEntry.Fields)
            {
                LiftInfo liftInfo = GetLiftInfoForField(field);
                switch (state)
                {
                    case States.LexEntry:
                        switch (liftInfo.LiftConcept)
                        {
                            case Concepts.Ignore: // dont add to LexEntry
                                break;
                            case Concepts.LexicalUnit:
                                liftLexEntry.LexicalForm[liftInfo.WritingSystem] = field.Value;
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
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, PalasoDataObject.WellKnownProperties.Note);
                                break;
                            case Concepts.BorrowedWord:
                                //AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, PalasoDataObject.);
                                break;
                            case Concepts.Confer:
                                //?
                                break;
                            case Concepts.DateModified:
                                var inModTime = field.Value;
                                liftLexEntry.ModificationTime = Convert.ToDateTime(inModTime); // convert string to DateTime
                                break;
                            case Concepts.HomonymNumber:
                                //?
                                break;
                            case Concepts.CitationForm:
                                //this.CitationForm = field.Value;
                                break;
                            case Concepts.Pronunciation:
                                //?
                                break;
                            case Concepts.Variant:
                                
                                break;
                            case Concepts.Reversal:
                                //?
                                break;
                            case Concepts.Etymology:
                                //?
                                break;
                            case Concepts.EtymologySource:
                                //?
                                break;
                            case Concepts.CustomField:
                                //?
                                break;
                            case Concepts.LexicalRelationType:
                                
                                break;
                            case Concepts.LexicalRelationLexeme:
                                //?
                                break;
                            case Concepts.SubEntry:
                                //make new LexEntry??
                                break;
                            case Concepts.Comment:
                                //?
                                break;

                            // change state
                            case Concepts.Sense:
                                currentSense = new LexSense();
                                liftLexEntry.Senses.Add(currentSense);
                                state = States.Sense;
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
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, PalasoDataObject.WellKnownProperties.Note);
                                break;
                            
                            case Concepts.Illustration:
                                //?
                                break;
                            case Concepts.GrammaticalInfo_PS:
                                //?
                                break;
                            case Concepts.Definition:
                                
                                break;
                            case Concepts.SemanticDomain:
                                //?
                                break;
                            case Concepts.CustomField:
                                //?
                                break;
                            case Concepts.Comment:
                                //?
                                break;

                            
                            case Concepts.Gloss:
                                currentSense.Gloss[liftInfo.WritingSystem] = field.Value;

                                break;
                            case Concepts.ExampleSentence:
                                currentExample = new LexExampleSentence();
                                currentSense.ExampleSentences.Add(currentExample);
                                currentExample.Sentence[liftInfo.WritingSystem] = field.Value;
                                state = States.Example;
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
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, PalasoDataObject.WellKnownProperties.Note);
                                break;
                            case Concepts.CustomField:
                                //?
                                break;

                            case Concepts.ExampleReference:
                                
                                break;
                            case Concepts.ExampleSentence:
                                currentExample.Sentence[liftInfo.WritingSystem] = field.Value;
                                break;
                            case Concepts.ExampleSentenceTranslation:
                                //?
                                break;
                            case Concepts.Comment:
                                //?
                                break;


                            default:
                                // change state
                                state = ConceptState(liftInfo.LiftConcept, state);
                                break;
                        }
                        break;
                    
                }

            }
        }

        private static void AddMultiTextToPalasoDataObject(string fieldValue, string writingSystem, LexEntry liftLexEntry, string type)
        {
            var mt = new MultiText();
            mt[writingSystem] = fieldValue;
            liftLexEntry.Properties.Add(new KeyValuePair<string, object>(type, mt));
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
                case Concepts.CustomField:
                    return state;
                case Concepts.LexicalRelationType:
                    return States.LexEntry;
                case Concepts.LexicalRelationLexeme:
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
            Console.WriteLine(s);
            return _conceptMap[s];
        }

    }
}
