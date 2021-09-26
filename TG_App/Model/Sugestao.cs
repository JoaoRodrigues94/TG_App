using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.Model
{
  [Table("Sugestao")]
  public class Sugestao
  {
    [AutoIncrement, PrimaryKey]
    public int SugestaoID { get; set; }
    public int TipoSugestao { get; set; }
    public string Tipo { get; set; }
    public int UsuarioID { get; set; }
    public string Resultado { get; set; }
    public int Dosagem { get; set; }
    public string Observacao { get; set; }
    public DateTime Data { get; set; }
    public string Horario { get; set; }
  }
}
