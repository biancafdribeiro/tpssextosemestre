using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlContainers.Models;

[Table("CONTAINER")]
public class Container
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Informe o número do container")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O número deve ter exatamente 11 caracteres")]
    [Display(Name = "Número")]
    public string Numero { get; set; }

    [Required(ErrorMessage = "Informe o tipo")]
    [RegularExpression("Dry|Reefer", ErrorMessage = "Tipo deve ser Dry ou Reefer")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "Informe o tamanho")]
    [Range(20, 40, ErrorMessage = "Tamanho deve ser 20 ou 40")]
    public int Tamanho { get; set; }

    [Required(ErrorMessage = "O container deve estar associado a um BL")]
    [Column("BL_ID")]
    [Display(Name = "BL")]
    public int? BLId { get; set; }

    [ForeignKey("BLId")]
    public BL BL { get; set; }
}
