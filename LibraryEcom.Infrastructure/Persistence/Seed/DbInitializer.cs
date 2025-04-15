// using LibraryEcom.Domain.Common.Enum;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Identity;
// using LibraryEcom.Application.Exceptions;
// using LibraryEcom.Domain.Entities.Identity;
// using LibraryEcom.Application.Interfaces.Data;
// using LibraryEcom.Domain.Common.Enum.Configurations;
// using LibraryEcom.Domain.Common.Property;
// using Microsoft.Extensions.Options;
//
// namespace LibraryEcom.Infrastructure.Persistence.Seed;
//
// public class DbInitializer(
//     UserManager<User> userManager,
//     RoleManager<Role> roleManager,
//     IApplicationDbContext dbContext,
//     IWebHostEnvironment webHostEnvironment,
//     IOptions<> seedSettings) : IDbInitializer
// {
//    
//
//     public async Task InitializeIdentityData(CancellationToken cancellationToken = default)
//     {
//         var user = await InitializeAdmin(cancellationToken);
//
//         await InitializeRoles();
//
//         if (!await userManager.IsInRoleAsync(user, Constants.Roles.Admin))
//         {
//             await userManager.AddToRoleAsync(user, Constants.Roles.Admin);
//         }
//
//         await dbContext.SaveChangesAsync(cancellationToken);
//     }
//
//     private async Task InitializeRoles()
//     {
//         var roles = new List<string>()
//         {
//             Constants.Roles.Admin,
//             Constants.Roles.Staff,
//             Constants.Roles.User
//         };
//
//         var roleIds = new List<Guid>()
//         {
//             new("def10000-e86b-52c2-2a9a-08dca196874f"),
//             new("def10000-e86b-52c2-3a9a-08dca196874f"),
//             new("def10000-e86b-52c2-42d2-08dca196874f"),
//             new("def10000-e86b-52c2-4389-08dca196874f"),
//             new("def10000-e86b-52c2-43de-08dca196874f")
//         };
//
//         for (var i = 0; i < roles.Count; i++)
//         {
//             var role = roles[i];
//
//             var roleExists = await roleManager.RoleExistsAsync(role);
//
//             if (roleExists) continue;
//
//             var newRole = new Role
//             {
//                 Id = roleIds[i],
//                 Name = role,
//                 NormalizedName = role.ToUpper()
//             };
//
//             var result = await roleManager.CreateAsync(newRole);
//
//             if (result.Succeeded) continue;
//
//             var exceptions = result.Errors.Select(x => x.Description);
//
//             throw new BadRequestException("Failed to create super admin role", exceptions.ToArray());
//         }
//     }
//
//     private async Task<User> InitializeAdmin(CancellationToken cancellationToken = default)
//     {
//         var isProduction = webHostEnvironment.IsProduction();
//         
//         var user = await userManager.FindByIdAsync(Constants.Admin.Identifier);
//
//         if (user is null)
//         {
//             var country = await dbContext.Countries.FirstOrDefaultAsync(cancellationToken: cancellationToken);
//
//             var AdminUser = new User
//             {
//                 Id = new Guid(Constants.Admin.Identifier),
//                 Name = isProduction ? Constants.Admin.Production.Name : Constants.Admin.Development.Name,
//                 UserName = isProduction ? Constants.Admin.Production.EmailAddress : Constants.Admin.Development.EmailAddress,
//                 Email = isProduction ? Constants.Admin.Production.EmailAddress : Constants.Admin.Development.EmailAddress,
//                 Gender = GenderType.Male,
//                 PhoneNumber = "+977-9800000000",
//                 CountryId = country!.Id,
//                 NormalizedUserName = isProduction ? Constants.Admin.Production.EmailAddress.ToUpper() : Constants.Admin.Development.EmailAddress.ToUpper(),
//                 NormalizedEmail = isProduction ? Constants.Admin.Production.EmailAddress.ToUpper() : Constants.Admin.Development.EmailAddress.ToUpper(),
//                 EmailConfirmed = true,
//                 PhoneNumberConfirmed = true,
//                 SecurityStamp = Guid.NewGuid().ToString("D"),
//                 IsActive = true,
//             };
//
//             var result = await userManager.CreateAsync(AdminUser, isProduction ? Constants.Admin.Production.DecryptedPassword : Constants.Admin.Development.DecryptedPassword);
//
//             if (result.Succeeded) return AdminUser;
//
//             var exceptions = result.Errors.Select(x => x.Description);
//
//             throw new BadRequestException("Failed to create super admin user", exceptions.ToArray());
//         }
//
//         user.Name = isProduction ? Constants.Admin.Production.Name : Constants.Admin.Development.Name;
//         user.UserName = isProduction ? Constants.Admin.Production.EmailAddress : Constants.Admin.Development.EmailAddress;
//         user.Email = isProduction ? Constants.Admin.Production.EmailAddress : Constants.Admin.Development.EmailAddress;
//         user.NormalizedEmail = isProduction ? Constants.Admin.Production.EmailAddress.ToUpper() : Constants.Admin.Development.EmailAddress.ToUpper();
//         user.NormalizedUserName = isProduction ? Constants.Admin.Production.EmailAddress.ToUpper() : Constants.Admin.Development.EmailAddress.ToUpper();
//         user.PasswordHash = isProduction ? Constants.Admin.Production.DecryptedPassword.HashPassword() : Constants.Admin.Development.DecryptedPassword.HashPassword();
//         
//         user.EmailConfirmed = true;
//         user.PhoneNumberConfirmed = true;
//         
//         await userManager.UpdateAsync(user);
//         
//         return user;
//     }
//
//     
//    
//     
//     private async Task OnInitializeMenuAndRoleAssignment(CancellationToken cancellationToken = default)
//     {
//         var menus = _menuSettings.Menus;
//
//         foreach (var menu in menus)
//         {
//             await ProcessMenu(menu, null, cancellationToken);
//         }
//
//         await dbContext.SaveChangesAsync(cancellationToken);
//     }
//
//     private async Task ProcessMenu(MenuConfiguration menuConfiguration, Guid? parentMenuId, CancellationToken cancellationToken)
//     {
//         var admin = await userManager.FindByIdAsync(Constants.Admin.Identifier)
//                     ?? throw new NotFoundException("An admin user has not been registered to the system.");
//
//         var existingMenu = await dbContext.Menus.FirstOrDefaultAsync(
//             m => m.Id == new Guid(menuConfiguration.Id),
//             cancellationToken);
//
//         if (existingMenu != null)
//         {
//             existingMenu.Title = menuConfiguration.Title;
//             existingMenu.Description = menuConfiguration.Description;
//             existingMenu.Sequence = menuConfiguration.Sequence;
//             existingMenu.Url = menuConfiguration.Url;
//             existingMenu.IsActive = menuConfiguration.IsActive;
//             existingMenu.ParentMenuId = parentMenuId;
//
//             dbContext.Menus.Update(existingMenu);
//         }
//         else
//         {
//             var newMenu = new Menu
//             {
//                 Id = new Guid(menuConfiguration.Id),
//                 Title = menuConfiguration.Title,
//                 Description = menuConfiguration.Description,
//                 Sequence = menuConfiguration.Sequence,
//                 Url = menuConfiguration.Url,
//                 IsActive = menuConfiguration.IsActive,
//                 CreatedBy = admin.Id,
//                 CreatedAt = DateTime.UtcNow,
//                 ParentMenuId = parentMenuId
//             };
//
//             await dbContext.Menus.AddAsync(newMenu, cancellationToken);
//
//             existingMenu = newMenu;
//         }
//
//         if (menuConfiguration.ChildMenus != null)
//         {
//             foreach (var childMenu in menuConfiguration.ChildMenus)
//             {
//                 await ProcessMenu(childMenu, existingMenu.Id, cancellationToken);
//             }
//         }
//     }
//     
//     
//
//     
// }
//
