using json_2_sql_parser_poc.Models.DocFileTranslations;
using Spire.Doc;
using System.Text;

namespace json_2_sql_parser_poc.Services;

public static class WordFileSearchAndReplaceService
{
    public static void SearchAndReplaceTagsForAllTemplates()
    {
        // get file names
        List<string> templateNames = Directory
            .GetFiles(@"Shared\Input", "*.docx", SearchOption.TopDirectoryOnly)
            .Select(x => x.Split('\\')
            .Last())
            .ToList();

        // process files
        foreach (string templateName in templateNames)
        {
            SearchAndReplaceTags(templateName);
        }
    }
    private static void SearchAndReplaceTags(string fileName)
    {
        string inputFilePath = @"Shared\Input\"; // Path to your word file
        string outputDirectory = @"Shared\Output"; // Directory to save new doc file

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // validate target dir exists
        Directory.CreateDirectory(outputDirectory);

        // create doc
        Document srcDoc = new Document();

        // load src file
        srcDoc.LoadFromFile(inputFilePath + fileName);

        // save original formatting
        var srcFormat = srcDoc.DetectedFormatType;

        // load dictionary values
        DocFileTranslations docFileTranslations = new DocFileTranslations();
        var dictionary = docFileTranslations.Dictionary;

        // search and replace tags
        foreach (KeyValuePair<string, string> kv in dictionary)
        {
            srcDoc.Replace(kv.Key, kv.Value, true, true);
        }

        // clone doc
        Document newDoc = srcDoc.Clone();

        // set path to new doc
        string outputPath = Path.Combine(outputDirectory, fileName);

        // initiate memory stream
        var stream = new MemoryStream();

        // save doc as memory stream
        newDoc.SaveToStream(stream, srcFormat);

        // convert stream to byte array
        byte[] output = stream.ToArray();

        // write new doc
        File.WriteAllBytes(outputPath, output);

        // dispose of resources
        stream.Close();
        srcDoc.Dispose();
        newDoc.Dispose();
    }
}
