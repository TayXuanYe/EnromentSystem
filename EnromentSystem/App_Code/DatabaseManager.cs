using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// DatabaseManager is a class used to manage database operations and provides a series of methods for interacting with the database. 
/// This class is designed to simplify common operations such as connecting to the database, data insertion, and data reading. 
/// Its main functions include setting the database address and name, connecting to the database, inserting data, and reading data.
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

    public static DataSet getRecord(string table, List<string> selectColumns)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        //create query
        String columns = String.Join(", ", selectColumns);
        String query = $"SELECT {columns} FROM {table}";

        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine("SQL error occurred: " + sqlEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    public static DataSet getRecord(string table, List<string> selectColumns, string condition)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        //create query
        String columns = String.Join(", ", selectColumns);
        String query = $"SELECT {columns} FROM {table} {condition}";

        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine("SQL error occurred: " + sqlEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }
}

