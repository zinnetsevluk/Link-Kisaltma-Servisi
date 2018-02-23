using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LinkKS.DTO;

namespace LinkKS.Business
{
     public interface IUser
    {
        Guid SignUp(string name, string email, string password);
        Guid? SignIn(string email, string password);
        void UpdateUser(Guid userId, string name, string email, string password);
        DTO_USER GetProfile(Guid userId);


    }
}
