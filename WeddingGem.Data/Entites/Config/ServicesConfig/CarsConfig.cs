using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Data.Entites.Config.ServicesConfig
{
    public class CarsConfig : IEntityTypeConfiguration<cars>
    {
        public void Configure(EntityTypeBuilder<cars> builder)
        {
            builder.ToTable("Cars");
        }
    }
}
