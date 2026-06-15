using MediatR;

namespace PrayerCycle.Application.Families.Commands.DeleteFamily;

public sealed record DeleteFamilyCommand(Guid Id) : IRequest;
