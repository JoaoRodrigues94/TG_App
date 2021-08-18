using System;
using TG_App.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      MainPage = new AlimentosPage();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
  }
}
