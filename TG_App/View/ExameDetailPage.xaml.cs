using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Model;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ExameDetailPage : ContentPage
  {
    public ExameDetailPage(Sugestao e, List<SugestaoAlimento> lista)
    {
      InitializeComponent();
      DataExame.Text = e.Data.ToString("dd/MM/yyyy HH:mm");
      ExameGlicemia.Text = "Resultado: " + e.Resultado;
      Observacao.Text = "Observação: " + e.Observacao;

      foreach(var item in lista)
      {
        Label lblResult = new Label();

        lblResult.Text = "Alimento: " + item.Nome + ", Consumo: " + item.Consumo + " " + item.Categoria;
        slAlimento.Children.Add(lblResult);
      }
      Label result = new Label();
      result.Text = "Sugestão de Dosagem: " + e.Dosagem + " Unidades";
      slAlimento.Children.Add(result);
    }
    public ExameDetailPage(Exame e)
    {
      InitializeComponent();
      DataExame.Text = e.Data.ToString("dd/MM/yyyy HH:mm");
      ExameGlicemia.Text = "Resultado: " + e.Resultado;
      Observacao.Text = "Observação: " + e.Observacao;

      Label dosagem = new Label();
      dosagem.Text = "Medicação - " + e.Dosagem.ToString() + " Unidades";
      slAlimento.Children.Add(dosagem);
    }
    private string Tipo(int id)
    {
      var result = "";
      if(id == 0)
      {
        result = "Correção de Glicemia";
      }
      else if(id == 1)
      {
        result = "Correção para alimentos";
      }
      else
      {
        result = "Correção de glicemia e alimentos";
      }
      return result;
    }
    public void VoltarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new ExamesListPage();
    }
  }
}