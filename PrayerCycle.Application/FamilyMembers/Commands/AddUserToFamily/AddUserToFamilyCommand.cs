using MediatR;
using PrayerCycle.Application.FamilyMembers.Dtos;

namespace PrayerCycle.Application.FamilyMembers.Commands.AddUserToFamily;

public sealed record AddUserToFamilyCommand(
    Guid FamilyId,
    string Email,
    string DisplayName,
    string Password,
    string Name,
    string AvatarColor,
    string? AvatarInitial,
    DateTime? BirthDate) : IRequest<AddUserToFamilyResultDto>;
