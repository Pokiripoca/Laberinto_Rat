using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaLaberinto : MonoBehaviour
{
    [Header("Nombres de Escenas Finales")]
    public string escenaFinalBueno = "FinalBueno";
    public string escenaFinalMalo = "FinalMalo";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EvaluarFinal();
        }
    }

    private void EvaluarFinal()
    {
        int comidos = 0;
        int requeridos = 5;

        if (QuesoContador.Instance != null)
        {
            comidos = QuesoContador.Instance.quesosBuenosComidos;
            requeridos = QuesoContador.Instance.totalQuesosBuenosRequeridos;
        }

        if (comidos >= requeridos)
        {
            Debug.Log("🏆 ¡5 Quesos comidos! Final Bueno.");
            SceneManager.LoadScene(escenaFinalBueno);
        }
        else
        {
            Debug.Log("💀 Menos de 5 quesos. Final Malo.");
            SceneManager.LoadScene(escenaFinalMalo);
        }
    }
}