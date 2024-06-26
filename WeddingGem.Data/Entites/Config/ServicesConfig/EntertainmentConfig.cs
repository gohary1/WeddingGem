﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Data.Entites.Config.ServicesConfig
{
    internal class EntertainmentConfig : IEntityTypeConfiguration<Entertainment>
    {
        public void Configure(EntityTypeBuilder<Entertainment> builder)
        {
            builder.ToTable("Entertainments");
        }
    }
}
