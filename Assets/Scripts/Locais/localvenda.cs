using UnityEngine;

public class localvenda : MonoBehaviour
{
    public string nome;
    public float bonus = 1;
    public Despesas despesas;
    

    public string[] itensCompra;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Jogador"))
        {

            Jogador j1 = collision.GetComponent<Jogador>();

            j1.bolsa.Venda(itensJogo.Pneu, bonus);
            j1.AtualizarHUD();

        }




    }

    //compras produtos

    //compra produto

}
