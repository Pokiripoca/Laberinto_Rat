using System.Collections.Generic;
using UnityEngine;

public class CheeseSpawner : MonoBehaviour
{
    [Header("Prefabs de Quesos")]
    public GameObject prefabQuesoBueno;   // Prefab del queso amarillo (sin moho)
    public GameObject prefabQuesoPodrido; // Prefab del queso podrido (con moho)

    [Header("Puntos de Aparición (Spawners)")]
    public Transform[] puntosDeAparicion; // Arrastra aquí todos los Transforms del mapa donde pueden aparecer quesos

    [Header("Configuración de Garantía")]
    public int cantidadQuesosBuenosGarantizados = 5;

    void Start()
    {
        GenerarQuesosEnMapa();
    }

    void GenerarQuesosEnMapa()
    {
        if (puntosDeAparicion == null || puntosDeAparicion.Length == 0)
        {
            Debug.LogError("⚠️ No has asignado ningún punto de aparición en el CheeseSpawner.");
            return;
        }

        if (puntosDeAparicion.Length < cantidadQuesosBuenosGarantizados)
        {
            Debug.LogWarning($"⚠️ Hay menos puntos de aparición ({puntosDeAparicion.Length}) que quesos requeridos ({cantidadQuesosBuenosGarantizados}). Se reducirán los quesos buenos.");
            cantidadQuesosBuenosGarantizados = puntosDeAparicion.Length;
        }

        // 1. Crear una lista con los índices de los puntos de aparición
        List<int> indicesPuntos = new List<int>();
        for (int i = 0; i < puntosDeAparicion.Length; i++)
        {
            indicesPuntos.Add(i);
        }

        // 2. Mezclar los índices de forma aleatoria (Algoritmo Fisher-Yates)
        for (int i = 0; i < indicesPuntos.Count; i++)
        {
            int indiceAleatorio = Random.Range(i, indicesPuntos.Count);
            int temp = indicesPuntos[i];
            indicesPuntos[i] = indicesPuntos[indiceAleatorio];
            indicesPuntos[indiceAleatorio] = temp;
        }

        // 3. Colocar exactamente 5 quesos buenos en las primeras posiciones mezcladas
        for (int i = 0; i < cantidadQuesosBuenosGarantizados; i++)
        {
            int indicePunto = indicesPuntos[i];
            Instantiate(prefabQuesoBueno, puntosDeAparicion[indicePunto].position, puntosDeAparicion[indicePunto].rotation);
        }

        // 4. Llenar el resto de los puntos con quesos podridos
        for (int i = cantidadQuesosBuenosGarantizados; i < indicesPuntos.Count; i++)
        {
            int indicePunto = indicesPuntos[i];
            if (prefabQuesoPodrido != null)
            {
                Instantiate(prefabQuesoPodrido, puntosDeAparicion[indicePunto].position, puntosDeAparicion[indicePunto].rotation);
            }
        }

        Debug.Log($"🧀 Se han generado {cantidadQuesosBuenosGarantizados} quesos buenos y {indicesPuntos.Count - cantidadQuesosBuenosGarantizados} quesos podridos aleatoriamente.");
    }
}