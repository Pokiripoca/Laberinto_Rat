using UnityEngine;
using System.Reflection;

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
            // Busca el método de registro implementado por el contador sin
            // depender de un nombre que no exista en esta versión.
            MethodInfo metodoRegistro = typeof(QuesoContador).GetMethod(
                "RegistrarQueso",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new[] { typeof(bool) },
                null);

            if (metodoRegistro != null)
            {
                metodoRegistro.Invoke(QuesoContador.Instance, new object[] { esQuesoBueno });
            }
        }
    }
}