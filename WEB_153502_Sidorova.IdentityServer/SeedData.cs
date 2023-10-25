using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using WEB_153502_Sidorova.IdentityServer.Data;
using WEB_153502_Sidorova.IdentityServer.Data.Migrations;
using WEB_153502_Sidorova.IdentityServer.Models;

namespace WEB_153502_Sidorova.IdentityServer
{
    public class SeedData
    {
        public async static Task EnsureSeedData(WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var userRole = await roleMgr.FindByNameAsync("user");
                if (userRole == null)
                {
                    userRole = new IdentityRole
                    {
                        Name = "user",
                    };
                    var result = await roleMgr.CreateAsync(userRole);

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                var adminRole = await roleMgr.FindByNameAsync("admin");
                if (adminRole == null)
                {
                    adminRole = new IdentityRole
                    {
                        Name = "admin",
                    };
                    var result = await roleMgr.CreateAsync(adminRole);

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }


                var user = await userMgr.FindByNameAsync("user");
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "user",
                        Email = "user@gmail.com",
                        EmailConfirmed = true,
                    };

                    var result = await userMgr.CreateAsync(user, "Ulya12345$");
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = await userMgr.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "User Userovich"),
                            new Claim(JwtClaimTypes.GivenName, "User"),
                            new Claim(JwtClaimTypes.FamilyName, "Userovich"),
                            new Claim(JwtClaimTypes.WebSite, "http://user.com"),
                        });
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = await userMgr.AddToRoleAsync(user, "user");
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Log.Debug("user created");
                }
                else
                {
                    Log.Debug("user already exists");
                }

                var admin = await userMgr.FindByNameAsync("admin");
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                    };

                    var result = await userMgr.CreateAsync(admin, "Pass12345$");
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = await userMgr.AddClaimsAsync(admin, new Claim[] { new Claim(JwtClaimTypes.Name, "Admin Adminovich"), new Claim(JwtClaimTypes.GivenName, "Admin"), new Claim(JwtClaimTypes.FamilyName, "Adminovich"), new Claim(JwtClaimTypes.WebSite, "http://admin.com") });

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = await userMgr.AddToRoleAsync(admin, "admin");
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Log.Debug("admin created");
                }
                else
                {
                    Log.Debug("admin already exists");
                }

                var alice = await userMgr.FindByNameAsync("alice");
                if (alice == null)
                {
                    alice = new ApplicationUser
                    {
                        UserName = "alice",
                        Email = "AliceSmith@email.com",
                        EmailConfirmed = true,
                    };
                    var result = await userMgr.CreateAsync(alice, "Pass123$");
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = await userMgr.AddClaimsAsync(alice, new Claim[] { new Claim(JwtClaimTypes.Name, "Alice Smith"), new Claim(JwtClaimTypes.GivenName, "Alice"), new Claim(JwtClaimTypes.FamilyName, "Smith"), new Claim(JwtClaimTypes.WebSite, "http://alice.com") });
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("alice created");
                }
                else
                {
                    Log.Debug("alice already exists");
                }

                var bob = await userMgr.FindByNameAsync("bob");
                if (bob == null)
                {
                    bob = new ApplicationUser
                    {
                        UserName = "bob",
                        Email = "BobSmith@email.com",
                        EmailConfirmed = true
                    };
                    var result = await userMgr.CreateAsync(bob, "Pass123$");
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = await userMgr.AddClaimsAsync(bob, new Claim[] { new Claim(JwtClaimTypes.Name, "Bob Smith"), new Claim(JwtClaimTypes.GivenName, "Bob"), new Claim(JwtClaimTypes.FamilyName, "Smith"), new Claim(JwtClaimTypes.WebSite, "http://bob.com"), new Claim("location", "somewhere") });
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("bob created");
                }
                else
                {
                    Log.Debug("bob already exists");
                }
            }
        }
    }
}