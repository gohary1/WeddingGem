using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGem.Data.Entites.Config.User
{
    internal class VendorBidConfig : IEntityTypeConfiguration<VendorBid>
    {
        public void Configure(EntityTypeBuilder<VendorBid> builder)
        {
            builder.HasKey(e => new { e.VendorId, e.AcceptedBid_Id });

            builder.HasOne(vb => vb.Vendor)
            .WithMany(v => v.Bidding)
            .HasForeignKey(vb => vb.VendorId)
            .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(vb => vb.Bidding)
                .WithMany(b => b.VendorBid)
                .HasForeignKey(vb => vb.AcceptedBid_Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
