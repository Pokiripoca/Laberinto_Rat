using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CheeseInspector : MonoBehaviour
{
    [Header("UI y Mensajes")]
    public GameObject textoPromptE;
    public GameObject panelInspeccion;
    public GameObject imagenHUD;
    public Button botonComer;
    public Button botonIgnorar;

    [Header("Referencias de Órbita y Cámara")]
    public Transform puntoInspeccion;
    public Camera camaraInspeccion;

    [Header("Configuración de Órbita")]
    public float distanciaCamara = 2.5f;
    public float velocidadSensibilidad = 180f;
    public float limiteVerticalMin = -20f;
    public float limiteVerticalMax = 80f;

    [Header("Efectos de Sonido")]
    public AudioClip sonidoOlfateo; // <- Asignar audio de olfateo en el Inspector

    [HideInInspector]
    public bool estaInspeccionando = false;

    private GameObject quesoInstanciado;
    private CheeseData quesoActualData;
    private GameObject quesoEnMapa;

    private Camera camaraJugador;
    private AudioSource audioSource;
    private float rotacionX = 0f;
    private float rotacionY = 20f;
    private FPSPlayer playerScript;

    void Start()
    {
        estaInspeccionando = false;
        if (panelInspeccion != null) panelInspeccion.SetActive(false);
        if (textoPromptE != null) textoPromptE.SetActive(false);

        if (camaraInspeccion != null) camaraInspeccion.gameObject.SetActive(false);

        playerScript = FindFirstObjectByType<FPSPlayer>();
        audioSource = GetComponent<AudioSource>();

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
            if (Input.GetMouseButton(1))
            {
                rotacionX += Input.GetAxis("Mouse X") * velocidadSensibilidad * Time.deltaTime;
                rotacionY -= Input.GetAxis("Mouse Y") * velocidadSensibilidad * Time.deltaTime;
                rotacionY = Mathf.Clamp(rotacionY, limiteVerticalMin, limiteVerticalMax);
            }

            ActualizarPosicionCamara();
        }
    }

    public void MostrarPromptInteraccion(bool mostrar)
    {
        if (textoPromptE != null)
        {
            textoPromptE.SetActive(mostrar);
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

        MostrarPromptInteraccion(false);

        if (imagenHUD != null) imagenHUD.SetActive(false);

        if (playerScript == null) playerScript = FindFirstObjectByType<FPSPlayer>();
        if (playerScript != null) playerScript.enabled = false;

        camaraJugador = Camera.main;
        if (camaraJugador != null) camaraJugador.gameObject.SetActive(false);

        if (camaraInspeccion != null) camaraInspeccion.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        quesoActualData = queso;
        quesoEnMapa = queso.gameObject;
        estaInspeccionando = true;

        if (panelInspeccion != null) panelInspeccion.SetActive(true);

        // Reproducir sonido de olfateo al entrar a inspección
        if (sonidoOlfateo != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoOlfateo);
        }

        // Crear el queso 3D en CentroQueso
        if (puntoInspeccion != null)
        {
            if (quesoInstanciado != null) Destroy(quesoInstanciado);

            GameObject prefabAUsar = (queso.modeloInspeccionPrefab != null) ? queso.modeloInspeccionPrefab : queso.gameObject;
            quesoInstanciado = Instantiate(prefabAUsar, puntoInspeccion);
            quesoInstanciado.transform.localPosition = Vector3.zero;
            quesoInstanciado.transform.localRotation = Quaternion.identity;

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
            Debug.Log("❌ ¡Te comiste un queso PODRIDO! Cargando escena 'petateada'...");
            SceneManager.LoadScene("petateada");
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

        if (imagenHUD != null) imagenHUD.SetActive(true);

        if (camaraInspeccion != null) camaraInspeccion.gameObject.SetActive(false);
        if (camaraJugador != null) camaraJugador.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerScript != null) playerScript.enabled = true;
    }
}