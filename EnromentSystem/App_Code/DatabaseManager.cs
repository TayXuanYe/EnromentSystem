using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DatabaseManager
/// </summary>
public static class DatabaseManager
{
    private static String serverAddress = null;
    private static String databaseName = null;
    public static SqlConnection connection = null;

    public static void ConnectDatabase()
    {
        string connectionString =
            "Server=" + serverAddress + ";"
            + "Database=" + databaseName + ";"
            + "Integrated Security=True;";

        SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine("SQL error occurred: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}