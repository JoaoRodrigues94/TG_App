using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.Model
{
  [Table("SugestaoAlimento")]
  public class SugestaoAlimento
  {
    [AutoIncrement, PrimaryKey]
    public int SugestaoAlimentoID { get; set; }
    public int SugestaoID { get; set; }
    public int UsuarioID { get; set; }
    public string Nome { get; set; }
    public string Categoria { get; set; }
    public decimal Consumo { get; set; }
  }
}
