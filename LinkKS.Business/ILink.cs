using LinkKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkKS.Business
{
    public interface ILink
    {
        string Shorten(string longLink, Guid? userId, string password, DateTime? expireDate, bool notification, bool oneshot,byte status);
        void UpdatePassword(Guid linkId, string password);
        void UpdateExpireDate(Guid linkId, DateTime? expireDate);
        void UpdateNotification(Guid linkId, bool notification);
        void UpdateOneShot(Guid linkId, bool oneshot);
        string CheckPassword(Guid linkId, string password);
        void Delete(Guid linkId);
        DTO_LINK[] List(Guid userId);
        DTO_LINK_LOG[] ListLog(Guid linkId);
        DTO_LINK_CONTROL_RESULT GetLongLink(string shortLink);
        void Log(Guid linkId, string Ip, string Referer, string Agent, DateTime Date);



    }
}
