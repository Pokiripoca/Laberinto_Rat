using UnityEngine;
using TMPro;

public class QuesoContador : MonoBehaviour {
    public static QuesoContador Instance { get; private set; }

    [Header("Progreso de Quesos")]
    public int quesosBuenosComidos = 0; // Contará la cantidad total de quesos comidos
    public int totalQuesosBuenosRequeridos = 5;

    [Header("UI del Contador")]
    public TextMeshProUGUI textoContadorUI;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        ActualizarTextoUI();
    }

    // Esta es la función que están buscando tus otros scripts
    public void RegistrarQuesoComido() {
        quesosBuenosComidos++;
        Debug.Log($"🧀 Queso comido. Total: {quesosBuenosComidos} / {totalQuesosBuenosRequeridos}");
        ActualizarTextoUI();
    }

    public void ActualizarTextoUI() {
        if (textoContadorUI != null) {
            textoContadorUI.text = $"{quesosBuenosComidos} / {totalQuesosBuenosRequeridos}";
        }
    }
}