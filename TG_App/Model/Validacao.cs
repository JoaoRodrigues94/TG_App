using System;
using System.Collections.Generic;
using System.Text;
using TG.Model;

namespace TG_App.Model
{
  class Validacao
  {
    private List<Usuario> Lista { get; set; }
    public void Add(Usuario index)
    {
      Lista = Listagem();
      Lista.Add(index);

      SalvarNoProperties(Lista);
    }
    public List<Usuario> Listagem()
    {

      return ListagemProperties();
    }
    private void SalvarNoProperties(List<Usuario> Lista)
    {
      if (App.Current.Properties.ContainsKey("User"))
      {
        App.Current.Properties.Remove("User");
      }
      App.Current.Properties.Add("User", Lista);
    }
    private List<Usuario> ListagemProperties()
    {
      if (App.Current.Properties.Count > 0)
      {
        return (List<Usuario>)App.Current.Properties["User"];
      }
      return new List<Usuario>();
    }
  }
}
