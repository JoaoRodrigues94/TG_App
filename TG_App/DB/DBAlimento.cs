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
            _conexao.CreateTable<Food>();
        }

        // Métodos  Cadastro
        public void CadastrarAlimento(Food alimento)
        {
            _conexao.Insert(alimento);
        }
        // Métodos Pesquisa
        public List<Food> PesquisarAlimento()
        {
            //.Where(x => x.UsuarioID == id).ToList();
            return _conexao.Table<Food>().ToList();
        }
        // Métodos Update
        public void UpdateAlimento(Food alimento)
        {
            _conexao.Update(alimento);
        }
        // Métodos Exclusão
        public void DeleteAlimento(Food alimento)
        {
            _conexao.Delete(alimento);
        }
    }
}
