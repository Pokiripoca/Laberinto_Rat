using UnityEngine;

public class CheeseTrigger : MonoBehaviour
{
    private bool jugadorCerca = false;
    private CheeseData datosQueso;
    private CheeseInspector sistemaInspeccion;

    void Start()
    {
        datosQueso = GetComponent<CheeseData>();
        sistemaInspeccion = FindAnyObjectByType<CheeseInspector>();
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (sistemaInspeccion != null && datosQueso != null && !sistemaInspeccion.estaInspeccionando)
            {
                sistemaInspeccion.AbrirInspeccion(datosQueso);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta al jugador o a la Main Camera en la escena de prueba
        if (other.CompareTag("Player") || other.GetComponent<Camera>() != null || other.name.Contains("Camera"))
        {
            jugadorCerca = true;
            Debug.Log(" Presiona 'E' para inspeccionar el queso.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<Camera>() != null || other.name.Contains("Camera"))
        {
            jugadorCerca = false;
        }
    }
}