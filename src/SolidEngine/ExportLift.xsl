<?xml version="1.0" encoding="utf-8" ?>
<!-- The Identity Transformation -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	 <!--Whenever you match any node or any attribute--> 
	<xsl:template match="node()|@*">
		 <!--Copy the current node--> 
		<xsl:copy>
			 <!--Including any attributes it has and any child nodes--> 
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="/">
		<lift>
			<xsl:for-each select="root">
				<entry id="" dateModified="">
					<lexical-unit>
						<form>
							
						</form>
					</lexical-unit>
					<xsl:for-each select="sn">
						<sense id="">
						</sense>
					</xsl:for-each>
				</entry>
			</xsl:for-each>
			</lift>
	</xsl:template>
</xsl:stylesheet>