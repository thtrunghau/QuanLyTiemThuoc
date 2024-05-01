using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set { instance = value; }
        }

        private BillDAO() { }

        public int GetUncheckBillByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("select * from Bill where idTable = " + id + " and status = 0");

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }

            return -1;
        }

        public void InserBill(int tableId)
        {
            DataProvider.Instance.ExcuteNoneQuery("exec UPS_InsertBill @idTable", new object[] { tableId });
        }

        public int GetMaxID()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("select max(id) from Bill");
            }
            catch
            {
                return 1;
            }
        }

        public void CheckOut(int idBill, int discount, float totalPrice)
        {
            string query = "update Bill set DateCheckOut = GetDate(), status = 1 " + ",discount = " + discount + ", totalPrice = " + totalPrice + "where id = " + idBill;
            DataProvider.Instance.ExcuteNoneQuery(query);
        }

        public DataTable GetListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExcuteQuery("exec USP_GetListBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }
    }
}
