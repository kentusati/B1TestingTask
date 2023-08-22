using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Entities;

[Table("tasktable")]
public partial class TaskTable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public string LatinSymbols { get; set; } = null!;

    public string RusSymbols { get; set; } = null!;
    [Column("number")]
    public int Number { get; set; }
    [Column("float")]
    public decimal Float { get; set; }
}
