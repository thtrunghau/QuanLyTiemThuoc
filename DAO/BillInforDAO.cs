using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class BillInforDAO
    {
        private static BillInforDAO instance;

        public static BillInforDAO Instance
        {
            get { if (instance == null) instance = new BillInforDAO(); return instance; }
            private set { instance = value; }
        }

        private BillInforDAO() { }

        public List<BillInfor> GetListBillInfor(int id)
        {
            List<BillInfor> listBillInfor = new List<BillInfor>();

            DataTable data = DataProvider.Instance.ExcuteQuery("select * from BillInfor where idBill = " + id);

            foreach (DataRow row in data.Rows)
            {
                BillInfor infor = new BillInfor(row);
                listBillInfor.Add(infor);
            }

            return listBillInfor;
        }

        public void InserBillInfor(int idBill, int idMedicine, int count)
        {
            DataProvider.Instance.ExcuteNoneQuery("exec USP_InserBillInfo @idBill , @idMedicine , @count", new object[] { idBill, idMedicine, count });
        }

        public void DeleteBillInforByMedicineID(int idMedicine)
        {
            DataProvider.Instance.ExcuteQuery("delete BillInfor where idMedicine = " + idMedicine);
        }
    }
}
