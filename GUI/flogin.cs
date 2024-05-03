using QuanLyTiemThuoc.DAO;
using QuanLyTiemThuoc.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTiemThuoc
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;
            if (loginCheck(userName, passWord))
            {
                ///Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                ///fSlaverManager fSlaverManager = new fSlaverManager(loginAccount);
                fSlaverManager fSlaverManager = new fSlaverManager();
                this.Hide();
                fSlaverManager.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }
        }

        bool loginCheck(string userName, string passWord)
        {
            return AccountDAO.Instance.loginCheck(userName, passWord);
        }
    }
}
