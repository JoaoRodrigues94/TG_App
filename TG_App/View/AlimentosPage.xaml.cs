﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AlimentosPage : ContentPage
  {
    public AlimentosPage()
    {
      InitializeComponent();
    }

    public void CadastrarAlimentoAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new CadastrarAlimentoPage();
    }
  }
}