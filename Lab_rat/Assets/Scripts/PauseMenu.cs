using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused;

    void Awake()
    {
        isPaused = false; //Porque al principio el juego no está pausado, puede ser en el Start, en el Update no hacerlo
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenuUI.SetActive(false); //false es para que el menú esté apagado al iniciar el juego, se hace referencia al GameObject, que en este caso es pauseMenu
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) //Está activa la pausa, es decir, es true, y queremos que se desactive la pausa, eso es lo que significa if(isPaused)
            {
                //Resume - Botones
                Resume(); //Esto significa que está activo
            }
            else //Esto es para que se pause
            {
                //Escape
                Pause(); //Esto es para activar la pausa
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); //Oculta el menu, por eso es false
        Time.timeScale = 1f; //Reanuda el tiempo, por eso es 1f, para activarlo es el número 1
        isPaused = false; //Es false para que se despause, es decir, que deje de ser verdadedero
        
        //Es para que no se muestre el ratón del cursor al jugar
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); //Es true para que se active el menu
        Time.timeScale = 0f; //Es cero porque el número cero hace que se desactive
        isPaused = true; //Esto es para que se active la pausa

        //Esto es para que muestre el ratón porque si no se queda en modo jugador
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Exit()
    {
        Debug.Log("Salir del juego");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Detiene el Play
#else
        Application.Quit();
#endif
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
