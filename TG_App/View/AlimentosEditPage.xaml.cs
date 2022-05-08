using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using TG_App.View.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AlimentosEditPage : ContentPage
  {
    public AlimentosEditPage(Food dados)
    {
      InitializeComponent();

      Codigo.Text = Convert.ToString(dados.AlimentoID);
      Alimento.Text = dados.NomeAlimento;
      Medida.SelectedIndex = dados.Medida;
      PorcaoAlimento.Text = Convert.ToString(dados.PorcaoAlimento);
      GramasCarbo.Text = Convert.ToString(dados.GramasCarbo);
      Categoria.SelectedIndex = dados.Categoria;
    }

    public void SalvarAction(object sender, EventArgs args)
    {
      var user = new Validacao().Listagem().SingleOrDefault();
      Food dados = new Food
      {
        AlimentoID = Convert.ToInt32(Codigo.Text),
        Categoria = Categoria.SelectedIndex,
        GramasCarbo = Convert.ToDecimal(GramasCarbo.Text.Replace(",", ".")),
        Medida = Medida.SelectedIndex,
        NomeAlimento = Alimento.Text,
        PorcaoAlimento = Convert.ToDecimal(PorcaoAlimento.Text),
        UsuarioID = user.UsuarioID
      };

      DBAlimento DB = new DBAlimento();
      DB.UpdateAlimento(dados);

      App.Current.MainPage = new Master("AlimentosPage");
    }

    public void VoltarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new Master("AlimentosPage");
    }
  }
}