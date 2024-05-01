using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class MedicineDAO
    {
        private static MedicineDAO instance;

        public static MedicineDAO Instance
        {
            get { if (instance == null) instance = new MedicineDAO(); return instance; }
            private set { instance = value; }
        }

        private MedicineDAO() { }

        public List<Medicine> GetMedicineByCategoryID(int id)
        {
            List<Medicine> list = new List<Medicine>();

            string query = "select *from Medicine where idCategory =" + id;

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Medicine medicine = new Medicine(row);
                list.Add(medicine);
            }
            return list;
        }

        public List<Medicine> GetListMedicine()
        {
            List<Medicine> list = new List<Medicine>();

            string query = "select * from Medicine";

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Medicine medicine = new Medicine(row);
                list.Add(medicine);
            }
            return list;
        }

        public bool InsertMedicine(string name, int idCategory, float price)
        {
            string query = string.Format("insert into Medicine (name, idCategory, price) values (N'{0}', {1}, {2})", name, idCategory, price);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool UpdataMedicine(int idFood, string name, int idCategory, float price)
        {
            string query = string.Format("update Medicine set name = N'{0}', idCategory = {1}, price = {2} where id = {3}", name, idCategory, price, idFood);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool DeleteMedicine(int idFood)
        {
            BillInforDAO.Instance.DeleteBillInforByMedicineID(idFood);

            string query = string.Format("delete Food where id = {0}", idFood);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public List<Medicine> SearchMedicineByName(string name)
        {
            List<Medicine> list = new List<Medicine>();

            string query = string.Format("SELECT * FROM Medicine WHERE name LIKE N'%{0}%'", name);

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Medicine medicine = new Medicine(row);
                list.Add(medicine);
            }
            return list;
        }
    }
}
