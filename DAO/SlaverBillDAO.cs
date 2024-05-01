using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class SlaverBillDAO
    {
        private static SlaverBillDAO instance;

        public static SlaverBillDAO Instance
        {
            get { if (instance == null) instance = new SlaverBillDAO(); return instance; }
            private set { instance = value; }
        }

        private SlaverBillDAO() { }

        public List<SlaverBill> GetListMenuByTableID(int id)
        {
            List<SlaverBill> listSlaverBill = new List<SlaverBill>();

            string query = "select f.name, bi.count, f.price, f.price*bi.count as totalPrice from Bill as b, BillInfor as bi, Food as f\r\nwhere bi.idBill = b.id and bi.idFood= f.id and b.status = 0 and b.idTable =" + id;

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                SlaverBill slaverBill = new SlaverBill(item);
                listSlaverBill.Add(slaverBill);
            }

            return listSlaverBill;
        }
    }
}
