using UnityEngine;

public class QuesoFinalTrigger : MonoBehaviour
{
    private bool yaSeActivo = false;

    private void OnTriggerEnter(Collider other) // Si tu juego es 2D, usa OnTriggerEnter2D(Collider2D other)
    {
        // Verifica que sea el jugador y que el trigger no se haya activado antes
        if (other.CompareTag("Player") && !yaSeActivo)
        {
            yaSeActivo = true;
            EvaluarYCambiarEscena();
        }
    }

    private void EvaluarYCambiarEscena()
    {
        // Revisa si existe el contador en la escena
        if (QuesoContador.Instance != null)
        {
            // Revisa si el jugador alcanzó o superó el objetivo de quesos
            if (QuesoContador.Instance.quesosBuenosComidos >= QuesoContador.Instance.totalQuesosBuenosRequeridos)
            {
                Debug.Log("🏆 Final Bueno: ¡Llegaste con los 5 quesos!");
                FindAnyObjectByType<ControladorFinal>().Victoria();
            }
            else
            {
                Debug.Log("💀 Final Malo: No conseguiste los 5 quesos necesarios.");
                FindAnyObjectByType<ControladorFinal>().Derrota();
            }
        }
        else
        {
            Debug.LogError("No se encontró el QuesoContador en la escena.");
        }
    }
}