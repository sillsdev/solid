<?xml version="1.0"?>
<xsl:transform xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="xml" indent="yes" encoding="utf-8"/>
  <xsl:variable name="vernacular-writing-system" select="'sza'"/>
  <xsl:variable name="national-writing-system" select="'ms-my'"/>
  
  <xsl:variable name="alternate-hierarchy" select="false()"/>
  
  <xsl:template match="/">
    <lift version="0.10">
      <xsl:apply-templates select="//entry"/>
    </lift>
  </xsl:template>
  
  <xsl:template match="entry">
    <entry>
      <xsl:if test="descendant::*[@lift='citation'][not(data = '')]">
        <xsl:attribute name="id">
          <xsl:value-of select="descendant::*[@lift='citation']/data"/>
        </xsl:attribute>
      </xsl:if>
		<xsl:if test="descendant::*[@lift='homonym']/data[not(.='')]">
			<xsl:attribute name="order">
				<xsl:value-of select="descendant::*[@lift='homonym']/data"/>
			</xsl:attribute>
		</xsl:if>
      <xsl:if test="descendant::*[@lift='dateModified'][not(data = '')]">
        <xsl:attribute name="dateModified">
          <xsl:apply-templates select="descendant::*[@lift='dateModified']"/>
        </xsl:attribute>
      </xsl:if>
      <lexical-unit>
      <xsl:apply-templates select="descendant::*[@lift='lexicalUnit']"/>
      </lexical-unit>
		<xsl:apply-templates select="descendant::*[@lift='citation']"/>
		<xsl:apply-templates select="descendant::*[@lift='pronunciation']"/>
		<xsl:apply-templates select="descendant::*[@lift='variant']"/>
		<xsl:apply-templates select="descendant::*[@lift='sense']"/>
		<xsl:apply-templates select="descendant::*[@lift='confer']"/>
		<!-- etymology is *only* in sense according to Lift 10.0 but many mdf sources have it at the entry level. -->
		<xsl:apply-templates select="descendant::*[@lift='etymology']"/>
		<xsl:apply-templates select="descendant::*[@lift='borrowedWord']"/>
	</entry>
  </xsl:template>
  
  <xsl:template match="*[@lift='variant']">
    <xsl:if test="not(data = '')">
      <variant>
        <form>
          <xsl:attribute name="lang">
            <xsl:value-of select="@writingsystem"/>
          </xsl:attribute>
          <text>
            <xsl:value-of select="data"/>
          </text>
        </form>
      </variant>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='confer']">
    <xsl:if test="not(data = '')">
      <relation name="confer">
        <xsl:attribute name="ref">
          <xsl:value-of select="data"/>
        </xsl:attribute>
      </relation>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='lexicalUnit']">
    <xsl:if test="not(data = '')">
	<form>
	  <xsl:attribute name="lang">
	    <xsl:value-of select="@writingsystem"/>
	  </xsl:attribute>
	  <text>
	    <xsl:value-of select="data"/>
	  </text>
	</form>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='sense']">
    <sense>
      <xsl:choose>
        <xsl:when test="$alternate-hierarchy">
          <xsl:apply-templates select="preceding-sibling::*[@lift='grammi']"/>
        </xsl:when>
        <xsl:otherwise>
          <xsl:apply-templates select="descendant::*[@lift='grammi']"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:apply-templates select="descendant::*[@lift='gloss']"/>
      <xsl:if test="descendant::*[@lift='definition'][not(data='')]">
        <definition>
          <xsl:apply-templates select="descendant::*[@lift='definition']"/>
        </definition>
      </xsl:if>
		<xsl:apply-templates select="descendant::*[@lift='example']"/>
		<xsl:apply-templates select="descendant::*[@lift='etymology']"/>
		<xsl:apply-templates select="descendant::*[@lift='borrowedWord']"/>
		<xsl:apply-templates select="descendant::*[@lift='reversal']"/>
		<xsl:apply-templates select="descendant::*[@lift='semanticDomain']"/>
		<xsl:apply-templates select="descendant::*[@lift='semanticDomainID']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteBibliographic']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteEncyclopedic']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteAnthropology']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteDiscourse']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteGrammar']"/>
		<xsl:apply-templates select="descendant::*[@lift='notePhonology']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteQuestion']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteSociolinguistic']"/>
		<xsl:apply-templates select="descendant::*[@lift='noteRestriction']"/>
		<xsl:apply-templates select="descendant::*[@lift='note']"/>
	</sense>
  </xsl:template>
  
  <xsl:template match="*[@lift='example']">
    <example>
      <xsl:if test="not(data='')">
        <xsl:attribute name="source">
          <xsl:value-of select="data" />
        </xsl:attribute>
      </xsl:if>
      <xsl:apply-templates select="descendant::*[@lift='exampleSentence']"/>
      <xsl:if test="descendant::*[@lift='exampleSentenceTranslation'][not(data='')]">
        <translation>
          <xsl:apply-templates select="descendant::*[@lift='exampleSentenceTranslation']"/>
        </translation>
      </xsl:if>
    </example>
  </xsl:template>
  
  <xsl:template match="*[@lift='grammi']">
    <xsl:if test="not(data = '')">
      <grammatical-info value="{data}"/>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='gloss']">
    <xsl:if test="not(data = '')">
      <gloss>
        <xsl:attribute name="lang">
          <xsl:value-of select="@writingsystem"/>
        </xsl:attribute>
        <text>
          <xsl:value-of select="data"/>
        </text>
      </gloss>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='pronunciation']">
    <xsl:if test="not(data = '')">
      <pronunciation>
        <xsl:attribute name="lang">
          <xsl:value-of select="@writingsystem"/>
        </xsl:attribute>
        <text>
          <xsl:value-of select="data"/>
        </text>
      </pronunciation>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='definition'] | 
    *[@lift='exampleSentence'] | 
    *[@lift='exampleSentenceTranslation']">
    <xsl:if test="not(data = '')">
      <form>
        <xsl:attribute name="lang">
          <xsl:value-of select="@writingsystem"/>
        </xsl:attribute>
        <text>
          <xsl:value-of select="data"/>
        </text>
      </form>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='dateModified']">
    <!-- shoebox dates are in form: DD/MMM/YYYY-->
    
    <xsl:variable name="year" select="substring(data, 8)"/>
    <xsl:variable name="monthAsAbbrev" select="substring(data, 4, 3)"/>
    <xsl:variable name="day" select="substring(data, 1, 2)"/>
    <xsl:variable name="month">
      <xsl:choose>
        <xsl:when test="$monthAsAbbrev='Jan'">
          <xsl:text>01</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Feb'">
          <xsl:text>02</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Mar'">
          <xsl:text>03</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Apr'">
          <xsl:text>04</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='May'">
          <xsl:text>05</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Jun'">
          <xsl:text>06</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Jul'">
          <xsl:text>07</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Aug'">
          <xsl:text>08</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Sep'">
          <xsl:text>09</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Oct'">
          <xsl:text>10</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Nov'">
          <xsl:text>11</xsl:text>
        </xsl:when>
        <xsl:when test="$monthAsAbbrev='Dec'">
          <xsl:text>12</xsl:text>
        </xsl:when>
        <xsl:otherwise>
          <xsl:message terminate="yes">
            Unknown Month <xsl:value-of select="$monthAsAbbrev"/>
          </xsl:message>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
    
    <!-- output as YYYY-MM-DD -->
    <xsl:value-of select="$year"/>
    <xsl:text>-</xsl:text>
    <xsl:value-of select="$month"/>
    <xsl:text>-</xsl:text>
    <xsl:value-of select="$day"/>
  </xsl:template>
  
  <xsl:template match="*[@lift='reversal']">
    <xsl:if test="not(data='')">
      <reversal>
        <form>
          <xsl:attribute name="lang">
            <xsl:value-of select="@writingsystem"/>
          </xsl:attribute>
          <text>
            <xsl:value-of select="descendant::data"/>
          </text>
        </form>
      </reversal>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='citation']">
    <xsl:if test="not(data='')">
      <citation>
        <form>
          <xsl:attribute name="lang">
            <xsl:value-of select="@writingsystem"/>
          </xsl:attribute>
          <text>
            <xsl:value-of select="descendant::data"/>
          </text>
        </form>
      </citation>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='semanticDomain']">
    <xsl:if test="not(data = '')">
      <trait name="SemanticDomain">
        <xsl:attribute name="value">
          <xsl:value-of select="data"/>
        </xsl:attribute>
      </trait>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='semanticDomainID']">
    <xsl:if test="not(data = '')">
      <field tag="ids">
        <form lang="en">
          <text>
            <xsl:value-of select="data"/>
          </text>
        </form>
      </field>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="*[@lift='note']">
    <xsl:if test="not(data = '')">
      <note>
        <form>
          <xsl:attribute name="lang">
            <xsl:value-of select="@writingsystem"/>
          </xsl:attribute>
          <text>
            <xsl:value-of select="data"/>
          </text>
        </form>
      </note>
    </xsl:if>
  </xsl:template>

	<xsl:template match="*[@lift='etymology']">
		<xsl:if test="not(data = '')">
			<etymology type="proto">
				<xsl:attribute name="source">
					<xsl:value-of select="descendant::*[@lift='etymologySource']/data"/>
				</xsl:attribute>
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
				<xsl:apply-templates select="descendant::*[@lift='gloss']"/>
			</etymology>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='borrowedWord']">
		<xsl:if test="not(data = '')">
			<etymology type="borrow">
				<xsl:attribute name="source">
					<xsl:value-of select="data"/>
				</xsl:attribute>
			</etymology>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteBibliographic']">
		<xsl:if test="not(data = '')">
			<note type="bibliographic">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteEncyclopedic']">
		<xsl:if test="not(data = '')">
			<note type="encyclopedic">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteAnthropology']">
		<xsl:if test="not(data = '')">
			<note type="anthropology">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteDiscourse']">
		<xsl:if test="not(data = '')">
			<note type="discourse">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteGrammar']">
		<xsl:if test="not(data = '')">
			<note type="grammar">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='notePhonology']">
		<xsl:if test="not(data = '')">
			<note type="phonology">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteQuestion']">
		<xsl:if test="not(data = '')">
			<note type="question">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteSociolinguistic']">
		<xsl:if test="not(data = '')">
			<note type="sociolinguistic">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='noteRestriction']">
		<xsl:if test="not(data = '')">
			<note type="restriction">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</note>
		</xsl:if>
	</xsl:template>

	<xsl:template match="*[@lift='comment']">
		<xsl:if test="not(data = '')">
			<field tag="comment">
				<form>
					<xsl:attribute name="lang">
						<xsl:value-of select="@writingsystem"/>
					</xsl:attribute>
					<text>
						<xsl:value-of select="data"/>
					</text>
				</form>
			</field>
		</xsl:if>
	</xsl:template>


</xsl:transform>