using Application.Interfaces;
using Domain.Repositories;
using System;

namespace Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _roleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _roleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
        }

    }
}
