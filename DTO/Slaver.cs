using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DTO
{
    public class Slaver
    {
        private int iD;

        private string name;

        private string status;



        public Slaver(int iD, string name, string status)
        {
            this.iD = iD;
            this.name = name;
            this.status = status;
        }

        public Slaver(DataRow row)
        {
            this.iD = (int)row["id"];
            this.name = row["name"].ToString();
            this.status = row["status"].ToString();
        }
        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
    }
}
