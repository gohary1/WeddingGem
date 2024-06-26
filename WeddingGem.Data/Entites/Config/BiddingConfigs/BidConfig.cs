using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGem.Data.Entites.Config.BiddingConfigs
{
    public class BiddingConfig : IEntityTypeConfiguration<Bidding>
    {
        public void Configure(EntityTypeBuilder<Bidding> builder)
        {
            builder.Property(o => o.Status)
                .HasConversion(
                o => o.ToString(),
                o => (BiddingStatus)Enum.Parse(typeof(BiddingStatus), o)
                );
        }
    }
    

}
