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
    public class ServiceConfig : IEntityTypeConfiguration<Items>
    {
        public void Configure(EntityTypeBuilder<Items> builder)
        {
          builder.HasOne(e=>e.Vendor)
                .WithMany(s=>s.Service)
                .HasForeignKey(e=>e.Vendor_Id); 
        }
    }
}
