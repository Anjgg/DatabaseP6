using Database.Context;

namespace Database.Context
{
    public static class Extension
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedData.Initialize(scope.ServiceProvider);
            }
            return app;
        }
    }
}
