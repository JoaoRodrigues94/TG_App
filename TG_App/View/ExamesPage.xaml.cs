using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ExamesPage : ContentPage
  {
    int count = 0;
    public ExamesPage()
    {
      InitializeComponent();

      BindingContext = new ExamesViewModel();
    }

    public void HI (object sender, EventArgs args)
    {
      ExameGlicemia.Text = "HI";
    }
    public void LO (object sender, EventArgs args)
    {
      ExameGlicemia.Text = "LO";
    }
    public void Alimento(object sender, EventArgs args)
    {
      DBAlimento DB = new DBAlimento();

      var pesquisa = DB.PesquisarAlimento().Where(x => x.NomeAlimento.ToUpper() == NomeAlimento.Text.ToUpper()).SingleOrDefault();

      if(pesquisa == null)
      {
        DisplayAlert("ERRO", "Alimento não cadastrado", "OK");
      }
      else
      {
        count++;
        Label nome = new Label
        {
          Text = pesquisa.NomeAlimento
        };

        Entry consumo = new Entry
        {
          Placeholder = "Quantidade Consumida",
          PlaceholderColor = Color.Blue,
          HorizontalOptions = LayoutOptions.FillAndExpand
        };

        Label medida = new Label
        {
          Text = Medidas(pesquisa.Medida)
        };

        StackLayout sl = new StackLayout
        {
          Orientation = StackOrientation.Horizontal,
        };
        sl.Children.Add(nome);
        sl.Children.Add(consumo);
        sl.Children.Add(medida);

        slAlimento.Children.Add(sl);
      }
    }
    public string Medidas(int id)
    {
      string nome = "";
      if (id == 0) nome = "Unidade";
      else if (id == 1) nome = "Gramas";
      else nome = "ml";

      return nome;
    }
    public void Limpar(object sender, EventArgs args)
    {
      NomeAlimento.Text = null;
    }

    public void Delete(object sender, EventArgs args)
    {
      count--;
      if(count == -1)
      {
        DisplayAlert("Erro", "Não existe alimentos para serem excluídos", "OK");
      }
      else
      {
        slAlimento.Children.RemoveAt(count);
      }
    }
  }
}