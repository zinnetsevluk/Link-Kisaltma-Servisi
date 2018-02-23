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
    public class UserController : BaseController
    {
        // GET: User
        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["User"] != null)
            {
                HttpCookie cookie = new HttpCookie("User");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
                
            }
            return Redirect("/Home/LinkShorten");
        }
       
        [HttpGet]
        public ActionResult SignIn()
        {
            var model = new SignInModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SignIn(SignInModel model)
        {
            var userId = Servis().UserSignIn(model.Email, model.Password);
            HttpCookie UserCookie = new HttpCookie("User", userId.ToString());
            Response.SetCookie(UserCookie);
            Response.Cookies.Add(UserCookie);
            if (userId == null)
            {
                throw new Exception("Kullanıcı Bulunamadı");
            }
            return Redirect("/Home/UyeLinkShorten");

        }

        [HttpGet]
        public ActionResult SignUp()
        {
            var model = new SignUpModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            var userId=Servis().UserSignUp(model.Name, model.Email, model.Password);
            //HttpCookie UserCookie = new HttpCookie("User", userId.ToString());
            //Response.SetCookie(UserCookie);
            //Response.Cookies.Add(UserCookie);

            return Redirect("/User/SignIn");
        }
        [HttpGet]
        public ActionResult UserProfileUpdate()
        {
            
            HttpCookie cookie = Request.Cookies["User"];
            if (cookie == null)
            {
                return Redirect("/User/SignIn");
            }
            var userProfile = Servis().GetUserProfile(new Guid(cookie.Value));
            ViewBag.userProfile = userProfile;
            return View();
        }
        [HttpPost]
        public ActionResult UserProfileUpdate(SignUpModel model)
        {
            HttpCookie cookie = Request.Cookies["User"];
            Servis().User_UpdateUser(new Guid(cookie.Value),model.Name,model.Email,model.Password);
           
            return Redirect("/Home/LinkShorten");
        }



    }
}