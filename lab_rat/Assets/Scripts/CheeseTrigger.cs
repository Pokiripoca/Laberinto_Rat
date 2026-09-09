using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheeseTrigger : MonoBehaviour
{
    private CheeseData datosQueso;
    private CheeseInspector sistemaInspeccion;
    private bool jugadorCerca = false;

    void Start()
    {
        datosQueso = GetComponent<CheeseData>();
        sistemaInspeccion = FindFirstObjectByType<CheeseInspector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (sistemaInspeccion != null && !sistemaInspeccion.estaInspeccionando)
            {
                sistemaInspeccion.MostrarPromptInteraccion(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (sistemaInspeccion != null)
            {
                sistemaInspeccion.MostrarPromptInteraccion(false);
            }
        }
    }

    void Update()
    {
        if (!jugadorCerca) return;

        if (Input.GetKeyDown(KeyCode.E) && sistemaInspeccion != null && !sistemaInspeccion.estaInspeccionando)
        {
            sistemaInspeccion.AbrirInspeccion(datosQueso);
        }
    }
}