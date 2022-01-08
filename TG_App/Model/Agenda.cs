using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TG.Model
{
    [Table("Agenda")]
    public class Agenda
    {
        [AutoIncrement, PrimaryKey]
        public int AgendaID { get; set; }
        public string Descrição { get; set; }
        public string Local { get; set; }
        public string Data { get; set; }
        public string Horario { get; set; }
        public int Status { get; set; }
        public string Observacao { get; set; }
        public int UsuarioID { get; set; }
    }
}
