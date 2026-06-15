using MediatR;
using PrayerCycle.Application.FamilyMembers.Commands.CreateFamilyMember;
using PrayerCycle.Application.FamilyMembers.Commands.DeleteFamilyMember;
using PrayerCycle.Application.FamilyMembers.Commands.UpdateFamilyMember;
using PrayerCycle.Application.FamilyMembers.Dtos;
using PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMemberById;
using PrayerCycle.Application.FamilyMembers.Queries.GetFamilyMembers;

namespace PrayerCycle.API.Endpoints;

public static class FamilyMemberEndpoints
{
    public static IEndpointRouteBuilder MapFamilyMemberEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/family-members").WithTags("Family Members");

        group
            .MapGet(string.Empty, async (
                Guid familyId,
                ISender sender,
                CancellationToken cancellationToken) =>
                await sender.Send(new GetFamilyMembersQuery(familyId), cancellationToken))
            .WithName("GetFamilyMembers")
            .WithSummary("Aile üyelerini listeler")
            .WithDescription("Belirtilen aile kimliğine bağlı tüm üyeleri döner.")
            .Produces<IReadOnlyList<FamilyMemberDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new GetFamilyMemberByIdQuery(id), cancellationToken))
            .WithName("GetFamilyMemberById")
            .WithSummary("Aile üyesi detayını getirir")
            .WithDescription("Belirtilen kimliğe sahip aile üyesini döner.")
            .Produces<FamilyMemberDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapPost(string.Empty, async (
                ISender sender,
                CreateFamilyMemberCommand createFamilyMemberCommand,
                CancellationToken cancellationToken) =>
                await sender.Send(createFamilyMemberCommand, cancellationToken))
            .WithName("CreateFamilyMember")
            .WithSummary("Yeni aile üyesi oluşturur")
            .WithDescription("Ebeveyn veya çocuk profili olarak yeni aile üyesi ekler.")
            .Produces<FamilyMemberDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group
            .MapPut("{id:guid}", async (
                Guid id,
                ISender sender,
                UpdateFamilyMemberCommand updateFamilyMemberCommand,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(updateFamilyMemberCommand with { Id = id }, cancellationToken);
            })
            .WithName("UpdateFamilyMember")
            .WithSummary("Aile üyesini günceller")
            .WithDescription("Üye adı, avatar bilgileri, doğum tarihi ve aktiflik durumunu günceller.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeleteFamilyMemberCommand(id), cancellationToken);
            })
            .WithName("DeleteFamilyMember")
            .WithSummary("Aile üyesini siler")
            .WithDescription("Belirtilen kimliğe sahip aile üyesini kalıcı olarak siler.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
