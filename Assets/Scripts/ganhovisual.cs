using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ganhovisual : MonoBehaviour
{
    public Vector3 poseInicial;
    public int distancia;
    public bool lucro;
    public float velocidade;
    public float velo;
    public bool visivel;
    public TMP_Text texto;
    public Color corGanho;
    public Color corPerda;
    public float alphaTransicao;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        poseInicial = transform.position;
        texto = GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(visivel)
        {
            velocidade = (lucro) ? velo : -velo;

            transform.position += Vector3.up * velocidade * Time.deltaTime;
            Vector3.Distance(transform.position, poseInicial);

            alphaTransicao -= 1.1F * Time.deltaTime;
            Color novaCor = texto.color;
            novaCor.a = alphaTransicao;
            texto.color = novaCor;


        }

        if (alphaTransicao <= 0)
        {
            Terminar();
        }


    }


    public void Ativador(bool ganho, float valor)
    {
        visivel = true;
        lucro = ganho;

        transform.position = poseInicial;
        texto.color = (lucro) ? corGanho : corPerda;
        texto.text = (lucro) ? "+" + valor : "-" + valor;
        alphaTransicao = 1;

    }


    void Terminar()
    {
        visivel = false;
        texto.color = new Color(1,1,1,0);
        
    }


}
