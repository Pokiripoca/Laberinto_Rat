using UnityEngine;
using TMPro;

public class QuesoContador : MonoBehaviour
{
    public static QuesoContador Instance { get; private set; }

    [Header("Progreso de Quesos")]
    public int quesosBuenosComidos = 0;
    public int quesosMalosComidos = 0;
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

    // Registra el queso recibido según si es bueno o malo
    public void RegistrarQuesoComido(bool esBueno)
    {
        if (esBueno)
        {
            quesosBuenosComidos++;
            Debug.Log($"🧀 Queso BUENO inspeccionado (+1). Total: {quesosBuenosComidos} / {totalQuesosBuenosRequeridos}");
        }
        else
        {
            quesosMalosComidos++;
            Debug.Log($"☣️ Queso MALO inspeccionado (+1). Total malos: {quesosMalosComidos}");
        }

        ActualizarTextoUI();
    }

    public void ActualizarTextoUI()
    {
        if (textoContadorUI != null)
        {
            textoContadorUI.text = $"{quesosBuenosComidos} / {totalQuesosBuenosRequeridos}";
        }
    }
}