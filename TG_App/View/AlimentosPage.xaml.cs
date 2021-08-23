using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AlimentosPage : ContentPage
  {
    List<ListaAlimentosViewModel> lista { get; set; }
    public AlimentosPage()
    {
      InitializeComponent();
      DBAlimento DB = new DBAlimento();

      var user = new Validacao().Listagem().SingleOrDefault();

      List<ListaAlimentosViewModel> dadosLista = new List<ListaAlimentosViewModel>();

      var cadastrados = DB.PesquisarAlimento().Where(x => x.UsuarioID == user.UsuarioID).ToList();
      foreach (var item in cadastrados)
      {
        var x = item.UsuarioID;
        ListaAlimentosViewModel dados = new ListaAlimentosViewModel
        {
          AlimentoID = Convert.ToInt32(item.AlimentoID),
          Alimento = Convert.ToString(item.NomeAlimento),
          Carboidratos = "Carbs:" + Convert.ToString(item.GramasCarbo),
          Categoria = Categoria(item.Categoria)
        };
        dadosLista.Add(dados);
      }


      ListaAlimentos.ItemsSource = dadosLista;
      lista = dadosLista;
    }

    public void ExcluirAction(object sender, EventArgs args)
    {
      Button btn = (Button)sender;
      ListaAlimentosViewModel lista = btn.CommandParameter as ListaAlimentosViewModel;
      DataBase DB = new DataBase();
      var encontrar = DB.PesquisarAlimento().SingleOrDefault(x => x.NomeAlimento == lista.Alimento);
      DB.DeleteAlimento(encontrar);
      App.Current.MainPage = new AlimentosPage();
    }

    public void EditarAction(object sender, EventArgs args)
    {
      Button btn = (Button)sender;
      ListaAlimentosViewModel lista = btn.CommandParameter as ListaAlimentosViewModel;

      DBAlimento DB = new DBAlimento();
      var dados = DB.PesquisarAlimento().Where(x => x.NomeAlimento == lista.Alimento).SingleOrDefault();

      App.Current.MainPage = new AlimentosEditPage(dados);
    }
    public void PesquisaAction(object sender, EventArgs args)
    {
      var busca = lista;

      if (!String.IsNullOrEmpty(SearchAlimento.Text))
        busca = (List<ListaAlimentosViewModel>)busca.Where(x => x.Alimento.ToUpper().Contains(SearchAlimento.Text.ToUpper())).ToList();

      ListaAlimentos.ItemsSource = busca;
    }
    public static string Categoria(int categoria)
    {
      string nome = "";
      if (categoria == 0) nome = "Cereais, Pães e Tubérculos";
      else if (categoria == 1) nome = "Hortaliças";
      else if (categoria == 2) nome = "Frutas";
      else if (categoria == 3) nome = "Leguminosas";
      else if (categoria == 4) nome = "Carnes e Ovos";
      else if (categoria == 5) nome = "Leite e Derivados";
      else if (categoria == 6) nome = "Óleos e Gorduras";
      else nome = "Açúcares";

      return nome;
    }
    public static string Medidas(int medida)
    {
      string nome = "";
      if (medida == 0) nome = "Unidade";
      else if (medida == 1) nome = "Gramas";
      else nome = "ml";

      return nome;
    }
    public void CadastrarAlimentoAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new CadastrarAlimentoPage();
    }
  }
}