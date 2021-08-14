using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TG.Model
{
  [Table("Horarios")]
  public class Horarios : INotifyPropertyChanged
  {
    [PrimaryKey, AutoIncrement]
    public int HorarioID { get; set; }
    public int UsuarioID { get; set; }
    public int Pickers { get; set; }
    public decimal Unidades { get; set; }
    public string Horario { get; set; }
    public string NomeMedicamento { get; set; }

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
