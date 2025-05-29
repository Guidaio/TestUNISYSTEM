using Microsoft.EntityFrameworkCore;
using UserAPI.Core.Entities;

namespace UserAPI.Application.Features.Users.Commands;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
} 