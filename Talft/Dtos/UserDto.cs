namespace Talft.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
}
