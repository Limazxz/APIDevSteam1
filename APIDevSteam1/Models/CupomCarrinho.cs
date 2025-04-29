namespace APIDevSteam1.Models
{
    public class CupomCarrinho
    {
        public DateTime DataAplicacao;

        public Guid CupomCarrinhoId { get; set; } // Chave primária
        public Guid CupomId { get; set; }
        public Guid CarrinhoId { get; set; }

        // Propriedades de navegação
        public Cupom? Cupom { get; set; }
        public Carrinho? Carrinho { get; set; }
    }
}
