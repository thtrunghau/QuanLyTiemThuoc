using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DTO
{
    public class SlaverBill
    {
        private string medicineName;

        private int count;

        private float price;

        private float totalPrice;

        public SlaverBill(string medicineName, int count, float price, float totalPrice)
        {
            this.medicineName = medicineName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;

        }

        public SlaverBill(DataRow row)
        {
            this.medicineName = row["name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble((row["price"]).ToString());
            this.TotalPrice = (float)Convert.ToDouble((row["totalPrice"]).ToString());

        }

        public string MedicineName { get => medicineName; set => medicineName = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
