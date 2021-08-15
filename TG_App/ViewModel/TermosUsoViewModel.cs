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

    public Command Behind { get; set; }

    public TermosUsoViewModel()
    {
      Behind = new Command(BehindPage);
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
