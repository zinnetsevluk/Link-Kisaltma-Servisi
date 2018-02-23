using LinkKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace LinkKSServiceContract
{
    [ServiceContract]
    public interface ILinkKSServiceContract
    {
        [OperationContract]
        void LinkDelete(Guid linkId);

        [OperationContract]
        string LinkCheckPassword(Guid linkId, string password);

        [OperationContract]
        Guid UserSignUp(string name ,string email,string password);

        [OperationContract]
        Guid? UserSignIn(string email, string password);

        [OperationContract]
        string LinkShorten(string longLink, Guid? userId, string password, DateTime? expireDate, bool notification, bool oneshot,byte status);

        [OperationContract]
        void User_UpdateUser(Guid userId, string name, string email, string password);

        [OperationContract]
        void UpdateLinkExpireDate(Guid linkId, DateTime? expireDate);

        [OperationContract]
        void UpdateLinkNotification(Guid linkId, bool notification);

        [OperationContract]
        void UpdateLinkOneShot(Guid linkId, bool oneshot);

        [OperationContract]
        void UpdateLinkPassword(Guid linkId,string password);

        [OperationContract]
        void linkLog(Guid linkId, string Ip, string Referer, string Agent, DateTime Date);

        [OperationContract]
        DTO_USER GetUserProfile(Guid userId);

        [OperationContract]
        DTO_LINK[] GetList(Guid userId);

        [OperationContract]
        DTO_LINK_LOG[] GetListLog(Guid linkId);

        [OperationContract]
        DTO_LINK_CONTROL_RESULT GetLonkLink(string shortLink);

    }
}
