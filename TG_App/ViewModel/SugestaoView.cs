using System;
using System.Collections.Generic;
using System.Text;

namespace TG_App.ViewModel
{
  class SugestaoView
  {
    public int SugestaoID { get; set; }
    public string TipoSugestao { get; set; }
    public int UsuarioID { get; set; }
    public string Resultado { get; set; }
    public string Dosagem { get; set; }
    public string Observacao { get; set; }
    public string Data { get; set; }
    public char Identificacao { get; set; }
  }
}
