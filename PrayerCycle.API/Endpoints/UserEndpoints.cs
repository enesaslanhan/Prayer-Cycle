using MediatR;
using PrayerCycle.Application.Users.Commands.CreateUser;
using PrayerCycle.Application.Users.Commands.DeleteUser;
using PrayerCycle.Application.Users.Commands.UpdateUser;
using PrayerCycle.Application.Users.Dtos;
using PrayerCycle.Application.Users.Queries.GetUserById;
using PrayerCycle.Application.Users.Queries.GetUsers;

namespace PrayerCycle.API.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").WithTags("Users");

        group
            .MapGet(string.Empty, async (ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new GetUsersQuery(), cancellationToken))
            .WithName("GetUsers")
            .WithSummary("Tüm kullanıcıları listeler")
            .WithDescription("Kayıtlı tüm kullanıcıları oluşturulma tarihine göre sıralı şekilde döner.")
            .Produces<IReadOnlyList<UserDto>>(StatusCodes.Status200OK);

        group
            .MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                await sender.Send(new GetUserByIdQuery(id), cancellationToken))
            .WithName("GetUserById")
            .WithSummary("Kullanıcı detayını getirir")
            .WithDescription("Belirtilen kimliğe sahip kullanıcıyı döner.")
            .Produces<UserDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapPost(string.Empty, async (
                ISender sender,
                CreateUserCommand createUserCommand,
                CancellationToken cancellationToken) =>
                await sender.Send(createUserCommand, cancellationToken))
            .WithName("CreateUser")
            .WithSummary("Yeni kullanıcı oluşturur")
            .WithDescription("E-posta ve görünen ad ile yeni kullanıcı kaydı oluşturur. Şifre opsiyoneldir; gönderilirse sistemde hash'lenerek saklanır.")
            .Produces<UserDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict);

        group
            .MapPut("{id:guid}", async (
                Guid id,
                ISender sender,
                UpdateUserCommand updateUserCommand,
                CancellationToken cancellationToken) =>
            {
                await sender.Send(updateUserCommand with { Id = id }, cancellationToken);
            })
            .WithName("UpdateUser")
            .WithSummary("Kullanıcıyı günceller")
            .WithDescription("Görünen ad, şifre ve aktiflik durumunu günceller. Şifre gönderilirse sistemde hash'lenerek saklanır.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group
            .MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                await sender.Send(new DeleteUserCommand(id), cancellationToken);
            })
            .WithName("DeleteUser")
            .WithSummary("Kullanıcıyı siler")
            .WithDescription("Belirtilen kimliğe sahip kullanıcıyı kalıcı olarak siler.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
