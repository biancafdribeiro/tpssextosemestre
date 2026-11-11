using System.ComponentModel.DataAnnotations;

namespace Patrimonio.Api.Dtos;

public record BemRequest(
    [Required, MaxLength(150)] string Descricao,
    [Required, MaxLength(60)] string Categoria,
    [MaxLength(100)] string Local,
    DateOnly DataAquisicao,
    [Range(0.01, double.MaxValue)] decimal ValorAquisicao,
    [Range(1, 100)] int VidaUtilAnos,
    [Required, MaxLength(20)] string Status
);

public record BemResponse(
    int Id,
    string Tombamento,
    string Descricao,
    string Categoria,
    string Local,
    DateOnly DataAquisicao,
    decimal ValorAquisicao,
    int VidaUtilAnos,
    string Status,
    decimal ValorAtual
);
