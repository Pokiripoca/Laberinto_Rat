using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheeseInspector : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelInspeccion;
    public Button botonComer;
    public Button botonIgnorar;

    [Header("Referencias de Órbita y Cámara")]
    public Transform puntoInspeccion;
    public Camera camaraInspeccion;

    [Header("Configuración de Órbita")]
    public float distanciaCamara = 3f;
    public float velocidadSensibilidad = 180f;
    public float limiteVerticalMin = -20f;
    public float limiteVerticalMax = 80f;

    private GameObject quesoInstanciado;
    private CheeseData quesoActualData;
    private GameObject quesoEnMapa;

    public bool estaInspeccionando = false;

    private Vector3 posicionCamaraOriginal;
    private Quaternion rotacionCamaraOriginal;

    private float rotacionX = 0f;
    private float rotacionY = 20f;

    private FPSPlayer playerScript;

    void Start()
    {
        estaInspeccionando = false;
        if (panelInspeccion != null) panelInspeccion.SetActive(false);

        playerScript = FindFirstObjectByType<FPSPlayer>();

        if (botonComer != null)
        {
            botonComer.onClick.RemoveAllListeners();
            botonComer.onClick.AddListener(ComerQueso);
        }

        if (botonIgnorar != null)
        {
            botonIgnorar.onClick.RemoveAllListeners();
            botonIgnorar.onClick.AddListener(IgnorarQueso);
        }
    }

    void Update()
    {
        if (estaInspeccionando)
        {
            if (Input.GetMouseButton(0))
            {
                rotacionX += Input.GetAxis("Mouse X") * velocidadSensibilidad * Time.deltaTime;
                rotacionY -= Input.GetAxis("Mouse Y") * velocidadSensibilidad * Time.deltaTime;
                rotacionY = Mathf.Clamp(rotacionY, limiteVerticalMin, limiteVerticalMax);
            }

            ActualizarPosicionCamara();
        }
    }

    private void ActualizarPosicionCamara()
    {
        if (puntoInspeccion == null || camaraInspeccion == null) return;

        Quaternion rotacion = Quaternion.Euler(rotacionY, rotacionX, 0);
        Vector3 posicionCalculada = puntoInspeccion.position + (rotacion * new Vector3(0.0f, 0.0f, -distanciaCamara));

        camaraInspeccion.transform.rotation = rotacion;
        camaraInspeccion.transform.position = posicionCalculada;
    }

    public void AbrirInspeccion(CheeseData queso)
    {
        if (queso == null || estaInspeccionando) return;

        // 1. Pausar control del jugador y liberar cursor para la UI
        if (playerScript == null) playerScript = FindFirstObjectByType<FPSPlayer>();
        if (playerScript != null) playerScript.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 2. Guardar posición previa de la cámara
        if (camaraInspeccion != null)
        {
            posicionCamaraOriginal = camaraInspeccion.transform.position;
            rotacionCamaraOriginal = camaraInspeccion.transform.rotation;
        }

        quesoActualData = queso;
        quesoEnMapa = queso.gameObject;
        estaInspeccionando = true;

        if (panelInspeccion != null) panelInspeccion.SetActive(true);

        // 3. Montar modelo 3D (Usa el prefab de inspección si existe, si no usa el objeto del mapa)
        if (puntoInspeccion != null)
        {
            if (quesoInstanciado != null) Destroy(quesoInstanciado);

            GameObject prefabAUsar = (queso.modeloInspeccionPrefab != null) ? queso.modeloInspeccionPrefab : queso.gameObject;
            quesoInstanciado = Instantiate(prefabAUsar, puntoInspeccion);
            quesoInstanciado.transform.localPosition = Vector3.zero;
            quesoInstanciado.transform.localRotation = Quaternion.identity;

            // Limpiar componentes de física o lógica en el objeto clonado de inspección
            foreach (var script in quesoInstanciado.GetComponents<MonoBehaviour>()) Destroy(script);
            foreach (var col in quesoInstanciado.GetComponentsInChildren<Collider>()) Destroy(col);
            foreach (var rb in quesoInstanciado.GetComponentsInChildren<Rigidbody>()) Destroy(rb);
        }

        rotacionX = 0f;
        rotacionY = 20f;
        ActualizarPosicionCamara();
    }

    public void ComerQueso()
    {
        if (quesoActualData != null && quesoActualData.tieneMoho)
        {
            Debug.Log("❌ ¡Te comiste un queso PODRIDO! Reiniciando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.Log("✅ ¡Queso BUENO comido! Eliminando queso del mapa...");
            if (quesoEnMapa != null) Destroy(quesoEnMapa);
            CerrarInspeccionYContinuar();
        }
    }

    public void IgnorarQueso()
    {
        Debug.Log("Elegiste IGNORAR. El queso se queda en el mapa.");
        CerrarInspeccionYContinuar();
    }

    private void CerrarInspeccionYContinuar()
    {
        if (quesoInstanciado != null) Destroy(quesoInstanciado);

        estaInspeccionando = false;
        if (panelInspeccion != null) panelInspeccion.SetActive(false);

        if (camaraInspeccion != null)
        {
            camaraInspeccion.transform.position = posicionCamaraOriginal;
            camaraInspeccion.transform.rotation = rotacionCamaraOriginal;
        }

        // Bloquear cursor nuevamente y reactivar al jugador
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerScript != null) playerScript.enabled = true;
    }
}