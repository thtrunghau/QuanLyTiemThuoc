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

        public List<SlaverBill> GetListMenuBySlaverID(int id)
        {
            List<SlaverBill> listSlaverBill = new List<SlaverBill>();

            string query = "select m.name, bi.count, m.price, m.price*bi.count as totalPrice from Bill as b, BillInfor as bi, Medicine as m\r\nwhere bi.idBill = b.id and bi.idMedicine= m.id and b.status = 0 and b.idSlaver =" + id;

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
