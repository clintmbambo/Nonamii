namespace Nonamii.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Restaurant", "Driver", "User" };
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var admin = new IdentityUser
            {
                UserName = "usernameadmin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "078933597",
                PhoneNumberConfirmed = true,
            };

            var UserExists = await userManager.FindByEmailAsync(admin.Email);
            if (UserExists is null)
            {
                await userManager.CreateAsync(admin, "1234%Ive");
                await userManager.AddToRoleAsync(admin, roles[0]);
            }
        }
    }
}
