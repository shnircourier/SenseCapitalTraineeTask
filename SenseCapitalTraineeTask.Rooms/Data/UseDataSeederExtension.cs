using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Rooms.Data;

/// <summary>
/// Метод расширения для запуска сидеров
/// </summary>
public static class UseDataSeederExtension
{
    /// <summary>
    /// Метод расширения
    /// </summary>
    /// <param name="app"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    [UsedImplicitly]
    public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app, Action callback)
    {
        callback.Invoke();

        return app;
    }
}