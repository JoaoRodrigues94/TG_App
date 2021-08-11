using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TG.View;
using Xamarin.Forms;

namespace TG_App.ViewModel
{
  public class TermosUsoViewModel : INotifyPropertyChanged
  {
    private bool _Status;
    public bool Status { get { return _Status; } set { _Status = value; OnPropertyChange("Status"); } }

    public Command Next { get; set; }
    public Command Behind { get; set; }

    public TermosUsoViewModel()
    {
      Next = new Command(NextPage);
      Behind = new Command(BehindPage);
    }

    public void NextPage()
    {
      if (Status)
        App.Current.MainPage = new CadastroPage();
      else
        App.Current.MainPage.DisplayAlert("Erro", "Leia e concorde com os termos de uso para prosseguir!", "OK");
    }
    public void BehindPage()
    {
      App.Current.MainPage = new LoginPage();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChange(string param)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(param));
      }
    }
  }
}
