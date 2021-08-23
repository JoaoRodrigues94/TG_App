using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AlimentosEditPage : ContentPage
  {
    public AlimentosEditPage(Alimento dados)
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
      Alimento dados = new Alimento
      {
        AlimentoID = Convert.ToInt32(Codigo.Text),
        Categoria = Categoria.SelectedIndex,
        GramasCarbo = Convert.ToDecimal(GramasCarbo.Text),
        Medida = Medida.SelectedIndex,
        NomeAlimento = Alimento.Text,
        PorcaoAlimento = Convert.ToDecimal(PorcaoAlimento.Text),
        UsuarioID = 1
      };

      DataBase DB = new DataBase();
      DB.UpdateAlimento(dados);

      App.Current.MainPage = new AlimentosPage();
    }

    public void VoltarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new AlimentosPage();
    }
  }
}