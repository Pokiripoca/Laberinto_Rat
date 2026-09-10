using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorFinal : MonoBehaviour
{
    public void Victoria()
    {
        EstadoJuego.JugadorGano = true;
        SceneManager.LoadScene("EscenaVideo");
    }

    public void Derrota()
    {
        EstadoJuego.JugadorGano = false;
        SceneManager.LoadScene("EscenaVideo");
    }
}