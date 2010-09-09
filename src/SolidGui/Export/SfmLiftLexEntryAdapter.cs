using System;
using System.Collections.Generic;
using System.Text;
using Palaso.DictionaryServices.Model;
using Palaso.Lift;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Export
{
    public class SfmLiftLexEntryAdapter : LexEntry
    {
        public enum Concepts
        {
            Ignore,
            LexicalUnit,
            A,
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
            LexicalRelationLexeme
        }

        private static readonly Dictionary<string, Concepts> _conceptMap = new Dictionary<string, Concepts>
        {
            { "ignore", Concepts.Ignore },
            { "lexicalUnit", Concepts.LexicalUnit },
            { "a", Concepts.A },
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
            { "lexicalRelationLexeme", Concepts.LexicalRelationLexeme }

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

        public SfmLiftLexEntryAdapter(SfmLexEntry entry)
        {
        }

        public void PopulateEntry(SfmLexEntry entry)
        {
            var state = States.LexEntry;
            LexSense currentSense = null;
            LexExampleSentence currentExample = null;
            foreach (var field in entry.Fields)
            {
                LiftInfo liftInfo = GetLiftInfoForField(field);
                switch (state)
                {
                    case States.LexEntry:
                        switch (liftInfo.LiftConcept)
                        {
                            case Concepts.Sense:
                                currentSense = new LexSense();
                                state = States.Sense;
                                break;
                        }
                        break;
                    case States.Sense:
                        switch (liftInfo.LiftConcept)
                        {
                            case Concepts.Gloss:
                                currentSense.Gloss[liftInfo.WritingSystem] = field.Value;
                                break;
                            case Concepts.ExampleSentence:
                                currentExample = new LexExampleSentence();
                                currentExample.Sentence[liftInfo.WritingSystem] = field.Value;
                                break;
                        }
                        break;
                    case States.Example:
                        switch (liftInfo.LiftConcept)
                        {
                            case Concepts.ExampleSentence:
                                currentExample.Sentence[liftInfo.WritingSystem] = field.Value;
                                break;
                            default:
                                state = ConceptState(liftInfo.LiftConcept, state);
                                break;
                        }
                        break;
                    
                }

            }
        }

        private static States ConceptState(Concepts concept, States state)
        {
            switch (concept)
            {
                case Concepts.Sense:
                    return States.Sense;
            }
            throw new ApplicationException("boo hoo");
        }

        private static LiftInfo GetLiftInfoForField(SfmFieldModel field)
        {
            var result = new LiftInfo();
            var s = new SolidSettings();

            var setting = s.FindOrCreateMarkerSetting(field.Marker);
            result.LiftConcept = StringToConcept(setting.Mappings[(int)SolidMarkerSetting.MappingType.Lift]);
            result.WritingSystem = setting.WritingSystemRfc4646;

            return result;
        }

        private static Concepts StringToConcept(string s)
        {
            return _conceptMap[s];
        }

    }
}
