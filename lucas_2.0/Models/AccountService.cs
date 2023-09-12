using lucas_2._0.Views.Admin;
using Microsoft.AspNetCore.Identity;

namespace lucas_2._0.Models
{
    public class AccountService
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        IHttpContextAccessor httpContextAccessor;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> TryRegisterAsync(RegisterVM viewModel)
        {
            // Todo: Try to create a new user
            var user = new ApplicationUser
            {
                UserName = viewModel.Username,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
               
            };

            IdentityResult result = await
                userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await signInManager.PasswordSignInAsync(
                        viewModel.Username,
                        viewModel.Password,
                        isPersistent: false,
                        lockoutOnFailure: false);
            }

            return result.Errors.FirstOrDefault()?.Description;
        }

        public async Task<bool> TryLoginAsync(LoginVM viewModel)
        {

            // Todo: Try to sign user
            SignInResult result = await signInManager.PasswordSignInAsync(
                        viewModel.Username,
                        viewModel.Password,
                        isPersistent: false,
                        lockoutOnFailure: false);



            return result.Succeeded;
        }



        internal async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }


    }
}
