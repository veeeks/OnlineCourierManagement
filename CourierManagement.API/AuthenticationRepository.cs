using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CourierManagement.API.Models;

namespace CourierManagement.API
{
    public class AuthenticationRepository:IDisposable
    {
        private AuthContext context = null;
        private UserManager<IdentityUser> userManager = null;
        public AuthenticationRepository()
        {
            context = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
            if (userManager != null)
            {
                userManager.Dispose();
            }
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                Email=userModel.Email,
                UserName=userModel.Email
            };
            return await userManager.CreateAsync(user, userModel.Password);
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            return await userManager.FindAsync(userName, password);
        }
    }
}