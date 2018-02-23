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
    public class User : IUser
    {
        public DTO_USER GetProfile(Guid userId)
        {

            using (var dc = new LinkKSDataContext())
            {
                return dc.USERs.Where(c =>c.USERID ==userId).Select(c => new DTO_USER
                {
                    Name = c.USERNAME,
                    EMail = c.E_MAIL,
                    Password = c.USER_PASSWORD
                }).FirstOrDefault();

            }
        }

        public Guid SignUp(string name, string email, string password)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = new USER();
                item.USERID = Guid.NewGuid();
                item.USERNAME = name;
                item.E_MAIL = email;
                item.USER_PASSWORD = password;
                item.CREATE_DATE = DateTime.Now;

                dc.USERs.InsertOnSubmit(item);
                dc.SubmitChanges();
                return item.USERID;
            }

        }

        public Guid? SignIn(string email, string password)
        {
            using (var dc = new LinkKSDataContext())
            {
                //var id = dc.USERs.Where(c => c.E_MAIL == email && c.USER_PASSWORD == password).Select(c => c.USERID).FirstOrDefault();
                var id = (from c in dc.USERs where c.E_MAIL == email select c.USERID).FirstOrDefault();
                if (id == Guid.Empty)
                {
                    return null;
                }
                return id;

            }
        }




        public void UpdateUser(Guid userId, string name, string email, string password)
        {
            using (var dc = new LinkKSDataContext())
            {
                var item = dc.USERs.First(c => c.USERID == userId);
                item.USERNAME = name;
                item.E_MAIL = email;
                item.USER_PASSWORD = password;
                item.UPDATE_DATE = DateTime.Now;
                dc.SubmitChanges();
            }
        }

    }
}
