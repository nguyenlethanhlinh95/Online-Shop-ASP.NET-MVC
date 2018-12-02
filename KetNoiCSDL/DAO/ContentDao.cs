using KetNoiCSDL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using PagedList;

namespace KetNoiCSDL.DAO
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Description = entity.Description;
                content.Detail = entity.Detail;
                content.CategoryID = entity.CategoryID;
                content.Image = entity.Image;
                content.Name = entity.Name;

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        //lay ra tag
        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        //Them moi bai viet
        public long Create(Content content)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreatedDate = DateTime.Now;
            content.ViewCount = 0;
            db.Contents.Add(content);
            db.SaveChanges();

            //Xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert to to tag table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(content.ID, tagId);

                }
            }

            return content.ID;
        }
        //end them bai viet moi
        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }
        //insert tag
        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }
        //end insert tag

        //insert content tag
        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        //end insert content tag

        /// <summary>
        /// List all content for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
