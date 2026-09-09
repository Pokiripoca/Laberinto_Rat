using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ControladorVideoMenu : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("Nombres exactos de tus escenas de menú")]
    [SerializeField] private string nombreMenuVictoria = "MenuVictoria";
    [SerializeField] private string nombreMenuDerrota = "MenuDerrota";

    void Start()
    {
        // Se suscribe al evento automático de Unity que avisa cuando el video llega al final
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += IrAlMenu;
        }
    }

    void Update()
    {
        // Si el jugador presiona Enter durante el video, salta directamente al menú
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            IrAlMenu(videoPlayer);
        }
    }

    private void IrAlMenu(VideoPlayer vp)
    {
        // Desuscribirse para evitar llamadas dobles
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= IrAlMenu;
        }

        // Evalúa la variable guardada previamente y carga el menú correspondiente
        if (EstadoJuego.JugadorGano)
        {
            SceneManager.LoadScene(nombreMenuVictoria);
        }
        else
        {
            SceneManager.LoadScene(nombreMenuDerrota);
        }
    }
}