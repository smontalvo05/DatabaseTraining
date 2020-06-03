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

            Console.WriteLine("Press a Key to Continue.");     
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
            conString += "uid=dsbAdmin;";
            conString += "pwd=password;";
            conString += "database=exampleDatabase;";
            conString += "port=3306;";

            _con.ConnectionString = conString;
        }
    }
}
