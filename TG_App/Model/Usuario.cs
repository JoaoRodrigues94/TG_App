using System;
using System.Collections.Generic;
using System.Text;

namespace TG.Model
{
  class Usuario
  {
    public int UsuarioID { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public string Idade { get; set; }
    public int TipoDiabete { get; set; }
    public int QtdMedicamento { get; set; }
    public int QtdDestros { get; set; }
    public bool Insulina { get; set; }
    public bool InsulinaLenta { get; set; }


    public string NomeInsulinaR { get; set; }
    public string NomeInsulinaL { get; set; }
    public string NomeComprimido { get; set; }
    public int Unidades { get; set; }
  }
}
