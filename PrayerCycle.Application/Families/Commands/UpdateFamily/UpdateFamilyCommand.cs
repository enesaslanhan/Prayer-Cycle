using MediatR;

namespace PrayerCycle.Application.Families.Commands.UpdateFamily;

public sealed record UpdateFamilyCommand(
    Guid Id,
    string Name,
    int MaxMembers,
    string? InviteCode) : IRequest;
