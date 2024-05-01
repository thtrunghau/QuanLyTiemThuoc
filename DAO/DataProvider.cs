using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }
        private string connectionString = "Data Source=TrungHau\\SQLEXPRESS;Initial Catalog=QuanLyTiemThuoc;Integrated Security=True";

        public DataProvider() { }

        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            { // sau khi khoi lenh ket thuc thi du lieu trong () se tu giai phong
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                //command.Parameters.AddWithValue("@userName", userName);
                if (parameter != null)
                {
                    string[] lisParams = query.Split(' ');
                    int i = 0;
                    foreach (string item in lisParams)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExcuteNoneQuery(string query, object[] parameter = null)
        {
            //DataTable data = new DataTable();
            int rows = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            { // sau khi khoi lenh ket thuc thi du lieu trong () se tu giai phong
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                //command.Parameters.AddWithValue("@userName", userName);
                if (parameter != null)
                {
                    string[] lisParams = query.Split(' ');
                    int i = 0;
                    foreach (string item in lisParams)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                //adapter.Fill(data);
                rows = command.ExecuteNonQuery();

                connection.Close();
            }

            return rows;
        }

        public object ExcuteScalar(string query, object[] parameter = null)
        {
            //DataTable data = new DataTable();
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            { // sau khi khoi lenh ket thuc thi du lieu trong () se tu giai phong
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                //command.Parameters.AddWithValue("@userName", userName);
                if (parameter != null)
                {
                    string[] lisParams = query.Split(' ');
                    int i = 0;
                    foreach (string item in lisParams)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                //adapter.Fill(data);
                data = command.ExecuteScalar();
                connection.Close();
            }

            return data;
        }
    }
}
