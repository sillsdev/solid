<?xml version="1.0" encoding="utf-8" ?>
<!--    The Identity Transformation    -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:rng="http://relaxng.org/ns/structure/1.0">
	<!--  <xsl:output method="xml" encoding="utf-8" /> -->
	<!--   <xsl:param name="file1" select="'../exporters/lift.rng'" />   -->
	<xsl:param name="file1" select="'lift.rng'"/>
	<xsl:variable name="docRoot" select="/" />
	<xsl:variable name="docScope" select="$docRoot" />
	<xsl:variable name="rngRoot" select="document($file1)" />
	<xsl:variable name="rngScope" select="$rngRoot" />
	<xsl:template match="/">
		<xsl:apply-templates select="$rngScope/rng:grammar/rng:start"/>
	</xsl:template>
	<xsl:template match="rng:element[@name='lift']">
		<xsl:element name="{@name}" >
		<xsl:apply-templates/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="rng:element[@name='entry']">
		<xsl:variable name="rngScope" select="." />
		<xsl:for-each select="$docRoot/root/entry">
			<xsl:variable name="docScope" select="."/>
                 <xsl:element name="entry" />
			<xsl:apply-templates select="$rngScope/*"/>
		</xsl:for-each>
	</xsl:template>
 <xsl:template match="rng:optional/rng:element">
		<xsl:variable name="rngScope" select="." />
		<xsl:for-each select="$docScope[@lift='{@name}']">
			<xsl:variable name="docScope" select="."/>
			<xsl:apply-templates select="$rngScope/*"/>
		</xsl:for-each>
 </xsl:template>
	<xsl:template match="rng:optional/rng:attribute">
	</xsl:template>
	<xsl:template match="rng:zeroOrMoree/rng:element">
		<xsl:variable name="rngScope" select="." />
		<xsl:for-each select="$docScope[@lift='{@name}']">
			<xsl:variable name="docScope" select="."/>
			<xsl:apply-templates select="$rngScope/*"/>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="rng:ref[@name='extensible-content']">		
	</xsl:template>
	<xsl:template match="rng:ref">
		<xsl:variable name="refTarget" select="@name"/>
		<xsl:apply-templates select="$rngScope/rng:grammar/rng:define[@name=$refTarget]"/>
	</xsl:template>
	<xsl:template match="rng:define[@name='span-content']"></xsl:template>
	<xsl:template match="rng:define">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="rng:attribute">
		<xsl:variable name="attributeName" select="@name"/>
		<xsl:attribute name="boo">
			<xsl:value-of select="//value"/>
		</xsl:attribute>
	</xsl:template>
	<xsl:template match="*">
		<xsl:apply-templates/>
	</xsl:template>
</xsl:stylesheet>
