using json_2_sql_parser_poc.Common.Enums;
using json_2_sql_parser_poc.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpSerivces(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//await XmlFileSearchAndReplaceService.SearchAndReplaceTagsForAllXmlFiles();

TranslationExcelSheetToXmlParser.ParseExcelTranslationToXmlFiles(DocTypeEnum.IAT);
// TranslationExcelSheetToXmlParser.ParseExcelTranslationToXmlFiles(DocTypeEnum.OFA);

// WordFileSearchAndReplaceService.SearchAndReplaceTagsForAllTemplates();

// await XmlValueParser.ParseValues();

//JsonToSqlQueryParserService.Parse();

app.Run();
