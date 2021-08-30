using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ListagemAtividadesPage : ContentPage
  {
    public ListagemAtividadesPage()
    {
      InitializeComponent();

      DBExercicios DB = new DBExercicios();

      var lista = DB.PesquisarAtividade();
    }
    public void CadastrarAtividadeAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new AtividadeFisicaPage();
    }
  }
}