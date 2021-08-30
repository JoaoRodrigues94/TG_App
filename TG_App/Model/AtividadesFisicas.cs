using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.Model
{
  class AtividadesFisicas
  {
    public int AtividadeFisicaID { get; set; }
    public int UsuarioID { get; set; }
    public string NomeAtividade { get; set; }
    public DateTime Data { get; set; }
    public string Inicio { get; set; }
    public string Fim { get; set; }
    public string Observacoao { get; set; }

  }
}
