using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks.Dataflow;

namespace GggDataflowBlockConsole.UsingBatchBlockAndBatchedJoinBlock
{
    class BatchBlockAndBatchedJoinBlockSample
    {
        // The number of employees to add to the database.
        // TODO: Change this value to experiment with different numbers of 
        // employees to insert into the database.
        static readonly int insertCount = 256;

        // The size of a single batch of employees to add to the database.
        // TODO: Change this value to experiment with different batch sizes.
        static readonly int insertBatchSize = 96;

        // The source database file.
        // TODO: Change this value if Northwind.sdf is at a different location
        // on your computer.
        static readonly string sourceDatabase =
            @"C:\Temp\Northwind.sdf";

        // TODO: Change this value if you require a different temporary location.
        static readonly string scratchDatabase =
            @"C:\Temp\Northwind2.sdf";

        // Adds new employee records to the database.
        static void InsertEmployees(Employee[] employees, string connectionString)
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
                    }
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

        // Retrieves the number of entries in the Employees table in 
        // the Northwind database.
        static int GetEmployeeCount(string connectionString)
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
        static int GetEmployeeID(string lastName, string firstName, string connectionString)
        {
            using (SqlCeConnection connection =
                new SqlCeConnection(connectionString))
            {
                SqlCeCommand command = new SqlCeCommand(
                    string.Format(
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

        // Posts random Employee data to the provided target block.
        static void PostRandomEmployees(ITargetBlock<Employee> target, int count)
        {
            Console.WriteLine("Adding {0} entries to Employee table...", count);

            for (int i = 0; i < count; i++)
            {
                target.Post(Employee.Random());
            }
        }

        // Adds random employee data to the database by using dataflow.
        static void AddEmployees(string connectionString, int count)
        {
            // Create an ActionBlock<Employee> object that adds a single
            // employee entry to the database.
            ActionBlock<Employee> insertEmployee = new ActionBlock<Employee>(e =>
            {
                InsertEmployees(new[] { e }, connectionString);
            });

            // Post several random Employee objects to the dataflow block.
            PostRandomEmployees(insertEmployee, count);

            // Set the dataflow block to the completed state and wait for 
            // all insert operations to complete.
            insertEmployee.Complete();
            insertEmployee.Completion.Wait();
        }

        // Adds random employee data to the database by using dataflow.
        // This method is similar to AddEmployees except that it uses batching
        // to add multiple employees to the database at a time.
        static void AddEmployeesBatched(string connectionString, int batchSize, int count)
        {
            // Create a BatchBlock<Employee> that holds several Employee objects and
            // then propagates them out as an array.
            BatchBlock<Employee> batchEmployees = new BatchBlock<Employee>(batchSize);

            // Create an ActionBlock<Employee[]> object that adds multiple
            // employee entries to the database.
            ActionBlock<Employee[]> insertEmployees = new ActionBlock<Employee[]>(a =>
            {
                InsertEmployees(a, connectionString);
            });

            // Link the batch block to the action block.
            batchEmployees.LinkTo(insertEmployees);

            // When the batch block completes, set the action block also to complete.
            batchEmployees.Completion.ContinueWith(obj =>
            {
                insertEmployees.Complete();
            });

            // Post several random Employee objects to the batch block.
            PostRandomEmployees(batchEmployees, count);

            // Set the batch block to the completed state and wait for 
            // all insert operations to complete.
            batchEmployees.Complete();
            insertEmployees.Completion.Wait();
        }

        // Displays information about several random employees to the console.
        static void GetRandomEmployees(string connectionString, int batchSize, int count)
        {
            // Create a BatchedJoinBlock<Employee, Exception> object that holds
            // both employee and exception data.
            BatchedJoinBlock<Employee, Exception> selectEmployees = new BatchedJoinBlock<Employee, Exception>(batchSize);

            // Holds the total number of exceptions that occurred.
            int totalErrors = 0;

            // Create an action block that prints employee and error information
            // to the console.
            ActionBlock<Tuple<IList<Employee>, IList<Exception>>> printEmployees =
               new ActionBlock<Tuple<IList<Employee>, IList<Exception>>>(data =>
               {
                   // Print information about the employees in this batch.
                   Console.WriteLine("Received a batch...");
                   foreach (Employee e in data.Item1)
                   {
                       Console.WriteLine("Last={0} First={1} ID={2}",
                 e.FirstName, e.LastName, e.EmployeeID);
                   }

                   // Print the error count for this batch.
                   Console.WriteLine("There were {0} errors in this batch...",
                      data.Item2.Count);

                   // Update total error count.
                   totalErrors += data.Item2.Count;
               });

            // Link the batched join block to the action block.
            selectEmployees.LinkTo(printEmployees);

            // When the batched join block completes, set the action block also to complete.
            selectEmployees.Completion.ContinueWith(delegate { printEmployees.Complete(); });

            // Try to retrieve the ID for several random employees.
            Console.WriteLine("Selecting random entries from Employees table...");
            for (int i = 0; i < count; i++)
            {
                try
                {
                    // Create a random employee.
                    Employee e = Employee.Random();

                    // Try to retrieve the ID for the employee from the database.
                    e.EmployeeID = GetEmployeeID(e.LastName, e.FirstName, connectionString);

                    // Post the Employee object to the Employee target of 
                    // the batched join block.
                    selectEmployees.Target1.Post(e);
                }
                catch (NullReferenceException e)
                {
                    // GetEmployeeID throws NullReferenceException when there is 
                    // no such employee with the given name. When this happens,
                    // post the Exception object to the Exception target of
                    // the batched join block.
                    selectEmployees.Target2.Post(e);
                }
            }

            // Set the batched join block to the completed state and wait for 
            // all retrieval operations to complete.
            selectEmployees.Complete();
            printEmployees.Completion.Wait();

            // Print the total error count.
            Console.WriteLine("Finished. There were {0} total errors.", totalErrors);
        }

        public static void Run()
        {
            // Create a connection string for accessing the database.
            // The connection string refers to the temporary database location.
            string connectionString = string.Format(@"Data Source={0}",
               scratchDatabase);

            // Create a Stopwatch object to time database insert operations.
            Stopwatch stopwatch = new Stopwatch();

            // Start with a clean database file by copying the source database to 
            // the temporary location.
            File.Copy(sourceDatabase, scratchDatabase, true);

            // Demonstrate multiple insert operations without batching.
            Console.WriteLine("Demonstrating non-batched database insert operations...");
            Console.WriteLine("Original size of Employee table: {0}.",
               GetEmployeeCount(connectionString));
            stopwatch.Start();
            AddEmployees(connectionString, insertCount);
            stopwatch.Stop();
            Console.WriteLine("New size of Employee table: {0}; elapsed insert time: {1} ms.",
               GetEmployeeCount(connectionString), stopwatch.ElapsedMilliseconds);

            Console.WriteLine();

            // Start again with a clean database file.
            File.Copy(sourceDatabase, scratchDatabase, true);

            // Demonstrate multiple insert operations, this time with batching.
            Console.WriteLine("Demonstrating batched database insert operations...");
            Console.WriteLine("Original size of Employee table: {0}.",
               GetEmployeeCount(connectionString));
            stopwatch.Restart();
            AddEmployeesBatched(connectionString, insertBatchSize, insertCount);
            stopwatch.Stop();
            Console.WriteLine("New size of Employee table: {0}; elapsed insert time: {1} ms.",
               GetEmployeeCount(connectionString), stopwatch.ElapsedMilliseconds);

            Console.WriteLine();

            // Start again with a clean database file.
            File.Copy(sourceDatabase, scratchDatabase, true);

            // Demonstrate multiple retrieval operations with error reporting.
            Console.WriteLine("Demonstrating batched join database select operations...");
            // Add a small number of employees to the database.
            AddEmployeesBatched(connectionString, insertBatchSize, 16);
            // Query for random employees.
            GetRandomEmployees(connectionString, insertBatchSize, 10);
        }
    }


}
