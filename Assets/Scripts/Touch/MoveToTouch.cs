using System;
using UnityEngine;
using UnityEngine.InputSystem; // Importa a biblioteca do novo Input System.

/// <summary>
/// Move o objeto suavemente em direção ao ponto do último toque usando o novo Input System.
/// </summary>
public class MoveToTouch : MonoBehaviour
{

    Animator m_Animator;


    [Tooltip("Velocidade de movimento do objeto.")]
    public float velocidade = 5f;

    private Camera cam;
    private Vector3 destino;
    private ControlesDeToque controles;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

    }
    private void Awake()
    {
        cam = Camera.main;
        controles = new ControlesDeToque();
        destino = transform.position; // Começa parado.
    }

    private void OnEnable()
    {
        controles.Enable();
        // Subscreve ao evento 'performed' da action 'ContatoPrimario'.
        // Este evento dispara uma única vez quando o toque/clique acontece.
        controles.Toque.ContatoPrimario.performed += ctx => DefinirDestino(ctx);
    }

    private void OnDisable()
    {
        controles.Toque.ContatoPrimario.performed -= ctx => DefinirDestino(ctx);
        controles.Disable();
    }

    /// <summary>
    /// Este método é chamado pelo evento do Input System.
    /// </summary>
    private void DefinirDestino(InputAction.CallbackContext context)
    {
        // Lê a posição do toque/rato no momento em que a ação foi executada.
        Vector2 posicaoTela = controles.Toque.PosicaoPrimaria.ReadValue<Vector2>();

        // Converte para coordenadas do mundo e define como o novo destino.
        destino = ObterPosicaoMundo(posicaoTela);

        Vector2 direcao = destino.normalized;
               

        m_Animator.SetFloat("horizontal", direcao.x);
        m_Animator.SetFloat("vertical", direcao.y);
    }

    /// <summary>
    /// A lógica de movimento continua no Update.
    /// </summary>
    void Update()
    {

        if (Vector3.Distance(transform.position, destino) == 0) 
        {
            m_Animator.SetFloat("horizontal", 0);


        }
        // Move o objeto em direção ao destino a cada frame.
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
    }

    private Vector3 ObterPosicaoMundo(Vector2 posicaoTela)
    {
        Vector3 posicaoMundo = cam.ScreenToWorldPoint(new Vector3(posicaoTela.x, posicaoTela.y, cam.nearClipPlane));
        posicaoMundo.z = 0f;
        return posicaoMundo;
    }
}