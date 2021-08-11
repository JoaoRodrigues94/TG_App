using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TG_App;
using Xamarin.Forms;

namespace TG.Model
{
  public class Gerenciador
  {
    private List<StackLayout> Lista { get; set; }
    private int ID { get; set; }

    public void Deletar(int id)
    {
      Lista = Listagem();
      Lista.RemoveAt(id);

      SalvarNoProperties(Lista);
    }
    public void Add(StackLayout index)
    {
      Lista = Listagem();
      Lista.Add(index);

      SalvarNoProperties(Lista);
    }
    public List<StackLayout> Listagem()
    {

      return ListagemProperties();
    }
    private void SalvarNoProperties(List<StackLayout> Lista)
    {
      if (App.Current.Properties.ContainsKey("Tarefas"))
      {
        App.Current.Properties.Remove("Tarefas");
      }
      App.Current.Properties.Add("Tarefas", Lista);
    }
    private List<StackLayout> ListagemProperties()
    {
      if (App.Current.Properties.Count > 0)
      {
        return (List<StackLayout>)App.Current.Properties["Tarefas"];
      }
      return new List<StackLayout>();
    }
  }
}
