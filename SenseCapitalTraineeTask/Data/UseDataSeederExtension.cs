namespace SenseCapitalTraineeTask.Data;

public static class UseDataSeederExtension
{
    public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app, Action callback)
    {
        callback.Invoke();

        return app;
    }
}