<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
     version="1.0">
<xsl:output method="xml" omit-xml-declaration="yes"/>


<xsl:template match="//Manager[@name='Provider']/Connector">
forloop(`PORT', eval(STARTPORT+1), eval(STARTPORT+ LINK_NUMBER), CONNECTOR)
</xsl:template>

<xsl:template match="//Manager[@name='LogClientTcpManager']/@name">
  <xsl:attribute name="class">chuhan.gsp.log.LogManager</xsl:attribute> 
  <xsl:attribute name="name">LogClientTcpManager</xsl:attribute> 
</xsl:template>

<xsl:template match="@*|*">
  <xsl:copy>
    <xsl:apply-templates select="@*|node()"/>
  </xsl:copy>
</xsl:template>

</xsl:stylesheet>
