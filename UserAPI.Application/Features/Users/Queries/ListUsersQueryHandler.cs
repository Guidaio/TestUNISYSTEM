using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Abstractions;
using UserAPI.Core.Entities;

namespace UserAPI.Application.Features.Users.Queries;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, PagedResult<User>>
{
    private readonly IApplicationDbContext _context;

    public ListUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<User>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize switch
        {
            < 1 => 1,
            > 100 => 100,
            _ => request.PageSize
        };

        var baseQuery = _context.Users
            .AsNoTracking()
            .OrderBy(user => user.CreatedAt)
            .ThenBy(user => user.Id);

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

        var items = await baseQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<User>(items, page, pageSize, totalCount, totalPages);
    }
} 