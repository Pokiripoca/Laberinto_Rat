using UnityEngine;
using System.Collections.Generic;

public class CheeseSpawner : MonoBehaviour
{
    [Header("Prefabs de Quesos (Asignar en Inspector)")]
    public GameObject prefabQuesoBueno;
    public GameObject prefabQuesoMalo1;
    public GameObject prefabQuesoMalo2;

    [Header("Puntos de Spawn en el Laberinto")]
    public Transform[] puntosDeSpawn;

    [Header("Configuración")]
    public int cantidadAQuesosGenerar = 5;

    void Start()
    {
        GenerarQuesosAleatorios();
    }

    public void GenerarQuesosAleatorios()
    {
        if (puntosDeSpawn == null || puntosDeSpawn.Length == 0)
        {
            Debug.LogWarning("No hay puntos de spawn asignados.");
            return;
        }

        // Buscar un prefab de respaldo por si alguno está vacío en el Inspector
        GameObject prefabRespaldo = prefabQuesoBueno;
        if (prefabRespaldo == null) prefabRespaldo = prefabQuesoMalo1;
        if (prefabRespaldo == null) prefabRespaldo = prefabQuesoMalo2;

        if (prefabRespaldo == null)
        {
            Debug.LogError("¡Debes asignar al menos UN prefab de queso o cubo en el Inspector del Spawner!");
            return;
        }

        List<Transform> posicionesDisponibles = new List<Transform>(puntosDeSpawn);
        int quesosAColocar = Mathf.Min(cantidadAQuesosGenerar, posicionesDisponibles.Count);

        for (int i = 0; i < quesosAColocar; i++)
        {
            int indicePosicion = Random.Range(0, posicionesDisponibles.Count);
            Transform puntoElegido = posicionesDisponibles[indicePosicion];
            posicionesDisponibles.RemoveAt(indicePosicion);

            int tipoQuesoAleatorio = Random.Range(0, 3); // 0 = Bueno, 1 = Malo1, 2 = Malo2
            GameObject prefabAEmitir = null;
            bool esMalo = false;

            switch (tipoQuesoAleatorio)
            {
                case 0:
                    prefabAEmitir = (prefabQuesoBueno != null) ? prefabQuesoBueno : prefabRespaldo;
                    esMalo = false;
                    break;
                case 1:
                    prefabAEmitir = (prefabQuesoMalo1 != null) ? prefabQuesoMalo1 : prefabRespaldo;
                    esMalo = true;
                    break;
                case 2:
                    prefabAEmitir = (prefabQuesoMalo2 != null) ? prefabQuesoMalo2 : prefabRespaldo;
                    esMalo = true;
                    break;
            }

            // Instanciar el objeto
            GameObject quesoCreado = Instantiate(prefabAEmitir, puntoElegido.position, puntoElegido.rotation);

            // Asignar los datos
            CheeseData datos = quesoCreado.GetComponent<CheeseData>();
            if (datos == null)
            {
                datos = quesoCreado.AddComponent<CheeseData>();
            }

            datos.tieneMoho = esMalo;

            // --- DISTINCIÓN VISUAL TEMPORAL PARA PRUEBAS ---
            // Cambiamos el color según su estado para que puedas identificarlos
            Renderer rend = quesoCreado.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                if (esMalo)
                {
                    // Queso Malo/Podrido -> Color Oscuro / Verdoso
                    rend.material.color = new Color(0.2f, 0.4f, 0.2f);
                }
                else
                {
                    // Queso Bueno -> Color Amarillo
                    rend.material.color = new Color(1f, 0.85f, 0.2f);
                }
            }
        }
    }
}