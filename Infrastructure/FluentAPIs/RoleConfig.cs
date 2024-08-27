using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Manager",
                },
                new Role
                {
                    Id = 2,
                    Name = "Purchasing Manager",
                },
                new Role
                {
                    Id = 3,
                    Name = "Purchasing Staff",
                },
                new Role
                {
                    Id = 4,
                    Name = "Warehouse Staff",
                },
                new Role
                {
                    Id = 5,
                    Name = "Inspector",
                },
                new Role
                {
                    Id = 6,
                    Name = "Supplier",
                },
                new Role
                {
                    Id = 7,
                    Name = "Admin",
                }

            );
        }
    }
}
