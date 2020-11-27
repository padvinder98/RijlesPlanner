using System;
namespace RijlesPlanner.IDataAccessLayer.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoleDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
