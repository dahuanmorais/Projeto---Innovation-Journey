using UnityEngine;

public class NivelEmpresa : MonoBehaviour
{
    public Despesas despesas;
    public localvenda caixa;
    public HUDGerenciador hudtela;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag("Jogador"))
        {
            Jogador j1 = coli.gameObject.GetComponent<Jogador>();
            if (j1.bolsa.carteira >= 3000)
            {
                despesas.nivelEmpresa ++ ;
                despesas.Funcionarios ++ ;
                caixa.bonus++;
                j1.bolsa.carteira -= 3000;
                hudtela.AtualizarGanho(j1.bolsa.carteira);
                hudtela.AtualizarDinheiro(j1.bolsa.carteira);

            }
            
        }
    }
}
