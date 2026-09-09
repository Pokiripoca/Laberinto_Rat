using UnityEngine;
using UnityEngine.SceneManagement; // Requerido para cambiar de escena

public class ControladorFinal : MonoBehaviour
{
    // Llama a esta función cuando el jugador cumpla la condición de victoria
    public void Victoria()
    {
        EstadoJuego.JugadorGano = true;
        SceneManager.LoadScene("EscenaVideo");
    }

    // Llama a esta función cuando el jugador pierda
    public void Derrota()
    {
        EstadoJuego.JugadorGano = false;
        SceneManager.LoadScene("EscenaVideo");
    }
}