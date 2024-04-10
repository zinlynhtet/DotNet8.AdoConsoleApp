namespace DotNet8.AdoConsoleApp.AdoDotNetExamples;

public class Queries
{
    public static string GetQuery { get; } = @"Select * from Tbl_Blogs";
    public static string GetbyIdQuery { get; } = @"Select * from Tbl_Blogs where BlogId = @BlogId";
    public static string CreateQuery { get; } = @"INSERT INTO [dbo].[Tbl_Blogs]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,
		   @BlogAuthor,
		   @BlogContent)";

    public static string UpdateQuery { get; } = @"UPDATE [dbo].[Tbl_Blogs]
     SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
     WHERE BlogId = @BlogId";
    public static string DeleteQuery { get; } = @"DELETE FROM [dbo].[Tbl_Blogs]
    WHERE BlogId = @BlogId";

    public static void ConsoleWrite(DataRow row)
    {
        Console.WriteLine(row["BlogId"]);
        Console.WriteLine(row["BlogTitle"]);
        Console.WriteLine(row["BlogAuthor"]);
        Console.WriteLine(row["BlogContent"]);
    }
    public static SqlCommand command(string title, string author, string content, SqlConnection connection, string query)
    {
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);
        return cmd;
    }

}
