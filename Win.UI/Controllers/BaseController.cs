using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using LinkKSServiceContract;

namespace Win.UI.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public LinkKSServiceContract.ILinkKSServiceContract Servis()
        {
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://localhost:7987/LinkKSservice.svc");
            var channel = ChannelFactory<ILinkKSServiceContract>.CreateChannel(binding, address);
            return channel;

        }
    }
}