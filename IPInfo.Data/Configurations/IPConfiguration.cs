using IPInfo.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IPInfo.Data.Configurations
{
    public class IPConfiguration : IEntityTypeConfiguration<IP>
    {
        public void Configure(EntityTypeBuilder<IP> builder)
        {
            throw new NotImplementedException();
        }
    }
}
