namespace SenseCapitalTraineeTask.Features.Auth;

/// <summary>
/// Тело ответа на запрос
/// </summary>
/// <param name="Id">ID</param>
/// <param name="Username">Имя пользователя</param>
/// <param name="Password">Пароль</param>
public record UserResponseDto(string Id, string Username, string Password);