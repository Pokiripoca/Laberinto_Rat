using UnityEngine;
using TMPro;

public class QuesoContador : MonoBehaviour
{
    public static QuesoContador Instance { get; private set; }

    [Header("Progreso de Quesos")]
    public int quesosBuenosComidos = 0;
    public int totalQuesosBuenosRequeridos = 5;

    [Header("UI del Contador")]
    public TextMeshProUGUI textoContadorUI; // Asigna el texto hijo aquí

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

    public void RegistrarQuesoComido()
    {
        quesosBuenosComidos++;
        Debug.Log($"🧀 Queso bueno comido. Total: {quesosBuenosComidos} / {totalQuesosBuenosRequeridos}");
        ActualizarTextoUI();
    }

    private void ActualizarTextoUI()
    {
        if (textoContadorUI != null)
        {
            textoContadorUI.text = $"{quesosBuenosComidos} / {totalQuesosBuenosRequeridos}";
        }
    }
}