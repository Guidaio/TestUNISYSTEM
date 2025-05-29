using UserAPI.Core.Entities;
using MediatR;

namespace UserAPI.Application.Features.Users.Queries;

public record ListUsersQuery : IRequest<IEnumerable<User>>; 