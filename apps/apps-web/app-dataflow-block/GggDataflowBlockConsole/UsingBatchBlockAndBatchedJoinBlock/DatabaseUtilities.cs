using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Threading;

namespace GggDataflowBlockConsole.UsingBatchBlockAndBatchedJoinBlock
{
    class DatabaseUtilities
    {
        // Adds new employee records to the database.
        public static void InsertEmployees(Employee[] employees, string connectionString, string caller)
        {
            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                try
                {
                    // Create the SQL command.
                    SqlCeCommand command = new SqlCeCommand(
                        "INSERT INTO Employees ([Last Name], [First Name])" +
                        "VALUES (@lastName, @firstName)",
                        connection);

                    connection.Open();
                    foreach (Employee employee in employees)
                    {
                        // Set parameters.
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@lastName", employee.LastName);
                        command.Parameters.AddWithValue("@firstName", employee.FirstName);

                        // Execute the command.
                        command.ExecuteNonQuery();
                        Console.WriteLine("DatabaseUtilities.InsertEmployees: " + employee.FirstName);
                        Console.WriteLine("Caller: " + caller);
                        Console.WriteLine("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                        Console.WriteLine("");
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // Retrieves the number of entries in the Employees table in 
        // the Northwind database.

        public static int GetEmployeeCount(string connectionString)
        {
            // UpgradeDatabasewithCaseSensitive(@"C:\temp\Northwind.sdf");
            int result = 0;
            using (SqlCeConnection sqlConnection = new SqlCeConnection(connectionString))
            {
                SqlCeCommand sqlCommand = new SqlCeCommand("SELECT COUNT(*) FROM Employees", sqlConnection);

                sqlConnection.Open();
                try
                {
                    result = (int)sqlCommand.ExecuteScalar();
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return result;
        }

        // Retrieves the ID of the first employee that has the provided name.

        public static int GetEmployeeID(string lastName, string firstName, string connectionString)
        {
            using (SqlCeConnection connection =
                new SqlCeConnection(connectionString))
            {
                SqlCeCommand command = new SqlCeCommand(
                    String.Format(
                        "SELECT [Employee ID] FROM Employees " +
                        "WHERE [Last Name] = '{0}' AND [First Name] = '{1}'",
                        lastName, firstName),
                    connection);

                connection.Open();
                try
                {
                    return (int)command.ExecuteScalar();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Demonstrates how to upgrade a database with case sensitivity.
        /// </summary>
        public static void UpgradeDatabasewithCaseSensitive(string filePath)
        {
            // <Snippet2>
            // Default case-insentive connection string.
            // Note that Northwind.sdf is an old 3.1 version database.

            string connStringCI = $"Data Source= {filePath}; LCID= 1033";

            // Set "Case Sensitive" to true to change the collation from CI to CS.
            string connStringCS = $"Data Source= {filePath}; LCID= 1033; Case Sensitive=true";

            SqlCeEngine engine = new SqlCeEngine(connStringCI);

            // The collation of the database will be case sensitive because of 
            // the new connection string used by the Upgrade method.                
            engine.Upgrade(connStringCS);

            SqlCeConnection conn = null;
            conn = new SqlCeConnection(connStringCI);
            conn.Open();

            //Retrieve the connection string information - notice the 'Case Sensitive' value.
            List<KeyValuePair<string, string>> dbinfo = conn.GetDatabaseInfo();

            Console.WriteLine("\nGetDatabaseInfo() results:");

            foreach (KeyValuePair<string, string> kvp in dbinfo)
            {
                Console.WriteLine(kvp);
            }

            // </Snippet2>
        }
    }
}
