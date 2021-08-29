using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ExamesPage : ContentPage
  {
    public ExamesPage()
    {
      InitializeComponent();
      BindingContext = new AtividadeFisicaViewModel();
    }
  }
}