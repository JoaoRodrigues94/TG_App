using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using TG_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ExamesPage : ContentPage
  {
    int count = 0;
    public ExamesPage()
    {
      InitializeComponent();

      DataExame.Text = "Data: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
      BindingContext = new ExamesViewModel();
    }

    public void HI(object sender, EventArgs args)
    {
      ExameGlicemia.Text = "HI";
    }
    public void LO(object sender, EventArgs args)
    {
      ExameGlicemia.Text = "LO";
    }
    public void Alimento(object sender, EventArgs args)
    {
      DBAlimento DB = new DBAlimento();

      var pesquisa = DB.PesquisarAlimento().Where(x => x.NomeAlimento.ToUpper() == NomeAlimento.Text.ToUpper()).SingleOrDefault();

      if (pesquisa == null)
      {
        DisplayAlert("ERRO", "Alimento não cadastrado", "OK");
      }
      else
      {
        count++;
        Label nome = new Label
        {
          Text = pesquisa.NomeAlimento
        };

        Entry consumo = new Entry
        {
          Placeholder = "Quantidade Consumida",
          PlaceholderColor = Color.Blue,
          HorizontalOptions = LayoutOptions.FillAndExpand
        };

        Label medida = new Label
        {
          Text = Medidas(pesquisa.Medida)
        };

        StackLayout sl = new StackLayout
        {
          Orientation = StackOrientation.Horizontal,
        };
        sl.Children.Add(nome);
        sl.Children.Add(consumo);
        sl.Children.Add(medida);

        slAlimento.Children.Add(sl);
      }
    }
    public string Medidas(int id)
    {
      string nome = "";
      if (id == 0) nome = "Unidade";
      else if (id == 1) nome = "Gramas";
      else nome = "ml";

      return nome;
    }
    public void Limpar(object sender, EventArgs args)
    {
      NomeAlimento.Text = null;
    }

    public void Delete(object sender, EventArgs args)
    {
      count--;
      if (count == -1)
      {
        DisplayAlert("Erro", "Não existe alimentos para serem excluídos", "OK");
      }
      else
      {
        slAlimento.Children.RemoveAt(count);
      }
    }
    public void CalcularAction(object sender, EventArgs args)
    {
      DBSugestao DB = new DBSugestao();
      DBAlimento DB2 = new DBAlimento();
      var user = new Validacao().Listagem().SingleOrDefault();
      decimal c = user.UnidadeCorrecao;
      int resultado = 0;

      if (TipoCalculo.SelectedIndex == 0 || TipoCalculo.SelectedIndex == 2)
      {
        int x = ExameGlicemia.Text == "HI" ? 600 : (ExameGlicemia.Text == "LO" ? 20 : Convert.ToInt32(ExameGlicemia.Text));
        int result = 0;
        if (x > 180) result = CalculoGlicemia(x, c);

        resultado += result;
      }
      if (TipoCalculo.SelectedIndex == 1 || TipoCalculo.SelectedIndex == 2)
      {
        int i = 0;
        decimal soma = 0;
        foreach (var item in slAlimento.Children)
        {
          StackLayout sl = (StackLayout)slAlimento.Children[i];
          var n = (Label)sl.Children[0];
          string nome = n.Text.ToUpper();
          var x = (Entry)sl.Children[1];
          var y = Convert.ToDecimal(x.Text);
          soma += y;
          i++;
        }
        int retorno = CalculoAlimento(soma, user.GramasCarbo, user.AlimentoUni);
        resultado += retorno;
      }

      Sugestao dados = new Sugestao()
      {
        UsuarioID = user.UsuarioID,
        Data = DataExame.Text,
        TipoSugestao = TipoCalculo.SelectedIndex, 
        Resultado = ExameGlicemia.Text,
        Observacao = Observacao.Text,
        Dosagem = resultado
      };

      DB.Cadastrar(dados);

      DisplayAlert("Sugestão", resultado.ToString() + " Unidades!", "Ok");
      App.Current.MainPage = new ExamesListPage();
    }
    private int CalculoGlicemia(int x, decimal carbs)
    {
      var ret = 0;
      decimal media = x;
      for (var i = 1; i < x; i++)
      {
        media -= carbs;
        if (media >= 80 && media <= 170)
        {
          ret = i;
          if (media < 80) ret = i--;
          break;
        }
      }
      return ret;
    }

    private int CalculoAlimento(decimal x, decimal y, decimal uni)
    {
      decimal unidades = y / uni;
      int val = 0;

      for (var i = 1; i < Convert.ToInt32(x); i++)
      {
        if (x >= unidades)
        {
          x -= unidades;
          val++;
        }
        else
        {
          break;
        }
      }
      return val;
    }
    public void VoltarAction(object sender, EventArgs args)
    {
      App.Current.MainPage = new ExamesListPage();
    }
  }
}