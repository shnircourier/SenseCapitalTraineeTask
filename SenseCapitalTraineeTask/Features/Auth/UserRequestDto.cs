namespace SenseCapitalTraineeTask.Features.Auth;

/// <summary>
/// Тело запроса
/// </summary>
/// <param name="Username">Имя пользователя</param>
/// <param name="Password">Пароль</param>
public record UserRequestDto(string Username, string Password);