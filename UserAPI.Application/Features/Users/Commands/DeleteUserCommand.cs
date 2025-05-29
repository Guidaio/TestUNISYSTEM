using MediatR;

namespace UserAPI.Application.Features.Users.Commands;

public record DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; init; }
} 