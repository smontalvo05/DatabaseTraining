using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.IO;
using System.Data;

namespace DatabaseTraining
{
    class Program
    {
        MySqlConnection _con=null;

        static void Main(string[] args)
        {
            Program instance = new Program();

            instance._con = new MySqlConnection();
            instance.Connect();

            DataTable data = instance.QueryDB("SELECT make, model, year FROM vehicle LIMIT 30");
            DataRowCollection rows = data.Rows;

            foreach (DataRow row in rows)
            {
                Console.WriteLine($"Make: {row["make"].ToString()}");
                Console.WriteLine($"Model: {row ["model"].ToString()}");
                Console.WriteLine($"Year: {row["year"].ToString()}");
                Console.WriteLine();
            }

            Console.WriteLine("\nPress a Key to Continue.");
            Console.ReadKey();
        }

        void Connect()
        {
            BuildConString();

            try
            {
                _con.Open();
                Console.WriteLine("Connection Succesful!");

            }
            catch (MySqlException e)
            {
                string msg = "";

                switch (e.Number)
                {
                    case 0:
                        {

                            msg = e.ToString();
                        }
                        break;
                    case 1042:
                        {

                            msg = "Cant resolve host address. \n " + _con.ConnectionString;
                        }
                        break;
                    case 1045:
                        {

                            msg = "Invalid Username and Password";
                        }
                        break;
                    default:
                        {

                            msg = e.ToString();

                        }

                        break;

                }
                Console.WriteLine(msg);
            }
        }

        void BuildConString()
        {
            string ip = "";

            using (StreamReader sr = new StreamReader("c:/VFW/connect.txt"))
            {
                ip = sr.ReadLine();
            }

            string conString = $"Server= {ip};";
            conString += "uid=dbsAdmin;";
            conString += "pwd=password;";
            conString += "database=exampleDatabase;";
            conString += "port=8889;";
           

            _con.ConnectionString = conString;
        }

        DataTable QueryDB (string query)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(query,_con);
            DataTable Data = new DataTable();

            adapter.SelectCommand.CommandType = CommandType.Text;
            adapter.Fill(Data);

            return Data;
            
        }
    }
}
