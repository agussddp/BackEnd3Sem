using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FilmesMoura.WebAPI.Models;

[Table("fIlme")]
public partial class FIlme
{
    [Key]
    [StringLength(40)]
    [Unicode(false)]
    public string IdFilme { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Tiulo { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Imagem { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? IdGenero { get; set; }

    [ForeignKey("IdGenero")]
    [InverseProperty("FIlmes")]
    public virtual Genero? IdGeneroNavigation { get; set; }
}
