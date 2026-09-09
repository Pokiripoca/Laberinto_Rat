using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas

public class CargarMenu : MonoBehaviour
{
    // Nombre exacto de la escena del menú a la que quieres ir
    [SerializeField] private string nombreSiguienteEscena;

    void Update()
    {
        // Detecta si se presiona Enter (o la tecla Return del teclado numérico)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CambiarEscena();
        }
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}