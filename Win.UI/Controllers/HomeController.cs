using LinkKS.Business;
using LinkKSServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Win.UI.Models;


namespace Win.UI.Controllers
{
    public class HomeController : BaseController
    {
        LinkKSDataContext dc = new LinkKSDataContext();
        // GET: Home
        public ActionResult Index()
        {
            //var userId = channel.UserSignUp("Znnettest", "zinnettest@gmail.com", "321"); 
            //channel.UpdateLinkPassword(new Guid("20216784-6a4a-43e0-ab13-4b2b802b2f56"), "12345");
            //var user = channel.GetUserProfile(new Guid("dfgh-xzg------"));
            return View();

        }

        [HttpGet]
        public ActionResult LinkShorten()
        {
            var model = new LinkShortenModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult LinkShorten(LinkShortenModel model)
        {

            var response = Servis().LinkShorten(model.LongLink, model.UserId, model.Password, model.ExpireDate, model.Notification,
                     model.OneShot,model.status);
            ViewData["KisaLink"] = response;
            return View(model);
        }
        [HttpGet]
       
        public ActionResult UyeLinkShorten()
        {
        
            return View();
        }

        [HttpPost]
        public ActionResult UyeLinkShorten(LinkShortenModel model)
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie == null)
            {
                return Redirect("/User/SignIn");
            }
            model.UserId = new Guid(cookie.Value);
            var response = Servis().LinkShorten(model.LongLink, model.UserId, model.Password, model.ExpireDate, model.Notification,
              model.OneShot,model.status);
            ViewData["KisaLink"] = response;
            return View(model);
        }
        [HttpGet]
        public ActionResult LinkPasswordUpdate()
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie == null)
            {
                return Redirect("/User/SignIn");
            }
            return View();
        }

        [HttpPost]
        public ActionResult LinkPasswordUpdate(LinkPasswordModel model, string id)
        {
            model.LinkId = new Guid(id);
            Servis().UpdateLinkPassword(model.LinkId, model.Password);
            return Redirect("/Home/LinkList");
        }

        [HttpPost]
        public ActionResult OneShotUpdate(bool oneShot, string id)
        {
            //bool state;
            //if (oneShot == "on") state = true;
            //else state = false;
            var linkId = new Guid(id);
            Servis().UpdateLinkOneShot(linkId, oneShot);
            return Redirect("/Home/linkList");
        }

        [HttpGet]
        public ActionResult NotificationUpdate(Guid id)
        {
            LinkKSDataContext dc = new LinkKSDataContext();

            var notification = dc.LINKs.Where(c => c.LINK_ID == id).Select(c => c.LINK_NOTIFICATION).FirstOrDefault();
            ViewBag.notification = notification;
            return View();
        }
        [HttpPost]
     
        public ActionResult NotificationUpdate(NotificationModel model,string id)
        {
            Servis().UpdateLinkNotification(new Guid(id), model.notification);
            return Redirect("/Home/LinkList");
        }
        [HttpGet]
        public ActionResult LinkList(LinkListModel model)
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie==null)
            {
                return Redirect("/User/SignIn");
            }
            var liste = Servis().GetList(new Guid(cookie.Value));
            ViewBag.liste = liste;
            return View(model);
        }
        [HttpGet]
        public ActionResult LinkLog(LinklogModel model,string id)
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie == null)
            {
                return Redirect("/User/SignIn");
            }
            var linklog = Servis().GetListLog(new Guid(id));
            ViewBag.linklog = linklog;
            return View(model);
        }

        [HttpGet]
        public ActionResult CheckPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckPassword(CheckPasswordModel model)
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie == null)
            {
                return Redirect("/User/SignIn");
            }
            var longLink = Servis().LinkCheckPassword(new Guid(Request.QueryString["id"]), model.Password);
            if (longLink == null) return null;
            else
            {
                Request.Cookies.Add(new HttpCookie(Request.QueryString["id"], "1"));
                return Redirect(longLink);
            }
        }

        public ActionResult Delete(string id)
        {
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie == null)
            {
                return Redirect("/User/SignIn");
            }
            Servis().LinkDelete(new Guid(id));
            return Redirect("/Home/LinkList");
        }




    }
}