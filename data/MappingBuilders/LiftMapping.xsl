<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:rng="http://relaxng.org/ns/structure/1.0">
  <xsl:template match="/">
    <root>
      <xsl:apply-templates select="/root/entry/*"/>
    </root>
  </xsl:template>
  <xsl:template match="entry">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="node()">
    <Field>
      <xsl:attribute name="id">
        <xsl:value-of select="local-name()"/>
      </xsl:attribute>
      <xsl:attribute name="uiname">
        <xsl:value-of select="local-name()"/>
      </xsl:attribute>
      <xsl:attribute name="mdf">
        <xsl:value-of select="local-name()"/>
      </xsl:attribute>
      <Help>
        <Usage></Usage>
        <Settings></Settings>
        <Mapping></Mapping>
        <Appends></Appends>
        <List></List>
        <Multilingual></Multilingual>
        <Examples>
          <Example></Example>
        </Examples>
        <Limitations>
          <Limitation></Limitation>
        </Limitations>
        <Extras>
          <Extra></Extra>
        </Extras>
      </Help>
    </Field>
  </xsl:template>
</xsl:stylesheet>
