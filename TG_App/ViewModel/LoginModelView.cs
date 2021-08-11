using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TG_App;
using Xamarin.Forms;

namespace TG.ModelView
{
  class LoginModelView: INotifyPropertyChanged
  {
    public Command Cadastrar { get; set; }

    public LoginModelView()
    {
      Cadastrar = new Command(CadastrarAction);
    }

    public void CadastrarAction()
    {
      App.Current.MainPage = new View.CadastroPage();
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
