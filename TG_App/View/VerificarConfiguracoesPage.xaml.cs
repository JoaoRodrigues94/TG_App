using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG_App.Model;
using TG_App.View.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificarConfiguracoesPage : ContentPage
    {
        public VerificarConfiguracoesPage()
        {
            InitializeComponent();
        }

        public void VoltarAction(object sender, EventArgs args)
        {
            App.Current.MainPage = new NavigationPage(new Master());
        }
        public void ConfirmarAction(object sender, EventArgs args)
        {
            var user = new Validacao().Listagem().SingleOrDefault();


            if (Senha.Text == user.Senha)
                App.Current.MainPage = new NavigationPage(new Master("Configuracoes"));
            else
                DisplayAlert("Erro", "Senha inválida", "Ok");
        }
    }
}