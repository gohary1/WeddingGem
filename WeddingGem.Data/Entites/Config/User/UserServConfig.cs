using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;

namespace WeddingGem.Data.Entites.Config.User
{
    public class UserServConfig : IEntityTypeConfiguration<UserService>
    {
        public void Configure(EntityTypeBuilder<UserService> builder)
        {
            builder.HasKey(e => new { e.ServId, e.UserId, e.purchaseDate });

            builder.HasOne(us => us.AppUser)
            .WithMany(u => u.UserServices)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Adjust delete behavior as needed
        }
    }
}
