using System.ComponentModel.DataAnnotations;

namespace Patrimonio.Api.Models;

public class Bem
{
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Tombamento { get; set; } = "";

    [Required, MaxLength(150)]
    public string Descricao { get; set; } = "";

    [Required, MaxLength(60)]
    public string Categoria { get; set; } = "";

    [MaxLength(100)]
    public string Local { get; set; } = "";

    public DateOnly DataAquisicao { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal ValorAquisicao { get; set; }

    [Range(1, 100)]
    public int VidaUtilAnos { get; set; } = 10;

    [Required, MaxLength(20)]
    public string Status { get; set; } = "Ativo";
}
