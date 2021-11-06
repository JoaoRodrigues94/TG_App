using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TG.Model;
using TG_App.Banco;
using TG_App.Model;
using Xamarin.Forms;

namespace TG_App.Banco
{
  class DataBase
  {
    // String de Conexão
    private SQLiteConnection _conexao;
    public DataBase()
    {
      var dep = DependencyService.Get<ICaminho>();
      string caminho = dep.ObterCaminho("diabetes.sqlite");

      _conexao = new SQLiteConnection(caminho);
      // Criar tabela
      _conexao.CreateTable<Usuario>();
      _conexao.CreateTable<Horarios>();
      _conexao.CreateTable<Alimento>();
    }

    // Métodos  Cadastro
    public void CadastrarUsuario(Usuario user)
    {
      _conexao.Insert(user);
    }
    public void CadastrarHorario(Horarios horario)
    {
      _conexao.Insert(horario);
    }
    public void CadastrarAlimento(Alimento alimento)
    {
      _conexao.Insert(alimento);
    }
    // Métodos Pesquisa
    public List<Usuario> PesquisarEmail(string email)
    {

      return _conexao.Table<Usuario>().Where(x => x.Email == email).ToList();
    }
    public List<Usuario> GetUsuarios()
    {
      return _conexao.Table<Usuario>().ToList();
    }

    public List<Horarios> GetHorarios()
    {
      return _conexao.Table<Horarios>().ToList();
    }
    public List<Alimento> PesquisarAlimento()
    {
      //.Where(x => x.UsuarioID == id).ToList();
      return _conexao.Table<Alimento>().ToList(); 
    }
    // Métodos Update
    public void UpdateUsuario(Usuario user)
    {
      _conexao.Update(user);
    }
    public void UpdateHorario(Horarios horarios)
    {
      _conexao.Update(horarios);
    }
    public void UpdateAlimento(Alimento alimento)
    {
      _conexao.Update(alimento);
    }
    // Métodos Exclusão
    public void DeleteUsuario(Horarios horario)
    {
      _conexao.Delete(horario);
    }
  }
}
