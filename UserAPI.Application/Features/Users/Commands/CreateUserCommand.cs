using MediatR;

namespace UserAPI.Application.Features.Users.Commands;

public record CreateUserCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
} 