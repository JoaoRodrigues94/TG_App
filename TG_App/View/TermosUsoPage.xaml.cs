using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}