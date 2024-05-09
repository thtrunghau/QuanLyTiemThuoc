using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class SlaverDAO
    {
        private static SlaverDAO instance;

        public static int SlaverWidth = 90;
        public static int SlaverHeight = 90;

        public static SlaverDAO Instance
        {
            get { if (instance == null) instance = new SlaverDAO(); return instance; }
            private set { instance = value; }
        }

        private SlaverDAO() { }

        public List<Slaver> LoadSlaverList()
        {
            List<Slaver> listSlaver = new List<Slaver>();

            DataTable data = DataProvider.Instance.ExcuteQuery("USP_GetSlaverList");

            foreach (DataRow row in data.Rows)
            {
                Slaver slaver = new Slaver(row);
                listSlaver.Add(slaver);
            }
            return listSlaver;
        }

        public bool InsertSlaver(string name)
        {
            string query = string.Format("INSERT INTO MedicineSlaver (name) VALUES ( N'{0}')", name);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool UpdateSlaver(string name, int idSlaver)
        {
            string query = string.Format("update MedicineSlaver set name = N'{0}' where id = {1}", name, idSlaver);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool DeleteSlaver(int idSlaver)
        {
            string query = "exec USP_DeleteSlaver @idSlaver";

            int result = DataProvider.Instance.ExcuteNoneQuery(query, new object[] { idSlaver });

            return result > 0;
        }

        public List<Slaver> SearchSlaverrByName(string name)
        {
            List<Slaver> list = new List<Slaver>();

            string query = string.Format("exec USP_SearchSlaver N'%{0}%'", name);

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Slaver slvaver = new Slaver(row);
                list.Add(slvaver);
            }
            return list;
        }
    }
}
