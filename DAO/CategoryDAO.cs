using QuanLyTiemThuoc.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiemThuoc.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return instance; }
            private set { instance = value; }
        }

        private CategoryDAO() { }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();

            string query = "select *from MedicineCategory";

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Category category = new Category(row);
                list.Add(category);
            }
            return list;
        }

        public Category GetCategoryByID(int id)
        {
            Category category = null;

            string query = "select *from MedicineCategory where id = " + id;

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                category = new Category(row);
                return category;
            }
            return category;

        }

        public bool InsertMedicineCategory(string name)
        {
            string query = string.Format("INSERT INTO MedicineCategory (name) VALUES ( N'{0}')", name);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool UpdateMedicineCategory(string name, int idCategory)
        {
            string query = string.Format("update MedicineCategory set name = N'{0}' where id = {1}", name, idCategory);

            int result = DataProvider.Instance.ExcuteNoneQuery(query);

            return result > 0;
        }

        public bool DeleteMedicineCategory(int idCategory)
        {
            string query = "exec USP_DeleteCategory @idCategory";

            int result = DataProvider.Instance.ExcuteNoneQuery(query, new object[] { idCategory });

            return result > 0;
        }

        public List<Category> SearchMedicineCategoryByName(string name)
        {
            List<Category> list = new List<Category>();

            string query = string.Format("exec USP_SearchCategory N'%{0}%'", name);

            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Category category = new Category(row);
                list.Add(category);
            }
            return list;
        }
    }
}
