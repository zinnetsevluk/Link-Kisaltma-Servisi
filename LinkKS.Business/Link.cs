using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkKS.DTO;

namespace LinkKS.Business
{
    public class Link : ILink
    {
        public void Delete(Guid linkId)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = dc.LINKs.Where(c => c.LINK_ID== linkId).Select(c => c);
                foreach (var i in item)
                {
                    dc.LINKs.DeleteOnSubmit(i);
                }
                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public string CheckPassword(Guid linkId, string password)
        {
            using (var dc = new LinkKSDataContext())
            {
                return dc.LINKs.Where(c => c.LINK_ID == linkId && c.LINK_PASSWORD == password).Select(c => c.LONG_LINK).FirstOrDefault();
                
            }
        }

        public string Shorten(string longLink, Guid? userId, string password, DateTime? expireDate, bool notification, bool oneshot,byte status)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = new LINK();
                item.LINK_ID = Guid.NewGuid();
                item.SHORT_LINK = this.GenerateShortLink(dc);
                item.LONG_LINK = longLink;
                item.LINK_PASSWORD = password;
                item.USERID = userId;
                item.CREATE_DATE = DateTime.Now;
                item.EXPIRE_DATE = expireDate;
                item.LINK_NOTIFICATION = notification;
                item.ONE_SHOT = oneshot;
                item.LINK_STATUS = status;

                dc.LINKs.InsertOnSubmit(item);
                dc.SubmitChanges();
                return item.SHORT_LINK;

            }
        }


        private const string ValidChars = "0123456789abcdefghijklmnoprstuwvxyz";

        public string GenerateShortLink(LinkKSDataContext dc)
        {
            var rnd = new Random();
            while (true)
            {
                var shortLink = "";
                for (int i = 0; i < 6; i++)
                {
                    var index = rnd.Next(Link.ValidChars.Length);
                    shortLink += Link.ValidChars[index];
                }
                if (!dc.LINKs.Any(c => c.SHORT_LINK == shortLink))
                {
                    return shortLink;
                }
            }
        }

        //private string GenerateShortLink()
        //{
        //    var rnd = new Random();
        //    var shortLink = "";

        //    for (int i = 0; i < 6; i++)
        //    {
        //        var index = rnd.Next(Link.ValidChars.Length);

        //        shortLink += Link.ValidChars[index];
        //    }

        //    return shortLink;
        //}

        public void UpdatePassword(Guid linkId, string password)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = dc.LINKs.First(c => c.LINK_ID == linkId);
                item.LINK_PASSWORD = password;
                dc.SubmitChanges();
            }
        }

        public void UpdateExpireDate(Guid linkId, DateTime? expireDate)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = dc.LINKs.First(c => c.LINK_ID == linkId);
                item.EXPIRE_DATE = expireDate;
                dc.SubmitChanges();
            }
        }

        public void UpdateNotification(Guid linkId, bool notification)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = dc.LINKs.First(c => c.LINK_ID == linkId);
                item.LINK_NOTIFICATION = notification;
                dc.SubmitChanges();
            }
        }

        public void UpdateOneShot(Guid linkId, bool oneshot)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = dc.LINKs.First((c => c.LINK_ID == linkId));
                item.ONE_SHOT = oneshot;
                dc.SubmitChanges();
            }
        }
        public void Log(Guid linkId,string Ip,string Referer,string Agent,DateTime Date)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = new LINK_LOG();
                item.LOG_ID = Guid.NewGuid();
                item.IP = Ip;
                item.LINK_ID = linkId;
                item.REFERRER = Referer;
                item.AGENT = Agent;
                item.LOG_DATE = DateTime.Now;
                dc.LINK_LOGs.InsertOnSubmit(item);
                dc.SubmitChanges();
            }
        }
        //public void UpdateStatus() yazzzzz

        public DTO_LINK[] List(Guid userId)
        {
            using (var dc = new LinkKSDataContext())
            {
                return dc.LINKs.Where(c => c.USERID == userId).Select(c => new DTO_LINK
                {
                    Id = c.LINK_ID,
                    password = c.LINK_PASSWORD,
                    shortLink = c.SHORT_LINK,
                    longLink = c.LONG_LINK,
                    expiredDate = c.EXPIRE_DATE,
                    oneShot = c.ONE_SHOT,
                    notification = c.LINK_NOTIFICATION,
                    createDateLink = c.CREATE_DATE,
                    STATUS = (Status) c.LINK_STATUS,
                    ClickCount = c.LINK_LOGs.Count

                }).ToArray();
            }
        }



        public DTO_LINK_LOG[] ListLog(Guid linkId)
        {
            using (var dc = new LinkKSDataContext())
            { 
                return dc.LINK_LOGs.Where(c => c.LINK_ID == linkId).Select(c => new DTO_LINK_LOG
                {
                    IP = c.IP,
                    Agent = c.AGENT,
                    Date = c.LOG_DATE,
                    Referrer = c.REFERRER

                }).ToArray();
            }
        }

        public DTO_LINK_CONTROL_RESULT GetLongLink(string shortLink)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = dc.LINKs.Where(c => c.SHORT_LINK == shortLink).Select(c => new
                {
                    c.LINK_ID,
                    c.LONG_LINK,
                    c.LINK_STATUS,
                    c.LINK_PASSWORD,
                    c.EXPIRE_DATE,
                    c.ONE_SHOT,
                    clicked = c.LINK_LOGs.Any()
                }).FirstOrDefault();

                if (item == null)
                {
                    return null;
                }
                if (item.LINK_STATUS != (byte) Status.Active)
                {
                    return null;
                }
                if (item.EXPIRE_DATE < DateTime.Now)
                {
                    return null;
                }
                return new DTO_LINK_CONTROL_RESULT
                {
                    linkId = item.LINK_ID,
                    longLink = item.LONG_LINK,
                    password = item.LINK_PASSWORD != null,
                    oneShot = item.ONE_SHOT && item.clicked
                };
            }
        }
    }
}
   

