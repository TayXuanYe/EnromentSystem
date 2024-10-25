using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

/// <summary>
/// DatabaseManager is a class used to manage database operations and provides a series of methods for interacting with the database. 
/// This class is designed to simplify common operations such as connecting to the database, data insertion, and data reading. 
/// Its main functions include setting the database address and name, connecting to the database, inserting data, and reading data.
/// </summary>

public static class DatabaseManager
{
    private static String serverName = "DESKTOP-GCII6U9\\SQLEXPRESS";
    private static String databaseName = "EnrolmentSystemDatabase";
    public static SqlConnection connection = null;

    public static void ConnectDatabase()
    {
        string connectionString =
            "Server=" + serverName + ";"
            + "Database=" + databaseName + ";"
            + "Integrated Security=True;";

        connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine("SQL error occurred - connect database: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred - connect database: " + ex.Message);
        }
    }

    public static DataSet getRecord(string table, List<string> selectColumns)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
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
            Console.WriteLine("SQL error occurred - get record: " + sqlEx.Message);
            Debug.WriteLine("SQL error occurred - get record: " + sqlEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred - get record: " + ex.Message);
            Debug.WriteLine("SQL error occurred - get record: " + ex.Message);
            return null;
        }
    }

    public static DataSet getRecord(string table, List<string> selectColumns, string condition)
    {
        if (connection == null)
        {
                ConnectDatabase();
        }

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
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
            Console.WriteLine("SQL error occurred - get record: " + sqlEx.Message);
            Debug.WriteLine("SQL error occurred - get record: " + sqlEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred - get record: " + ex.Message);
            Debug.WriteLine("SQL error occurred - get record: " + ex.Message);
            return null;
        }
    }
    public static bool UpdateData(String table, List<string> columnNames, List<object> values, String condition)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        //check is the amount of cloumn names and values have a same amount
        if (columnNames.Count != values.Count)
        {
            throw new ArgumentException("Column names and values must have the same length.");
        }

        //create the query
        List<String> setClauses = new List<String>();
        for (int i = 0; i < columnNames.Count; i++)
        {
            // generarte 'columnName = value' format
            setClauses.Add($"{columnNames[i]} = @{columnNames[i]}");
        }
        String setClause = String.Join(", ", setClauses);
        String query = $"UPDATE {table} SET {setClause} {condition}";

        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                //add value into SQL command
                for (int i = 0; i < columnNames.Count; i++)
                {
                    command.Parameters.AddWithValue($"@{columnNames[i]}", values[i]);
                }

                int rowsAffected = command.ExecuteNonQuery();

                // Check if rows were inserted successfully
                return rowsAffected > 0;
            }
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine("SQL error occurred - update record: " + sqlEx.Message);
            Debug.WriteLine("SQL error occurred - update record: " + sqlEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred - update record: " + ex.Message);
            Debug.WriteLine("An error occurred - update record: " + ex.Message);
            return false;
        }
    }
}

