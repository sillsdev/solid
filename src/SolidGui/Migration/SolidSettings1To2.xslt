<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template match="MultipleAdjacent">
    <xsl:element name="Multiplicity">
       <xsl:apply-templates select="descendant::node()"/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="Version">
    <Version>2</Version>
  </xsl:template>

</xsl:stylesheet>
