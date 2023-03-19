namespace SenseCapitalTraineeTask.Data;

/// <summary>
/// Метод расширения для запуска сидеров
/// </summary>
public static class UseDataSeederExtension
{
    public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app, Action callback)
    {
        callback.Invoke();

        return app;
    }
}