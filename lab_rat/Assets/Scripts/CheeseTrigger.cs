using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class CheeseTrigger : MonoBehaviour
{
    private CheeseData datosQueso;
    private CheeseInspector sistemaInspeccion;

    void Start()
    {
        datosQueso = GetComponent<CheeseData>();
        sistemaInspeccion = FindAnyObjectByType<CheeseInspector>();
    }

    void Update()
    {
        bool presionoE = false;

        // Comprobación compatible con el New Input System
#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            presionoE = true;
        }
#endif

        // Comprobación clásica por si acaso
        if (Input.GetKeyDown(KeyCode.E))
        {
            presionoE = true;
        }

        if (presionoE)
        {
            if (sistemaInspeccion != null && datosQueso != null && !sistemaInspeccion.estaInspeccionando)
            {
                sistemaInspeccion.AbrirInspeccion(datosQueso);
            }
        }
    }
}