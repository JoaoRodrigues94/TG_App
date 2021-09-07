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
    public Command Validar { get; set; }
    public ExamesViewModel()
    {
      Validar = new Command(Mostrar);
    }
    public void Mostrar()
    {
      if(Calculo == 0)
      {
        BoolGlicemia = true;
        Alimento = false;
      } 
      else if(Calculo == 1)
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
