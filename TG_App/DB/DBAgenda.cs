using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TG.Model;
using TG_App.Banco;
using Xamarin.Forms;

namespace TG_App.DB
{
    class DBAgenda
    {
        // String de Conexão
        private SQLiteConnection _conexao;
        public DBAgenda()
        {
            var dep = DependencyService.Get<ICaminho>();
            string caminho = dep.ObterCaminho("diabetes.sqlite");

            _conexao = new SQLiteConnection(caminho);
            // Criar tabela
            _conexao.CreateTable<Agenda>();
        }

        // Métodos  Cadastro
        public void CadastrarAgenda(Agenda agenda)
        {
            _conexao.Insert(agenda);
        }
        // Métodos Pesquisa
        public List<Agenda> PesquisarAgenda()
        {
            //.Where(x => x.UsuarioID == id).ToList();
            return _conexao.Table<Agenda>().ToList();
        }
        // Métodos Update
        public void UpdateAgenda(Agenda agenda)
        {
            _conexao.Update(agenda);
        }
        // Métodos Exclusão
        public void DeleteAgenda(Agenda agenda)
        {
            _conexao.Delete(agenda);
        }
    }
}
