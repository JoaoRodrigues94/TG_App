using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TG.Model;
using TG_App.Model;
using Xamarin.Forms;

namespace TG_App.ViewModel
{
    class AgendaViewModel : INotifyPropertyChanged
    {
        private int _Status;
        public int Status { get { return _Status; } set { _Status = value; OnPropertyChange("Status"); } }
        public int AgendaID { get; set; }
        public string Descrição { get; set; }
        public string Local { get; set; }
        public string Data { get; set; }
        public string Horario { get; set; }
        public string Observacao { get; set; }
        public int UsuarioID { get; set; }

        public Command Update { get; set; }

        public AgendaViewModel()
        {
            Update = new Command(UpdateStatus);
        }

        public void UpdateStatus()
        {
            var user = new Validacao().Listagem().SingleOrDefault();

            Agenda dados = new Agenda
            {
                Descrição = Descrição,
                Local = Local,
                Data = Data.Substring(0, 10),
                Horario = Data.Substring(12, 5),
                Status = Status,
                Observacao = Observacao,
                UsuarioID = user.UsuarioID
            };
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
