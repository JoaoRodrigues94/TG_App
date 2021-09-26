using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.Model
{
  [Table("Exame")]
  public class Exame
  {
    [AutoIncrement, PrimaryKey]
    public int ExameID { get; set; }
    public int UsuarioID { get; set; }
    public string Resultado { get; set; }
    public DateTime Data { get; set; }
    public string Horario { get; set; }
    public string Observacao { get; set; }
  }
}
