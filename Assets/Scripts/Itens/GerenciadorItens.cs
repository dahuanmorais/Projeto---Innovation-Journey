using UnityEditor.Experimental.GraphView;
using UnityEngine;


public enum itensJogo
{
   
    Pneu,
    Outro

}


public class GerenciadorItens : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



public class Item
{
    public string nome;
    public string descricao;
    public int valorMin;
    public int valorMax;
    public int valorAtual;
    public string categoria;
    public int quantidade;
    public bool podeUsar;
    public int tamanho;

    public Item(string novoNome, string novaDescricao, int novoValorMin, int novoValorMax, string novaCategoria, int novaQuantidade, bool novoPodeUsar, int novoTamanho)
    {
        nome = novoNome;
        descricao = novaDescricao;
        valorMin = novoValorMin;
        valorMax = novoValorMax;
        categoria = novaCategoria;
        quantidade = novaQuantidade;
        podeUsar = novoPodeUsar;
        tamanho = novoTamanho;

    }

    public void Resetar()
    {
        quantidade = 0;
    }

    public void SortearValor()
    {
        valorAtual = Random.Range(valorMin, valorMax);
    }



}



public class Mochila
{
    public Item[] itens;
    public int limiteCargaMax = 100;
    public int limiteRestante;
    public float carteira;
    public float ultimoGanho;

    public Mochila()
    {
        PrepararListaItens();
    }
    void PrepararListaItens()
    {
        itens = new Item[] {
            new Item("Pneu", "é um pneu que pode ser vendido na borracharia", 800, 1000, "borracha", 0, false, 100 ),
            new Item("Pneumático", "é um pneu que pode ser vendido na borracharia", 800, 1000, "borracha", 0, false, 100 )
        };

        limiteRestante = limiteCargaMax;
    }



    public int BuscarItem(string nomeItem)
    {
        //percorrer array e retornar a index
        for (int i = 0; i < itens.Length; i++)
        {
            if (itens[i].nome == nomeItem)
            {
                return i;
            }
        }

        return -1;
    }

    public string adicionarItem(itensJogo nome)
    {
        //tenho item
        int indexItem = BuscarItem(nome.ToString());
        if (indexItem == -1)
        {
            return "não achou";
        }

        //posso adicionar (verificar se limite > pesoItem)
        if (limiteRestante < itens[indexItem].tamanho)
        {

            return "Tamanho nao cabivel";

        }

        //adicionar
        itens[indexItem].quantidade++;
        limiteRestante -= itens[indexItem].tamanho;
        return "Sucesso";
    }


    public void MostrarMochila()
    {
        
        Debug.Log(limiteRestante + " " + itens[0].nome + " " + itens[0].quantidade);

    }


    public void Venda (itensJogo coisaPraVenda, float bonus)
    {

        int indexItem = BuscarItem(coisaPraVenda.ToString());
        if (indexItem == -1)
        {
            Debug.Log("Item não existivel");
            return;
        }

        //venda
        itens[indexItem].SortearValor();
        ultimoGanho = itens[indexItem].quantidade * bonus * itens[indexItem].valorAtual;
        carteira += ultimoGanho;
        carteira = (float)System.Math.Round(carteira, 2);
        Debug.Log("Você vendeu " + itens[indexItem].quantidade  + " tantos " + coisaPraVenda.ToString() + ultimoGanho);

        //limpar
        itens[indexItem].quantidade = 0;
        limiteRestante += itens[indexItem].tamanho;

    }


}
