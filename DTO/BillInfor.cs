using QuanLyTiemThuoc.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DTO
{
    public class BillInfor
    {
        private int id;

        private int billID;

        private int medicineID;

        private int count;

        public BillInfor(int id, int billID, int medicineID, int count)
        {
            this.Id = id;
            this.BillID = billID;
            this.MedicineID = medicineID;
            this.Count = count;

        }

        public BillInfor(DataRow row)
        {
            this.Id = (int)row["id"];
            this.BillID = (int)row["idBill"];
            this.MedicineID = (int)row["idMedicine"];
            this.Count = (int)row["count"];
        }


        public int Id { get => id; set => id = value; }
        public int BillID { get => billID; set => billID = value; }
        public int MedicineID { get => medicineID; set => medicineID = value; }
        public int Count { get => count; set => count = value; }
    }
}
