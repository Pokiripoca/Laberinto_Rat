using UnityEngine;
using TMPro;

public class QuesoContador : MonoBehaviour
{
    public static QuesoContador Instance { get; private set; }

    [Header("Progreso de Quesos")]
    public int quesosBuenosInspeccionados = 0;
    public int quesosMalosInspeccionados = 0;
    public int totalQuesosBuenosRequeridos = 5;

    [Header("UI del Contador")]
    public TextMeshProUGUI textoContadorUI; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ActualizarTextoUI();
    }

    public void RegistrarQueso(bool esBueno)
    {
        if (esBueno)
        {
            quesosBuenosInspeccionados++;
            Debug.Log($"🧀 Queso BUENO inspeccionado. Total: {quesosBuenosInspeccionados} / {totalQuesosBuenosRequeridos}");
        }
        else
        {
            quesosMalosInspeccionados++;
            Debug.Log($"☣️ Queso MALO inspeccionado. Total malos: {quesosMalosInspeccionados}");
        }

        ActualizarTextoUI();
    }

    private void ActualizarTextoUI()
    {
        if (textoContadorUI != null)
        {
            textoContadorUI.text = $"{quesosBuenosInspeccionados} / {totalQuesosBuenosRequeridos}";
        }
    }
}