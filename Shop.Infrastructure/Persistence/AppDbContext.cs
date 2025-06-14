﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Common;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
	private readonly IDateTimeService _dateTimeService;
	private readonly ICurrentUserService _currentUserService;

	// jako parametr przyjmuje konfigurację, która jest przekazywana przez Dependency Injection
	public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService) : base(options)
	{
		_currentUserService = currentUserService;
		_dateTimeService = dateTimeService;
	}

	public DbSet<Order> Orders { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		// przechodzi przez wszystkie encje, które są śledzone przez ChangeTracker i wyszukuje w projekcie wszystkie klasy, które dziedziczą po AuditableBaseEntity
		// i aktualizuje ich właściwości Created, CreatedBy, LastModified i LastModifiedBy w zależności od stanu encji
		foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
		{
			switch (item.State)
			{
				case EntityState.Detached:
					break;
				case EntityState.Unchanged:
					break;
				case EntityState.Deleted:
					break;
				case EntityState.Modified:
					item.Entity.LastModified = _dateTimeService.Now;
					item.Entity.LastModifiedBy = _currentUserService.UserId;
					break;
				case EntityState.Added:
					item.Entity.Created = _dateTimeService.Now;
					item.Entity.CreatedBy = _currentUserService.UserId;
					break;
				default:
					break;
			}
		}

		return await base.SaveChangesAsync(cancellationToken);
	}
}