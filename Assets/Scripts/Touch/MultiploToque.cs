using UnityEngine;
using UnityEngine.InputSystem; // Importa a biblioteca do novo Input System.

/// <summary>
/// Instancia um Prefab na posição de cada novo toque na tela ou clique do rato
/// usando o novo Input System.
/// </summary>
public class MultiploToque : MonoBehaviour
{
    [Tooltip("O Prefab do GameObject que será instanciado.")]
    public GameObject objetoParaInstanciar;

    private Camera cam;
    private ControlesDeToque controles;

    private void Awake()
    {
        cam = Camera.main;
        controles = new ControlesDeToque();
    }

    private void OnEnable()
    {
        controles.Enable();
        // Subscreve ao evento 'performed'. Como a action 'ContatoPrimario' é do tipo Button,
        // este evento dispara exatamente quando o toque/clique começa.
        controles.Toque.ContatoPrimario.performed += ctx => GerarObjeto(ctx);
    }

    private void OnDisable()
    {
        controles.Toque.ContatoPrimario.performed -= ctx => GerarObjeto(ctx);
        controles.Disable();
    }

    /// <summary>
    /// Método chamado pelo evento do Input System para criar o objeto.
    /// </summary>
    private void GerarObjeto(InputAction.CallbackContext context)
    {
        // Lê a posição do toque/rato no momento do evento.
        Vector2 posicaoTela = controles.Toque.PosicaoPrimaria.ReadValue<Vector2>();

        // Converte a posição para coordenadas do mundo.
        Vector3 posicaoMundo = cam.ScreenToWorldPoint(new Vector3(posicaoTela.x, posicaoTela.y, cam.nearClipPlane));
        posicaoMundo.z = 0f;

        if (objetoParaInstanciar != null)
        {
            GameObject clone = Instantiate(objetoParaInstanciar, posicaoMundo, Quaternion.identity);
            Destroy(clone, 1f);
        }
        else
        {
            Debug.LogWarning("O Prefab 'objetoParaInstanciar' não foi definido no Inspector.");
        }
    }
}