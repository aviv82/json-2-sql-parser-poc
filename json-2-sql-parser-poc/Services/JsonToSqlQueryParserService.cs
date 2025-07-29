namespace json_2_sql_parser_poc.Services;

public static class JsonToSqlQueryParserService
{
    public static void Parse()
    {
        StreamReader readSNs = new StreamReader(@"toClear.txt");

        string asString = readSNs.ReadToEnd();
        readSNs.Close();

        string[] asArray = asString.Split("\r\n");

        //StreamReader readSNs2 = new StreamReader(@"sns.txt");

        //string asString2 = readSNs2.ReadToEnd();
        //readSNs2.Close();
        //string[] asArray2 = asString2.Split("\r\n");

        List<string> toDelete = [];

        foreach (string s in asArray)
        {
            if (s != string.Empty) toDelete.Add(s);
        }

        StreamWriter write = new StreamWriter(@"deleteQuery.txt");

        foreach (string line in toDelete)
        {
            write.WriteLine($"DELETE FROM ShopOrders WHERE SerialNumber = '{line}'");
        }
        write.Close();

        //StreamReader read = new StreamReader(@"Results.json");

        //string json = read.ReadToEnd();
        //read.Close();

        //List<ShopOrder> firstList = JsonConvert.DeserializeObject<List<ShopOrder>>(json)!;

        //List<ShopOrder> orders = [];

        //foreach (ShopOrder order in firstList)
        //{
        //    if (order.OperationNumber == string.Empty) order.OperationNumber = "0";
        //    if (order.BcpsStatus == null) order.BcpsStatus = 32;
        //    if (order.Model == string.Empty) order.Model = "N/A";

        //    if (asArray.Contains(order.SerialNumber))
        //    {
        //        orders.Add(order);
        //    }
        //}

        //orders = orders.Where(x => x.SerialNumber != null && x.SerialNumber != string.Empty).OrderBy((o) => Int32.Parse(o.OperationNumber)).Reverse().ToList();

        //List<string> lines = [];
        //List<string> sns = [];

        //var count = 0;
        //foreach (ShopOrder order in orders)
        //{
        //    if (order.Model != null && order.Model.Trim() != "")
        //    {

        //        int operationNumberAsInt = Int32.Parse(order.OperationNumber);
        //        var blocked = 0;

        //        if (operationNumberAsInt >= 799)
        //        {
        //            blocked = 1;
        //        }

        //        List<string> languageArray = order.LANGUAGES.Split(',').Distinct().ToList();

        //        var businessLine = 1;

        //        if (order.ProductCompany == "AIF")
        //        {
        //            businessLine = 2;
        //        }

        //        var clientLanguage = languageArray.Where((l) => l != "EN").FirstOrDefault();

        //        if (clientLanguage != null)
        //        {
        //            lines.Add($"INSERT INTO [ShopOrders] ([SerialNumber],[ModelName],[DefaultLanguage],[CustomerLanguage],[State],[NotificationTypeId],[ProductCompanyId],[CreatedOn],[Blocked]) VALUES ('{order.SerialNumber}','{order.Model.Trim()}','EN','{clientLanguage}',1,3,{businessLine},'{order.StartDate}',{blocked})");
        //        }
        //        else
        //        {
        //            lines.Add($"INSERT INTO [ShopOrders] ([SerialNumber],[ModelName],[DefaultLanguage],[State],[NotificationTypeId],[ProductCompanyId],[CreatedOn],[Blocked]) VALUES ('{order.SerialNumber}','{order.Model.Trim()}','EN',1,3,{businessLine},'{order.StartDate}',{blocked})");
        //        }

        //        sns.Add(order.SerialNumber);

        //        Console.WriteLine("index:" + count + " - serial: " + order.SerialNumber + " op: " + order.OperationNumber + " is blocked? :" + blocked + " client langs: " + clientLanguage + " pc breakdown: " + businessLine);

        //        count++;
        //    }
        //}

        //StreamWriter write = new StreamWriter(@"query.txt");

        //foreach (string line in lines)
        //{
        //    write.WriteLine(line);
        //}
        //write.Close();

        //StreamWriter writeSns = new StreamWriter(@"sns.txt");
        //foreach (string line in sns)
        //{
        //    writeSns.WriteLine(line);
        //}
        //writeSns.Close();
    }
}
