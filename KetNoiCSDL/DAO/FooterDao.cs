using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetNoiCSDL.DAO
{
    public class FooterDao
    {
        OnlineShopDbContext db = null;
        public FooterDao()
        {
            db = new OnlineShopDbContext();
        }


        public Footer LayFooter()
        {
            return db.Footers.SingleOrDefault();
        }
    }
}
