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
  public partial class CadastrarAlimentoPage : ContentPage
  {
    public int UsuarioID { get; set; }
    public CadastrarAlimentoPage()
    {
      InitializeComponent();

      BindingContext = new AlimentoViewModel();
    }
    public void VoltarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new AlimentosPage();
    }
    public void SalvarAlimentoAction(object sender, EventArgs args)
    {
      string nome = Alimento.Text;
      int medida = Medida.SelectedIndex;
      decimal porcao = Convert.ToDecimal(PorcaoAlimento.Text.Replace(",", "."));
      decimal carbo = Convert.ToDecimal(GramasCarbo.Text.Replace(",", "."));
      int categoria = Categoria.SelectedIndex;
      bool next = true;
      string message = "";

      DBAlimento DB = new DBAlimento();

      if(nome == null)
      {
        next = false;
        message += "Nome do alimento é um campo obrigatório!\n";
      }
      if(porcao == 0)
      {
        next = false;
        message += "Informe um valor válido para a porção!\n";
      }
      if(carbo == 0)
      {
        next = false;
        message += "informe um valor válido para a quantidade de carboidratos!";
      }
      if (next)
      {
        var user = new Validacao().Listagem().SingleOrDefault();
        var id = DB.PesquisarAlimento().Count() + 1;
        Alimento dados = new Alimento
        {
          AlimentoID = id,
          Categoria = categoria,
          GramasCarbo = carbo,
          NomeAlimento = nome,
          Medida = medida,
          PorcaoAlimento = porcao,
          UsuarioID = user.UsuarioID
        };
        DB.CadastrarAlimento(dados);
        App.Current.MainPage = new AlimentosPage();
      }
    }

    public void Usuario(int id)
    {
      UsuarioID = id;
    }
    public void ExcluirAlimentoAction(object sender, EventArgs args)
    {

    }
  }
}