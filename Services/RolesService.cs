﻿using BlazorServerTestDynamicAccess.Data;
using BlazorServerTestDynamicAccess.Infralayer;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTestDynamicAccess.Services
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Role> _roles;
        private readonly DbSet<User> _users;

        public RolesService(IUnitOfWork uow)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _roles = _uow.Set<Role>();
            _users = _uow.Set<User>();
        }

        public Task<List<Role>> FindUserRolesAsync(int userId)
        {
            var userRolesQuery = from role in _roles
                from userRoles in role.UserRoles
                where userRoles.UserId == userId
                select role;

            return userRolesQuery.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<bool> IsUserInRole(int userId, string roleName)
        {
            var userRolesQuery = from role in _roles
                where role.Name == roleName
                from user in role.UserRoles
                where user.UserId == userId
                select role;
            var userRole = await userRolesQuery.FirstOrDefaultAsync();
            return userRole != null;
        }

        public Task<List<User>> FindUsersInRoleAsync(string roleName)
        {
            var roleUserIdsQuery = from role in _roles
                where role.Name == roleName
                from user in role.UserRoles
                select user.UserId;
            return _users.Where(user => roleUserIdsQuery.Contains(user.Id))
                .ToListAsync();
        }
    }
}
