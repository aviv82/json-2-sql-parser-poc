using ExcelDataReader;
using json_2_sql_parser_poc.Common.Enums;
using System.Text;
using System.Xml;

namespace json_2_sql_parser_poc.Services;

public static class TranslationExcelSheetToXmlParser
{
    public static void ParseExcelTranslationToXmlFiles(DocTypeEnum docType)
    {
        string dictionaryPrefix = GetPrefix(docType);

        string inputFilePath = @$"Shared\Input\{dictionaryPrefix}Dictionary.xlsx"; // Path to your Excel file
        string outputDirectory = @"Shared\Output\"; // Directory to save the XML files

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Read the Excel file
        using (var stream = File.Open(inputFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = reader.AsDataSet();
                var dataTable = dataSet.Tables[0]; // Assuming the first sheet contains the data

                // Extract the headers
                var columns = dataTable.Columns;
                var rows = dataTable.Rows;

                // Process each language column
                for (int col = 1; col < columns.Count; col++) // Start from 1 to skip the Key column
                {
                    string languageCode = rows[0][col]?.ToString() ?? "";
                    string languageName = GetLanguageName(languageCode) ?? "";
                    if (languageName == null) continue;

                    // Create the XML document
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlElement root = xmlDoc.CreateElement("Xml");
                    xmlDoc.AppendChild(root);

                    XmlElement dictionary = xmlDoc.CreateElement("Dictionary");
                    dictionary.SetAttribute("Name", languageName);
                    root.AppendChild(dictionary);

                    // Add key-translation pairs
                    for (int row = 1; row < rows.Count; row++)
                    {
                        string key = rows[row][0]?.ToString() ?? ""; // Key column
                        string translation = rows[row][col]?.ToString() ?? ""; // Translation column
                        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(translation)) continue;

                        XmlElement keyElement = xmlDoc.CreateElement("Key");
                        keyElement.SetAttribute("Id", "AcTechData." + key);
                        keyElement.InnerXml = $"<![CDATA[{translation}]]>";
                        dictionary.AppendChild(keyElement);
                    }

                    // Save the XML file
                    string outputPath = docType == DocTypeEnum.OFA ?
                        Path.Combine(outputDirectory, $"{languageName.Replace(" ", "_")}-AC.xml") :
                        Path.Combine(outputDirectory, $"{languageName.Replace(" ", "_")}.xml");


                    // outputPath.Dump();

                    using (var writer = new XmlTextWriter(outputPath, Encoding.UTF8) { Formatting = Formatting.Indented })
                    {
                        xmlDoc.Save(writer);
                    }
                }
            }
        }

        Console.WriteLine("XML files generated successfully.");
    }

    private static string GetPrefix(DocTypeEnum docType)
    {
        return docType switch
        {
            DocTypeEnum.IAT => "iat",
            DocTypeEnum.OFA => "ofa",
            DocTypeEnum.QQ => "qq",
            _ => ""
        };
    }

    // Function to map language codes to language names
    static string? GetLanguageName(string languageCode)
    {
        return languageCode switch
        {
            "bg-BG" => "Bulgarian",
            "cs-CZ" => "Czech",
            "da-DK" => "Danish",
            "de-DE" => "German",
            "el-GR" => "Greek",
            "en-GB" => "GB English",
            "en-US" => "US English",
            "es-ES" => "Spanish",
            "fi-FI" => "Finnish",
            "fr-FR" => "French",
            "hr-HR" => "Croatian",
            "hu-HU" => "Hungarian",
            "it-IT" => "Italian",
            "ja-JP" => "Japanese",
            "ko-KR" => "Korean",
            "lt-LT" => "Lithuanian",
            "lv-LV" => "Latvian",
            "nl-NL" => "Dutch",
            "no-NO" => "Norwegian",
            "pl-PL" => "Polish",
            "pt-BR" => "BR Portuguese",
            "pt-PT" => "Portuguese",
            "ro-RO" => "Romanian",
            "ru-RU" => "Russian",
            "sk-SK" => "Slovak",
            "sl-SI" => "Slovenian",
            "sr-RS" => "Serbian",
            "sv-SE" => "Swedish",
            "tr-TR" => "Turkish",
            "vi-VN" => "Vietnamese",
            "zh-CN" => "Chinese",
            "zh-TW" => "TW Chinese",
            _ => null
        };
    }
}
