using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WeddingGem.Data.Context;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Repository
{
    public class StoreContextSeed
    {

        public static string VendorId { get; set; }

        public static async Task StoreSeed(AppDbContext context,ILogger logger,UserManager<AppUser> userManager)
        {
            #region WeedingSeed
            try
            {
                if (context.WeddingHalls != null && !context.WeddingHalls.Any())
                {
                    var weddingHalls = File.ReadAllText("../WeddingGem.Repository/DataSeed/weddingSeed.json");
                    var halls = JsonSerializer.Deserialize<List<WeddingHall>>(weddingHalls);
                    if (halls != null)
                    {
                        if(VendorId == null) {var id= await userManager.FindByEmailAsync("AdminVendor@gmail.com");VendorId = id.Id; }
                        var final = halls.Select(e => { e.Vendor_Id = VendorId; return e; }).ToList();
                        await context.WeddingHalls.AddRangeAsync(final);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            #endregion

            #region hotelSeed
            try
            {
                if (context.Hotel != null && !context.Hotel.Any())
                {
                    var hotels = File.ReadAllText("../WeddingGem.Repository/DataSeed/hotelSeed.json");
                    var hotel = JsonSerializer.Deserialize<List<Hotel>>(hotels);
                    if (hotel != null)
                    {
                        if (VendorId == null) { var id = await userManager.FindByEmailAsync("AdminVendor@gmail.com"); VendorId = id.Id; }
                        var final = hotel.Select(e => { e.Vendor_Id = VendorId; return e; }).ToList();
                        await context.Hotel.AddRangeAsync(final);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            #endregion

            #region cars
            try
            {
                if (context.cars != null && !context.cars.Any())
                {
                    var cars = File.ReadAllText("../WeddingGem.Repository/DataSeed/carSeed.json");
                    var car = JsonSerializer.Deserialize<List<cars>>(cars);
                    if (car != null)
                    {
                        if (VendorId == null) { var id = await userManager.FindByEmailAsync("AdminVendor@gmail.com"); VendorId = id.Id; }
                        var final = car.Select(e => { e.Vendor_Id = VendorId; return e; }).ToList();
                        await context.cars.AddRangeAsync(final);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            #endregion

            #region honeyMoon
            try
            {
                if (context.HoneyMoons != null && !context.HoneyMoons.Any())
                {
                    var HoneyMoons = File.ReadAllText("../WeddingGem.Repository/DataSeed/honeyMoonSeed.json");
                    var HoneyMoon = JsonSerializer.Deserialize<List<HoneyMoon>>(HoneyMoons);
                    if (HoneyMoon != null)
                    {
                        if (VendorId == null) { var id = await userManager.FindByEmailAsync("AdminVendor@gmail.com"); VendorId = id.Id; }
                        var final = HoneyMoon.Select(e => { e.Vendor_Id = VendorId; return e; }).ToList();
                        await context.HoneyMoons.AddRangeAsync(final);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            #endregion

            #region selfCare
            try
            {
                if (context.SelfCares != null && !context.SelfCares.Any())
                {
                    var SelfCares = File.ReadAllText("../WeddingGem.Repository/DataSeed/selfCareSeed.json");
                    var SelfCare = JsonSerializer.Deserialize<List<SelfCare>>(SelfCares);
                    if (SelfCare != null)
                    {
                        if (VendorId == null) { var id = await userManager.FindByEmailAsync("AdminVendor@gmail.com"); VendorId = id.Id; }
                        var final = SelfCare.Select(e => { e.Vendor_Id = VendorId; return e; }).ToList();
                        await context.SelfCares.AddRangeAsync(final);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            #endregion

            #region entertainments
            try
            {
                if (context.Entertainment != null && !context.Entertainment.Any())
                {
                    var Entertainments = File.ReadAllText("../WeddingGem.Repository/DataSeed/entertainmentsSeed.json");
                    var Entertainment = JsonSerializer.Deserialize<List<Entertainment>>(Entertainments);
                    if (Entertainment != null)
                    {
                        if (VendorId == null) { var id = await userManager.FindByEmailAsync("AdminVendor@gmail.com"); VendorId = id.Id; }
                        var final = Entertainment.Select(e => { e.Vendor_Id = VendorId; return e; }).ToList();
                        await context.Entertainment.AddRangeAsync(final);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            #endregion

           

        }

        public static async Task RolesSeed(RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    var roles = new List<string> { "Admin", "Vendor", "User" };

                    foreach (var roleName in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(roleName))
                        {
                            var identityRole = new IdentityRole { Name = roleName };
                            var result = await roleManager.CreateAsync(identityRole);

                            if (!result.Succeeded)
                            {
                                logger.LogError($"Error creating role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding roles.");
            }
        }

        public static async Task PackageSeed(AppDbContext context,ILogger logger)
        {
            try
            {
                if (context.Packages != null && !context.Packages.Any())
                {
                    var packages = File.ReadAllText("../WeddingGem.Repository/DataSeed/packagesSeed.json");
                    var package = JsonSerializer.Deserialize<List<Packages>>(packages);
                    if (package != null)
                    {
                        await context.Packages.AddRangeAsync(package);
                        await context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public static async Task AccountSeed(AppDbContext context,UserManager<AppUser> userManager, ILogger logger)
        {
            try
            {
                if (userManager.Users.Count()<=2)
                {
                    AppUser user = new AppUser()
                    {
                        UserName="AdminUser",
                        Email="AdminUser@gmail.com",
                        Address="nasr city",
                        PhoneNumber="01155353197",
                        BirthDate="3/24/2001"
                    };
 
                    AppUser admin = new AppUser()
                    {
                        UserName="Admin",
                        Email="Admin@gmail.com",
                        Address="nasr city",
                        PhoneNumber="01155353197",
                        BirthDate="3/24/2001"
                    };
                   

                    AppUser vendor = new AppUser()
                    {
                        UserName="AdminVendor",
                        Email="AdminVendor@gmail.com",
                        Address="nasr city",
                        PhoneNumber="01155353197",
                        BirthDate="3/24/2001"
                    };
                    await userManager.CreateAsync(user, "Admin@1");
                    await userManager.AddToRoleAsync(user, "User");

                    await userManager.CreateAsync(admin, "Admin@1");
                    await userManager.AddToRoleAsync(admin, "Admin");

                    await userManager.CreateAsync(vendor, "Admin@1");
                    await userManager.AddToRoleAsync(vendor, "Vendor");
                    var package = await context.Packages.FirstOrDefaultAsync(p => p.PlanName == "Professional");
                    Vendor vendor1 = new Vendor()
                    {
                        Id = vendor.Id,
                        PackageId =package.Id 
                    };
                    context.Vendor.Add(vendor1);
                    await context.SaveChangesAsync();
                    VendorId = vendor.Id;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
