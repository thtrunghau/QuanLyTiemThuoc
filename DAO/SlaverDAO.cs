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

        public void SwitchTable(int idTable1, int idTable2)
        {
            DataProvider.Instance.ExcuteQuery("exec USP_SwitchTable @idTable1 , @idTable2", new object[] { idTable1, idTable2 });
        }
    }
}
