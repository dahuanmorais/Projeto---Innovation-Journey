using UnityEngine;
using UnityEngine.InputSystem; // Importa a biblioteca do novo Input System.

/// <summary>
/// Move um objeto para a posição do toque ou do rato usando o novo Input System.
/// O objeto seguirá continuamente a posição do dedo ou do cursor.
/// </summary>
public class TouchScript : MonoBehaviour
{
    private Camera cam;
    private ControlesDeToque controles; // Referência para a classe gerada pelo Input Actions asset.

    private Vector2 posicaoNaTela; // Armazena a posição lida pela action.
    private bool toquePressionado = false; // Controla se o ecrã está a ser pressionado.

    /// <summary>
    /// Awake é chamado antes do Start. Ideal para inicializar referências.
    /// </summary>
    private void Awake()
    {
        cam = Camera.main;
        controles = new ControlesDeToque();
    }

    /// <summary>
    /// OnEnable é chamado quando o objeto se torna ativo.
    /// É o local ideal para subscrever aos eventos das actions.
    /// </summary>
    private void OnEnable()
    {
        controles.Enable();

        // Subscreve aos eventos da action 'ContatoPrimario'.
        // 'performed' é chamado quando o botão/toque é pressionado.
        // 'canceled' é chamado quando o botão/toque é solto.
        controles.Toque.ContatoPrimario.performed += ctx => ToquePressionado(ctx);
        controles.Toque.ContatoPrimario.canceled += ctx => ToqueSolto(ctx);
    }

    /// <summary>
    /// OnDisable é chamado quando o objeto se torna inativo.
    /// É crucial cancelar a subscrição aos eventos para evitar erros de memória.
    /// </summary>
    private void OnDisable()
    {
        controles.Toque.ContatoPrimario.performed -= ctx => ToquePressionado(ctx);
        controles.Toque.ContatoPrimario.canceled -= ctx => ToqueSolto(ctx);

        controles.Disable();
    }

    private void ToquePressionado(InputAction.CallbackContext context)
    {
        toquePressionado = true;
    }

    private void ToqueSolto(InputAction.CallbackContext context)
    {
        toquePressionado = false;
    }

    /// <summary>
    /// Update continua a ser útil para lógicas que ocorrem a cada frame.
    /// </summary>
    void Update()
    {
        // Se o ecrã estiver a ser pressionado, move o objeto.
        if (toquePressionado)
        {
            // Lê o valor da action 'PosicaoPrimaria' (um Vector2).
            posicaoNaTela = controles.Toque.PosicaoPrimaria.ReadValue<Vector2>();
            MoverObjeto(posicaoNaTela);
        }
    }

    void MoverObjeto(Vector2 posicaoNaTela)
    {
        Vector3 posicaoMundo = cam.ScreenToWorldPoint(new Vector3(posicaoNaTela.x, posicaoNaTela.y, cam.nearClipPlane));
        posicaoMundo.z = 0f;
        transform.position = posicaoMundo;
    }
}