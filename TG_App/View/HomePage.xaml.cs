using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Model;
using TG_App.DB;
using TG_App.Model;
using TG_App.View.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TG_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            DBAgenda DB = new DBAgenda();
            var user = new Validacao().Listagem().SingleOrDefault();

            var dadosData = DB.PesquisarAgenda().Where(c => c.UsuarioID == user.UsuarioID).ToList();


            List<Agenda> dadosDT = new List<Agenda>();
            foreach (var item in dadosData)
            {
                string dia = item.Data.Substring(0, 2);
                string mes = item.Data.Substring(3, 2);
                string ano = item.Data.Substring(6, 4);

                string newDate = mes + "/" + dia + "/" + ano;

                item.Data = newDate;

                dadosDT.Add(item);
            };
            var dados = dadosDT.Where(c => c.Status == 0).OrderBy(c => Convert.ToDateTime(c.Data + " " + c.Horario)).OrderBy(c => c.Status).ToList();

            List<Agenda> lstAgenda = new List<Agenda>();
            if (dados.Count == 0)
            {
                Agenda dadosnulos = new Agenda();
                dadosnulos.Descrição = "Nenhum Evento Pendente.";

                lstAgenda.Add(dadosnulos);
             }


            foreach (var item in dados)
            {
                string dia = item.Data.Substring(0, 2);
                string mes = item.Data.Substring(3, 2);
                string ano = item.Data.Substring(6, 4);

                string newDate = mes + "/" + dia + "/" + ano;

                item.Data = newDate;

                Agenda dadosAgenda = new Agenda
                {
                    AgendaID = item.AgendaID,
                    Data = "Data: " + item.Data + " " + item.Horario,
                    Descrição = item.Descrição + " - " + item.Data + " " + item.Horario,
                    Local = item.Local,
                    Observacao = item.Observacao,
                    Horario = item.Horario,
                    Status = item.Status
                };
                lstAgenda.Add(dadosAgenda);
            }

            Agenda.ItemsSource = lstAgenda;
        }

        public void Atualizar(object sender, EventArgs args)
        {
            try
            {
                var user = new Validacao().Listagem().SingleOrDefault();
                DBAgenda DB = new DBAgenda();

                Button btn = (Button)sender;
                Agenda lista = btn.CommandParameter as Agenda;
                var busca = DB.PesquisarAgenda().SingleOrDefault(x => x.UsuarioID == user.UsuarioID && x.AgendaID == lista.AgendaID);

                Agenda dados = new Agenda
                {
                    AgendaID = busca.AgendaID,
                    Descrição = busca.Descrição,
                    Local = busca.Horario,
                    Data = busca.Data,
                    Horario = busca.Horario,
                    Status = busca.Status == 0 ? 1 : 0,
                    UsuarioID = user.UsuarioID
                };

                DB.UpdateAgenda(dados);
                App.Current.MainPage = new Master("AgendaPage");
            }
            catch
            {
                DisplayAlert("Erro", "Não foi possível alterar os dados!", "Ok");
            }
        }
    }
}