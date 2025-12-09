using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Despesas : MonoBehaviour
{

    public int dificuldade;
    public float custoVida;
    public int Funcionarios;
    public float contasEmpresa;
    public float imposto;
    public float multas;
    public float salario;
    public float salarios;
    public int nivelEmpresa;
    public Jogador j1;
    public HUDGerenciador hudtela;
    public CarregarFase perdeu;


    public float fechaMes;


    public float intervalo = 10.0f; // Tempo entre execuções (em segundos)
    private float proximaExecucao = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= proximaExecucao)
        {
            Contas();
            proximaExecucao = Time.time + intervalo;
        }


    }

    public void Contas()
    {

        imposto = nivelEmpresa * 200;
        salarios = salario * Funcionarios;
        float somaCusto = custoVida + salarios + imposto;
        j1.bolsa.carteira -= somaCusto  ;
        Debug.Log("perdeu" + somaCusto);
        
        hudtela.AtualizarDinheiro(j1.bolsa.carteira);


        if (j1.bolsa.carteira <= -6000)
        {

            Debug.Log("voce faliu");
            
            SceneManager.LoadScene("Menu");


        }

    }


}
