using MediatR;
using Talft.Dtos;

namespace Talft.Messaging;

public class UserAddedNotification : INotification
{
    public required UserDto User { get; set; }
}
