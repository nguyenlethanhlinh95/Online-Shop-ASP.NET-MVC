using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetNoiCSDL.DAO
{
    public class SlideDao
    {
        OnlineShopDbContext db = null;
        public SlideDao()
        {
            db = new OnlineShopDbContext();
        }

        /// <summary>
        /// Get all slide
        /// </summary>
        /// <returns></returns>
        public List<Slide> ListAll()
        {
            return db.Slides
                .Where(x => x.Status == true)
                .OrderBy(y =>y.DisplayOrder)
                .ToList();
        }
    }
}
