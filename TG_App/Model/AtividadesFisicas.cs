using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.Model
{
  [Table("AtividadesFisicas")]
  class AtividadesFisicas
  {
    [AutoIncrement,PrimaryKey]
    public int AtividadeFisicaID { get; set; }
    public int UsuarioID { get; set; }
    public string NomeAtividade { get; set; }
    public DateTime Data { get; set; }
    public string Inicio { get; set; }
    public string Fim { get; set; }
    public string Observacao { get; set; }

  }
}
