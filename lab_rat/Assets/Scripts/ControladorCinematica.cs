using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ControladorCinematica : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip videoVictoria;
    [SerializeField] private VideoClip videoDerrota;

    [SerializeField] private string nombreMenuVictoria = "MenuVictoria";
    [SerializeField] private string nombreMenuDerrota = "MenuDerrota";

    void Start()
    {
        if (EstadoJuego.JugadorGano)
        {
            videoPlayer.clip = videoVictoria;
        }
        else
        {
            videoPlayer.clip = videoDerrota;
        }

        videoPlayer.loopPointReached += IrAlMenu;
        videoPlayer.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            IrAlMenu(videoPlayer);
        }
    }

    private void IrAlMenu(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= IrAlMenu;

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