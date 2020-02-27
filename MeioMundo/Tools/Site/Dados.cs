namespace Tools.Site
{
    public class Dados
    {
        public class Site
        {
            public int ID { get; set; }
            public string Ref { get; set; }
            public string Name { get; set; }
            public string Price { get; set; }
            public float Stock { get; set; }
        }

        public class Produto
        {
            public string REF { get; set; }
            public string Nome { get; set; }
            public string DescriçãoBreve { get; set; }
            public string Descrição { get; set; }
            public string Stock { get; set; }
            public string Preço { get; set; }
        }
    }
}
