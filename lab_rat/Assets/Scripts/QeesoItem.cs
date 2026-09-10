using UnityEngine;

public class CheeseItem : MonoBehaviour
{
    [Header("Configuración del Queso")]
    [Tooltip("Marca esta casilla si el queso es bueno. Desmárcala si es malo.")]
    public bool esQuesoBueno = true;

    // Candado para evitar que el queso se lea más de una vez
    private bool yaFueInspeccionado = false;

    private void OnTriggerEnter(Collider other) // Usa OnTriggerEnter2D si tu juego es 2D
    {
        if (other.CompareTag("Player"))
        {
            InspeccionarQueso();
        }
    }

    public void InspeccionarQueso()
    {
        // Si ya fue inspeccionado anteriormente, bloquea y no hace nada
        if (yaFueInspeccionado) return;

        // Marca el queso como inspeccionado inmediatamente
        yaFueInspeccionado = true;

        // Envía el dato al contador de la escena
        if (QuesoContador.Instance != null)
        {
            QuesoContador.Instance.RegistrarQuesoComido(esQuesoBueno);
        }
        else
        {
            Debug.LogError("No se encontró 'QuesoContador' en la escena.");
        }
    }
}