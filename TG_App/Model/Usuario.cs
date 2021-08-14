using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG.Model
{
  [Table("Usuarios")]
  class Usuario
  {
    [PrimaryKey, AutoIncrement]
    public int UsuarioID { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public string Senha { get; set; }
    public string Idade { get; set; }
    public int TipoDiabete { get; set; }
    public string InsulinaLenta { get; set; }
    public string InsulinaRapida { get; set; }
    public decimal UnidadesLenta { get; set; }
    public decimal AlimentoUni { get; set; }
    public decimal GramasCarbo { get; set; }
    public decimal UnidadeCorrecao { get; set; }
    public decimal UnidadeGlicemia { get; set; }
  }
}
