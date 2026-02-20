using MediatR;
using UserAPI.Core.Entities;

namespace UserAPI.Application.Features.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<User?>;
