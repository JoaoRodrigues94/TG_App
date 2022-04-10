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
    class DBSugestao
    {
        // String de Conexão
        private SQLiteConnection _conexao;
        public DBSugestao()
        {
            var dep = DependencyService.Get<ICaminho>();
            string caminho = dep.ObterCaminho("diabetes.sqlite");

            _conexao = new SQLiteConnection(caminho);
            // Criar tabela
            _conexao.CreateTable<Sugestao>();
        }

        // Métodos  Cadastro
        public void Cadastrar(Sugestao sugestao)
        {
            _conexao.Insert(sugestao);
        }
        // Métodos Pesquisa
        public List<Sugestao> Pesquisar()
        {
            //.Where(x => x.UsuarioID == id).ToList();
            return _conexao.Table<Sugestao>().ToList();
        }
        // Métodos Update
        public void Update(Sugestao sugestao)
        {
            _conexao.Update(sugestao);
        }
        // Métodos Exclusão
        public void Delete(Sugestao sugestao)
        {
            _conexao.Delete(sugestao);
        }
    }
}
