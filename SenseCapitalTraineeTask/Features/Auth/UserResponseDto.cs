namespace SenseCapitalTraineeTask.Features.Auth;

/// <summary>
/// Тело ответа на запрос
/// </summary>
/// <param name="Id">ID</param>
/// <param name="Username">Логин</param>
/// <param name="Password">Пароль</param>
public record UserResponseDto(Guid Id, string Username, string Password);