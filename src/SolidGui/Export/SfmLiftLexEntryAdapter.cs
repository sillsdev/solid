using System;
using System.Collections.Generic;
using System.Text;
using Palaso.DictionaryServices.Model;
using Palaso.Lift;
using Palaso.Lift.Options;
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
            var currentLexEntry = liftLexEntry;
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
                            case Concepts.SubEntry:
                                currentLexEntry = new LexEntry();
                                currentLexEntry.LexicalForm[liftInfo.WritingSystem] = field.Value;
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
                                // NOTE Palaso does not support lift etymology as a first class element yet, so write it out as a lift trait (OptionRef).
                                var o = new OptionRef("borrowed");
                                o.Value = field.Value;
                                liftLexEntry.Properties.Add(new KeyValuePair<string, object>("etymology", o));
                                break;
                            case Concepts.Confer:
                                // TODO This is a relation, come back to this when we know how to do relations. CP 2010-09
                                break;
                            case Concepts.DateModified:
                                var inModTime = field.Value;
                                liftLexEntry.ModificationTime = Convert.ToDateTime(inModTime); // convert string to DateTime
                                break;
                            case Concepts.HomonymNumber:
                                liftLexEntry.OrderInFile = int.Parse(field.Value);
                                break;
                            case Concepts.CitationForm:
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, LexEntry.WellKnownProperties.Citation);
                                break;
                            case Concepts.Pronunciation:
                                // NOTE Palaso does not support first class <pronunciation> element yet
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, "pronunciation");
                                break;
                            case Concepts.Variant:
                                // NOTE Palaso does not support first class <variant> element yet
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, "variant");
                                break;
                            case Concepts.Reversal:
                                // NOTE Palaso does not support first class <reversal> element yet
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, "reversal"); 
                                break;
                            case Concepts.Etymology:
                                // NOTE Palaso does not support first class <etymology> element yet
                                var op = new OptionRef("proto");
                                op.Value = field.Value;
                                liftLexEntry.Properties.Add(new KeyValuePair<string, object>("etymology", op));
                                break;
                            case Concepts.EtymologySource:
                                // NOTE Palaso does not support etymology-source yet
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, "etymology-source");
                                break;
                            case Concepts.CustomField:
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, field.Marker);
                                break;
                            case Concepts.LexicalRelationType:
                                // TODO
                                break;
                            case Concepts.LexicalRelationLexeme:
                                // TODO
                                break;
                            case Concepts.Comment:
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, PalasoDataObject.WellKnownProperties.Note);
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
                                var illustration = new PictureRef();
                                illustration.Value = field.Value;
                                currentSense.Properties.Add(new KeyValuePair<string, object>(LexSense.WellKnownProperties.Picture, illustration));
                                break;
                            case Concepts.GrammaticalInfo_PS:
                                var gi = new OptionRef(field.Value); // TODO One we could check the fieldValue against some RangeSet (OptionRefCollection)
                                // var optRefCollection = new OptionRefCollection();
                                currentSense.Properties.Add(new KeyValuePair<string, object>(LexSense.WellKnownProperties.PartOfSpeech, gi));
                                break;
                            case Concepts.Definition:
                                currentSense.Definition[liftInfo.WritingSystem] = field.Value;
                                break;
                            case Concepts.SemanticDomain:
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, "semantic-domain");
                                break;
                            case Concepts.CustomField:
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, field.Marker);
                                break;
                            case Concepts.Comment:
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, PalasoDataObject.WellKnownProperties.Note);
                                break;
                                
                            case Concepts.Gloss:
                                currentSense.Gloss[liftInfo.WritingSystem] = field.Value;
                                break;

                            // change state
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
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, field.Marker);
                                break;
                            case Concepts.ExampleReference:
                                // NOTE Palaso does not support ExampleReference yet
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, "example-reference");
                                break;
                            case Concepts.ExampleSentence:
                                currentExample.Sentence[liftInfo.WritingSystem] = field.Value;
                                break;
                            case Concepts.ExampleSentenceTranslation:
                                currentExample.Translation[liftInfo.WritingSystem] = field.Value;
                                break;
                            case Concepts.Comment:
                                AddMultiTextToPalasoDataObject(field.Value, liftInfo.WritingSystem, liftLexEntry, PalasoDataObject.WellKnownProperties.Note);
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

        private static void AddMultiTextToPalasoDataObject(string fieldValue, string writingSystem, LexEntry liftLexEntry, string propertyName)
        {
            var mt = new MultiText();
            mt[writingSystem] = fieldValue;
            liftLexEntry.Properties.Add(new KeyValuePair<string, object>(propertyName, mt));
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
