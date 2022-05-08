using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TG_App.ViewModel
{
    class ExamesViewModel : INotifyPropertyChanged
    {
        public int _Calculo;
        public int Calculo { get { return _Calculo; } set { _Calculo = value; Mostrar(); OnPropertyChange("Calculo"); } }
        public bool _BoolGlicemia;
        public bool BoolGlicemia { get { return _BoolGlicemia; } set { _BoolGlicemia = value; OnPropertyChange("BoolGlicemia"); } }
        public bool _Alimento;
        public bool Alimento { get { return _Alimento; } set { _Alimento = value; OnPropertyChange("Alimento"); } }
        public bool _Btn;
        public bool Btn { get { return _Btn; } set { _Btn = value; OnPropertyChange("Btn"); } }
        public bool _Resultado = true;
        public bool Resultado { get { return _Resultado; } set { _Resultado = value; OnPropertyChange("Resultado"); } }

        public bool _VerResultado;
        public bool VerResultado { get { return _VerResultado; } set { _VerResultado = value; OnPropertyChange("VerResultado"); } }
        public Command Validar { get; set; }
        public Command AlimentoVisibility { get; set; }
        public Command MostrarPesquisa { get; set; }
        public Command ResultadoOk { get; set; }
        public Command Visibilidade { get; set; }
        public ExamesViewModel()
        {
            Validar = new Command(Mostrar);
            AlimentoVisibility = new Command(NaoVisualizar);
            MostrarPesquisa = new Command(MostrarPesquisaAction);
            ResultadoOk = new Command(MostraResultado);
        }
        public void Mostrar()
        {
            if (Calculo == 0)
            {
                BoolGlicemia = true;
                Alimento = false;
                Btn = false;
            }
            else if (Calculo == 1)
            {
                BoolGlicemia = false;
                Alimento = true;
            }
            else
            {
                BoolGlicemia = true;
                Alimento = true;
            }
        }
        public void MostrarPesquisaAction()
        {
            Alimento = true;
        }
        public void NaoVisualizar()
        {
            Alimento = false;
            Btn = true;
        }

        public void MostraResultado()
        {
            Resultado = false;
            VerResultado = true;
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
