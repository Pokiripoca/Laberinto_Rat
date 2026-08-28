using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheeseInspector : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelInspeccion;
    public Button botonComer;
    public Button botonIgnorar;

    [Header("Referencias de Órbita")]
    public Transform puntoInspeccion; // Objeto CentroQueso
    public Camera camaraInspeccion;   // Main Camera

    [Header("Configuración de Órbita")]
    public float distanciaCamara = 3f;
    public float velocidadSensibilidad = 180f;
    public float limiteVerticalMin = -20f;
    public float limiteVerticalMax = 80f;

    [Header("Referencias Jugador")]
    public PlayerController jugador;

    private GameObject quesoInstanciado;
    private CheeseData quesoActualData;

    // Inicia en FALSE para que el jugador controle la cámara normalmente hasta activar el queso
    public bool estaInspeccionando = false;

    private float rotacionX = 0f;
    private float rotacionY = 20f;

    void Start()
    {
        if (panelInspeccion != null) panelInspeccion.SetActive(false);

        if (botonComer != null) botonComer.onClick.AddListener(ComerQueso);
        if (botonIgnorar != null) botonIgnorar.onClick.AddListener(IgnorarQueso);
    }

    void Update()
    {
        if (!estaInspeccionando) return;

        // Arrastrar clic izquierdo para girar
        if (Input.GetMouseButton(0))
        {
            rotacionX += Input.GetAxis("Mouse X") * velocidadSensibilidad * Time.deltaTime;
            rotacionY -= Input.GetAxis("Mouse Y") * velocidadSensibilidad * Time.deltaTime;

            rotacionY = Mathf.Clamp(rotacionY, limiteVerticalMin, limiteVerticalMax);
        }

        ActualizarPosicionCamara();
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
        quesoActualData = queso;
        estaInspeccionando = true;

        if (jugador != null) jugador.canMove = false;
        if (panelInspeccion != null) panelInspeccion.SetActive(true);

        // Instanciar el modelo 3D en CentroQueso
        if (queso != null && queso.modeloInspeccionPrefab != null && puntoInspeccion != null)
        {
            if (quesoInstanciado != null) Destroy(quesoInstanciado);
            quesoInstanciado = Instantiate(queso.modeloInspeccionPrefab, puntoInspeccion.position, Quaternion.identity, puntoInspeccion);
        }

        rotacionX = 0f;
        rotacionY = 20f;
        ActualizarPosicionCamara();
    }

    public void ComerQueso()
    {
        if (quesoActualData != null && quesoActualData.tieneMoho)
        {
            Debug.Log("¡El queso tenía moho! Reiniciando la escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.Log("¡Queso comestible fresco!");
            if (quesoActualData != null) Destroy(quesoActualData.gameObject);
            CerrarInspeccion();
        }
    }

    public void IgnorarQueso()
    {
        CerrarInspeccion();
    }

    private void CerrarInspeccion()
    {
        if (quesoInstanciado != null) Destroy(quesoInstanciado);

        estaInspeccionando = false;
        if (panelInspeccion != null) panelInspeccion.SetActive(false);
        if (jugador != null) jugador.canMove = true;
    }
}