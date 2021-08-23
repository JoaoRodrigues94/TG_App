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
  class DBAlimento
  {
    // String de Conexão
    private SQLiteConnection _conexao;
    public DBAlimento()
    {
      var dep = DependencyService.Get<ICaminho>();
      string caminho = dep.ObterCaminho("diabetes.sqlite");

      _conexao = new SQLiteConnection(caminho);
      // Criar tabela
      _conexao.CreateTable<Alimento>();
    }

    // Métodos  Cadastro
    public void CadastrarAlimento(Alimento alimento)
    {
      _conexao.Insert(alimento);
    }
    // Métodos Pesquisa
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
    // Métodos Exclusão
    public void DeleteAlimento(Alimento alimento)
    {
      _conexao.Delete(alimento);
    }
  }
}
