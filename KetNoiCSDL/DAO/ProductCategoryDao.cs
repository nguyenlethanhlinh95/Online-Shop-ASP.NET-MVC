using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetNoiCSDL.DAO
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;

        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        //List Product
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories
                .Where(x => x.Status == true)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
        }

        //Lay danh sach san pham theo id
        public List<ProductCategory> DStheoID(long id)
        {
            return db.ProductCategories.Where(x => x.ParentID == id).ToList();
        }
    }
}
