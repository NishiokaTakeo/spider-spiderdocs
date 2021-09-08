using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebSpiderDocs.Repos
{
    //public class AuthRepository : IDisposable
    //{

    //    private UserManager<ApplicationUser> _userManager;

    //    public AuthRepository()
    //    {
            
    //        _userManager = new UserManager<ApplicationUser>(new UserStore());
    //    }

    //    public async Task<IdentityResult> RegisterUser(SpiderDocsModule.User userModel)
    //    {
    //        ApplicationUser user = new ApplicationUser()
    //        {
    //            Id = userModel.id.ToString(),
    //            UserName = userModel.login
    //        };

    //        var result = await _userManager.CreateAsync(user, userModel.password);

    //        return result;
    //    }

    //    public async Task<ApplicationUser> FindUser(string userName, string password)
    //    {
    //        ApplicationUser user = await _userManager.FindAsync(userName, password);

    //        return user;
    //    }

    //    public void Dispose()
    //    {
    //        _userManager.Dispose();

    //    }
    //}

    //public enum OAuthGrant
    //{
    //    Code = 1,
    //    Implicit = 2,
    //    ResourceOwner = 3,
    //    Client = 4
    //}

    public class ApplicationUser :IUser<string>
    {
        public ApplicationUser() { }

        public ApplicationUser(string username) 
        {
            Id = username;
            UserName = username;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string ClientSecretHash  { get; set; }
    }

    //public class UserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    //{
    //    static List<ApplicationUser> Users = new List<ApplicationUser>();
    //    AuthRepository  Repos ;
    //    public UserStore( AuthRepository repo)
    //    {
    //        Repos = repo;
    //    }
    //    public Task CreateAsync(ApplicationUser user)
    //    {
    //        return Task.Run( () => Users.Add(user));
    //        //throw new NotImplementedException();
    //    }

    //    public Task DeleteAsync(ApplicationUser user)
    //    {
    //        return Task.Run(() => Users.Remove(user));
    //        //throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //        //Users.
    //        //throw new NotImplementedException();
    //    }

    //    public Task<ApplicationUser> FindByIdAsync(string userId)
    //    {
    //        return Task.Run<ApplicationUser>( () => Users.Find(x => x.Id == userId));

    //        //throw new NotImplementedException();
    //    }

    //    public Task<ApplicationUser> FindByNameAsync(string userName)
    //    {
    //        return Task.Run<ApplicationUser>(() => Users.Find(x => x.UserName == userName));
    //    }

    //    public Task UpdateAsync(ApplicationUser user)
    //    {
    //        return Task.Run(() =>
    //        {
    //            var xx = Users.Find(x => x.Id == user.Id);
    //            xx = user;
    //        });
    //        //throw new NotImplementedException();
    //    }


    //    static List<ApplicationUser> PUsers = new List<ApplicationUser>();

    //    public Task<string> GetPasswordHashAsync(ApplicationUser user)
    //    {
    //        return Task.Run(() => Users.Find(u => u.Id == user.Id).ClientSecretHash );
    //    }

    //    public Task<bool> HasPasswordAsync(ApplicationUser user)
    //    {
    //        return Task.Run(() => !string.IsNullOrWhiteSpace( Users.Find(u => u.Id == user.Id).ClientSecretHash ) );
    //    }

    //    public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
    //    {
    //        return Task.Run(() => {
    //            Users.Remove(user);
    //            user.ClientSecretHash  = passwordHash;
    //            Users.Add(user);
    //        });
    //    }

    //}
}