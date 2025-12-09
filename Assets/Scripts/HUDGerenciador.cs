using UnityEngine;
using TMPro;


public class HUDGerenciador : MonoBehaviour
{
    public TMP_Text carteira;
    public TMP_Text pneu;
    public TMP_Text ganho;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Atualizar(float dinheiro, int item, float last)
    {
        carteira.text = dinheiro.ToString("0.00");
        pneu.text = item + "/1";

        if (last != 0)
        {
            ganho.GetComponent<ganhovisual>().Ativador(true, last);
        }

       


    }


    public void AtualizarDinheiro(float dinheiro)
    {
        carteira.text = dinheiro.ToString("0.00");
        ganho.GetComponent<ganhovisual>().Ativador(true, dinheiro);
    }

    public void AtualizarPneu(int item)
    {
        pneu.text = item + "/1";
    }

    public void AtualizarGanho(float ultimo)
    {
        ganho.GetComponent<ganhovisual>().Ativador(true, ultimo);
    }

}
