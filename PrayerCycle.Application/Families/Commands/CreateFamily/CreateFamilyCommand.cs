using MediatR;
using PrayerCycle.Application.Families.Dtos;

namespace PrayerCycle.Application.Families.Commands.CreateFamily;

public sealed record CreateFamilyCommand(
    Guid OwnerUserId,
    string Name,
    int? MaxMembers) : IRequest<FamilyDto>;
