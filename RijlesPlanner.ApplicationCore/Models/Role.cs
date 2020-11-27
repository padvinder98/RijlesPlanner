using System;
using RijlesPlanner.IDataAccessLayer.Dtos;

namespace RijlesPlanner.ApplicationCore.Models
{
    public class Role
    {
        public Guid Id { get; }
        public string Name { get; }

        public Role(RoleDto roleDto)
        {
            this.Id = roleDto.Id;
            this.Name = roleDto.Name;
        }
    }
}
