// See https://aka.ms/new-console-template for more information
//using System.Data;
//using System.Data.SqlClient;
////Ctrl+.
////Ctrl+D -duplicate
////Alt+Up key,down 
////F10 summary
////Ctrl+M+H -for summarize or create region
////F11-detail
///ctrl +k+d= format
//Console.WriteLine("Hello, World!");
//SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
//{
//    DataSource = ".",
//    InitialCatalog = "WaiPhyoMaung_A",
//    UserID = "sa",
//    Password = "091537"
//};
// SqlConnection connection = new SqlConnection(connectionStringBuilder.ToString());
//Console.WriteLine("Connection opening....");
//connection.Open();
//Console.WriteLine("Connection Opened.");

//string query = @"SELECT [Blog_Id]
//      ,[Blog_Title]
//      ,[Blog_Author]
//      ,[Blog_Content]
//  FROM [dbo].[Tbl_Blog]";

//SqlCommand command =new SqlCommand(query,connection);
//SqlDataAdapter adapter = new SqlDataAdapter(command);
////old style
//DataTable dt=new DataTable();
//adapter.Fill(dt);
////new style
////dt = adapter.Fill(dt);

//Console.WriteLine("Connection closing....");
//connection.Close();
//Console.WriteLine("Connection Closed.");

////Dataset -multiple table
////DataTable -foronly one table
////DataRow
////DataColum
//foreach(DataRow dr in dt.Rows)
//{
//    //Console.WriteLine($"Id=> { dr["Blog_Id"]}");
//    Console.WriteLine("Id=>" + dr["Blog_Id"]);
//    Console.WriteLine("Title=>"+ dr["Blog_Title"]);
//    Console.WriteLine("Author=>" + dr["Blog_Author"]);
//    Console.WriteLine("Content=>" + dr["Blog_Content"]);
//    Console.WriteLine("--------------------------------------");
//}
using Serilog;
using Serilog.Sinks.MSSqlServer;
using WaiphyomaungDotNetCore.ConsoleApp.AdoDotNetExamples;
using WaiphyomaungDotNetCore.ConsoleApp.DapperExamples;
using WaiphyomaungDotNetCore.ConsoleApp.EFCoreExamples;
using WaiphyomaungDotNetCore.ConsoleApp.HttpClientExamples;
using WaiphyomaungDotNetCore.ConsoleApp.RefitExamples;
using WaiphyomaungDotNetCore.ConsoleApp.RestClientExamples;

#region Log
//https://github.com/serilog/serilog/wiki/Getting-Started
//https://github.com/serilog/serilog/wiki/Provided-Sinks

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error() //change in level
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Hour)

#region for Dblog //Serilog.Sinks.MSSqlServer Nugetpackage
             .WriteTo
    .MSSqlServer(
        connectionString: "Server=.;Database=WaiPhyoMaung_A;User Id=sa;Password=091537;TrustServerCertificate = true;",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Tbl_Log", AutoCreateSqlTable = true, })
#endregion

            .CreateLogger();
Log.Information("Hello, world!");

int a = 10, b = 0;
try
{
    Log.Debug("Dividing {A} by {B}", a, b);
    Console.WriteLine(a / b);
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    await Log.CloseAndFlushAsync();
}
#endregion





//AdoDotNetExample adoDotnewexample=new AdoDotNetExample();
//adoDotnewexample.Run();

//DapperExample dapperExample=new DapperExample();
//dapperExample.Run();
//Console.ReadKey();

//EFCoreExample _EFCoreExample=new EFCoreExample();
//_EFCoreExample.Run();


//HttpClientExample httpClientExample=new HttpClientExample();
//await httpClientExample.Run();


//RestClientExample restClientExample = new RestClientExample();
//await restClientExample.Run();
//Console.ReadKey();

//Console.WriteLine("Please wait for api...");
//Console.ReadKey();

//RefitExample refitExample=new RefitExample();
//await refitExample.Run();

//Console.ReadLine();
