﻿using System;
using System.Globalization;
using TG.View;
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

      MainPage = new AtividadeFisicaPage();
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
