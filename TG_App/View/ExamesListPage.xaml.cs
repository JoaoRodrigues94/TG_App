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
  public partial class ExamesListPage : ContentPage
  {
    public ExamesListPage()
    {
      InitializeComponent();
    }

    public void Sugestao(object sender, EventArgs args)
    {
      App.Current.MainPage = new ExamesPage();
    }
    public void RegistrarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new RegistrarExamePage();
    }
  }
}