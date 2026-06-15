namespace PrayerCycle.API.Endpoints;

public static class EndpointRegistration
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUserEndpoints();
        app.MapFamilyEndpoints();
        app.MapFamilyMemberEndpoints();
        app.MapPrayerLogEndpoints();

        return app;
    }
}
