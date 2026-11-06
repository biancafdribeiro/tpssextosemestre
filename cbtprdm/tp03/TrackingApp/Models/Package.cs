// Autora: Bianca Fonseca Dantas Ribeiro - CB3025683
namespace TrackingApp.Models;

// Modelo: informações sobre um pacote
public class Package
{
    public string Id { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime SentDate { get; set; }
    public DateTime EstimatedDelivery { get; set; }
    public string CurrentLocation { get; set; } = string.Empty;
    public List<TrackingEvent> Events { get; set; } = new();
}
