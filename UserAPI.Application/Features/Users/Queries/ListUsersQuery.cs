using MediatR;
using UserAPI.Application.Abstractions;
using UserAPI.Core.Entities;

namespace UserAPI.Application.Features.Users.Queries;

public record ListUsersQuery(int Page = 1, int PageSize = 20) : IRequest<PagedResult<User>>;