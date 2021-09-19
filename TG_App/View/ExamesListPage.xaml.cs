using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using TG_App.Model;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ExamesListPage : ContentPage
  {
    public ExamesListPage()
    {
      InitializeComponent();

      DBExame DB = new DBExame();
      DBSugestao DB2 = new DBSugestao();
      var user = new Validacao().Listagem().SingleOrDefault();
      var lista = DB.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();
      var lista2 = DB2.Pesquisar().Where(c => c.UsuarioID == user.UsuarioID).ToList();

      List<SugestaoView> dados = new List<SugestaoView>();
      foreach (var item in lista)
      {
        SugestaoView x = new SugestaoView
        {
          Data = "DATA: " + item.Data,
          Resultado = "RESULTADO DO EXAME: " +  item.Resultado,
          TipoSugestao = "E"
        };
        dados.Add(x);
      }
      foreach (var item in lista2)
      {
        SugestaoView x = new SugestaoView
        {
          Data = item.Data + ":00",
          Resultado = "RESULTADO DO EXAME: " + item.Resultado,
          Dosagem = "SUGESTÃO DE DOSAGEM: " + item.Dosagem.ToString() + " UNIDADES",
          TipoSugestao = "S"
        };
        dados.Add(x);
      }

      var ret = dados.OrderBy(c => c.Data);
      ListaExame.ItemsSource = ret;

    }

    public void Sugestao(object sender, EventArgs args)
    {
      App.Current.MainPage = new ExamesPage();
    }
    public void RegistrarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new RegistrarExamePage();
    }

    public void Pesquisar(object sender, EventArgs args)
    {
      DBExame DB = new DBExame();
      DBSugestao DB2 = new DBSugestao();
      var user = new Validacao().Listagem().SingleOrDefault();

      var listaE = DB.Pesquisar().ToList();
      var listaS = DB2.Pesquisar().ToList();

      if (!String.IsNullOrEmpty(DataSearch.Text))
      {
        listaE = listaE.Where(x => x.Data.Contains(DataSearch.Text)).ToList();
        listaS = listaS.Where(x => x.Data.Contains(DataSearch.Text)).ToList();
      }

      if (!String.IsNullOrEmpty(ResultadoSearch.Text))
      {
        listaE = listaE.Where(x => x.Resultado == ResultadoSearch.Text).ToList();
        listaS = listaS.Where(x => x.Resultado == ResultadoSearch.Text).ToList();
      }

      if (!String.IsNullOrEmpty(SugestaoSearch.Text))
      {
        listaE = new List<Exame>();
        listaS = listaS.Where(x => x.Dosagem == Convert.ToInt32(SugestaoSearch.Text)).ToList();
      }

      List<SugestaoView> dados = new List<SugestaoView>();

      foreach (var item in listaE)
      {
        SugestaoView x = new SugestaoView
        {
          Data = "DATA: " + item.Data,
          Resultado = "RESULTADO DO EXAME: " + item.Resultado,
          TipoSugestao = "E"
        };
        dados.Add(x);
      }
      foreach (var item in listaS)
      {
        SugestaoView x = new SugestaoView
        {
          Data = item.Data + ":00",
          Resultado = "RESULTADO DO EXAME: " + item.Resultado,
          Dosagem = "SUGESTÃO DE DOSAGEM: " + item.Dosagem.ToString() + " UNIDADES",
          TipoSugestao = "S"
        };
        dados.Add(x);
      }

      ListaExame.ItemsSource = dados.OrderBy(x => x.Data);
      DataSearch.Text = null;
      ResultadoSearch.Text = null;
      SugestaoSearch.Text = null;
    }
  }
}