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
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }

  }
}
