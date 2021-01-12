using Application.Interfaces;
using Application.Mapper;
using Application.Models.Role;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<IEnumerable<RoleModel>> GetRoleList()
        {
            var roles = await _roleRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<RoleModel>>(roles);
            return mapped;
        }
    }
}
