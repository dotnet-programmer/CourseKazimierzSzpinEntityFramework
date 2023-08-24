﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accountancy.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountancy.DataLayer.Configurations.Views;
internal class CustomerAddressViewConfiguration : IEntityTypeConfiguration<CustomerAddressView>
{
	public void Configure(EntityTypeBuilder<CustomerAddressView> builder) => builder.HasNoKey().ToView("CustomerAddressView");
}
