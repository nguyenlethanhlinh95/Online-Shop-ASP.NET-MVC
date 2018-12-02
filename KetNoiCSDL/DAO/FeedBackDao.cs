using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetNoiCSDL.DAO
{
    public class FeedBackDao
    {
        OnlineShopDbContext db = null;

        public FeedBackDao()
        {
            db = new OnlineShopDbContext();
        }

        //Insert contact
        public long Insert(Feedback entity)
        {
            db.Feedbacks.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
    }
}
