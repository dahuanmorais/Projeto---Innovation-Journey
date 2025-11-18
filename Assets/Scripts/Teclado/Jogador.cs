using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Jogador : MonoBehaviour
{
    Animator m_Animator;


    public Mochila bolsa;
    public int pontos = 0;

    public float velocidade = 5f;

    private Rigidbody2D rb;
    private Vector2 direcao;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();


        Debug.Log($"Dinheiro: {pontos}");
        bolsa = new Mochila();
    }

    



    void Update()
    {
        float moverX = Input.GetAxisRaw("Horizontal");
        float moverY = Input.GetAxisRaw("Vertical");

        direcao = new Vector2(moverX, moverY);

        m_Animator.SetFloat("horizontal", moverX);
        //m_Animator.SetFloat("vertical", moverY);




    }
        

    void FixedUpdate()
    {
        // Move o jogador com base na direção e velocidade
        rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
    }


    void OnTriggerEnter2D(Collider2D outro)
    {
        // Mensagem de qual objeto foi tocado
        Debug.Log("Você colidiu com o Objeto de Nome: " + outro.name + " e Tag: " + outro.tag);

        // 1. Coletar Moeda
        if (outro.CompareTag("Dinheiro"))
        {
            pontos += 100;
            Debug.Log("Você achou dinheiro!");
            Debug.Log($"Dinheiro: {pontos}");
            Destroy(outro.gameObject);
        }



        if (outro.CompareTag("Item"))
        {
            //pontos += 1000;
            itemcoletavel ic = outro.gameObject.GetComponent<itemcoletavel>();
            bolsa.MostrarMochila();

            string resposta = bolsa.adicionarItem(ic.nome);

            bolsa.MostrarMochila();
            if (resposta == "Sucesso")
            {
                Destroy(outro.gameObject);
            }
            else
            {
                Debug.Log("Falha de coletar item: " + resposta);
            }
           
            


        }


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

    public Item(string novoNome,string novaDescricao, int novoValorMin, int novoValorMax, string novaCategoria, int novaQuantidade, bool novoPodeUsar, int novoTamanho)
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
    public int limiteCargaMax = 100 ;
    public int limiteRestante;

    public Mochila()
    {
        PrepararListaItens();
    }
    void PrepararListaItens()
    {
        itens = new Item[] {
            new Item("Pneu", "é um pneu que pode ser vendido na borracharia", 800, 100, "borracha", 0, false, 100 ),
            new Item("Pneumático", "é um pneu que pode ser vendido na borracharia", 800, 100, "borracha", 0, false, 100 )
        };

        limiteRestante = limiteCargaMax;
    }



    public int BuscarItem(string nomeItem)
    {
        //percorrer array e retornar a index
        for (int i = 0; i < itens.Length; i++)
        {
            if(itens[i].nome == nomeItem)
            {
                return i;
            }
        }

        return -1;
    }

    public string adicionarItem(string nome)
    {
        //tenho item
        int indexItem = BuscarItem(nome);
        if (indexItem == -1)
        {
            return "não achou";
        }

        //posso adicionar (verificar se limite > pesoItem)
        if(limiteRestante < itens[indexItem].tamanho )
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

    public class Local
    {
        public string nome;
        public string categoria;

        public Local(string novoNome, string novaCategoria)
        {
            nome = novoNome;
            categoria = novaCategoria;
        }

        
    }
}