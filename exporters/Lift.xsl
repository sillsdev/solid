<?xml version="1.0" encoding="utf-8"?>
<!-- The Identity Transformation -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns:rng="http://relaxng.org/ns/structure/1.0">
	<xsl:param name="file1" select="'../exporters/lift.rng'" />
	<xsl:template match="/">
		<xsl:apply-templates select="document($file1)/rng:grammar/rng:start" />
	</xsl:template>

	<xsl:template match="rng:element">
		<xsl:apply-templates />
	</xsl:template>

	<xsl:template match="rng:ref">
		<xsl:variable name="refTarget" select="@name" />
		<xsl:apply-templates select="rng:grammar/rng:define[@name='$refTarget']" />
	</xsl:template>

	<xsl:template match="rng:attribute">
		<xsl:attribute name="xx">
			<xsl:value-of select="//value" />
		</xsl:attribute>
	</xsl:template>

	<xsl:template match="rng:element[@name='entry']">
		<xsl:for-each select="/root/entry">
			<xsl:apply-templates />
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="entry">
		<xsl:apply-templates select="document($file1)/rng:grammar/rng:define[@name='entry-content']" />
	</xsl:template>
	
</xsl:stylesheet>