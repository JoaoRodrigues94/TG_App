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

            // TODO - Modificar ao alterar o bando de alimentos
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
            decimal c = user.UnidadeGlicemia;
            int resultado = 0;
            string returning = "";
            bool verification = true;

            if (TipoCalculo.SelectedIndex == 0 || TipoCalculo.SelectedIndex == 2)
            {
                int x = ExameGlicemia.Text == "HI" ? 600 : (ExameGlicemia.Text == "LO" ? 20 : Convert.ToInt32(ExameGlicemia.Text));

                if (x > 600 || x < 20)
                {
                    verification = false;
                }

                int result = 0;
                if (x > 180) result = CalculoGlicemia(x, c);
                else if (x < 70)
                {
                    returning = "Para evitar uma sugestão de unidadas que possa causar uma hipoglicemia severa, o sistema impediu que fosse realizado os cálculos de sugestão!";
                }

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
                    var alm = DB2.PesquisarAlimento().Where(j => j.NomeAlimento.ToUpper() == nome).FirstOrDefault();
                    var y = Convert.ToDecimal(x.Text);
                    var grm = (alm.GramasCarbo * y) / alm.PorcaoAlimento;
                    soma += grm;
                    i++;
                }
                int retorno = CalculoAlimento(soma, user.GramasCarbo, user.AlimentoUni);
                resultado += retorno;
            }
            string horas = DataExame.Text.Substring(11, 4);


            Sugestao dados = new Sugestao()
            {
                UsuarioID = user.UsuarioID,
                Data = DateTime.Now,
                TipoSugestao = TipoCalculo.SelectedIndex,
                Resultado = ExameGlicemia.Text,
                Observacao = Observacao.Text,
                Dosagem = resultado,
                Confirmar = false
            };

            DB.Cadastrar(dados);


            DBSugestaoAlimento DBs = new DBSugestaoAlimento();
            for (var i = 0; i < slAlimento.Children.Count(); i++)
            {
                var lits = (StackLayout)slAlimento.Children[i];
                var lbl = (Label)lits.Children[0];
                var ent = (Entry)lits.Children[1];
                var lblG = (Label)lits.Children[2];
                SugestaoAlimento sql = new SugestaoAlimento
                {
                    SugestaoID = dados.SugestaoID,
                    UsuarioID = user.UsuarioID,
                    Consumo = Convert.ToDecimal(ent.Text),
                    Categoria = lblG.Text,
                    Nome = lbl.Text
                };
                DBs.Cadastrar(sql);
            }

            returning = String.IsNullOrEmpty(returning) ? resultado.ToString() : returning;

            if (verification)
            {
                //DisplayAlert("Sugestão", returning + " Unidades!", "Ok");
                //App.Current.MainPage = new NavigationPage(new Master("ExameList"));

                Resultado.Text = "Sugestão: " + returning + " Unidades";

            }
            else
            {
                DisplayAlert("Erro", "Informe um valor válido!", "Ok");
                //App.Current.MainPage = new ExamesPage();
            }
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
                if ((x) >= unidades)
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

        public void ConfirmarDados(object sender, EventArgs args)
        {
            var user = new Validacao().Listagem().SingleOrDefault();
            DBSugestao DB = new DBSugestao();

            DB.Pesquisar().LastOrDefault();

            Sugestao dados = new Sugestao
            {
                Aplicado = Convert.ToInt32(ConfirmarAplicacao.Text),
                Confirmar = true
            };

            DB.Update(dados);
            App.Current.MainPage = new NavigationPage(new Master("ExameList"));
        }
        public void VoltarAction(object sender, EventArgs args)
        {
            App.Current.MainPage = new NavigationPage(new Master("ExameList"));
        }
    }
}