using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.Model
{
  [Table("Sugestao")]
  class Sugestao
  {
    [AutoIncrement, PrimaryKey]
    public int SugestaoID { get; set; }
    public string TipoSugestao { get; set; }
    public int UsuarioID { get; set; }
    public string Resultado { get; set; }
    public DateTime Data { get; set; }
  }
}
