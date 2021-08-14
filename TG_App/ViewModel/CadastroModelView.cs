using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TG.Model;
using TG.View;
using TG_App;
using TG_App.DB;
using Xamarin.Forms;

namespace TG.ModelView
{
  public class CadastroModelView : INotifyPropertyChanged
  {
    private int _ID;
    public int ID { get { return _ID; } set { _ID = value; OnPropertyChange("ID"); } }
    private string _Nome;
    public string Nome { get { return _Nome; } set { _Nome = value; OnPropertyChange("Nome"); } }
    private string _NomeEntry;
    public string NomeEntry { get { return _NomeEntry; } set { _NomeEntry = value; OnPropertyChange("Nome"); } }
    private string _Email;
    public string Email { get { return _Email; } set { _Email = value; OnPropertyChange("Email"); } }
    private string _Celular;
    public string Celular { get { return _Celular; } set { _Celular = value; OnPropertyChange("Celular"); } }
    private string _InsulinaLenta;
    public string InsulinaLenta { get { return _InsulinaLenta; } set { _InsulinaLenta = value; OnPropertyChange("InsulinaLenta"); } }
    private string _InsulinaRapida;
    public string InsulinaRapida { get { return _InsulinaRapida; } set { _InsulinaRapida = value; OnPropertyChange("InsulinaRapida"); } }
    private string _Senha;
    public string Senha { get { return _Senha; } set { _Senha = value; OnPropertyChange("Senha"); } }
    private string _ConfirmarSenha;
    public string ConfirmarSenha { get { return _ConfirmarSenha; } set { _ConfirmarSenha = value; OnPropertyChange("ConfirmarSenha"); } }
    private string _UnidadeCorrecao;
    public string UnidadeCorrecao { get { return _UnidadeCorrecao; } set { _UnidadeCorrecao = value; OnPropertyChange("UnidadeCorrecao"); } }
    private string _UnidadeGlicemia;
    public string UnidadeGlicemia { get { return _UnidadeGlicemia; } set { _UnidadeGlicemia = value; OnPropertyChange("UnidadeGlicemia"); } }
    // Números
    private string _AlimentoUni;
    public string AlimentoUni { get { return _AlimentoUni; } set { _AlimentoUni = value; OnPropertyChange("AlimentoUni"); } }
    private string _GramasCarbo;
    public string GramasCarbo { get { return _GramasCarbo; } set { _GramasCarbo = value; OnPropertyChange("GramasCarbo"); } }
    public int _TipoDiabete { get; set; }
    public int TipoDiabete { get { return _TipoDiabete; } set { _TipoDiabete = value; ProximoAction(); } }
    private int _UnidadesLenta;
    public int UnidadesLenta { get { return _UnidadesLenta; } set { _UnidadesLenta = value; OnPropertyChange("UnidadesLenta"); } }
    public int QtdeDiaR { get; set; }
    // Boolean
    private bool _Visible;
    public bool Visible { get { return _Visible; } set { _Visible = value; OnPropertyChange("Visible"); } }
    private bool _VisibleInsulinaL;
    public bool VisibleInsulinaL { get { return _VisibleInsulinaL; } set { _VisibleInsulinaL = value; OnPropertyChange("VisibleInsulinaL"); } }
    public bool _VisibleInsulinaR;
    public bool VisibleInsulinaR { get { return _VisibleInsulinaR; } set { _VisibleInsulinaR = value; OnPropertyChange("VisibleInsulinaR"); } }
    private bool _CkMedicamento;
    public bool CkMedicamento { get { return _CkMedicamento; } set { _CkMedicamento = value; OnPropertyChange("CkMedicamento"); } }
    private bool _CkHorario;
    public bool CkHorario { get { return _CkHorario; } set { _CkHorario = value; OnPropertyChange("CkHorario"); } }
    private bool _CkUnidades;
    public bool CkUnidades { get { return _CkUnidades; } set { _CkUnidades = value; OnPropertyChange("CkUnidades"); } }
    private bool _CkSenha;
    public bool CkSenha { get { return _CkSenha; } set { _CkSenha = value; OnPropertyChange("CkSenha"); } }
    private bool _Prox_1;
    public bool Prox_1 { get { return !_Prox_1; } set { _Prox_1 = value; OnPropertyChange("Prox_1"); } }
    private bool _Prox_2;
    public bool Prox_2 { get { return !_Prox_2; } set { _Prox_2 = value; OnPropertyChange("Prox_2"); } }
    private bool _Prox_3;
    public bool Prox_3 { get { return !_Prox_3; } set { _Prox_3 = value; OnPropertyChange("Prox_3"); } }
    private bool _Prox_4;
    public bool Prox_4 { get { return !_Prox_4; } set { _Prox_4 = value; OnPropertyChange("Prox_4"); } }
    // Command
    public Command Proximo { get; set; }
    public Command Salvar { get; set; }
    public Command Confirmar { get; set; }
    public Command Proximo_1 { get; set; }
    public Command Proximo_2 { get; set; }
    public Command Proximo_3 { get; set; }
    public Command Proximo_4 { get; set; }
    private List<Horarios> horas;
    public CadastroModelView()
    {
      Proximo = new Command(ProximoAction);
      Salvar = new Command(SalvarAction);
      Confirmar = new Command(ConfirmarAction);
      Proximo_1 = new Command(NextAction);
      Proximo_2 = new Command(Next2Action);
      Proximo_3 = new Command(Next3Action);
      Proximo_4 = new Command(Next4Action);
    }
    public void ProximoAction()
    {
      Visible = TipoDiabete < 2 ? false : true;
      VisibleInsulinaL = TipoDiabete <= 2 ? true : false;
      VisibleInsulinaR = TipoDiabete < 2 || TipoDiabete == 3 ? true : false;
    }
    public void SalvarAction()
    {
      if (Senha != ConfirmarSenha)
      {
        App.Current.MainPage.DisplayAlert("ERRO", "As Senhas não Conferem!", "OK");
      }
      if (Senha.Length < 6)
      {
        App.Current.MainPage.DisplayAlert("Erro", "Informe uma senha com no mínimo 6 caracteres!", "OK");
      }
      else
      {
        Usuario user = new Usuario
        {
          Nome = NomeEntry,
          Email = this.Email,
          Celular = this.Celular,
          TipoDiabete = this.TipoDiabete,
          InsulinaLenta = this.InsulinaLenta,
          UnidadesLenta = this.UnidadesLenta,
          InsulinaRapida = this.InsulinaRapida,
          AlimentoUni = Convert.ToDecimal(this.AlimentoUni),
          GramasCarbo = Convert.ToDecimal(this.GramasCarbo),
          UnidadeCorrecao = Convert.ToDecimal(this.UnidadeCorrecao),
          UnidadeGlicemia = Convert.ToDecimal(this.UnidadeGlicemia),
          Senha = this.Senha
        };
      }
    }

    public void AddHorarios(List<Horarios> id)
    {
      foreach(var item in id)
      {
        Horarios dados = new Horarios
        {
          Horario = item.Horario,
          NomeMedicamento = item.NomeMedicamento,
          Pickers = item.Pickers,
          Unidades = item.Unidades
        };
      }
    }

    public void ConfirmarAction()
    {
      int id = QtdeDiaR;
    }

    public void NextAction()
    {
      bool next = true;
      string message = "";
      if (NomeEntry == null || NomeEntry == " ")
      {
        next = false;
        message += "Nome inválido \n";
      }

      if (Email == "Email" || Email == "" || Email == null)
      {
        next = false;
        message += "Email inválido \n";
      }

      if (Celular == null || Celular == "Celular" || Celular.Length < 14)
      {
        next = false;
        message += "Número de Celular Inválido!";
      }

      if (next)
      {
        CkMedicamento = true;
        Prox_1 = true;
      }
      else
      {
        App.Current.MainPage.DisplayAlert("ERRO", message, "OK");
      }
    }
    public void Next2Action()
    {
      bool next = true;
      string message = "";
      if (TipoDiabete == 0 || TipoDiabete == 1 || TipoDiabete == 2)
      {
        if (InsulinaLenta == null || InsulinaLenta == "")
        {
          message += "Informe o Nome do Medicamento \n";
          next = false;
        }
        if(UnidadesLenta <= 0)
        {
          message += "Informe um valor de unidade válido! \n";
          next = false;
        }
        if(TipoDiabete == 0 || TipoDiabete == 1)
        {
          if(InsulinaRapida == null || InsulinaRapida == " ]")
          {
            message += "Informe o nome do medicamento de ação rápida! \n";
            next = false;
          }
        }
      }

      if(TipoDiabete == 3)
      {
        if(InsulinaRapida == null || InsulinaRapida == "")
        {
          message += "Informe o nome do medicamento de ação rápida! \n";
          next = false;
        }
      }

      if (next)
      {
        CkHorario = true;
        Prox_2 = true;
      }
      else
      {
        App.Current.MainPage.DisplayAlert("ERRO!", message, "OK");
      }
    }
    public void Next3Action()
    {
      CkUnidades = true;
      Prox_3 = true;
    }
    public void Next4Action()
    {

      CkSenha = true;
      Prox_4 = true;
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
