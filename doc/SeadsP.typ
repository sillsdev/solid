\+DatabaseType Seads Phonemic Dictionary
\ver 5.0
\desc Based on MDF, supports multiple scripts with a phonemic head field

 \pre_xml <lift version="0.10" lang="%V">
    <header>
        <fields>
            <field tag="comment"><form lang="en"><text>Used for comments</text></form></field>
            <field tag="relation:sequence"><form lang="en"><text>Stores a sequence relation as a space separated string of items</text></form></field>
            <field tag="sortkey"><form lang="en"><text>Stores a sort key to use in preference to the parent. Primarily used for Chinese.</text></form></field>
            <field tag="unknown"><form lang="en"><text>For storing unknown markers</text></form></field>
        </fields>
    </header>
 \post_xml </lift>
 \xpath_default field[@type="unknown" and trait[@name="marker" and @value=$m] and form[@lang=ss($ll,$ls)]/text=$v]
\+mkrset 
\lngDefault Default Unicode
\mkrRecord lx

\+mkr _id
\nam Identifier
\desc Stores a unique identifier for its parent

 \xpath @id=$v
 \mkrsOverThis ps
\lng English Unicode
\mkrOverThis lx
\CharStyle
\Hide
\-mkr

\+mkr a
\nam Allomorph
\desc Stores a free variant of the lexeme

 \xpath variant/form[@lang=ss($vl,"fonipa",$vs)]/text=$v
\lng IPAUni
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr bb
\nam Bibliography
\desc Used to record any bibliographic information pertinent to the lexeme. MDF adds the label 'Read:' to this field. For basic information (to reference an entry in a more complete bibliography database) include the following: AuthorLastName, FirstName/Initials date:pp. For a more complete reference include: AuthorLastName, FirstName/Initials. date. Title. City, Publisher. pp. xxx-yyy.

 \xpath exist::note[@type="bibliographic"]/form[@lang="en"]/text=$v or note[@type="bibliographic"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr bw
\nam Borrowed word (loan)
\desc Used for denoting the source language of a borrowed word.

 \xpath etymology[@type="borrow" and @source=$v]
\lng Default Unicode
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr cf
\nam Cross-reference
\desc This is a generic reference marker used to link together any two related entries in the lexicon. The content is a phonemic lexeme. If the relationship is known, the lexical function \lf field is a better way to cross-reference two lexemes.

 \xpath relation[@type="cross" and @ref=$v]
\lng IPAUni
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dat
\nam Date (last edited)
\desc A Toolbox bookkeeping field to help keep track of the last time an entry was edited. One per record (usually the last field) is adequate. Usually inserted automatically by Shoebox. The datestamp field is set up under the Toolbox menu option: Database-Properties-Options tab.

 \xtype date
 \xpath @dateModified=$v
\lng Date Unicode
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr dc
\nam Definition (Chinese)
\desc Used to fully express the semantic domains of each sense of a lexeme in Chinese. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="zh-cmn-Hans"]/text=$v or definition/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr de
\nam Definition (E)
\desc Used to fully express the semantic domains of each sense of a lexeme in English. May be verbose. Other fields (\ee, \ue, and \oe) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="en"]/text=$v or definition/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dev
\nam Definition (Vietnamese)
\desc Used to fully express the semantic domains of each sense of a lexeme in Vietnamese. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="vi"]/text=$v or definition/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr def
\nam Definition (French)
\desc Used to fully express the semantic domains of each sense of a lexeme in French. May be verbose. Other fields (\ee, \ue, and \oe) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="fr"]/text=$v or definition/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dia
\nam Dialect
\desc Dialect variant. Specifies the name of the dialect. The pronounciation fields \dph follow this.

 \xpath variant[trait[@name="dialect" and @value=$v]]
\lng Default Unicode
\mkrOverThis lx
\CharStyle
\name Dialect
\-mkr

\+mkr dk
\nam Definition (Khmer)
\desc Used to fully express the semantic domains of each sense of a lexeme in Khmer. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="kh"]/text=$v or definition/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dl
\nam Definition (Lao)
\desc Used to fully express the semantic domains of each sense of a lexeme in Lao. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="lo"]/text=$v or definition/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dm
\nam Definition (Myanmar)
\desc Used to fully express the semantic domains of each sense of a lexeme in Burmese. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="my"]/text=$v or definition/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr do
\nam Definition (Korean)
\desc Used to fully express the semantic domains of each sense of a lexeme in Korean. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="ko"]/text=$v or definition/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dt
\nam Definition (Thai)
\desc Used to fully express the semantic domains of each sense of a lexeme in Thai. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang="th"]/text=$v or definition/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dv
\nam Definition Vernacular (Phonemic)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using IPA. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "fonipa", $vs)]/text=$v or definition/form[@lang=ss($vl, "fonipa", $vs)]/text=$v
\lng IPAUni
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvk
\nam Definition Vernacular (Khmer)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using Khmer script. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "Khmr", $vs)]/text=$v or definition/form[@lang=ss($vl, "Khmr", $vs)]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvl
\nam Definition Vernacular (Lao)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using Lao script. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "Laoo", $vs)]/text=$v or definition/form[@lang=ss($vl, "Laoo", $vs)]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvm
\nam Definition Vernacular (Myanmar)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using Burmese script. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "Mymr", $vs)]/text=$v or definition/form[@lang=ss($vl, "Mymr", $vs)]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvm1
\nam Definition Vernacular (Myanmar)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using Burmese script. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "Mymr-variant", $vs)]/text=$v or definition/form[@lang=ss($vl, "Mymr-variant", $vs)]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvr
\nam Definition Vernacular (Roman)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using Roman script. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "Latn", $vs)]/text=$v or definition/form[@lang=ss($vl, "Latn", $vs)]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvt
\nam Definition Vernacular (Thai)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using Thai script. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "Thai", $vs)]/text=$v or definition/form[@lang=ss($vl, "Thai", $vs)]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvv
\nam Definition Vernacular (Vietnamese)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language using Vietnamese script. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, "Latn-VN", $vs)]/text=$v or definition/form[@lang=ss($vl, "Latn-VN", $vs)]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvx
\nam Definition Vernacular (Unknown)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, $ls, $vs)]/text=$v or definition/form[@lang=ss($vl, $ls, $vs)]/text=$v
\lng vernacular
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr dvx1
\nam Definition Vernacular (Unknown)
\desc Used to fully express the semantic domains of each sense of a lexeme expressed in the vernacular language. May be verbose. Other fields (\en, \un, and \on) provide for expanded information. Should contain 1) the bundle of semantic distinctive features necessary and sufficient to describe its core meaning, and 2) the range of denotation of the lexeme. Generally, no initial capital is used.

 \xpath exist::definition/form[@lang=ss($vl, $ls, $vs)]/text=$v or definition/form[@lang=ss($vl, $ls, $vs)]/text=$v
\lng vernacular
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ec
\nam Etymology comment
\desc Any comments the researcher needs to add concerning the etymology of the lexeme can be given here. Not intended for printing.

 \xpath field[@tag="comment"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr eg
\nam Etymology gloss (E)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="en"]/text=$v or gloss/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr egc
\nam Etymology gloss (Chinese)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="zh-cmn-Hans"]/text=$v or gloss/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr egf
\nam Etymology gloss (French)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="fr"]/text=$v or gloss/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis et
\CharStyle
\-mkr

\+mkr egk
\nam Etymology gloss (Khmer)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="kh"]/text=$v or gloss/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr egl
\nam Etymology gloss (Lao)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="lo"]/text=$v or gloss/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr egm
\nam Etymology gloss (Burmese)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="my"]/text=$v or gloss/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis et
\CharStyle
\-mkr

\+mkr ego
\nam Etymology gloss (Korean)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="ko"]/text=$v or gloss/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr egt
\nam Etymology gloss (Thai)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="th"]/text=$v or gloss/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr egv
\nam Etymology gloss (Vietnamese)
\desc The published gloss for the etymological reference is given here.

 \xpath exist::gloss/form[@lang="vi"]/text=$v or gloss/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr enc
\nam Encyclopedic info (Chinese)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="encyclopedic"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ene
\nam Encyclopedic info (E)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="en"]/text=$v or note[@type="encyclopedic"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr enf
\nam Encyclopedic info (French)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="fr"]/text=$v or note[@type="encyclopedic"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr enk
\nam Encyclopedic info (Khmer)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="km"]/text=$v or note[@type="encyclopedic"]/form[@lang="km"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr enl
\nam Encyclopedic info (Lao)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="lo"]/text=$v or note[@type="encyclopedic"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr enm
\nam Encyclopedic info (Burmese)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="my"]/text=$v or note[@type="encyclopedic"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr eno
\nam Encyclopedic info (Korean)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="ko"]/text=$v or note[@type="encyclopedic"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ent
\nam Encyclopedic info (Thai)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="th"]/text=$v or note[@type="encyclopedic"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr env
\nam Encyclopedic info (Vietnamese)
\desc This field crosses over with the \de, \ue, and \oe fields, but is intended for more verbose explanations of the headword (for each sense). The researcher should use this field to encode any additional information needed by a non-native speaker to understand and use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="encyclopedic"]/form[@lang="vi"]/text=$v or note[@type="encyclopedic"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr es
\nam Etymology source
\desc The reference or source abbreviation for etymology of the lexeme is given here. Use a Range Set.

 \xpath @source=$v
\lng Default Unicode
\mkrOverThis et
\CharStyle
\-mkr

\+mkr et
\nam Etymology (proto form)
\desc The etymology for the lexeme is put here, e.g.: \et *babuy

 \xpath etymology[@type="proto"]=$v
\lng IPAUni
\mkrOverThis sn
\CharStyle
\-mkr

\+mkr gc
\nam Gloss (Chinese)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if an \rec field is not present (or is present but empty); also as an English definition in a formatted dictionary if there is no \dc field (or it is present but empty).

 \xpath gloss[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ge
\nam Gloss (English)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if an \re field is not present (or is present but empty); also as an English definition in a formatted dictionary if there is no \de field (or it is present but empty).

 \xpath gloss[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr gf
\nam Gloss (French)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if an \re field is not present (or is present but empty); also as a French definition in a formatted dictionary if there is no \de field (or it is present but empty).

 \xpath gloss[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr gk
\nam Gloss (Khmer)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if a \rek field is not present (or is present but empty); also as a Khmer definition in a formatted dictionary if there is no \dk field (or it is present but empty).

 \xpath gloss[@lang="km"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr gl
\nam Gloss (Lao)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if a \rel field is not present (or is present but empty); also as a Lao definition in a formatted dictionary if there is no \dl field (or it is present but empty).

 \xpath gloss[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr gm
\nam Gloss (Burmese)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if a \rem field is not present (or is present but empty); also as a Burmese definition in a formatted dictionary if there is no \dm field (or it is present but empty).

 \xpath gloss[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr go
\nam Gloss (Korean)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if an \reo field is not present (or is present but empty); also as an English definition in a formatted dictionary if there is no \do field (or it is present but empty).

 \xpath gloss[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr gt
\nam Gloss (Thai)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if a \ret field is not present (or is present but empty); also as a Thai definition in a formatted dictionary if there is no \dt field (or it is present but empty).

 \xpath gloss[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr gv
\nam Gloss (Vietnamese)
\desc Intended for interlinear morpheme-level glossing. Join multi-word glosses with (_), e.g. wild_boar. For multiple glosses each gloss should be listed in its own field. Used for reversing the dictionary if a \rv field is not present (or is present but empty); also as a Vietnamese definition in a formatted dictionary if there is no \dv field (or it is present but empty).

 \xpath gloss[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr hm
\nam Homonym number
\desc Used to differentiate homonym entries (lexemes that are spelled the same but have no semantic relationship). This field comes directly after the \lx field and simply contains a number, e.g. 1, 2, or 3, etc. Use a Character Range Set.

 \xpath ancestor::entry[@order=$v]
\lng Default Unicode
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr lc
\nam Citation form
\desc This should be added only if the lexical entry form is inappropriate for the printed dictionary, and you want to substitute another form for the printed entry. When formatting a document, MDF always replaces the contents of the \lx field with the contents of the \lc field (if present).

 \xpath citation/form[@lang=ss($vl, "fonipa", $vs)]/text=$v
\lng IPAUni
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr lec
\nam Lexical function usage (Chinese)
\desc This is for giving the Chinese explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="zh-cmn-Hans"]/text=$v or usage/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr le
\nam Lexical function usage (English)
\desc This is for giving the English explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="en"]/text=$v or usage/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr lef
\nam Lexical function usage (French)
\desc This is for giving the French explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="fr"]/text=$v or usage/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr lek
\nam Lexical function usage (Khmer)
\desc This is for giving the Khmer explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="km"]/text=$v or usage/form[@lang="km"]/text=$v
\lng Khmer Unicode
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr lel
\nam Lexical function usage (Lao)
\desc This is for giving the Lao explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="lo"]/text=$v or usage/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr lem
\nam Lexical function usage (Burmese)
\desc This is for giving the Burmese explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="my"]/text=$v or usage/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr leo
\nam Lexical function usage (Korean)
\desc This is for giving the Korean explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="ko"]/text=$v or usage/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr let
\nam Lexical function usage (Thai)
\desc This is for giving the Thai explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="th"]/text=$v or usage/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr lev
\nam Lexical function usage (Vietnamese)
\desc This is for giving the Vietnamese explanation of when to use the lexeme referenced by the lexical function.

 \xpath exist::usage/form[@lang="vi"]/text=$v or usage/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr lf
\nam Lexical function
\desc Used to encode the semantic networks of a language. For consistency, a Range Set should be maintained on the lexical function labels used.

 \xpath relation[@type=$v and @type != "cross" and @type != "subhead" and @type != "thesaurus"]
\lng Default Unicode
\+fnt 
\Name Times New Roman
\Size 11
\Italic
\rgbColor 0,0,0
\-fnt
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr lt
\nam Literally
\desc Used to elucidate the distinct meanings of the parts of an idiom or complex phrase in a lexical entry (\lx) or subentry (\se).

 \xpath field[@tag="relation:sequence"]/form/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr lv
\nam Lexical function lexeme
\desc References the lexeme associated with the lexical function preceding, written in phonemics

 \xpath @ref=$v
\lng IPAUni
\mkrOverThis lf
\CharStyle
\-mkr

\+mkr lx
\nam Lexeme
\desc The Record marker for each record in a lexical entry. It contains the lexeme or headword. The word is stored phonemically and acts as a record identifier when referenced from other records.

 \xvar g="$v:$rn"
 \xpath entry[@id=$g and lexical-unit/form[@lang=ss($vl, "fonipa", $vs)]/text=$v] or exist::entry
\lng IPAUni
\+fnt 
\Name Doulos SIL
\Size 12
\Bold
\rgbColor 0,0,0
\-fnt
\-mkr

\+mkr lxk
\nam Lexeme (Khmer)
\desc The lexeme in Khmer script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Khmr",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,"Khmr",$vs)]/text=$v]
\lng Khmer Unicode
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr lxl
\nam Lexeme (Lao)
\desc The lexeme in Lao script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Laoo",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,"Laoo",$vs)]/text=$v]
\lng Lao Unicode
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr lxm
\nam Lexeme (Myanmar)
\desc The lexeme in Myanmar script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Mymr",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,"Mymr",$vs)]/text=$v]
\lng Burmese Academy
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr lxm1
\nam Lexeme (Myanmar)
\desc The lexeme in Myanmar script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Mymr",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,"Mymr",$vs)]/text=$v]
\lng Burmese Academy
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr lxr
\nam Lexeme (Roman)
\desc The lexeme in Roman script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Latn",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,"Latn",$vs)]/text=$v]
\lng English Unicode
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr lxt
\nam Lexeme (Thai)
\desc The lexeme in Thai script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Thai",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,"Thai",$vs)]/text=$v]
\lng Thai Unicode
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr lxv
\nam Lexeme (Vietnamese)
\desc The lexeme in Vietnamese script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Latn-VN",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,"Latn-VN",$vs)]/text=$v]
\lng Vietnamese Unicode
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr lxx
\nam Lexeme (Extra)
\desc The lexeme in Another script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Zyyy",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,$ls,$vs)]/text=$v]
\lng vernacular
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr lxx1
\nam Lexeme (Extra 1)
\desc The lexeme in yet Another script.

 \mkrsOverThis lx
 \xpath exist::lexical-unit[form[@lang=ss($vl,"Zyyy",$vs)]/text=$v] or parent::variant[form[@lang=ss($vl,$ls,$vs)]/text=$v]
\lng vernacular
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr mr
\nam Morphology
\desc Used to show the underlying morphemic structure for complex lexemes.

 \xpath field[@tag="relation:sequence"]/form/text=$v
\lng IPAUni
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr na
\nam Notes (anthropology)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="en"]/text=$v or note[@type="anthropology"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nac
\nam Notes (anthropology, Chinese)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="anthropology"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr naf
\nam Notes (anthropology, French)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="fr"]/text=$v or note[@type="anthropology"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nak
\nam Notes (anthropology, Khmer)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="kh"]/text=$v or note[@type="anthropology"]/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nal
\nam Notes (anthropology, Lao)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="lo"]/text=$v or note[@type="anthropology"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nam
\nam Notes (anthropology, Burmese)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="my"]/text=$v or note[@type="anthropology"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nao
\nam Notes (anthropology, Korean)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="ko"]/text=$v or note[@type="anthropology"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nat
\nam Notes (anthropology, Thai)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="th"]/text=$v or note[@type="anthropology"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nav
\nam Notes (anthropology, Vietnamese)
\desc For any ethnographic note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="anthropology"]/form[@lang="vi"]/text=$v or note[@type="anthropology"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nd
\nam Notes (discourse)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="en"]/text=$v or note[@type="discourse"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndc
\nam Notes (discourse, Chinese)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="discourse"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndf
\nam Notes (discourse, French)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="fr"]/text=$v or note[@type="discourse"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndk
\nam Notes (discourse, Khmer)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="kh"]/text=$v or note[@type="discourse"]/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndl
\nam Notes (discourse, Lao)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="lo"]/text=$v or note[@type="discourse"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndm
\nam Notes (discourse, Burmese)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="my"]/text=$v or note[@type="discourse"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndo
\nam Notes (discourse, Korean)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="ko"]/text=$v or note[@type="discourse"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndt
\nam Notes (discourse, Thai)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="th"]/text=$v or note[@type="discourse"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ndv
\nam Notes (discourse, Vietnamese)
\desc For any discourse/text analysis note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="discourse"]/form[@lang="vi"]/text=$v or note[@type="discourse"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ng
\nam Notes (grammar)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="en"]/text=$v or note[@type="grammar"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngc
\nam Notes (grammar, Chinese)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="grammar"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngf
\nam Notes (grammar, French)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="fr"]/text=$v or note[@type="grammar"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngk
\nam Notes (grammar, Khmer)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="kh"]/text=$v or note[@type="grammar"]/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngl
\nam Notes (grammar, Lao)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="lo"]/text=$v or note[@type="grammar"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngm
\nam Notes (grammar, Burmese)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="my"]/text=$v or note[@type="grammar"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngo
\nam Notes (grammar, Korean)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="ko"]/text=$v or note[@type="grammar"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngt
\nam Notes (grammar)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="th"]/text=$v or note[@type="grammar"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ngv
\nam Notes (grammar, Vietnamese)
\desc For any grammar note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="grammar"]/form[@lang="vi"]/text=$v or note[@type="grammar"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr np
\nam Notes (phonology)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="en"]/text=$v or note[@type="phonology"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npc
\nam Notes (phonology, Chinese)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="phonology"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npf
\nam Notes (phonology, French)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="fr"]/text=$v or note[@type="phonology"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npk
\nam Notes (phonology, Khmer)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="kh"]/text=$v or note[@type="phonology"]/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npl
\nam Notes (phonology, Lao)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="lo"]/text=$v or note[@type="phonology"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npm
\nam Notes (phonology, Burmese)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="my"]/text=$v or note[@type="phonology"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npo
\nam Notes (phonology)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="ko"]/text=$v or note[@type="phonology"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npt
\nam Notes (phonology, Thai)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="th"]/text=$v or note[@type="phonology"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr npv
\nam Notes (phonology, Vietnamese)
\desc For any phonology note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="phonology"]/form[@lang="vi"]/text=$v or note[@type="phonology"]/form[@lang="vi"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nq
\nam Notes (questions)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="en"]/text=$v or note[@type="questions"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nqc
\nam Notes (questions, Chinese)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="questions"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nqf
\nam Notes (questions, French)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="fr"]/text=$v or note[@type="questions"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nqk
\nam Notes (questions, Khmer)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="kh"]/text=$v or note[@type="questions"]/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nql
\nam Notes (questions, Lao)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="lo"]/text=$v or note[@type="questions"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nqm
\nam Notes (questions, Burmese)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="my"]/text=$v or note[@type="questions"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nqo
\nam Notes (questions, Korean)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="ko"]/text=$v or note[@type="questions"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nqt
\nam Notes (questions, Thai)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="th"]/text=$v or note[@type="questions"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nqv
\nam Notes (questions, Vietnamese)
\desc For any question or note for further study, pertinent to the lexeme, that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="questions"]/form[@lang="vi"]/text=$v or note[@type="questions"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ns
\nam Notes (sociolinguistics)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="en"]/text=$v or note[@type="sociolinguistics"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nsc
\nam Notes (sociolinguistics, Chinese)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="sociolinguistics"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nsf
\nam Notes (sociolinguistics, French)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="fr"]/text=$v or note[@type="sociolinguistics"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nsk
\nam Notes (sociolinguistics, Khmer)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="kh"]/text=$v or note[@type="sociolinguistics"]/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nsl
\nam Notes (sociolinguistics, Lao)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="lo"]/text=$v or note[@type="sociolinguistics"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nsm
\nam Notes (sociolinguistics, Burmese)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="my"]/text=$v or note[@type="sociolinguistics"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nso
\nam Notes (sociolinguistics, Korean)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="ko"]/text=$v or note[@type="sociolinguistics"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nst
\nam Notes (sociolinguistics, Thai)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="th"]/text=$v or note[@type="sociolinguistics"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nsv
\nam Notes (sociolinguistics, Vietnamese)
\desc For any sociolinguistics note pertinent to the lexeme that you want separate from general notes. Capitalization and punctuation should be used as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="sociolinguistics"]/form[@lang="vi"]/text=$v or note[@type="sociolinguistics"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nt
\nam Notes (general)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="en"]/text=$v or note[@type="general"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ntc
\nam Notes (general, Chinese)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="zh-cmn-Hans"]/text=$v or note[@type="general"]/form[@lang="zh-cmn-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ntf
\nam Notes (general, French)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="fr"]/text=$v or note[@type="general"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ntk
\nam Notes (general, Khmer)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="kh"]/text=$v or note[@type="general"]/form[@lang="kh"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ntl
\nam Notes (general, Lao)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="lo"]/text=$v or note[@type="general"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ntm
\nam Notes (general, Burmese)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="my"]/text=$v or note[@type="general"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr nto
\nam Notes (general, Korean)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="ko"]/text=$v or note[@type="general"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ntt
\nam Notes (general, Thai)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="th"]/text=$v or note[@type="general"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ntv
\nam Notes (general, Vietnamese)
\desc A generic dump for all personal notes about an entry, subentry, or sense. More specific note fields provide a finer differentiation to one's notes: \np (phonology), \ng (grammar), \nd (discourse), \na (anthropology), \ns (sociolinguistics), and \nq (questions). All "note fields" should use capitalization and punctuation as needed.

 \mkrsOverThis lx
 \xpath exist::note[@type="general"]/form[@lang="vi"]/text=$v or note[@type="general"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oe
\nam Only/restrictions (E)
\desc For denoting in English any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="en"]/text=$v or note[@type="restrictions"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oec
\nam Only/restrictions (Chinese)
\desc For denoting in Chinese any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="cnm-Hans"]/text=$v or note[@type="restrictions"]/form[@lang="cnm-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oef
\nam Only/restrictions (French)
\desc For denoting in French any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="fr"]/text=$v or note[@type="restrictions"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oek
\nam Only/restrictions (Khmer)
\desc For denoting in Khmer any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="km"]/text=$v or note[@type="restrictions"]/form[@lang="km"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oel
\nam Only/restrictions (Lao)
\desc For denoting in Lao any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="lo"]/text=$v or note[@type="restrictions"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oem
\nam Only/restrictions (Burmese)
\desc For denoting in Burmese any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="my"]/text=$v or note[@type="restrictions"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oeo
\nam Only/restrictions (Korean)
\desc For denoting in Korean any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="ko"]/text=$v or note[@type="restrictions"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oet
\nam Only/restrictions (Thai)
\desc For denoting in Thai any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="th"]/text=$v or note[@type="restrictions"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr oev
\nam Only/restrictions (Vietnamese)
\desc For denoting in Vietnamese any semantic and/or grammatical restrictions pertinent to the lexeme. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="restrictions"]/form[@lang="vi"]/text=$v or note[@type="restrictions"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr pc
\nam Picture
\desc This contains the URL to a picture to be associated with this sense.

 \xpath picture[@href=$v]
\lng Default Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ph
\nam Phonetic form
\desc Used as needed to retain the phonetic information that is lost when an orthographic spelling is used for an entry. Details on how to interpret symbols in this field should be included in pronunciation guide.

 \xpath phonetic/form[@lang=ss($vs,"fonipa",$vl)]/text=$v
 \mkrsOverThis lx
\lng IPAUni
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr ps
\nam Part of speech
\desc Classifies the part of speech. This must reflect the part of speech of the vernacular lexeme (not the national or English gloss). Consistent labeling is important; use the Range Set feature. Sense numbers are beneath \ps in this hierarchy; don't mark different \ps fields with sense numbers.

 \mkrsOverThis lx
 \xvar g="$k_$v"
 \xpath parent::sense[subsense[grammatical-info/@value=$v]] or sense[grammatical-info/@value=$v] or exist::sense
\lng Default Unicode
\rngset ADJ ADV ART AUX CLF COMP CONJ COP DEM EXCL FOC N NMLZ NUM Nprop ONOM P PFX POL POST PREP PRO PRT Q QNT QUOT REL SFX TEMP TOP V VLZ Vditr Vintr Vmot Vst Vtr 
\SingleWord
\mkrOverThis sg
\CharStyle
\-mkr

\+mkr re
\nam Reversal (English)
\desc English word(s)/phrase(s); used to reverse the dictionary for an English index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="en"]/text=$v]
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr rec
\nam Reversal (Chinese)
\desc Chinese word(s)/phrase(s); used to reverse the dictionary for a Chinese index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index. Put {space semicolon space} between multiple items, e.g. hut ; house.

 \xpath reversal[form[@lang="zh-cnm-Hans"]/text=$v]
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr recs
\name Reversal Sort (Chinese)
\desc Stores the sort key for the Chinese reversal generated from the Chinese and Pinyin representations of the reversal key

 \xpath form[@lang="zh-cnm-Latn"]/text=$v
\lng Chinese Unicode
\mkrOverThis rec
\CharStyle
\-mkr

\+mkr ref
\nam Reversal (French)
\desc French word(s)/phrase(s); used to reverse the dictionary for an English index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="fr"]/text=$v]
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr rek
\nam Reversal (Khmer)
\desc Khmer word(s)/phrase(s); used to reverse the dictionary for a Khmer index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="km"]/text=$v]
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr rel
\nam Reversal (Lao)
\desc Lao word(s)/phrase(s); used to reverse the dictionary for a Lao index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="lo"]/text=$v]
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr rem
\nam Reversal (Burmese)
\desc Burmese word(s)/phrase(s); used to reverse the dictionary for a Burmese index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="my"]/text=$v]
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr reo
\nam Reversal (Korean)
\desc Korean word(s)/phrase(s); used to reverse the dictionary for a Korean index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="ko"]/text=$v]
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ret
\nam Reversal (Thai)
\desc Thai word(s)/phrase(s); used to reverse the dictionary for a Thai index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="th"]/text=$v]
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr rev
\nam Reversal (Vietnamese)
\desc Vietnamese word(s)/phrase(s); used to reverse the dictionary for a Vietnamese index. If no \re field is given, the \ge field is used. If an \re * is present, the relevant entry, subentry, or sense will not be included in the reversed index.

 \xpath reversal[form[@lang="vi"]/text=$v]
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr rf
\nam Reference
\desc Used to note the reference for the following example sentence.

 \xpath example[@source=$v]
\lng Default Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr sc
\nam Scientific name
\desc Used to provide a scientific name for a lexeme.

 \xpath exist::note[@type="scientific_name" and form[@lang="en"]/text=$v] or note[@type="scientific_name" and form[@lang="en"]/text=$v]
\lng English Unicode
\+fnt 
\Name Times New Roman
\Size 11
\Italic
\Underline
\rgbColor 0,0,0
\-fnt
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr sd
\nam Semantic domain
\desc The English version of the \th field and probably the one to use first. Differentiate and catalog the semantic domains of an entry, being careful to not let the English force or mask the vernacular relations. Moving to the vernacular terms (given in \th field) as early as possible is best. Use a Range Set.

 \xpath trait[@name="semantic_domain" and @value=$v]
\lng Default Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr sg
\nam Sense Group
\desc Where a lexeme has more than one sense, \ps is used to mark the beginning of a new sense. Senses may be grouped using the \sg field along with a number. In sense hierarchy terms a \ps field marks a new sense while a \sg marks a super sense of subsenses. The \sg field is optional if there is only one sense group for a lexeme.

 \xvar g="$k_$v"
 \xpath sense[not(exist::grammatical-info)]
\lng Default Unicode
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr sh
\nam Subentry head
\desc Gives the lexeme of the head word of which this record is a subentry.

 \xpath relation[@type="subhead" and @ref=$v]
\lng IPAUni
\mkrOverThis lx
\CharSTyle 
\-mkr

\+mkr so
\nam Source
\desc Used to indicate the source of the information: informant, text, village, etc.

 \xpath exist::note[@type="source"]/form[@lang="en"]/text=$v or note[@type="source"]/form[@lang="en"]/text=$v
\lng Default Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr st
\nam Status
\desc Used to indicate how complete or thoroughly checked an entry is. Use a Range Set.

 \xpath trait[@name="status" and @value=$v]
\lng Default Unicode
\mkrOverThis lx
\CharStyle
\-mkr

\+mkr th
\nam Thesaurus
\desc Used for developing a vernacular-based thesaurus. It is to be labeled with the vernacular term governing the semantic domain of the entry. Sorting on this field (within Shoebox) would yield a vernacular thesaurus. Use a Range Set.

 \xpath relation[@type="thesaurus" and @ref=$v]
\lng IPAUni
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr uc
\nam Usage (Chinese)
\desc This field should cover, in Chinese, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="cnm-Hans"]/text=$v or note[@type="usage"]/form[@lang="cnm-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ue
\nam Usage (English)
\desc This field should cover, in English, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="en"]/text=$v or note[@type="usage"]/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr uf
\nam Usage (French)
\desc This field should cover, in French, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="fr"]/text=$v or note[@type="usage"]/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr uk
\nam Usage (Khmer)
\desc This field should cover, in Khmer, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="km"]/text=$v or note[@type="usage"]/form[@lang="km"]/text=$v
\lng Khmer Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr ul
\nam Usage (Lao)
\desc This field should cover, in Lao, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="lo"]/text=$v or note[@type="usage"]/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr um
\nam Usage (Burmese)
\desc This field should cover, in Burmese, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="my"]/text=$v or note[@type="usage"]/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr uo
\nam Usage (Korean)
\desc This field should cover, in Khmer, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="ko"]/text=$v or note[@type="usage"]/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis ps
\CharStyle
\-mkr
\+mkr ut
\nam Usage (Thai)
\desc This field should cover, in Thai, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="th"]/text=$v or note[@type="usage"]/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr uv
\nam Usage (Vietnamese)
\desc This field should cover, in Vietnamese, such information as common usage, or restrictions in usage (such as taboos) that is needed so a non-native speaker can use this lexeme properly. Use capitalization and punctuation as needed.

 \xpath exist::note[@type="usage"]/form[@lang="vi"]/text=$v or note[@type="usage"]/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis ps
\CharStyle
\-mkr

\+mkr va
\nam Variant
\desc This field contains an alternative form of the \lx phonemic field due either to a spelling difference or allomorph. Under a \dia field it represents a phonemic representation of the dialectal difference. The \va field may have orthographic representation fields below it which mark orthographic representations of this variant.

 \xpath variant[form/text=$v]
\lng IPAUni
\mkrOverThis dia
\CharStyle
\-mkr

\+mkr xe
\nam Example free trans. (English)
\desc English translation of the example sentence given in the \xv field. Use capitalization and punctuation as needed.

 \xpath exist::trans/form[@lang="en"]/text=$v or trans/form[@lang="en"]/text=$v
\lng English Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xec
\nam Example free trans. (Chinese)
\desc Chinese translation of the example sentence given in the \xv field.

 \xpath exist::trans/form[@lang="zh-cnm-Hans"]/text=$v or trans/form[@lang="zh-cnm-Hans"]/text=$v
\lng Chinese Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xef
\nam Example free trans. (French)
\desc French translation of the example sentence given in the \xv field. Use capitalization and punctuation as needed.

 \xpath exist::trans/form[@lang="fr"]/text=$v or trans/form[@lang="fr"]/text=$v
\lng FrenchU
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xek
\nam Example free trans. (Khmer)
\desc Khmer translation of the example sentence given in the \xv field.

 \xpath exist::trans/form[@lang="km"]/text=$v or trans/form[@lang="km"]/text=$v
\lng Khmer Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xel
\nam Example free trans. (Lao)
\desc Lao translation of the example sentence given in the \xv field.

 \xpath exist::trans/form[@lang="lo"]/text=$v or trans/form[@lang="lo"]/text=$v
\lng Lao Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xem
\nam Example free trans. (Burmese)
\desc Burmese translation of the example sentence given in the \xv field.

 \xpath exist::trans/form[@lang="my"]/text=$v or trans/form[@lang="my"]/text=$v
\lng Burmese Academy
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xeo
\nam Example free trans. (Korean)
\desc Korean translation of the example sentence given in the \xv field.

 \xpath exist::trans/form[@lang="ko"]/text=$v or trans/form[@lang="ko"]/text=$v
\lng Korean Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xet
\nam Example free trans. (Thai)
\desc Thai translation of the example sentence given in the \xv field.

 \xpath exist::trans/form[@lang="th"]/text=$v or trans/form[@lang="th"]/text=$v
\lng Thai Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xev
\nam Example free trans. (Vietnamese)
\desc Vietnamese translation of the example sentence given in the \xv field. Use capitalization and punctuation as needed.

 \xpath exist::trans/form[@lang="vi"]/text=$v or trans/form[@lang="vi"]/text=$v
\lng Vietnamese Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xv
\nam Example (v)
\desc Used to give an example or illustrative sentence in the vernacular (phonemic) to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,"fonipa",$vs)]/text=$v
\lng IPAUni
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvk
\nam Example (Khmer)
\desc Used to give an example or illustrative sentence in the vernacular, using Khmer script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,"Khmr",$vs)]/text=$v
\lng Khmer Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvl
\nam Example (Lao)
\desc Used to give an example or illustrative sentence in the vernacular, using Lao script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,"Laoo",$vs)]/text=$v
\lng Lao Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvm
\nam Example (Myanmar)
\desc Used to give an example or illustrative sentence in the vernacular, using Myanmar script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,"Mymr",$vs)]/text=$v
\lng Burmese Academy
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvm1
\nam Example (Myanmar)
\desc Used to give an example or illustrative sentence in the vernacular, using Myanmar script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,"Mymr",$vs)]/text=$v
\lng Burmese Academy
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvr
\nam Example (Roman)
\desc Used to give an example or illustrative sentence in the vernacular, using Roman script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural. Use capitalization and punctuation as needed.

 \xpath form[parent::example and @lang=ss($vl,"Latn",$vs)]/text=$v
\lng English Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvt
\nam Example (Thai)
\desc Used to give an example or illustrative sentence in the vernacular, using Thai script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,"Thai",$vs)]/text=$v
\lng Thai Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvt1
\nam Example (Thai)
\desc Used to give an example or illustrative sentence in the vernacular, using Thai script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,"Thai",$vs)]/text=$v
\lng Thai Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvv
\nam Example (Vietnamese)
\desc Used to give an example or illustrative sentence in the vernacular, using Vietnamese script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural. Use capitalization and punctuation as needed.

 \xpath form[parent::example and @lang=ss($vl,"Latn-VN",$vs)]=$v
\lng Vietnamese Unicode
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvx
\nam Example (Extra)
\desc Used to give an example or illustrative sentence in the vernacular, using Another script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,$ls,$vs)]/text=$v
\lng vernacular
\mkrOverThis rf
\CharStyle
\-mkr

\+mkr xvx1
\nam Example (Extra 1)
\desc Used to give an example or illustrative sentence in the vernacular, using yet Another script, to exemplify each separate sense and demonstrate legitimacy of translation equivalents. Should be short and natural.

 \xpath form[parent::example and @lang=ss($vl,$ls,$vs)]/text=$v
\lng vernacular
\mkrOverThis rf
\CharStyle
\-mkr

\-mkrset

\iInterlinCharWd 10
\+filset 

\-filset

\+jmpset 
\-jmpset

\+template 
\fld \ph
\fld \ps
\fld \ge
\fld \de\n
\fld \rf
\fld \xv
\fld \xe\n
\fld \so
\fld \nt
\fld \dt
\-template
\mkrRecord lx
\mkrSecKey hm
\mkrDateStamp dat
\mkrMultipleMatchShow ge
\+PrintProperties 
\header File: &f, Date: &d
\footer Page &p
\topmargin 1.00 in
\leftmargin 0.25 in
\bottommargin 1.00 in
\rightmargin 0.25 in
\recordsspace 100
\-PrintProperties
\+expset 
\MDF
\verMDF 4.1
\ShowConvertOlderMDF

\+expRTF Rich Text Format
\+rtfPageSetup 
\paperSize letter
\topMargin 1
\bottomMargin 1
\leftMargin 1.25
\rightMargin 1.25
\gutter 0
\headerToEdge 0.5
\footerToEdge 0.5
\columns 1
\columnSpacing 0.5
\-rtfPageSetup
\-expRTF

\+expSF Standard Format
\-expSF

\expDefault Standard Format
\AutoOpen
\SkipProperties
\-expset
\-DatabaseType
