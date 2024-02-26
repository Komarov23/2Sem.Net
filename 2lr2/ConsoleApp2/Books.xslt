<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<html>
			<head>
				<title>Books Catalog</title>
			</head>
			<body>
				<h1>Books Catalog</h1>
				<table border="1">
					<tr>
						<th>Title</th>
						<th>Author</th>
					</tr>
					<xsl:apply-templates select="Books/Book" />
				</table>
			</body>
		</html>
	</xsl:template>

	<xsl:template match="Book">
		<tr>
			<td>
				<xsl:value-of select="Title" />
			</td>
			<td>
				<xsl:value-of select="Author" />
			</td>
		</tr>
	</xsl:template>

</xsl:stylesheet>