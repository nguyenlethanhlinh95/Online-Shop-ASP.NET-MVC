using KetNoiCSDL.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetNoiCSDL.DAO
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;

        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        //Phan trang danh sach Danh muc va tim kiem
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));

            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            //IQueryable<Category> model = from a in db.Categories 
            //                             join b in db.Categories
            //                             on (a.ID)
        }
    }
}
