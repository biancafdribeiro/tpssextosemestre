// Autora: Bianca Fonseca Dantas Ribeiro - CB3025683
namespace TrackingApp.Models;

// Um evento do histórico de rastreamento (ex.: "Pedido postado")
public record TrackingEvent(DateTime Date, string Description, string Location);
