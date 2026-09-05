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

    // Variables para guardar dónde estaba la cámara antes de inspeccionar
    private Vector3 posicionCamaraOriginal;
    private Quaternion rotacionCamaraOriginal;

    private float rotacionX = 0f;
    private float rotacionY = 20f;

    void Start()
    {
        estaInspeccionando = false;
        if (panelInspeccion != null) panelInspeccion.SetActive(false);

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
        if (Input.GetKeyDown(KeyCode.E) && !estaInspeccionando)
        {
            AbrirSiguienteQueso();
        }

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

    public void AbrirSiguienteQueso()
    {
        CheeseData[] quesosEnEscena = FindObjectsByType<CheeseData>(FindObjectsSortMode.None);

        if (quesosEnEscena.Length > 0)
        {
            AbrirInspeccion(quesosEnEscena[0]);
        }
    }

    public void AbrirInspeccion(CheeseData queso)
    {
        if (queso == null) return;

        // 1. Guardar la posición de la cámara del jugador antes de cambiar la vista
        if (camaraInspeccion != null)
        {
            posicionCamaraOriginal = camaraInspeccion.transform.position;
            rotacionCamaraOriginal = camaraInspeccion.transform.rotation;
        }

        quesoActualData = queso;
        quesoEnMapa = queso.gameObject;
        estaInspeccionando = true;

        if (panelInspeccion != null) panelInspeccion.SetActive(true);

        // 2. Montar el modelo en el centro de inspección
        if (puntoInspeccion != null)
        {
            if (quesoInstanciado != null) Destroy(quesoInstanciado);

            quesoInstanciado = Instantiate(queso.gameObject, puntoInspeccion);
            quesoInstanciado.transform.localPosition = Vector3.zero;
            quesoInstanciado.transform.localRotation = Quaternion.identity;

            CheeseData cd = quesoInstanciado.GetComponent<CheeseData>();
            if (cd != null) Destroy(cd);
        }

        rotacionX = 0f;
        rotacionY = 20f;
        ActualizarPosicionCamara();
    }

    public void ComerQueso()
    {
        if (quesoActualData != null && quesoActualData.tieneMoho)
        {
            // ❌ SI ES MALO: Reinicia la escena (o resta vida)
            Debug.Log("❌ ¡Te comiste un queso PODRIDO! Reiniciando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // ✅ SI ES BUENO: Elimina el queso de la escena y te regresa al juego
            Debug.Log("✅ ¡Queso BUENO comido! Eliminando queso del mapa...");

            if (quesoEnMapa != null)
            {
                Destroy(quesoEnMapa);
            }

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
        // 1. Destruir la copia 3D de inspección
        if (quesoInstanciado != null) Destroy(quesoInstanciado);

        // 2. Ocultar la UI
        estaInspeccionando = false;
        if (panelInspeccion != null) panelInspeccion.SetActive(false);

        // 3. Regresar la cámara a la posición del jugador
        if (camaraInspeccion != null)
        {
            camaraInspeccion.transform.position = posicionCamaraOriginal;
            camaraInspeccion.transform.rotation = rotacionCamaraOriginal;
        }
    }
}