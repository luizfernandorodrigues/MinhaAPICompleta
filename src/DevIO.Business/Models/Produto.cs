using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models
{
    public class Produto
    {
        public Guid FornedorId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        /*EF Relations*/
    }
}
