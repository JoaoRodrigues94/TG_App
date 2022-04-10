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
using TG_App.Banco;
using TG_App.Model;
using TG_App.View;
using TG_App.View.Utils;
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
            //CarregarHorarios('A');
        }
        public void DadosHorarios(StackLayout dados)
        {
            var t = (Picker)dados.Children[0];
            if (t.SelectedIndex != -1)
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

            if (Senha.Text.Length < 6)
            {
                DisplayAlert("ERRO", "A senha deve conter no mínimo 6 caracteres!", "OK");
            }
            else
            {


                if (Senha.Text != ConfirmarSenha.Text)
                {
                    DisplayAlert("ERRO", "As Senhas não conferem!", "OK");
                }
                else
                {
                    DataBase DB = new DataBase();
                    Usuario user = new Usuario
                    {
                        Nome = Nome.Text,
                        Email = Email.Text,
                        Celular = Celular.Text,
                        TipoDiabete = TipoDiabete.SelectedIndex,
                        InsulinaLenta = NomeInsulinaL.Text,
                        UnidadesLenta = Convert.ToDecimal(UnidadesL.Text),
                        InsulinaRapida = NomeInsulinaR.Text,
                        AlimentoUni = Convert.ToDecimal(UniAlimento.Text),
                        GramasCarbo = Convert.ToDecimal(Carboidratos.Text),
                        UnidadeCorrecao = Convert.ToDecimal(Correcao.Text),
                        UnidadeGlicemia = Convert.ToDecimal(GlicemiaUnd.Text),
                        Senha = Senha.Text
                    };
                    DB.CadastrarUsuario(user);
                    new Validacao().Add(user);

                    App.Current.MainPage = new NavigationPage(new Master());
                }
            }
        }
        public void Voltar(object sender, EventArgs args)
        {
            App.Current.MainPage = new LoginPage();
        }
        private void ExcluirAction(object sender, EventArgs args)
        {
            var itens = new Gerenciador().Listagem();
            var dados = itens.Count - 1;

            if (itens.Count > 1)
            {
                new Gerenciador().Deletar(dados);
                //CarregarHorarios('D');
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