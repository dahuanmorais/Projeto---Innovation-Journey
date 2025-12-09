using UnityEngine;

public class localtrabalho : MonoBehaviour
{
    public float ganhoMes;
    public Jogador j1;
    public bool trabalhando;
    public HUDGerenciador hudtela;

    public float intervalo = 10.0f;
    private float proximaExecucao = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (trabalhando)
        {

            if (Time.time >= proximaExecucao)
            {
                Recebe();
                proximaExecucao = Time.time + intervalo;
            }

        }


    }

    public void Recebe()
    {

        float somaSaldo = ganhoMes;
        j1.bolsa.carteira += somaSaldo;
        Debug.Log("ganhou" + somaSaldo);
        hudtela.AtualizarGanho(j1.bolsa.carteira);
        hudtela.AtualizarDinheiro(j1.bolsa.carteira);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Jogador"))
        {
            trabalhando = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Jogador"))
        {
            trabalhando = false;
        }

    }
}
