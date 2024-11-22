using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// DatabaseManager is a class used to manage database operations and provides a series of methods for interacting with the database. 
/// This class is designed to simplify common operations such as connecting to the database, data insertion, and data reading. 
/// Its main functions include setting the database address and name, connecting to the database, inserting data, and reading data.
/// </summary>

public static class DatabaseManager
{
    //private static string serverName = "DESKTOP-GCII6U9\\SQLEXPRESS";// XY laptop
    //private static string serverName = "DESKTOP-EMOGFRG\\SQLEXPRESS";// XY desktop
    //private static string serverName = "LAPTOP-25QCMRDF\\SQLEXPRESS";// Mizana Laptop
    private static string serverName = "云烟\\SQLEXPRESS";// Cai Yi Laptop
    private static string databaseName = "EnrolmentSystemDatabase";
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

    public static SqlConnection GetConnection()
    {
        string connectionString =
            "Server=" + serverName + ";"
            + "Database=" + databaseName + ";"
            + "Integrated Security=True;";

        return new SqlConnection(connectionString);
    } 

    public static DataSet GetRecord(string table, List<string> selectColumns)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
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

    public static DataSet GetRecord(string table, List<string> selectColumns, string condition)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
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
            Debug.WriteLine("An error occurred - get record: " + ex.Message);
            return null;
        }
    }

    public static DataSet GetDistinctRecord(string table, List<string> selectColumns)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        //create query
        String columns = String.Join(", ", selectColumns);
        String query = $"SELECT DISTINCT {columns} FROM {table}";

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
            Console.WriteLine("SQL error occurred - get distinct record: " + sqlEx.Message);
            Debug.WriteLine("SQL error occurred - get distinct record: " + sqlEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred - get distinct record: " + ex.Message);
            Debug.WriteLine("An error occurred - get distinct record: " + ex.Message);
            return null;
        }
    }

    public static DataSet GetDistinctRecord(string table, List<string> selectColumns, string condition)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        //create query
        String columns = String.Join(", ", selectColumns);
        String query = $"SELECT DISTINCT {columns} FROM {table} {condition}";

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
            Console.WriteLine("SQL error occurred - get distinct record: " + sqlEx.Message);
            Debug.WriteLine("SQL error occurred - get distinct record: " + sqlEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred - get distinct record: " + ex.Message);
            Debug.WriteLine("An error occurred - get distinct record: " + ex.Message);
            return null;
        }
    }
    
    public static int GetRecordCount(string table, string condition)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        //create query
        String query = $"SELECT * FROM {table} {condition}";

        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    if(dataSet != null)
                    {
                        return dataSet.Tables[0].Rows.Count;
                    }
                    return 0;
                }
            }
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine("SQL error occurred - get record count: " + sqlEx.Message);
            Debug.WriteLine("SQL error occurred - get record count: " + sqlEx.Message);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred - get record count: " + ex.Message);
            Debug.WriteLine("An error occurred - get record count: " + ex.Message);
            return 0;
        }
    }

    public static bool UpdateData(string table, List<string> columnNames, List<object> values, string condition)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        //check is the amount of cloumn names and values have a same amount
        if (columnNames.Count != values.Count)
        {
            throw new ArgumentException("Column names and values must have the same length.");
        }

        //create the query
        List<string> setClauses = new List<string>();
        for (int i = 0; i < columnNames.Count; i++)
        {
            // generarte 'columnName = value' format
            setClauses.Add($"{columnNames[i]} = @{columnNames[i]}");
        }
        string setClause = string.Join(", ", setClauses);
        string query = $"UPDATE {table} SET {setClause} {condition}";

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

    public static bool InsertData(string table, List<string> columnNames, List<object> values)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        //check is the amount of cloumn names and values have a same amount
        if (columnNames.Count != values.Count)
        {
            throw new ArgumentException("Column names and values must have the same length.");
        }

        //create the query
        String columns = String.Join(", ", columnNames);
        String parameters = String.Join(", ", values.Select((_, index) => $"@param{index}"));
        String query = $"INSERT INTO {table} ({columns}) VALUES ({parameters})";

        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                //add value into SQL command
                for (int i = 0; i < values.Count; i++)
                {
                    command.Parameters.AddWithValue($"@param{i}", values[i]);
                }

                int rowsAffected = command.ExecuteNonQuery();

                // Check if rows were inserted successfully
                return rowsAffected > 0;
            }
        }
        catch (SqlException sqlEx)
        {
            Debug.WriteLine("SQL error occurred - InsertData: " + sqlEx.Message);
            Console.WriteLine("SQL error occurred - InsertData: " + sqlEx.Message);
            return false;

        }
        catch (Exception ex)
        {
            Debug.WriteLine("An error occurred - InsertData: " + ex.Message);
            Console.WriteLine("An error occurred - InsertData: " + ex.Message);
            return false;
        }
    }

    public static bool DeleteData(string table, string condition)
    {
        if (connection == null)
        {
            ConnectDatabase();
        }

        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        string query = $"DELETE FROM {table} {condition}";
        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        catch (SqlException sqlEx)
        {
            Debug.WriteLine("SQL error occurred - Delete data: " + sqlEx.Message);
            Console.WriteLine("SQL error occurred - Delete data: " + sqlEx.Message);
            return false;

        }
        catch (Exception ex)
        {
            Debug.WriteLine("An error occurred - Delete data: " + ex.Message);
            Console.WriteLine("An error occurred - Delete data: " + ex.Message);
            return false;
        }
    }
}

