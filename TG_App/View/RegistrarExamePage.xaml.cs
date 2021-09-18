using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Banco;
using TG_App.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class RegistrarExamePage : ContentPage
  {
    public RegistrarExamePage()
    {
      InitializeComponent();

      Data.Text = DateTime.Now.ToString("dd/MM/yyyy");
      Minutos.Time = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));
    }
    public void HI(object sender, EventArgs args)
    {
      ExameGlicemia.Text = "HI";
    }
    public void LO(object sender, EventArgs args)
    {
      ExameGlicemia.Text = "LO";
    }
    public void VoltarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new ExamesListPage();
    }
    public void SalvarAction(object sender, EventArgs args)
    {
      var exame = ExameGlicemia.Text;
      bool next = true;
      string message = "";

      if(exame == null || exame == "")
      {
        next = false;
        message = "Informe um valor válido para o resultado do exame!";
      }
      if(exame != "HI" && exame != "LO" && exame != null && exame != "")
      {
        int val = Convert.ToInt32(exame);

        if(val > 599 || val < 20)
        {
          next = false;
          message = "O valor informado é inválido!";
        }
      }
      if (next)
      {
        var user = new Validacao().Listagem().SingleOrDefault();

        DBExame DB = new DBExame();


        var horas = Convert.ToString(Minutos.Time);

        Exame dados = new Exame
        {
          Resultado = ExameGlicemia.Text,
          UsuarioID = user.UsuarioID,
          Data = Data.Text + " " + horas,
          Observacao = Observacao.Text
        };

        DB.Cadastrar(dados);
        DisplayAlert("Sucesso", "Exame Cadastrado!", "Continuar");
      }
      else
      {
        DisplayAlert("ERRO", message, "OK");
      }
    }
  }
}