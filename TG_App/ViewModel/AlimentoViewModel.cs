using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TG_App.ViewModel
{
  class AlimentoViewModel : INotifyPropertyChanged
  {
    private int _Categoria;
    public int Categoria { get { return _Categoria; } set { _Categoria = value; OnPropertyChange("Categoria"); } }

    private int _Medida;
    public int Medida { get { return _Medida; } set { _Medida = value; OnPropertyChange("Medida"); } }

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
