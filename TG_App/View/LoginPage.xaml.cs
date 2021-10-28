using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.ModelView;
using TG_App;
using TG_App.Banco;
using TG_App.Model;
using TG_App.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LoginPage : ContentPage
  {
    public LoginPage()
    {
      InitializeComponent();

      BindingContext = new ModelView.LoginModelView();
    }
    public void EntrarAction(object sender, EventArgs args)
    {
      DataBase DB = new DataBase();

      var user = DB.PesquisarEmail(Login.Text).SingleOrDefault();

      if (user == null)
      {
        DisplayAlert("ERRO", "Usuário e senha inválidos!", "OK");
      }
      else
      {
        if (user.Email == Login.Text && user.Senha == Senha.Text)
        {
          new Validacao().Add(user);
          App.Current.MainPage = new NotificacaoPage();
        }
        else
        {
          DisplayAlert("ERRO", "Usuário e senha inválido!", "OK");
        }
      }
    }
  }
}