<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:sfm="output.xsl" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:my-scripts">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

 <!--
================================================================
Convert SFM Import result to an XSLT transform to perform step 2
  Input:    XML output from SFMImport tool
  Output: Step 1 XSLT
================================================================
Revision History is at the end of this file.

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
Preamble
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  -->

<xsl:namespace-alias stylesheet-prefix="sfm" result-prefix="xsl"/>
  <!--
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
Main template
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
 -->
<!--   <xsl:template match="/">-->
  <xsl:template match="/database">
    <!-- output header info -->
    <sfm:stylesheet>
      <xsl:attribute name="version">1.0</xsl:attribute>
      <xsl:element name="xsl:output">
        <xsl:attribute name="method">xml</xsl:attribute>
        <xsl:attribute name="version">1.0</xsl:attribute>
        <xsl:attribute name="encoding">utf-8</xsl:attribute>
        <xsl:attribute name="indent">yes</xsl:attribute>
      </xsl:element>
<!--
      <xsl:element name="xsl:strip-space">
        <xsl:attribute name="elements">Variant Example Subentry Entry Sense</xsl:attribute>
      </xsl:element>
-->
      <xsl:comment>
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
   </xsl:comment>

		<xsl:text disable-output-escaping="yes">
			&lt;msxsl:script language="C#" implements-prefix="user"&gt;
				&lt;![CDATA[
	// create a new GUID and return it
	public string CreateGUID_REAL()
	{
		System.Guid newGuid = Guid.NewGuid();
		return "I" + newGuid.ToString().ToUpper();
	}
	public static double ID = 1000;	// this is a global ID
	public string CreateGUID()
	{
		ID++;
		return "I" + ID.ToString();
	}

      ]]&gt;
			&lt;/msxsl:script&gt;
		</xsl:text>
		<xsl:text disable-output-escaping="no"/>

		
      <sfm:template match="/database">
        <sfm:element name="dictionary">
          <sfm:attribute name="affixMarker">
            <xsl:choose>
              <xsl:when test="setting[@affixMarker]">
                <xsl:value-of select="setting/@affixMarker"/>
              </xsl:when>
              <xsl:otherwise><xsl:text>-</xsl:text></xsl:otherwise>
            </xsl:choose>
          </sfm:attribute>
          <sfm:for-each select="//Entry">
			  <sfm:element name="Entry">
				  <sfm:attribute name="guid">
					 <xsl:text disable-output-escaping="yes">&lt;xsl:value-of select="user:CreateGUID()"/&gt;</xsl:text>
				  </sfm:attribute>				  
				  <sfm:apply-templates/>
			  </sfm:element>
		  </sfm:for-each>
        </sfm:element>
      </sfm:template>
      
      <!-- Only process each field that has a meaning[@id] that isn't empty START -->
      <xsl:for-each select="//fieldDescriptions/field/meaning[@id!='']">
        <sfm:template>
          <!-- Determine what the match value should be. If autofield include the class name. -->
            <xsl:variable name="safeSfm">
              <xsl:choose>
                <xsl:when test="../@autoSfm">
                  <xsl:value-of select="../@autoSfm"/>
                </xsl:when>
                <xsl:otherwise><xsl:value-of select="../@sfm"/></xsl:otherwise>
              </xsl:choose>
            </xsl:variable>
            <xsl:variable name="matchName">
              <xsl:choose>
                <xsl:when test="../@autoImportClassName">
                  <xsl:value-of select="../@autoImportClassName"/>
                  <xsl:text>/</xsl:text>
<!--                  <xsl:value-of select="../@sfm"/> -->
                  <xsl:value-of select="$safeSfm"/>
                </xsl:when>
                <xsl:otherwise><xsl:value-of select="$safeSfm"/></xsl:otherwise>
<!--                <xsl:otherwise><xsl:value-of select="../@sfm"/></xsl:otherwise> -->
              </xsl:choose>
            </xsl:variable>
          <!-- Special processing for autofields -->
          <!-- Now regular fields -->
          <xsl:attribute name="match"><xsl:value-of select="$matchName"/></xsl:attribute>

		  <sfm:element>
           <xsl:attribute name="name"><xsl:value-of select="@id"/></xsl:attribute>
			  <!-- guid attribute on child elements if desired at some point -->
			  <!-- sfm:attribute name="guid">
				  <xsl:text disable-output-escaping="yes">&lt;xsl:value-of select="user:CreateGUID()"/&gt;</xsl:text>
			  </sfm:attribute -->

			  <xsl:if test="../@sfm">
              <sfm:attribute name="sfm">
                <!-- Here we don't want to put out the auto-generated sfm, use the orig even if invalid -->
                <xsl:value-of select="../@sfm"/>
<!--                <xsl:value-of select="$safeSfm"/> -->
              </sfm:attribute>
            </xsl:if>
            <xsl:if test="@func">
              <sfm:attribute name="func">
                <xsl:value-of select="@func"/>
              </sfm:attribute>
            </xsl:if>
            <xsl:if test="../@xml:lang">
              <sfm:attribute name="ws">
                <xsl:value-of select="../@xml:lang"/>
              </sfm:attribute>
            </xsl:if>
            <xsl:if test="../@abbr">
              <sfm:attribute name="abbr">
                <xsl:value-of select="../@abbr"/>
              </sfm:attribute>
            </xsl:if>

			  <sfm:apply-templates/>
          </sfm:element>
        </sfm:template>
      </xsl:for-each>
      <!-- Only process each field that has a meaning[@id] that isn't empty END -->

      <!-- Create a template that does nothing for fields that are missing the id START -->
      <xsl:for-each select="//fieldDescriptions/field/meaning[@id='']">
        <sfm:template>
        
            <!-- Determine what the match value should be. If autofield include the class name. -->
            <xsl:variable name="safeSfm">
              <xsl:choose>
                <xsl:when test="../@autoSfm">
                  <xsl:value-of select="../@autoSfm"/>
                </xsl:when>
                <xsl:otherwise><xsl:value-of select="../@sfm"/></xsl:otherwise>
              </xsl:choose>
            </xsl:variable>
            <xsl:variable name="matchName">
              <xsl:choose>
                <xsl:when test="../@autoImportClassName">
                  <xsl:value-of select="../@autoImportClassName"/>
                  <xsl:text>/</xsl:text>
                  <xsl:value-of select="$safeSfm"/>
                </xsl:when>
                <xsl:otherwise><xsl:value-of select="$safeSfm"/></xsl:otherwise>
              </xsl:choose>
            </xsl:variable>
          <!-- Special processing for autofields -->
          <!-- Now regular fields -->
          <xsl:attribute name="match"><xsl:value-of select="$matchName"/></xsl:attribute>
<!--          <xsl:attribute name="match"><xsl:value-of select="../@sfm"/></xsl:attribute> -->
          <!-- sfm:comment>The marker '<xsl:value-of select="../@sfm"/>' is ignored due to empty meaning@id. </sfm:comment -->

			<xsl:if test="@id and not(@id='')">
				<xsl:attribute name="name"><xsl:value-of select="@id"/></xsl:attribute>
			</xsl:if>
          <sfm:comment>The marker '<xsl:value-of select="../@sfm"/>' is ignored due to empty meaning@id. </sfm:comment>
        </sfm:template>
      </xsl:for-each>
      <!-- Only process each field that has a meaning[@id] that isn't empty END -->
      
      <xsl:for-each select="//inFieldMarkers/ifm">
        <sfm:template>
          <xsl:attribute name="match"><xsl:value-of select="@element"/></xsl:attribute>
          <sfm:element>
<!--            <xsl:attribute name="name"><xsl:value-of select="@element"/></xsl:attribute> -->
            <xsl:attribute name="name">InFieldMarker</xsl:attribute>
            <xsl:if test="@xml:lang">
              <sfm:attribute name="ws">
                <xsl:value-of select="@xml:lang"/>
              </sfm:attribute>
            </xsl:if>
            <xsl:if test="@lang">
              <sfm:attribute name="ws">
                <xsl:value-of select="@lang"/>
              </sfm:attribute>
            </xsl:if>
            <xsl:if test="@style">
              <sfm:attribute name="style">
                <xsl:value-of select="@style"/>
              </sfm:attribute>
            </xsl:if>
            <xsl:if test="@ignore">
              <sfm:attribute name="ignore">
                <xsl:value-of select="@ignore"/>
              </sfm:attribute>
            </xsl:if>


			  <sfm:apply-templates/>
          </sfm:element>
        </sfm:template>
      </xsl:for-each>

      <sfm:template match=" Sense | Example | Function | Subentry | Variant | Etymology | Picture | Pronunciation | SemanticDomain">
        <xsl:comment>
        Only elements that have content are copied and propigated through the xsl's.
		All high level Elements are now 'tagged' with a GUID for future identification and reference.
        </xsl:comment>
        <sfm:if test="@* or ''!=.">
          <sfm:copy>
			  <sfm:attribute name="guid">
				  <xsl:text disable-output-escaping="yes">&lt;xsl:value-of select="user:CreateGUID()"/&gt;</xsl:text>
			  </sfm:attribute>
			  <sfm:apply-templates/>
          </sfm:copy>
        </sfm:if>
      </sfm:template>

    </sfm:stylesheet>
  </xsl:template>
</xsl:stylesheet>
<!--

================================================================
Revision History
- - - - - - - - - - - - - - - - - - -
03-Mar-2005    Andy Black  Began working on Initial Draft
28-Mar-2005	   dlh - modifications for the Abbr attribute.
20-Jun-2005	   dlh - modifications new class names for 'Entry',
				 'Sense' and 'Example'.
17-Aug-2005    dlh - modifications for the auto fields.
16-Nov-2005	   dlh - adding restriction that elements have to have content to be passed on.
================================================================
 -->
