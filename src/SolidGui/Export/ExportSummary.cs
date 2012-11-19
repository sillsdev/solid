using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Palaso.Progress;
using Palaso.UI.WindowsForms.Progress;
using SolidGui.Engine;
using SolidGui.Model;

namespace SolidGui.Export
{
    public class ExportSummary : IExporter
    {
        public static ExportSummary Create()
        {
            return new ExportSummary();
        }

        public void Export(string inputFilePath, string outputFilePath)
        {
            
        }

        public void Export(IEnumerable<Record> records, string outputFilePath) // added to implement interface smw:10sep2010
        {
            throw new System.NotImplementedException(); 
        }

        public void Export(IEnumerable<Record> records, SolidSettings solidSettings, string outputFilePath, IProgress progress)
        {
            throw new System.NotImplementedException();
        }

        public void ExportAsync(object sender, DoWorkEventArgs args)
        {
            const string g = @"1d,First dual
1e,First plural exclusive
1i,First plural inclusive
1p,First plural
1s,First singular
2d,Second dual
2p,Second plural
2s,Second singular
3d,Third dual
3p,Third plural
3s,Third singular
4d,Non-animate dual
4p,Non-animate plural
4s,Non-animate singular
a,Alternate for parse
an,Antonym
bb,Bibliography
bw,Borrowed word (loan)
ce,Cross-ref. gloss (E)
cf,Cross-reference
cn,Cross-ref. gloss (n)
cr,Cross-ref. gloss (r)
de,Definition (E)
dn,Definition (n)
dr,Definition (r)
dt,Date (last edited)
dv,Definition (v)
ec,Etymology comment
ee,Encyclopedic info. (E)
eg,Etymology gloss (E)
en,Encyclopedic info. (n)
er,Encyclopedic info. (r)
es,Etymology source
et,Etymology (proto form)
ev,Encyclopedic info. (v)
ge,Gloss (E)
gn,Gloss (n)
gr,Gloss (r)
gv,Gloss (v)
hm,Homonym number
is,Index of semantics
lc,Citation form
le,Lexical function gloss (E)
lf,Lexical function label
ln,Lexical function gloss (n)
lr,Lexical function gloss (r)
lt,Literally
lv,Lexical function value
lx,Lexeme
mn,Main entry cross-ref.
mr,Morphology
na,Notes (anthropology)
nd,Notes (discourse)
ng,Notes (grammar)
np,Notes (phonology)
nq,Notes (questions)
ns,Notes (sociolinguistics)
nt,Notes (general)
oe,Only/restrictions (E)
on,Only/restrictions (n)
or,Only/restrictions (r)
ov,Only/restrictions (v)
pc,Picture
pd,Paradigm
pde,Paradigm form gloss (E)
pdl,Paradigm label
pdn,Paradigm form gloss (n)
pdr,Paradigm form gloss (r)
pdv,Paradigm form
ph,Phonetic form
pl,Plural form
pn,Part of speech (n)
ps,Part of speech
rd,Reduplication form(s)
re,Reversal (E)
rf,Reference
rn,Reversal (n)
rr,Reversal (r)
sc,Scientific name
sd,Semantic domain
se,Subentry
sg,Singular form
sn,Sense number
so,Source
st,Status
sy,Synonym
tb,Table
th,Thesaurus
u,Underlying for parse
ue,Usage (E)
un,Usage (n)
ur,Usage (r)
uv,Usage (v)
va,Variant form(s)
ve,Variant comment (E)
vn,Variant comment (n)
vr,Variant comment (r)
we,Word-level gloss (E)
wn,Word-level gloss (n)
wr,Word-level gloss (r)
xe,Example free trans. (E)
xn,Example free trans. (n)
xr,Example free trans. (r)
xv,Example (v)";

            var guesses = new Dictionary<string, string>();
            foreach (var marker in g.Split((new []{'\n'})))
            {
                var pairs = marker.Split(new[] {','});
                var m = pairs[0];
                var desc = pairs[1];
                guesses.Add(m.Trim(), desc.Trim());
            }

            var progress = (ProgressState)args.Argument;
            var exportArguments = (ExportArguments)progress.Arguments;
            using (var w = new StreamWriter(exportArguments.outputFilePath))
            {
                foreach (var rawMarker in exportArguments.markerSettings.MarkersInDictioanary)
                {
                    var setting = exportArguments.markerSettings.GetMarkerSetting(rawMarker);

                    var marker = rawMarker.Replace("_x005F_", "").Replace("_x0031_", "1").Replace("_x0032_", "2").Replace("_x0033_", "3").Replace("_x0034_", "4");
                    w.Write(marker+'\t');
                    bool first = true;

                    foreach (var property in setting.StructureProperties)
                    {
                        if(!first)
                            w.Write(",");
                        first = false;
                        w.Write(property.Parent);
                    }
                    w.Write('\t');
                    if (guesses.ContainsKey(marker))
                    {
                        w.Write(guesses[marker]);
                    }
                    else
                    {
                        w.Write("?");
                    }
                    w.WriteLine();
                }
            }
        }

        public string ModifyDestinationIfNeeded(string destinationFilePath)
        {
            return destinationFilePath;
        }

        public static ExportHeader GetHeader()
        {
            return new ExportHeader { Driver = DriverName, FileNameFilter = "SFM Field Summary (*.txt)|*.txt", Name = "Field Summary" };
        }

        public const string DriverName="FieldSummary";
        
    }
}