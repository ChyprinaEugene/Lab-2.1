using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Lab_2._1
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString = @"Data Source=sqlite.db";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int choose;
            do
            {
                Console.WriteLine("Select an option from the list below");
                Console.WriteLine("0 - Get all tables");
                Console.WriteLine("1 - Add row to the table");
                Console.WriteLine("2 - Close");

                choose = Int32.Parse(Console.ReadLine());

                switch (choose)
                {
                    case 0:
                        {
                            DisplayData(connection, "SELECT * from COMPANY", 0);
                            DisplayData(connection, "SELECT * from EMPLOYEE", 0);
                            DisplayData(connection, "SELECT * from CUSTOMER", 0);
                            DisplayData(connection, "SELECT * from INCOME", 0);
                            DisplayData(connection, "SELECT * from COSTS", 0);
                            break;
                        }
                    case 1:
                        {
                            try
                            {
                                int id;
                                Console.WriteLine("Write id:");
                                id = Int32.Parse(Console.ReadLine());
                                AddRow(connection, "COMPANY", new object[] { id, "Company 1", "Address 1", "10000" });
                                AddRow(connection, "EMPLOYEE", new object[] { id, "Employee 1", "accountant 1", "111" });
                                AddRow(connection, "CUSTOMER", new object[] { id, "Custumer 1", "888005553535" });
                                AddRow(connection, "INCOME", new object[] { id, "2020-01-01", "99" });
                                AddRow(connection, "COSTS", new object[] { id, "2020-01-01", "88" });
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("This id exists");
                            }
                            break;
                        }
                }
            } while (choose != 2);

            connection.Close();

        }

        static void DisplayData(SQLiteConnection connection, string com, int tableName)
        {
            SQLiteCommand command = new SQLiteCommand(com, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            Console.WriteLine("Data from " + reader.GetTableName(tableName).ToString() + ":");

            for (int i = 0; i < reader.FieldCount; i++)
            {
                string a = reader.GetName(i);
                Console.Write("{0, -18}", a);
            }

            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string a = reader[i].ToString();
                    Console.Write("{0, -18}", a);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        static void AddRow(SQLiteConnection connection, string tableName, object[] values)
        {
            string query = "INSERT INTO " + tableName + " VALUES (";
            for (int i = 0; i < values.Length; i++)
            {
                query += "@value" + i + ", ";
            }
            query = query.Remove(query.Length - 2) + ")";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            for (int i = 0; i < values.Length; i++)
            {
                command.Parameters.AddWithValue("@value" + i, values[i]);
            }

            command.ExecuteNonQuery();
        }
    }
}