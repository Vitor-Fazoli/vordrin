namespace Domain.Interfaces;

public interface IUserRepository<UserDto>
{
    Task<UserDto> GetByIdAsync(Guid id);
    Task AddAsync(UserDto entity);
    Task UpdateAsync(UserDto entity);
    Task DeleteAsync(Guid id);
}