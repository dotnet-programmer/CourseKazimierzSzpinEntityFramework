using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Application.Common.Interfaces;

public interface IAppDbContext
{
	DbSet<Order> Orders { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}