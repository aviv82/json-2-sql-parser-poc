using json_2_sql_parser_poc.Models.DocFileTranslations;
using System.Text;
using System.Xml;

namespace json_2_sql_parser_poc.Services;

public static class XmlFileSearchAndReplaceService
{
    public static async Task SearchAndReplaceTagsForAllXmlFiles()
    {
        // get file names
        List<string> fileList = Directory
            .GetFiles(@"Shared\Input", "*.xml", SearchOption.TopDirectoryOnly)
            .Select(x => x.Split('\\')
            .Last())
            .ToList();

        // process files
        foreach (string fileName in fileList)
        {
            await SearchAndReplaceTags(fileName);
        }
    }

    private static async Task SearchAndReplaceTags(string fileName)
    {
        string inputFilePath = @"Shared\Input\"; // Path to your word file
        string outputDirectory = @"Shared\Output\"; // Directory to save new doc file

        byte[] byteArray = File.ReadAllBytes(inputFilePath + fileName);

        Stream arrayAsStream = new MemoryStream(byteArray);

        FormFile formFile = new FormFile(arrayAsStream, arrayAsStream.Position, arrayAsStream.Length, "someString", fileName);

        using var streamReader = new StreamReader(formFile.OpenReadStream());
        var xmlContent = await streamReader.ReadToEndAsync();

        XmlDocument xmlDoc = new();

        xmlDoc.LoadXml(xmlContent);

        var dictionaryNode = xmlDoc.SelectSingleNode("//Dictionary");

        if (dictionaryNode == null) return;

        string language = dictionaryNode.Attributes?["Name"]?.Value?.ToString() ?? "";

        if (string.IsNullOrEmpty(language)) return;

        var keyNodes = dictionaryNode!.SelectNodes("Key");

        List<XmlNode> newKeys = [];

        DocFileTranslations dictionaryClass = new();
        Dictionary<string, string> dictionary = dictionaryClass.Dictionary;

        // Create the XML document
        XmlDocument newXmlDoc = new XmlDocument();
        XmlElement root = newXmlDoc.CreateElement("Xml");
        newXmlDoc.AppendChild(root);

        XmlElement newDictionary = newXmlDoc.CreateElement("Dictionary");
        newDictionary.SetAttribute("Name", language);
        root.AppendChild(newDictionary);

        foreach (XmlNode keyNode in keyNodes!)
        {
            string keyNodeValue = keyNode.Attributes?["Id"]?.Value ?? "";

            string innerXml = keyNode.InnerXml ?? "";

            if (string.IsNullOrWhiteSpace(keyNodeValue) || string.IsNullOrWhiteSpace(innerXml) || !keyNodeValue.Contains("ACTechData.", StringComparison.CurrentCultureIgnoreCase)) continue;

            XmlElement newKey = newXmlDoc.CreateElement("Key");

            if (dictionary.ContainsKey(keyNodeValue.Replace("ACTechData.", "")))
            {
                newKey.SetAttribute("Id", keyNodeValue.Replace(keyNodeValue.Replace("ACTechData.", ""), dictionary[keyNodeValue.Replace("ACTechData.", "")]));
            }
            else
            {
                newKey.SetAttribute("Id", keyNodeValue);
            }

            newKey.InnerXml = innerXml;
            newKeys.Add(newKey);

            newDictionary.AppendChild(newKey);
        }

        // Save the new XML file
        string outputPath = Path.Combine(outputDirectory, fileName);

        using (var writer = new XmlTextWriter(outputPath, Encoding.UTF8) { Formatting = Formatting.Indented })
        {
            newXmlDoc.Save(writer);

            writer.Close();
        }

        streamReader.Close();
        arrayAsStream.Close();
    }
}
