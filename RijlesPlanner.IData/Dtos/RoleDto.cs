using System;
namespace RijlesPlanner.IData.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public RoleDto()
        {

        }

        public RoleDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
