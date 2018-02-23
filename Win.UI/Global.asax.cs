using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LinkKSServiceContract;
using System.ServiceModel;

namespace Win.UI
{
    public class MvcApplication : System.Web.HttpApplication 
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
        }

        protected void Application_BeginRequest()
        {
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://localhost:7987/LinkKSservice.svc");
            var channel = ChannelFactory<ILinkKSServiceContract>.CreateChannel(binding, address);
            var segments = this.Request.Url.Segments;
            if (segments.Length != 2 || segments[1].Length != 6 ||
                segments[1].Any(c => !"0123456789abcdefghijklmnoprstuwvxyz".Contains(c)))
            {
                return;
            }
            var shortLink = segments[1];

            var kontrolResult = channel.GetLonkLink(shortLink);
            if (kontrolResult == null) // Link bulunamadı
            {
                return;
            }
            if (kontrolResult.oneShot)
            {
                if (!this.Request.Cookies.AllKeys.Contains(shortLink))
                {
                    return;
                }
            }
            if (kontrolResult.password)
            {
                this.Response.Redirect("/Home/CheckPassword?id=" + kontrolResult.linkId + "&shortLink=" + shortLink);
            }
            this.Request.Cookies.Add(new HttpCookie(shortLink, "1"));

            channel.linkLog(kontrolResult.linkId, this.Request.UserHostAddress, this.Request.UrlReferrer?.AbsoluteUri, this.Request.UserAgent, DateTime.Now);
            // TODO Log
          
            this.Response.Redirect(kontrolResult.longLink);
        }
    }
}
