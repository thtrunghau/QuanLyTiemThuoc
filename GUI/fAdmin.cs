using QuanLyTiemThuoc.DAO;
using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTiemThuoc.GUI
{
    public partial class fAdmin : Form
    {
        BindingSource medicineList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource slaverList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            LoadAll();
        }


        #region method
        void LoadAll()
        {
            LoadDateTimePicker();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListMedicine();
            LoadMedicineCategory();
            LoadListSlaver();
            dtgvMedicine.DataSource = medicineList;
            dtgvCategory.DataSource = categoryList;
            dtgvSlaver.DataSource = slaverList;
            LoadCategoryInforMedicine(cbMedicineCategory);
            AddMedicineBinding();
            AddMedicineCategoryBinding();
            AddSlaverBinding();
        }
        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
        }

        void LoadListMedicine()
        {
            medicineList.DataSource = MedicineDAO.Instance.GetListMedicine();
        }

        void LoadListSlaver() 
        {
            slaverList.DataSource = SlaverDAO.Instance.LoadSlaverList();
        }
        void AddMedicineBinding()
        {
            txbMedicineName.DataBindings.Add(new Binding("Text", dtgvMedicine.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbMedicineID.DataBindings.Add(new Binding("Text", dtgvMedicine.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmMedicinePrice.DataBindings.Add(new Binding("Value", dtgvMedicine.DataSource, "Price", true, DataSourceUpdateMode.Never));

        }

        void AddMedicineCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));

        }

        void AddSlaverBinding()
        {
            txbSlaverID.DataBindings.Add(new Binding("Text", dtgvSlaver.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbSlaverName.DataBindings.Add(new Binding("Text", dtgvSlaver.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbTrangThai.DataBindings.Add(new Binding("Text", dtgvSlaver.DataSource, "status", true, DataSourceUpdateMode.Never));

        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisPlayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadMedicineCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadCategoryInforMedicine(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        List<Medicine> SearchMedicineByName(string medicineName)
        {
            List<Medicine> listMedicine = MedicineDAO.Instance.SearchMedicineByName(medicineName);

            return listMedicine;
        }

        List<Category> SearchCategoryByName(string categoryName)
        {
            List<Category> listCategory = CategoryDAO.Instance.SearchMedicineCategoryByName(categoryName);

            return listCategory;
        }

        List<Slaver> SearchSlaverByName(string slaverName)
        {
            List<Slaver> listSlaver = SlaverDAO.Instance.SearchSlaverrByName(slaverName);

            return listSlaver;
        }

        List<Account> SearchUserByName(string userName)
        {
            List<Account> listUser = AccountDAO.Instance.SearchUserByName(userName);

            return listUser;
        }

        
        #endregion

        #region event

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }


        #region Medicine
        private void txbMedicineID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvMedicine.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvMedicine.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbMedicineCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;

                    foreach (Category item in cbMedicineCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;

                    }

                    cbMedicineCategory.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnShowMedicine_Click(object sender, EventArgs e)
        {
            LoadListMedicine();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txbMedicineName.Text;
            int idCategory = (cbMedicineCategory.SelectedItem as Category).ID;
            float price = (float)nmMedicinePrice.Value;

            if (MedicineDAO.Instance.InsertMedicine(name, idCategory, price))
            {
                MessageBox.Show("Thêm thành công!");
                LoadListMedicine();
                if (insertMeducine != null)
                {
                    insertMeducine(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm!");
            }
        }
        private event EventHandler insertMeducine;

        public event EventHandler InsertMedicine
        {
            add { insertMeducine += value; }
            remove { insertMeducine -= value; }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idMedicine = Convert.ToInt32(txbMedicineID.Text);

            if (MedicineDAO.Instance.DeleteMedicine(idMedicine))
            {
                MessageBox.Show("Xóa thành công!");
                LoadListMedicine();
                if (deleteMedicine != null)
                {
                    deleteMedicine(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!");
            }
        }
        private event EventHandler deleteMedicine;

        public event EventHandler DeleteMedicine
        {
            add { deleteMedicine += value; }
            remove { deleteMedicine -= value; }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string name = txbMedicineName.Text;
            int idCategory = (cbMedicineCategory.SelectedItem as Category).ID;
            float price = (float)nmMedicinePrice.Value;
            int idMedicine = Convert.ToInt32(txbMedicineID.Text);

            if (MedicineDAO.Instance.UpdateMedicine(idMedicine, name, idCategory, price))
            {
                MessageBox.Show("Sửa thông tin thành công!");
                LoadListMedicine();
                if (updateMedicine != null)
                {
                    updateMedicine(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thông tin!");
            }
        }
        private event EventHandler updateMedicine;

        public event EventHandler UpdateMedicine
        {
            add { updateMedicine += value; }
            remove { updateMedicine -= value; }
        }

        private void btnSearchMedicine_Click(object sender, EventArgs e)
        {
            medicineList.DataSource =  SearchMedicineByName(txbSearchMedicineName.Text);
        }
        #endregion

        #region Medicine Category
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadMedicineCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;


            if (CategoryDAO.Instance.InsertMedicineCategory(name))
            {
                MessageBox.Show("Thêm thành công!");
                LoadMedicineCategory();
                if (insertCategory != null)
                {
                    insertCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm!");
            }
        }

        private event EventHandler insertCategory;

        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instance.DeleteMedicineCategory(id))
            {
                MessageBox.Show("Xóa thành công!");
                LoadMedicineCategory();
                if (deleteCategory != null)
                {
                    deleteCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!");
            }
        }
        private event EventHandler deleteCategory;

        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instance.UpdateMedicineCategory(name, id))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadMedicineCategory();
                if (updateCategory != null)
                {
                    updateCategory(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật!");
            }
        }
        private event EventHandler updateCategory;

        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            categoryList.DataSource = CategoryDAO.Instance.SearchMedicineCategoryByName(txbSearchCategory.Text);
        }
        #endregion

        #region Slaver

        private void btnShowSlaver_Click(object sender, EventArgs e)
        {
            LoadListSlaver();
        }

        private void btnAddSlaver_Click(object sender, EventArgs e)
        {
            string name = txbSlaverName.Text;


            if (SlaverDAO.Instance.InsertSlaver(name))
            {
                MessageBox.Show("Thêm thành công!");
                LoadListSlaver();
                if (insertSlaver != null)
                {
                    insertSlaver(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm!");
            }
        }
        private event EventHandler insertSlaver;

        public event EventHandler InsertSlaver
        {
            add { insertSlaver += value; }
            remove { insertSlaver -= value; }
        }
        private void btnDeleteSlaver_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbSlaverID.Text);

            if (SlaverDAO.Instance.DeleteSlaver(id))
            {
                MessageBox.Show("Xóa thành công!");
                LoadListSlaver();
                if (deleteSlaver != null)
                {
                    deleteSlaver(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!");
            }
        }
        private event EventHandler deleteSlaver;

        public event EventHandler DeleteSlaver
        {
            add { deleteSlaver += value; }
            remove { deleteSlaver -= value; }
        }
        private void btnEditSlaver_Click(object sender, EventArgs e)
        {
            string name = txbSlaverName.Text;
            int id = Convert.ToInt32(txbSlaverID.Text);

            if (SlaverDAO.Instance.UpdateSlaver(name, id))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadListSlaver();
                if (updateSlaver != null)
                {
                    updateSlaver(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi cập nhật!");
            }
        }
        private event EventHandler updateSlaver;

        public event EventHandler UpdateSlaver
        {
            add { updateSlaver += value; }
            remove { updateSlaver -= value; }
        }

        private void bntSearchSlaver_Click(object sender, EventArgs e)
        {
            slaverList.DataSource = SlaverDAO.Instance.SearchSlaverrByName(txbSearchSlaver.Text);
        }

        #endregion

        #endregion


    }
}
