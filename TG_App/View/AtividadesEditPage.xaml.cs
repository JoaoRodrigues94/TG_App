using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AtividadesEditPage : ContentPage
  {
    public AtividadesEditPage(AtividadesFisicas dados)
    {
      InitializeComponent();
    }
  }
}