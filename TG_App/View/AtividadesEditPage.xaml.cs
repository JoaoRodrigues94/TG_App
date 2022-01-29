using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using TG_App.View.Utils;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AtividadesEditPage : ContentPage
  {
    public AtividadesEditPage(AtividadesFisica dados)
    {
      InitializeComponent();

      DataCadastro.Text = dados.Data.ToString("dd/MM/yyyy");
      Atividade.Text = dados.NomeAtividade;
      Inicio.Time = TimeSpan.Parse(dados.Inicio);
      Termino.Time = TimeSpan.Parse(dados.Fim);
      Observacao.Text = dados.Observacao;
      id.Text = dados.AtividadeFisicaID.ToString();
    }

    public void SalvarAction(object sender, EventArgs args)
    {
      var user = new Validacao().Listagem().SingleOrDefault();
      bool next;
      string message = "";

      int dia = Convert.ToInt32(DataCadastro.Text.Substring(0, 2));
      int mes = Convert.ToInt32(DataCadastro.Text.Substring(3, 2));
      int ano = Convert.ToInt32(DataCadastro.Text.Substring(6, 4));

      next = dia > 31 || dia < 1 ? false : true;
      next = mes > 12 || mes < 1 ? false : true;
      next = ano < 2021 ? false : true;

      message = next ? "" : "A data informada é inválida!\n";

      if (Atividade.Text == null)
      {
        next = false;
        message += "O nome da atividade deve ser informado!\n";
      }

      if (Inicio.Time >= Termino.Time)
      {
        next = false;
        message += "A data de término deve ser maior do que a data de inicio!";
      }

      if (next)
      {
        DBExercicios DB = new DBExercicios();

        AtividadesFisica dados = new AtividadesFisica
        {
          UsuarioID = user.UsuarioID,
          Data = Convert.ToDateTime(mes + "/" + dia + "/" + ano),
          Inicio = Inicio.Time.ToString(),
          Fim = Termino.Time.ToString(),
          NomeAtividade = Atividade.Text,
          Observacao = Observacao.Text,
          AtividadeFisicaID = Convert.ToInt32(id.Text)
        };

        DB.UpdateAtividade(dados);
        DisplayAlert("", "Dados atualizados com sucesso!", "OK");
        App.Current.MainPage = new Master("AtividadesFisicas");
      }
      else
      {
        DisplayAlert("ERRO!", message, "OK");
      }
    }

    public void VoltarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new Master("AtividadesFisicas");
    }
  }
}