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
using static QuanLyTiemThuoc.GUI.fAccountFrofile;

namespace QuanLyTiemThuoc.GUI
{
    public partial class fSlaverManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }
        public fSlaverManager(Account loginAcc)
        {
            InitializeComponent();
            this.LoginAccount = loginAcc;
            LoadSlaver();
            LoadCategory();
        }

        #region Method
        void LoadCategory()
        {
            List<Category> list = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = list;
            cbCategory.DisplayMember = "name";
        }

        void LoadMedicineListByCategoryID(int id)
        {
            List<Medicine> list = MedicineDAO.Instance.GetMedicineByCategoryID(id);
            cbMedicine.DataSource = list;
            cbMedicine.DisplayMember = "name";
        }
        void LoadSlaver()
        {
            flpslaver.Controls.Clear();
            List<Slaver> slaverList = SlaverDAO.Instance.LoadSlaverList();

            foreach (Slaver slaver in slaverList)
            {
                Button btn = new Button() { Width = SlaverDAO.SlaverWidth, Height = SlaverDAO.SlaverHeight };
                btn.Text = slaver.Name + Environment.NewLine + slaver.Status;
                btn.Click += Btn_Click;
                btn.Tag = slaver;

                switch (slaver.Status)
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
            List<SlaverBill> listBillInfor = SlaverBillDAO.Instance.GetListMenuBySlaverID(id);
            double totalPrice = 0;
            foreach (SlaverBill item in listBillInfor)
            {
                ListViewItem lsvItem = new ListViewItem(item.MedicineName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            txbTotalPrice.Text = totalPrice.ToString("c", cultureInfo);

        }

        void ChangeAccount(int type)
        {
            ///phân quyền  
            adminToolStripMenuItem.Enabled = type == 1;
            ///thongTinToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
        #endregion

        #region events

        private void Btn_Click(object sender, EventArgs e)
        {
            int slaverId = ((sender as Button).Tag as Slaver).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(slaverId);
        }
        private void dangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thongTinCaNhanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountFrofile f = new fAccountFrofile(loginAccount);
            f.E_updateAccount += F_E_updateAccount;
            f.ShowDialog();
        }

        private void F_E_updateAccount(object sender, AccountEvent e)
        {
            ///thongTinToolStripMenuItem.Text += " (" + e.Acc.DisplayName + ")";
            thongTinCaNhanToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.currentAccount = LoginAccount;
            f.InsertMedicine += F_InsertMedicine;
            f.DeleteMedicine += F_DeleteMedicine;
            f.UpdateMedicine += F_UpdateMedicine;
            f.InsertCategory += F_InsertCategory;
            f.UpdateCategory += F_UpdateCategory;
            f.DeleteCategory += F_DeleteCategory;
            f.InsertSlaver += F_InsertSlaver;
            f.UpdateSlaver += F_UpdateSlaver;
            f.DeleteSlaver += F_DeleteSlaver;
            f.E_updateAccount += F_E_updateAccount1;
            f.ShowDialog();
        }

        private void F_E_updateAccount1(object sender, EventArgs e)
        {
            thongTinCaNhanToolStripMenuItem.Text = "Thông tin tài khoản (" + loginAccount.DisplayName + ")";
        }

        private void F_DeleteSlaver(object sender, EventArgs e)
        {
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
            LoadSlaver();
        }

        private void F_UpdateSlaver(object sender, EventArgs e)
        {
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
            LoadSlaver();
        }

        private void F_InsertSlaver(object sender, EventArgs e)
        {
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
            LoadSlaver();
        }

        private void F_DeleteCategory(object sender, EventArgs e)
        {
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
            LoadSlaver();
        }

        private void F_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategory();
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
        }

        private void F_InsertCategory(object sender, EventArgs e)
        {
            LoadCategory();
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
        }

        private void F_UpdateMedicine(object sender, EventArgs e)
        {
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
        }

        private void F_DeleteMedicine(object sender, EventArgs e)
        {
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
            LoadSlaver();
        }

        private void F_InsertMedicine(object sender, EventArgs e)
        {
            LoadMedicineListByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Slaver).ID);
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedItem == null)
            {
                return;
            }

            Category selectedCategory = comboBox.SelectedItem as Category;
            id = selectedCategory.ID;
            LoadMedicineListByCategoryID(id);
        }

        private void btnAddMedicine_Click(object sender, EventArgs e)
        {
            Slaver slaver = lsvBill.Tag as Slaver;
            if (slaver == null)
            {
                MessageBox.Show("Vui lòng chọn khay");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillBySlaverID(slaver.ID);
            int idMedicine = (cbMedicine.SelectedItem as Medicine).ID;
            int count = (int)nmMedicineCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InserBill(slaver.ID);
                BillInforDAO.Instance.InserBillInfor(BillDAO.Instance.GetMaxID(), idMedicine, count);
            }
            else
            {
                BillInforDAO.Instance.InserBillInfor(idBill, idMedicine, count);
            }
            ShowBill(slaver.ID);
            LoadSlaver();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Slaver slaver = lsvBill.Tag as Slaver; /// lấy khay
            int idBill = BillDAO.Instance.GetUncheckBillBySlaverID(slaver.ID);

            int discount = (int)nmDiscount.Value;
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Replace(".", "").Split(',')[0]);
            double finalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hóa đơn cho bàn {0} \n Tổng tiền - (Tổng tiền/100) x Giảm giá \n=> {1} - ({1}/100) x {2} = {3}", slaver.Name, totalPrice, discount, finalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalPrice);
                    ShowBill(slaver.ID);
                    LoadSlaver();
                }
            }
        }
        #endregion


    }
}
