﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCard.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool isSucceed, string userId)> CreateUserAsync(string password, string email, string fullName, List<string> roles);
        Task<bool> ConfirmEmail(string email, string activationToken);
        Task<bool> SigninUserAsync(string email, string password);
        Task<string> GetUserIdAsync(string userName);
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
        Task<string> GetUserNameAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> IsUniqueUserName(string userName);
        Task<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync();
        Task<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles);

        // Role Section
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<List<(string id, string roleName)>> GetRolesAsync();
        Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        Task<bool> UpdateRole(string id, string roleName);

        // User's Role section
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task<bool> AssignUserToRole(string userName, IList<string> roles);
        Task<bool> UpdateUsersRole(string userName, IList<string> usersRole);
    }
}
