using System.Xml;

namespace json_2_sql_parser_poc.Services;

public static class XmlValueParser
{

    public static async Task ParseValues()
    {
        string inputFilePath = @"Shared\Input\"; // Path to your word file
        string outputDirectory = @"Shared\Output\"; // Directory to save new doc file

        byte[] byteArray = File.ReadAllBytes(inputFilePath + "someXml.xml");

        Stream arrayAsStream = new MemoryStream(byteArray);

        FormFile formFile = new FormFile(arrayAsStream, arrayAsStream.Position, arrayAsStream.Length, "someString", "someXml.xml");

        using var streamReader = new StreamReader(formFile.OpenReadStream());
        var xmlContent = await streamReader.ReadToEndAsync();

        XmlDocument xmlDoc = new();

        xmlDoc.LoadXml(xmlContent);

        var dictionaryNode = xmlDoc.SelectSingleNode("//Dictionary");

        var keyNodes = dictionaryNode!.SelectNodes("Key");

        List<string> keys = [];

        foreach (XmlNode keyNode in keyNodes!)
        {
            string keyNodeValue = keyNode.Attributes?["Id"]?.Value ?? "";

            if (string.IsNullOrWhiteSpace(keyNodeValue) || !keyNodeValue.Contains("actechdata.", StringComparison.CurrentCultureIgnoreCase)) continue;

            keys.Add(keyNodeValue.Replace("ACTechData.", string.Empty, StringComparison.CurrentCultureIgnoreCase));
        }

        keys.Sort();


        StreamWriter write = new StreamWriter(outputDirectory + "ofaTranslationKeys.txt");

        foreach (string key in keys)
        {
            write.WriteLine(key);
        }

        write.Close();
        streamReader.Close();
        arrayAsStream.Close();
    }
}
