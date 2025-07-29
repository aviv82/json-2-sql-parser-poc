namespace json_2_sql_parser_poc.Common;

public class Constants
{
    public static class Environment
    {
        public static class Variables
        {
            public static class ServiceConnect
            {
                public const string BaseUrl = "Sc:BaseUrl";
                public const string ApiKey = "Sc:ApiKey";
                public const string ApiValue = "Sc:ApiValue";
            }
            public static class TridionDocs
            {
                public const string BaseUrl = "Td:BaseUrl";
                public const string ClientId = "Td:ClientId";
                public const string ClientSecret = "Td:ClientSecret";
            }
        }
    }
    public static class TridionDocs
    {
        public static class Field
        {
            public const string Title = "FTITLE";
            public const string Version = "VERSION";
            public const string PublicationType = "FISHPUBLICATIONTYPE";
            public const string MasterRef = "FISHMASTERREF";
            public const string Resources = "FISHRESOURCES";
            public const string PubSourceLanguages = "FISHPUBSOURCELANGUAGES";
            public const string RequiredResolutions = "FISHREQUIREDRESOLUTIONS";
            public const string Baseline = "FISHBASELINE";
            public const string RequestedLanguages = "FREQUESTEDLANGUAGES";
            public const string ProjectName = "FATCPROJECTNAME";
            public const string MachineCategory = "FATCMACHINECATEGORY";
            public const string StartDate = "FATCSTARTDATE";
            public const string PubStatus = "FISHPUBSTATUS";
            public const string LanguageCombination = "FISHPUBLNGCOMBINATION";
            public const string OutputFormat = "FISHOUTPUTFORMATREF";
            public const string PrintedMatterNumber = "FATCPRINTEDMATTERNUMBER";
        }

        public static class Variable
        {
            public const string LanguagePrefix = "VLANGUAGE";
            public const string OutputFormatModelBook = "VOUTPUTFORMATMODELBOOK";
        }

    }
}