using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool loginCheck(string userName, string passWord)
        {

            string query = "USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] { userName, passWord });
            return result.Rows.Count > 0;
        }
        public Account GetAccountByUserName(string userName)
        {

            DataTable data = DataProvider.Instance.ExcuteQuery("select * from Account where UserName = '" + userName + "'");

            foreach (DataRow row in data.Rows)
            {
                return new Account(row);
            }
            return null;
        }

        public bool UpdateAccount(string userName, string displayName, string passWord, string newPassword)
        {
            int result = DataProvider.Instance.ExcuteNoneQuery("exec USP_UpdateAccount @userName , @displayName , @passWord , @newPassWord",
                                                                new object[] { userName, displayName, passWord, newPassword });
            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExcuteQuery("select UserName, DisplayName, Type from Account");
        }

        public bool InsertAccount(string userName, string displayName, int type)
        {
            string query = string.Format("insert into Account (UserName, DisplayName, Type) values (N'{0}', N'{1}', {2})", userName, displayName, type);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string userName, string displayName, int type)
        {
            string query = string.Format("update Account set  DisplayName = N'{1}', Type = {2} where UserName = N'{0}'", userName, displayName, type);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string userName)
        {
            string query = string.Format("delete Account where UserName = N'{0}'", userName);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool ResetPassword(string userName)
        {


            string query = string.Format("update Account set PassWord = N'123456' where UserName = N'{0}'", userName);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public List<Account> SearchUserByName(string name)
        {
            List<Account> list = new List<Account>();

            string query = string.Format("exec USP_SearchAccount N'%{0}%'", name);

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Account acc = new Account(row);
                list.Add(acc);
            }
            return list;
        }
    }
}
