<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
     version="1.0">
<xsl:output method="xml" omit-xml-declaration="yes"/>
<xsl:template match="@remoteIp">
  <xsl:attribute name="remoteIp">UNIQNAME_SERVER_IP</xsl:attribute> 
</xsl:template>

<xsl:template match="@localId">
  <xsl:attribute name="localId">UN_LOCAL_ID</xsl:attribute> 
</xsl:template>

<xsl:template match="@remotePort">
  <xsl:attribute name="remotePort">eval(STARTPORT+24)</xsl:attribute> 
</xsl:template>

<xsl:template match="@*|*">
  <xsl:copy>
    <xsl:apply-templates select="@*|node()"/>
  </xsl:copy>
</xsl:template>

</xsl:stylesheet>
