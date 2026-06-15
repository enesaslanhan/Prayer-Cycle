using MediatR;
using PrayerCycle.Application.Families.Commands.CreateFamily;
using PrayerCycle.Application.Families.Commands.DeleteFamily;
using PrayerCycle.Application.Families.Commands.UpdateFamily;
using PrayerCycle.Application.Families.Dtos;
using PrayerCycle.Application.Families.Queries.GetFamilies;
using PrayerCycle.Application.Families.Queries.GetFamilyById;

namespace PrayerCycle.API.Endpoints;

public static class FamilyEndpoints
{
    public static IEndpointRouteBuilder MapFamilyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/families").WithTags("Families");

        group
            .MapGet(string.Empty, async (
                Guid? ownerUserId,
                ISender sender,
                CancellationToken cancellationToken) =>
                await sender.Send(new GetFamiliesQuery(ownerUserId), cancellationToken))
            .WithName("GetFamilies")
            .WithSummary("Aileleri listeler")
            .WithDescription("Tüm aileleri döner. ownerUserId parametresi ile sahip kullanıcıya göre filtrelenebilir.")
            .Produces<IReadOnlyList<FamilyDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new GetFamilyByIdQuery(id), cancellationToken))
            .WithName("GetFamilyById")
            .WithSummary("Aile detayını getirir")
            .WithDescription("Belirtilen kimliğe sahip aileyi döner.")
            .Produces<FamilyDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapPost(string.Empty, async (
                ISender sender,
                CreateFamilyCommand createFamilyCommand,
                CancellationToken cancellationToken) =>
                await sender.Send(createFamilyCommand, cancellationToken))
            .WithName("CreateFamily")
            .WithSummary("Yeni aile oluşturur")
            .WithDescription("Belirtilen sahip kullanıcı için yeni bir aile grubu oluşturur.")
            .Produces<FamilyDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapPut("{id:guid}", async (
                Guid id,
                ISender sender,
                UpdateFamilyCommand updateFamilyCommand,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(updateFamilyCommand with { Id = id }, cancellationToken);
            })
            .WithName("UpdateFamily")
            .WithSummary("Aileyi günceller")
            .WithDescription("Aile adı, üye limiti ve davet kodunu günceller.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeleteFamilyCommand(id), cancellationToken);
            })
            .WithName("DeleteFamily")
            .WithSummary("Aileyi siler")
            .WithDescription("Belirtilen kimliğe sahip aileyi kalıcı olarak siler.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
