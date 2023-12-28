using System.Data;
using System.Data.SqlClient;

namespace WaiphyomaungDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        public void Run()
        {
            //  Read();
            //Edit(1);
            // Create("Test Title","Test Author","Test content");
            Delete(14);
            //Update(1,"Test Title", "Test Author", "Test content");
        }
        private void Read()
        {

            //Ctrl+.
            //Ctrl+D -duplicate
            //Alt+Up key,down 
            //F10 summary
            //
            //F11-detail
            Console.WriteLine("Hello, World!");
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "WaiPhyoMaung_A",
                UserID = "sa",
                Password = "091537"
            };
            SqlConnection connection = new SqlConnection(connectionStringBuilder.ToString());
            Console.WriteLine("Connection opening....");
            connection.Open();
            Console.WriteLine("Connection Opened.");

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //old style
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            //new style
            //dt = adapter.Fill(dt);

            Console.WriteLine("Connection closing....");
            connection.Close();
            Console.WriteLine("Connection Closed.");

            //Dataset -multiple table
            //DataTable -foronly one table
            //DataRow
            //DataColum
            foreach (DataRow dr in dt.Rows)
            {
                //Console.WriteLine($"Id=> { dr["Blog_Id"]}");
                Console.WriteLine("Id=>" + dr["Blog_Id"]);
                Console.WriteLine("Title=>" + dr["Blog_Title"]);
                Console.WriteLine("Author=>" + dr["Blog_Author"]);
                Console.WriteLine("Content=>" + dr["Blog_Content"]);
                Console.WriteLine("--------------------------------------");
            }



        }

        private void Edit(int id)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "WaiPhyoMaung_A",
                UserID = "sa",
                Password = "091537"
            };
            SqlConnection connection = new SqlConnection(connectionStringBuilder.ToString());
            Console.WriteLine("Connection opening....");
            connection.Open();
            Console.WriteLine("Connection Opened.");

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] where Blog_Id=@Blog_Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);//to prevent sql injection
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //old style
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Console.WriteLine("Id=>" + dr["Blog_Id"]);
                Console.WriteLine("Title=>" + dr["Blog_Title"]);
                Console.WriteLine("Author=>" + dr["Blog_Author"]);
                Console.WriteLine("Content=>" + dr["Blog_Content"]);
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                Console.WriteLine("No data found");
                return;
            }
        }
        private void Create(string title, string author, string content)
        {

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "WaiPhyoMaung_A",
                UserID = "sa",
                Password = "091537"
            };
            SqlConnection connection = new SqlConnection(connectionStringBuilder.ToString());
            Console.WriteLine("Connection opening....");
            connection.Open();
            Console.WriteLine("Connection Opened.");

            string query = @"
INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title,@Blog_Author,@Blog_Content)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);//to prevent sql injection

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //old style
            int result = command.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed";
            connection.Close();
            Console.WriteLine(message);

        }
        private void Update(int id, string title, string author, string content)
        {

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "WaiPhyoMaung_A",
                UserID = "sa",
                Password = "091537"
            };
            SqlConnection connection = new SqlConnection(connectionStringBuilder.ToString());
            Console.WriteLine("Connection opening....");
            connection.Open();
            Console.WriteLine("Connection Opened.");

            string query = @"
UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] =@Blog_Content
 WHERE Blog_Id=@Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);//to prevent sql injection

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //old style
            int result = command.ExecuteNonQuery();
            string message = result > 0 ? "Updating Successful." : "Updating Failed";
            connection.Close();
            Console.WriteLine(message);

        }
        private void Delete(int id)
        {

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "WaiPhyoMaung_A",
                UserID = "sa",
                Password = "091537"
            };
            SqlConnection connection = new SqlConnection(connectionStringBuilder.ToString());
            Console.WriteLine("Connection opening....");
            connection.Open();
            Console.WriteLine("Connection Opened.");

            string query = @"
DELETE FROM [dbo].[Tbl_Blog]
      WHERE Blog_Id=@Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);

            int result = command.ExecuteNonQuery();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";
            connection.Close();
            Console.WriteLine(message);

        }
    }
}