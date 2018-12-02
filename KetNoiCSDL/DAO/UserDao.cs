using Common;
using KetNoiCSDL.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetNoiCSDL.DAO
{
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User entity)
        {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;           
        }

        public int Login(string UserName, string PassWord, bool isLoginAdmin = false)
        {
            var res = db.Users.SingleOrDefault(x => x.UserName == UserName);
            if (res == null)
            {
                return 0; //Sai ten dang nhap
            }            else
            {
                if (isLoginAdmin == true)
                {
                    if (res.GroupID == CommonConstants.ADMIN_GROUP || res.GroupID == CommonConstants.MOD_GROUP)
                    {
                        if (res.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (res.Password == PassWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3; 
                    }
                }
                else
                {
                    if (res.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (res.Password == PassWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }

        //Lay danh sach quyen cho user
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();

        }

        public User GetByID(string username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }

        //Get list member
        public List<UserGroup> GetListMember()
        {
            return db.UserGroups.ToList();
        }


        //Phan trang danh sach user va tim kiem
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users.Where(x=>x.GroupID != "ADMIN");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) ||
               x.Name.Contains(searchString));

            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page,pageSize);
        }

        // Cap nhat user
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Name = entity.Name;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete User
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Lay user
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        //Check User Name
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        //Check Email
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}