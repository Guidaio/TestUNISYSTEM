using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Core.Entities;
using UserAPI.Application.Abstractions;

namespace UserAPI.Application.Features.Users.Queries;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IEnumerable<User>>
{
    private readonly IApplicationDbContext _context;

    public ListUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }
} 