using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DTO
{
    public class Bill
    {
        private int id;

        private DateTime? DataCheckIn;

        private DateTime? DataCheckOut;

        private int status;

        private int discount;

        public Bill(int id, DateTime? DataCheckIn, DateTime? DataCheckOut, int status, int discount = 0)
        {
            this.Id = id;
            this.DataCheckIn = DataCheckIn;
            this.DataCheckOut = DataCheckOut;
            this.Status = status;
            this.Discount = discount;
        }

        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.DataCheckIn = (DateTime?)row["DateCheckIn"];
            var dateCheckOutTemp = row["DateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
            {
                this.DataCheckOut = (DateTime?)dateCheckOutTemp;
            }

            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }

        public int Id { get => id; set => id = value; }
        public DateTime? DataCheckIn1 { get => DataCheckIn; set => DataCheckIn = value; }
        public DateTime? DataCheckOut1 { get => DataCheckOut; set => DataCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}
