using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ControladorCinematica : MonoBehaviour
{
    [Header("Componentes y Videos")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoVictoria;
    [SerializeField] private VideoClip videoDerrota;

    [Header("Nombres de las Escenas de Menú")]
    [SerializeField] private string menuVictoria = "MenuVictoria";
    [SerializeField] private string menuDerrota = "MenuDerrota";

    void Start()
    {
        // 1. Asignar el video correspondiente según el estado guardado
        if (EstadoJuego.JugadorGano)
        {
            videoPlayer.clip = videoVictoria;
        }
        else
        {
            videoPlayer.clip = videoDerrota;
        }

        // 2. Suscribirse al evento que detecta cuando el video llega al final
        videoPlayer.loopPointReached += OnVideoTerminado;

        // 3. Iniciar reproducción
        videoPlayer.Play();
    }

    void Update()
    {
        // Opcional: Permitir saltar el video presionando Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CargarMenuCorrespondiente();
        }
    }

    // Se ejecuta automáticamente al finalizar la reproducción del video
    private void OnVideoTerminado(VideoPlayer vp)
    {
        CargarMenuCorrespondiente();
    }

    private void CargarMenuCorrespondiente()
    {
        // Desuscribirse del evento para evitar llamadas duplicadas
        videoPlayer.loopPointReached -= OnVideoTerminado;

        // Cargar el menú que toca
        if (EstadoJuego.JugadorGano)
        {
            SceneManager.LoadScene(menuVictoria);
        }
        else
        {
            SceneManager.LoadScene(menuDerrota);
        }
    }
}