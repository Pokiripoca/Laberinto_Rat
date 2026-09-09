using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaLaberinto : MonoBehaviour
{
    [Header("Nombres de Escenas Finales")]
    public string escenaFinalBueno = "FinalBueno";
    public string escenaFinalMalo = "FinalMalo";

    private void OnTriggerEnter(Collider other)
    {
        // 1. Detectar si lo que entró en el Collider fue el Jugador
        if (other.CompareTag("Player"))
        {
            EvaluarFinal();
        }
    }

    private void EvaluarFinal()
    {
        int comidos = 0;
        int requeridos = 5;

        // 2. Obtener los datos del QuesoContador
        if (QuesoContador.Instance != null)
        {
            comidos = QuesoContador.Instance.quesosBuenosComidos;
            requeridos = QuesoContador.Instance.totalQuesosBuenosRequeridos;
        }

        // 3. Revisar si tiene los 5 quesos para decidir la escena
        if (comidos >= requeridos)
        {
            Debug.Log($"🏆 ¡Tiene {comidos} quesos! Cargando Final Bueno...");
            SceneManager.LoadScene(escenaFinalBueno);
        }
        else
        {
            Debug.Log($"💀 Solo tiene {comidos} de {requeridos} quesos. Cargando Final Malo...");
            SceneManager.LoadScene(escenaFinalMalo);
        }
    }
}