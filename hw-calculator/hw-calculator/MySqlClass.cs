using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace hw_calculator
{
    class MySqlClass
    {
        private static string connectionStr = "server=localhost;user id=root;password=;database=csharp_hw";
        //private string query_con;

        public MySqlClass()
        {

        }
        /*public MySqlClass(string query_con)
        { 
            this.query_con = query_con;
        }*/
        private static MySqlConnection GetConn(string connStr)
        {
            MySqlConnection connection = new MySqlConnection(connStr);
            return connection;
        }
        public void DBConnection1(string que)
        {
            MySqlConnection connection = new MySqlConnection(connectionStr);   //與DBConnection2寫法不同,但意思一樣
            MySqlCommand DBcommand = new MySqlCommand(que, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = DBcommand.ExecuteReader();

                if(reader.HasRows)
                {
                    Console.WriteLine("資料庫連線 有東西");
                    while(reader.Read())
                    {
                        //list.Add(new record(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetInt32(4)));
                        //Console.WriteLine(reader.GetString(0)+" - "+ reader.GetString(1)+" - "+reader.GetString(2) + " - "+reader.GetString(3) + " - "+reader.GetString(4) );
                    }
                }
                else
                {
                    Console.WriteLine("資料庫連線 沒東西");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public static List< CustomRecord> DBConnection2(string que)
        {
            MySqlConnection connection = GetConn(connectionStr);
            MySqlCommand DBcommand = new MySqlCommand(que, connection);
            List<CustomRecord> resultList = new List<CustomRecord>();

            try
            {
                connection.Open();
                MySqlDataReader reader = DBcommand.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("資料庫連線 有東西");
                    while (reader.Read())
                    {
                        CustomRecord cr = new CustomRecord(
                            reader.GetString("expression"),
                            reader.GetString("preorder"),
                            reader.GetString("postorder"),
                            reader.GetDouble("deci"),
                            reader.GetInt32("bin"));
                                            
                        resultList.Add(cr);
                    }
                }
                else
                {
                    Console.WriteLine("資料庫連線 沒東西");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return resultList;
        }
        public void DBInsert(string que)
        {
            MySqlConnection connection = new MySqlConnection(connectionStr);
            connection.Open();
            MySqlCommand DBcommand = new MySqlCommand(que, connection);

            if (DBcommand.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Insert");
                Console.WriteLine("insert");
            }
            else
            {
                Console.WriteLine("no insert");
            }

            connection.Close();
        }
        public void DBDelete(string que)
        {
            MySqlConnection connection = new MySqlConnection(connectionStr);
            connection.Open();
            MySqlCommand DBcommand = new MySqlCommand(que, connection);

            if (DBcommand.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("delete");
            }
            else
            {
                Console.WriteLine("no delete");
            }
            connection.Close();
        }
        public int DBCheck(string que, string ex)
        {
            MySqlConnection connection = GetConn(connectionStr);
            MySqlCommand DBcommand = new MySqlCommand(que, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = DBcommand.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("資料庫連線 有東西");
                    while (reader.Read())
                    {
                        if(reader.GetString(0)==ex)
                        {
                            MessageBox.Show("算式已重複");
                            return 1;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("資料庫連線 沒東西");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return 0;
        }
    }
}
