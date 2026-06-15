using MediatR;
using PrayerCycle.Application.PrayerLogs.Commands.CreatePrayerLog;
using PrayerCycle.Application.PrayerLogs.Commands.DeletePrayerLog;
using PrayerCycle.Application.PrayerLogs.Commands.UpdatePrayerLog;
using PrayerCycle.Application.PrayerLogs.Dtos;
using PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogById;
using PrayerCycle.Application.PrayerLogs.Queries.GetPrayerLogs;

namespace PrayerCycle.API.Endpoints;

public static class PrayerLogEndpoints
{
    public static IEndpointRouteBuilder MapPrayerLogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/prayer-logs").WithTags("Prayer Logs");

        group
            .MapGet(string.Empty, async (
                Guid familyMemberId,
                DateOnly? date,
                ISender sender,
                CancellationToken cancellationToken) =>
                await sender.Send(new GetPrayerLogsQuery(familyMemberId, date), cancellationToken))
            .WithName("GetPrayerLogs")
            .WithSummary("Namaz kayıtlarını listeler")
            .WithDescription("Belirtilen aile üyesine ait namaz kayıtlarını döner. date parametresi ile güne göre filtrelenebilir.")
            .Produces<IReadOnlyList<PrayerLogDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new GetPrayerLogByIdQuery(id), cancellationToken))
            .WithName("GetPrayerLogById")
            .WithSummary("Namaz kaydı detayını getirir")
            .WithDescription("Belirtilen kimliğe sahip namaz kaydını döner.")
            .Produces<PrayerLogDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapPost(string.Empty, async (
                ISender sender,
                CreatePrayerLogCommand createPrayerLogCommand,
                CancellationToken cancellationToken) =>
                await sender.Send(createPrayerLogCommand, cancellationToken))
            .WithName("CreatePrayerLog")
            .WithSummary("Yeni namaz kaydı oluşturur")
            .WithDescription("Aile üyesi için yeni bir namaz kaydı ekler.")
            .Produces<PrayerLogDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapPut("{id:guid}", async (
                Guid id,
                ISender sender,
                UpdatePrayerLogCommand updatePrayerLogCommand,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(updatePrayerLogCommand with { Id = id }, cancellationToken);
            })
            .WithName("UpdatePrayerLog")
            .WithSummary("Namaz kaydını günceller")
            .WithDescription("Namaz durumu, türü, kaza bilgisi ve notu günceller.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeletePrayerLogCommand(id), cancellationToken);
            })
            .WithName("DeletePrayerLog")
            .WithSummary("Namaz kaydını siler")
            .WithDescription("Belirtilen kimliğe sahip namaz kaydını kalıcı olarak siler.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
