namespace DotNet8.AdoConsoleApp.AdoDotNetExamples;

public class AdoDotNetExample
{
    private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;
    public AdoDotNetExample()
    {
        sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
        sqlConnectionStringBuilder.DataSource = ".";
        sqlConnectionStringBuilder.InitialCatalog = "TestDb";
        sqlConnectionStringBuilder.UserID = "sa";
        sqlConnectionStringBuilder.Password = "sasa@123";
        sqlConnectionStringBuilder.TrustServerCertificate = true;
    }
    public void Run()
    {
        //Read();
        GetById(2);
        Delete(6);
        Create("test", "author", "content");
        Update(6, "6", "author", "content");
    }

    public void Read()
    {
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        Console.WriteLine("Connection opening...");
        connection.Open();
        Console.WriteLine("Connection opened...");

        string query = Queries.GetQuery;
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dataAdapter.Fill(dt);

        Console.WriteLine("Connection closing...");
        connection.Close();
        Console.WriteLine("Connection closed...");
        foreach (DataRow row in dt.Rows)
        {
            Queries.ConsoleWrite(row);
        }
    }

    public void GetById(int id)
    {
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

        connection.Open();

        string query = Queries.GetbyIdQuery;
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dataAdapter.Fill(dt);

        connection.Close();
        if (dt.Rows.Count is 0)
        {
            Console.WriteLine("No data found");
            return;
        }
        DataRow row = dt.Rows[0];
        Queries.ConsoleWrite(row);
    }

    public void Create(string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        string query = Queries.CreateQuery;
        SqlCommand cmd = Queries.command(title, author, content, connection, query);
        int result = cmd.ExecuteNonQuery();
        connection.Close();
        string message = result > 0 ? "Saving Successful" : "Saving failed.";
        Console.WriteLine(message);
    }

    public void Update(int id, string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = Queries.UpdateQuery;
        SqlCommand cmd = Queries.command(title, author, content, connection, query);
        cmd.Parameters.AddWithValue("@BlogId", id);
        int result = cmd.ExecuteNonQuery();
        connection.Close();
        string message = result > 0 ? "Updating Successful." : "Updating Failed";
        Console.WriteLine(message);
    }

    public void Delete(int id)
    {
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = Queries.DeleteQuery;
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);

        int result = cmd.ExecuteNonQuery();
        connection.Close();
        string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
        Console.WriteLine(message);
    }
}
