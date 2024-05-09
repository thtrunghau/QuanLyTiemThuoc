using QuanLyTiemThuoc.DAO;
using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyTiemThuoc.GUI.fAccountFrofile;

namespace QuanLyTiemThuoc.GUI
{
    public partial class fAccountFrofile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public fAccountFrofile(Account loginAcc)
        {
            InitializeComponent();
            LoginAccount = loginAcc;
        }

        #region Method
        void ChangeAccount(Account account)
        {
            txbUserName.Text = LoginAccount.UserName;
            txbDisplayName.Text = LoginAccount.DisplayName;
        }

        void UpdateAccount()
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            string password = txbAccountPassWord.Text;
            string newPassword = txbNewAccountPassWord.Text;
            string reEnterPassword = txbReEnterPass.Text;

            ////Console.WriteLine(newPassword + " " + reEnterPassword);

            if (!newPassword.Equals(reEnterPassword))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu mới!");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, password, newPassword))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    if(e_updateAccount != null) {
                        e_updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu để cập nhật thông tin!");
                }
            }

        }
        #endregion


        #region event
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

        private event EventHandler<AccountEvent> e_updateAccount;
        public event EventHandler<AccountEvent> E_updateAccount
        {
            add { e_updateAccount += value; }
            remove { e_updateAccount -= value; }
        }
        #endregion

        public class AccountEvent : EventArgs
        {
            private Account acc;

            public Account Acc { get { return acc; } set { acc = value; } }
            public AccountEvent(Account account)
            {
                this.acc = account;
            }
        }
    }
}
