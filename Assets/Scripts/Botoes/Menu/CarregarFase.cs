using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarFase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void CarregarCena(string JogarTuto)
    {
        SceneManager.LoadScene(JogarTuto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
