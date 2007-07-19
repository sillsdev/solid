<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:my-scripts">
  <xsl:output method="xml" version="1.0" encoding="utf-8" indent="yes" />
  <!--
================================================================
DO NOT EDIT!!  This transform is automatically generated

Produce Phase 2 XML of SFM Import
  
  Input:    XML output from SFM Import tool
  Output: An XSLT that produces Phase 2
               (Note: each possible parse is within its own seq element)
================================================================

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
Preamble
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
   -->
			<msxsl:script language="C#" implements-prefix="user">
				<![CDATA[
	// create a new GUID and return it
	public string CreateGUID()
	{
		System.Guid newGuid = Guid.NewGuid();
		return "I" + newGuid.ToString().ToUpper();
	}
      ]]>
			</msxsl:script>
		<xsl:template match="/database"><xsl:element name="dictionary"><xsl:attribute name="affixMarker">-</xsl:attribute><xsl:for-each select="//Entry"><xsl:element name="Entry"><xsl:attribute name="guid"><xsl:value-of select="user:CreateGUID()"/></xsl:attribute><xsl:apply-templates /></xsl:element></xsl:for-each></xsl:element></xsl:template><xsl:template match="ps"><xsl:element name="pos"><xsl:attribute name="sfm">ps</xsl:attribute><xsl:attribute name="ws">en</xsl:attribute><xsl:attribute name="abbr">True</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="gEng"><xsl:element name="glos"><xsl:attribute name="sfm">gEng</xsl:attribute><xsl:attribute name="ws">en</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="dt"><xsl:element name="creat"><xsl:attribute name="sfm">dt</xsl:attribute><xsl:attribute name="ws">en</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="Entry/isEng"><xsl:element name="eires"><xsl:attribute name="sfm">isEng</xsl:attribute><xsl:attribute name="ws">en</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="sn"><xsl:element name="sn"><xsl:attribute name="sfm">sn</xsl:attribute><xsl:attribute name="ws">en</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="lxWCI"><xsl:element name="lex"><xsl:attribute name="sfm">lxWCI</xsl:attribute><xsl:attribute name="ws">xwct__THAI</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="mn"><xsl:element name="meref"><xsl:attribute name="sfm">mn</xsl:attribute><xsl:attribute name="ws">xwct__IPA</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="sdEng"><xsl:element name="snote"><xsl:attribute name="sfm">sdEng</xsl:attribute><xsl:attribute name="ws">en</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="va"><xsl:element name="vari"><xsl:attribute name="sfm">va</xsl:attribute><xsl:attribute name="ws">xwct__IPA</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match="lx"><xsl:element name="lex"><xsl:attribute name="sfm">lx</xsl:attribute><xsl:attribute name="ws">xwct__IPA</xsl:attribute><xsl:apply-templates /></xsl:element></xsl:template><xsl:template match=" Sense | Example | Function | Subentry | Variant | Etymology | Picture | Pronunciation | SemanticDomain"><!--
        Only elements that have content are copied and propigated through the xsl's.
		All high level Elements are now 'tagged' with a GUID for future identification and reference.
        --><xsl:if test="@* or ''!=."><xsl:copy><xsl:attribute name="guid"><xsl:value-of select="user:CreateGUID()"/></xsl:attribute><xsl:apply-templates /></xsl:copy></xsl:if></xsl:template></xsl:stylesheet>