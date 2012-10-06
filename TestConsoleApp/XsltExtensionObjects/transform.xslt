<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:extension="urn:ExtensionObject"
    exclude-result-prefixes="msxsl extension">
    <xsl:output method="xml" indent="yes"/>

    <xsl:variable name="ProductId" select="extension:GetProductId()"/>
    <xsl:variable name="FundsXPathString">
        <xsl:text>ComparisonRequest/ProductsRequested/Product[Id='</xsl:text>
            <xsl:value-of select="$ProductId"/>
        <xsl:text>']/FundsRequested</xsl:text>
    </xsl:variable>
    
    <xsl:template match="/">
        <output>
            <name>
                <xsl:value-of select="/top/first/name"/>
            </name>
            <age>
                <xsl:value-of select="/top/second/age"/>
            </age>
            <details>
                <xsl:apply-templates select="extension:GetNodeSet($FundsXPathString)" />
            </details>
        </output>
    </xsl:template>
    <xsl:template match="FundsRequested">
                <xsl:apply-templates/>
    </xsl:template>
    <xsl:template match="*">
        <xsl:copy>
            <xsl:apply-templates/>
        </xsl:copy>
    </xsl:template>
</xsl:stylesheet>
