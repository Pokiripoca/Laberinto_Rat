using UnityEngine;

public class QuesoContador : MonoBehaviour
{
    // Singleton para acceder fácilmente desde cualquier parte del proyecto
    public static QuesoContador Instance { get; private set; }

    [Header("Progreso de Quesos")]
    public int quesosBuenosComidos = 0;
    public int totalQuesosBuenosRequeridos = 5;

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

    public void RegistrarQuesoComido()
    {
        quesosBuenosComidos++;
        Debug.Log($"🧀 Queso bueno comido. Total: {quesosBuenosComidos} / {totalQuesosBuenosRequeridos}");
    }
}