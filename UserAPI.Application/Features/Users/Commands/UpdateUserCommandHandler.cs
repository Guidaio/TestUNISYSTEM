using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Abstractions;

namespace UserAPI.Application.Features.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (user == null)
            return false;

        user.Name = request.Name;
        user.Email = request.Email;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
} 