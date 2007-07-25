<?xml version="1.0" encoding="utf-8"?>
<!-- The Identity Transformation -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<!--Copy the current node-->
		<lift>
			<xsl:apply-templates select="node()"/>
		</lift> 
	</xsl:template>
	<xsl:template match="entry">
		<entry>
			<xsl:attribute name="id">
				<xsl:value-of select="lx/data" />
			</xsl:attribute>
			<xsl:attribute name="dateModified">
				<xsl:value-of select="lx/dat/data" />
			</xsl:attribute>
			<xsl:apply-templates select="node()" />
		</entry>
	</xsl:template>
	<xsl:template match="lx">
		<lexical-unit>
			<form lang="">
				<text>
					<xsl:value-of select="child::data" />
				</text>
			</form>
			<xsl:for-each select="child::lx">
				<form lang="">
					<text>
						<xsl:value-of select="./data" />
					</text>
				</form>
			</xsl:for-each>
		</lexical-unit>
		<xsl:apply-templates select="node()"/>
	</xsl:template>
	<xsl:template match="sn">
		<sense id="">
			<xsl:apply-templates select="node()"/>
		</sense>
	</xsl:template>
	<xsl:template match="root">
		<xsl:apply-templates select="node()"/>
	</xsl:template>
	<xsl:template match="node()">
		<xsl:copy />
	</xsl:template>
</xsl:stylesheet>