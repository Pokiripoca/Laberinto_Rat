using UnityEditor.Build;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControlled : MonoBehaviour
{
    public int index;
    public int iCredits;
    public int iBack1;
    public int iBack2;
    public int iEnter1;
    public int iEnter2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        iBack1 = 0;
        iCredits = 1;
        iEnter1 = 2;
        iBack2 = 2;
        iEnter2 = 3;
        index = 4;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SiguienteEscena();
        }
    }

    void SiguienteEscena()
    {
        int iEscenaActual = SceneManager.GetActiveScene().buildIndex;

        int iSiguienteEscena = iEscenaActual + 1;

        if (iSiguienteEscena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(iSiguienteEscena);
        }
        else
        {
            Debug.LogWarning("Se ha llegado a la última escena");
        }
    }

    public void Enter1()
    {
        SceneManager.LoadScene(iEnter1);
    }

    public void Enter2()
    {
        SceneManager.LoadScene(iEnter2);
    }
    public void PlayGame()
    {
        //Es para que no se muestre el ratón del cursor al jugar
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene(index);
    }

    public void Credits()
    {
        SceneManager.LoadScene(iCredits);
    }

    public void Back1()
    {
        SceneManager.LoadScene(iBack1);
    }

    public void Back2()
    {
        SceneManager.LoadScene(iBack2);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Detiene el Play
#else
        Application.Quit();
#endif
    }
}

