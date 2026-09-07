using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }

    void Update()
    {
        if (!jugadorCerca) return;

        bool presionoE = false;

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            presionoE = true;
        }
#endif

        if (Input.GetKeyDown(KeyCode.E))
        {
            presionoE = true;
        }

        if (presionoE && sistemaInspeccion != null && datosQueso != null && !sistemaInspeccion.estaInspeccionando)
        {
            sistemaInspeccion.AbrirInspeccion(datosQueso);
        }
    }
}