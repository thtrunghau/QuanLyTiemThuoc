using QuanLyTiemThuoc.DAO;
using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTiemThuoc.GUI
{
    public partial class fSlaverManager : Form
    {
        public fSlaverManager()
        {
            InitializeComponent();
            LoadSlaver();
        }

        #region Method
        void LoadSlaver()
        {
            flpslaver.Controls.Clear();
            List<Slaver> slaverList = SlaverDAO.Instance.LoadSlaverList();

            foreach (Slaver table in slaverList)
            {
                Button btn = new Button() { Width = SlaverDAO.SlaverWidth, Height = SlaverDAO.SlaverHeight };
                btn.Text = table.Name + Environment.NewLine + table.Status;
                btn.Click += Btn_Click;
                btn.Tag = table;

                switch (table.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Green; break;
                    default: btn.BackColor = Color.Yellow; break;
                }

                flpslaver.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<QuanLyQuanCafe.DTO.Menu> listBillInfor = MenuDAO.Instance.GetListMenuByTableID(id);
            float totalPrice = 0;
            foreach (QuanLyQuanCafe.DTO.Menu item in listBillInfor)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            txbTotalPrice.Text = totalPrice.ToString("c", cultureInfo);

        }
        #endregion

        #region events

        private void Btn_Click(object sender, EventArgs e)
        {
            int slaverId = ((sender as Button).Tag as Slaver).ID;
            lsvBill.Tag = (sender as Button).Tag;
            //ShowBill(tableId);
        }
        private void dangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thongTinCaNhanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountFrofile f = new fAccountFrofile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
