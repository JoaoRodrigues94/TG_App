using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG.Model
{
  [Table("Alimentos")]
  public class Alimento
  {
    [PrimaryKey, AutoIncrement]
    public int AlimentoID { get; set; }
    public int UsuarioID { get; set; }
    public int Medida { get; set; }
    public int Categoria { get; set; }
    public string NomeAlimento { get; set; }
    public decimal PorcaoAlimento { get; set; }
    public decimal GramasCarbo { get; set; }
  }
}
