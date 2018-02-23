using LinkKS.DTO;
using LinkKSServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LinkKS.Business;


namespace LinkKSService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LinkKSservice" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LinkKSservice.svc or LinkKSservice.svc.cs at the Solution Explorer and start debugging.
    public class LinkKSservice : ILinkKSServiceContract
    {
        public void linkLog(Guid linkId, string Ip, string Referer, string Agent, DateTime Date)
        {
            var link = new Link();
            link.Log(linkId,Ip,Referer,Agent,Date);
        }
        
        public void LinkDelete(Guid linkId)
        {
            var link = new Link();
            link.Delete(linkId);
        }
       
        public string LinkCheckPassword(Guid linkId, string password)
        {
            var link = new Link();
            return link.CheckPassword(linkId, password);
        }
        //LinkKSBusiness daki User sınıfındaki metotlar çağrıldı
        public Guid UserSignUp(string name, string email, string password)
        {
            var user = new User();
            return user.SignUp(name,email,password);
        }

        public Guid? UserSignIn(string email, string password)
        {
            var user = new User();
            return user.SignIn(email, password);
        }

        public DTO_USER GetUserProfile(Guid userId)
        {
            var user = new User();
            return user.GetProfile(userId);
        }

        public DTO_LINK[] GetList(Guid userId)
        {
            var link = new Link();
            return link.List(userId);
        }

        public DTO_LINK_LOG[] GetListLog(Guid linkId)
        {
            var link = new Link();
            return link.ListLog(linkId);
        }

        public DTO_LINK_CONTROL_RESULT GetLonkLink(string shortLink)
        {
            var link = new Link();
            return link.GetLongLink(shortLink);
        }


        public void User_UpdateUser(Guid userId, string name, string email, string password)
        {
            var user = new User();
            user.UpdateUser(userId, name, email, password);
        }

        //LinkKSBusiness daki Link sınıfında ki metotlar çağrıldı.
        public string LinkShorten(string longLink, Guid? userId, string password, DateTime? expireDate, bool notification, bool oneshot,byte status)
        {
            var link = new Link();
            return link.Shorten(longLink,userId,password,expireDate,notification,oneshot,status);
        }

        public void UpdateLinkPassword(Guid linkId, string password)
        {
            var link = new Link();
            link.UpdatePassword(linkId,password);
        }

        public void UpdateLinkExpireDate(Guid linkId, DateTime? expireDate)
        {
            var link = new Link();
            link.UpdateExpireDate(linkId,expireDate);
        }

        public void UpdateLinkNotification(Guid linkId, bool notification)
        {
            var link = new Link();
            link.UpdateNotification(linkId,notification);
        }

        public void UpdateLinkOneShot(Guid linkId, bool oneshot)
        {
            var link = new Link();
            link.UpdateOneShot(linkId,oneshot);
        }
        //public void UpdateLinkStatus() yazzzz


    }
}
