using Dominio.Endity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFContext
{
    public class BaseConfiguration<TKey> : IEntityTypeConfiguration<Base>
    {
        public void Configure(EntityTypeBuilder<Base> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
