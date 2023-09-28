using BuildingBlock.Application.IntegrationEvents.Events;

namespace People.Application.IntegrationEvents.Events;

public record AccountCreatedIntegrationEvent(Guid AccountId, string Name, string? Email) : IntegrationEvent
{
    public Guid AccountId = AccountId;
    public string? Email = Email;
    public string Name = Name;
}