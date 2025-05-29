using MediatR;

namespace UserAPI.Application.Features.Users.Commands;

public record UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
} 