using KetNoiCSDL.EF;
using KetNoiCSDL.ViewModel;
using PagedList;
using System.Collections.Generic;
using System.Linq;
namespace KetNoiCSDL.DAO
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;

        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        //Get List new product
        public List<Product> NewProList(int numpro)
        {
            return db.Products
                .Where(x => x.Status == true)
                .OrderByDescending(x => x.CreatedDate)
                .Take(numpro)
                .ToList();
        }

        // San pham Noi bat, xem nhieu
        public List<Product> FeatureProList(int numpro)
        {
            return db.Products
                .Where(x => x.Status == true)
                .OrderByDescending(x => x.ViewCount)
                .Take(numpro)
                .ToList();
        }

        //Lay san pham
        public Product LaySP(long id)
        {
            return db.Products.Find(id);
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        

        // Lay danh sach phan pham theo id
        public IEnumerable<Product> ListAllPage(int categoryId,int page, int pageSize)
        {
            return db.Products
                .Where(x=>x.CategoryID==categoryId)
                .OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        //IQueryable<User> model = db.Users;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.UserName.Contains(searchString) ||
        //       x.Name.Contains(searchString));

        //    }
        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
    // Tim kiem san pham theo tu khoa
    public IEnumerable<Product> Search(string keyword, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(keyword))
            {
                model = model.Where(x => x.Name.Contains(keyword));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        //Lay san pham lien qua
        public IEnumerable<Product> ListRelatedProducts(long productId)
        {
            //Tim san pham co id nay.
            var product = db.Products.Find(productId);
            return db.Products
                .Where(x => x.CategoryID == product.CategoryID && x.ID != productId)
                .OrderByDescending(x => x.CreatedDate).ToList();
        }

        //Search
        public List<string> ListName(string keyword)
        {
            return db.Products
                .Where(x => x.Name.Contains(keyword))
                .Select(x => x.Name)
                .ToList();
        }

    }
}
