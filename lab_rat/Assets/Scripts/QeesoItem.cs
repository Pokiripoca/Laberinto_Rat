using UnityEngine;

public class QuesoItem : MonoBehaviour
{
    [Header("Tipo de Queso")]
    [Tooltip("Marca la casilla si es un queso bueno. Desmárcala si es malo.")]
    public bool esQuesoBueno = true;

    // Evita que se sume dos veces si el jugador se vuelve a acercar
    private bool yaInspeccionado = false;

    private void OnTriggerEnter(Collider other) // Usa OnTriggerEnter2D si tu juego es en 2D
    {
        if (other.CompareTag("Player") && !yaInspeccionado)
        {
            Inspeccionar();
        }
    }

    public void Inspeccionar()
    {
        if (yaInspeccionado) return; // Si ya se inspeccionó, ignora la llamada

        yaInspeccionado = true; // Bloquea este queso para que no sume doble

        // Le avisa al contador Singleton
        if (QuesoContador.Instance != null)
        {
            QuesoContador.Instance.RegistrarQueso(esQuesoBueno);
        }
    }
}