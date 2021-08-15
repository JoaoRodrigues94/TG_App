using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TermosUsoPage : ContentPage
  {
    public TermosUsoPage()
    {
      InitializeComponent();

      BindingContext = new ViewModel.TermosUsoViewModel();
    }
    public void Validacao(object sender, EventArgs args)
    {
      if (Termos.IsChecked)
      {
        App.Current.MainPage = new CadastroPage();
      }
      else
      {
        DisplayAlert("Erro", "Leia e concorde com os termos de uso para prosseguir!", "OK");
      }
    }
  }
}