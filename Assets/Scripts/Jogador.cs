using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Jogador : MonoBehaviour
{
    Animator m_Animator;


    public Mochila bolsa;
    public int pontos = 0;
    public HUDGerenciador tela;

    public float velocidade = 5f;

    private Rigidbody2D rb;
    private Vector2 direcao;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        bolsa = new Mochila();

        AtualizarHUD();
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
            bolsa.ultimoGanho = 100;
            bolsa.carteira += 100;
            Debug.Log("Você achou dinheiro!");
            Debug.Log($"Dinheiro: {bolsa.carteira}");
            Destroy(outro.gameObject);

            AtualizarDinheiro();
            
        }



        if (outro.CompareTag("Item"))
        {
            //pontos += 1000;
            itemcoletavel ic = outro.gameObject.GetComponent<itemcoletavel>();
            bolsa.MostrarMochila();

            string resposta = bolsa.adicionarItem(ic.Item);

            bolsa.MostrarMochila();
            if (resposta == "Sucesso")
            {
                Destroy(outro.gameObject);
            }
            else
            {
                Debug.Log("Falha de coletar item: " + resposta);
            }



            AtualizarPneu();

        }


    }

    public void AtualizarHUD()
    {
        //carteira
        //pneu
        int indexItem = bolsa.BuscarItem(itensJogo.Pneu.ToString());


        //tela
        //tela.Atualizar(bolsa.carteira, bolsa.itens[indexItem].quantidade, bolsa.ultimoGanho);

        AtualizarDinheiro();
        AtualizarPneu();
        AtualizarGanho();
    }

    public void AtualizarDinheiro()
    {
        tela.AtualizarDinheiro(bolsa.carteira);
        tela.AtualizarGanho(bolsa.ultimoGanho);
    }

    public void AtualizarPneu()
    {
        int indexItem = bolsa.BuscarItem(itensJogo.Pneu.ToString());
        tela.AtualizarPneu(bolsa.itens[indexItem].quantidade);
    }

    public void AtualizarGanho()
    {
        tela.AtualizarGanho(bolsa.ultimoGanho);
    }

}





//public class Local
//{
//    public string nome;
//    public string categoria;

//    public Local(string novoNome, string novaCategoria)
//    {
//        nome = novoNome;
//        categoria = novaCategoria;
//    }


//}