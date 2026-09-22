using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlContainers.Models;

[Table("BL")]
public class BL
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Informe o número do BL"), StringLength(255)]
    [Display(Name = "Número")]
    public string Numero { get; set; }

    [StringLength(255)]
    public string Consignee { get; set; }

    [StringLength(255)]
    public string Navio { get; set; }

    public List<Container> Containers { get; set; } = new();
}
