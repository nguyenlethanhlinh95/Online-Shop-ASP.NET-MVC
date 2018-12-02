using KetNoiCSDL.EF;
using System.Collections.Generic;
using System.Linq;

namespace KetNoiCSDL.DAO
{
    public class MenuDao
    {
        OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();
        }

        //Lay danh sach menu
        public List<Menu> ListByGroupID(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId).OrderBy(x=>x.DisplayOrder).ToList();
        }
    }
}
