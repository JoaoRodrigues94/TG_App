using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG.ModelView;
using TG_App;
using TG_App.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TG.Model.Horarios;

namespace TG.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class CadastroPage : ContentPage
  {
    int cont;
    int picker;
    DateTime TimeCadastro;
    List<Horarios> ListaDados = new List<Horarios>();
    List<Horarios> stackLayout = new List<Horarios>();
    public CadastroPage()
    {
      InitializeComponent();

      BindingContext = new CadastroModelView();

      List<StackLayout> ListaS = new Gerenciador().Listagem();
      CarregarHorarios('A');
    }
    public void DadosHorarios(StackLayout dados)
    {
      var t = (Picker)dados.Children[0];
      if(t.SelectedIndex != -1)
      {
        
        var sl = (StackLayout)dados.Children[1];

        var sqlSl = (StackLayout)sl.Children[0];
        var entrySql = (StackLayout)sl.Children[1];
        var ts = (TimePicker)sqlSl.Children[0];
        var g = (Entry)entrySql.Children[0];
        
        var y = t.SelectedIndex;
        Horarios dadosHorario = new Horarios();
        //{
        dadosHorario.Horario = Convert.ToString(ts.Time);
        dadosHorario.Pickers = t.SelectedIndex;
        dadosHorario.Unidades = Convert.ToInt32(g.Text);
        //};
        stackLayout.Add(dadosHorario);
      }
    }
    public void Salvar(object sender, EventArgs args)
    {
      List<Horarios> dadosLista = new List<Horarios>();

      if(Senha.Text != ConfirmarSenha.Text)
      {
        DisplayAlert("ERRO", "deu ruim", "ok");
      }

      if (TipoDiabete.SelectedIndex != 3)
      {
        Horarios lenta = new Horarios
        {
          Horario = Convert.ToString(HorarioL.Time),
          Pickers = 2,
          NomeMedicamento = NomeInsulinaL.Text,
          Unidades = Convert.ToDecimal(UnidadesL.Text)
        };

        dadosLista.Add(lenta);
      }


      foreach(var item in stackLayout)
      {
        Horarios dados = new Horarios
        {
          Horario = item.Horario,
          Pickers = item.Pickers,
          Unidades = item.Unidades,
          NomeMedicamento = NomeInsulinaR.Text
        };
        dadosLista.Add(dados);
      }
      new CadastroModelView().AddHorarios(dadosLista);
      App.Current.MainPage = new LoginPage();
    }
    public void CarregarHorarios(char verif)
    {
      AddHorarios.Children.Clear();
      stackLayout.Clear();

      List<StackLayout> Lista = new Gerenciador().Listagem();

      int i = 0;
      var y = picker;
      foreach (var item in Lista)
      {
        DadosHorarios(item);
        ListaStacks(i, 1, Lista);
        i++;
      }
      if (verif == 'A')
      {
        ListaStacks(cont, 0, Lista);
        cont++;
      }
    }
    public void Next(object sender, EventArgs args)
    {
      CarregarHorarios('N');
    }
    public void ListaStacks(int id, int verif, List<StackLayout> lista)
    {
      Picker p = new Picker { Title = "Selecione" };
      p.Items.Add("Exame de Glicemia e Medicação");
      p.Items.Add("Exame de Glicemia");
      p.Items.Add("Medicação");

      TimePicker tp = new TimePicker { Format = "HH:mm"};
      Entry un = new Entry { Placeholder = "Unidades" };
      Image btn2 = new Image { WidthRequest = 25, VerticalOptions = LayoutOptions.Center };
      btn2.Source = "delete.png";

      StackLayout horizontal = new StackLayout
      {
        Orientation = StackOrientation.Horizontal,
        Padding = 0,
      };

      p.PropertyChanged += delegate
      {
        horizontal.Children.Clear();
        if (p.SelectedIndex != 1)
        {
          horizontal.Children.Add(new StackLayout
          {
            Padding = 0,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Children =
          {
            tp
          }
          });
          horizontal.Children.Add(new StackLayout
          {
            Padding = 0,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Children =
          {
            un
          }
          });
        }
        else
        {
          horizontal.Children.Add(new StackLayout
          {
            Padding = 0,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            Children =
          {
            tp
          }
          });
          un.Text = "0";
          horizontal.Children.Add(new StackLayout
          {
            IsVisible = false,
            Children =
          {
            un
          }
          });
        }
      };

      var dadosLayout = new StackLayout
      {
        Padding = 0,
        Children =
            {
              p,
              horizontal
            }
      };

      if (verif == 0)
      {
        new Gerenciador().Add(dadosLayout);
        CarregarHorarios('D');
      }

      List<StackLayout> Lista = new Gerenciador().Listagem();

      foreach (var item in Lista)
      {
        AddHorarios.Children.Add(item);
      }
    }
    private void AddHorarioAcion(object sender, EventArgs args)
    {
      CarregarHorarios('A');
    }

    private void ExcluirAction(object sender, EventArgs args)
    {
      var itens = new Gerenciador().Listagem();
      var dados = itens.Count - 1;

      if (itens.Count > 1)
      {
        new Gerenciador().Deletar(dados);
        CarregarHorarios('D');
      }
    }

    public void Unidade(object sender, TextChangedEventArgs args)
    {
      int und = Convert.ToInt32(args.NewTextValue);
    }
    public void CapturaLista(object sender, EventArgs args)
    {
      Picker p = (Picker)sender;
      picker = (int)p.SelectedIndex;
    }
    public void TimeAction(object sender, TextChangedEventArgs args)
    {
      TimeCadastro = Convert.ToDateTime(args.NewTextValue);
    }
    private void ProximaPag(object sender, EventArgs args)
    {
      //Navigation.PushModalAsync(new View.CadastroMedicamentoPage());
    }
  }
  public class MaskedTelefone : Behavior<Entry>
  {
    private string _mask = "";
    public string Mask
    {
      get => _mask;
      set
      {
        _mask = value;
        SetPositions();
      }
    }

    protected override void OnAttachedTo(Entry entry)
    {
      entry.TextChanged += OnEntryTextChanged;
      base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
      entry.TextChanged -= OnEntryTextChanged;
      base.OnDetachingFrom(entry);
    }

    IDictionary<int, char> _positions;

    void SetPositions()
    {
      if (string.IsNullOrEmpty(Mask))
      {
        _positions = null;
        return;
      }

      var list = new Dictionary<int, char>();
      for (var i = 0; i < Mask.Length; i++)
        if (Mask[i] != 'X')
          list.Add(i, Mask[i]);

      _positions = list;
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
      var entry = sender as Entry;

      var text = entry.Text;

      if (string.IsNullOrWhiteSpace(text) || _positions == null)
        return;

      if (text.Length > _mask.Length)
      {
        entry.Text = text.Remove(text.Length - 1);
        return;
      }

      foreach (var position in _positions)
        if (text.Length >= position.Key + 1)
        {
          var value = position.Value.ToString();
          if (text.Substring(position.Key, 1) != value)
            text = text.Insert(position.Key, value);
        }

      if (entry.Text != text)
        entry.Text = text;
    }
  }
}