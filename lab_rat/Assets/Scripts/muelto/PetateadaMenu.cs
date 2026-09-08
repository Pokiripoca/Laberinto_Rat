using UnityEngine;
using UnityEngine.SceneManagement;

public class PetateadaMenu : MonoBehaviour
{
    [Header("Nombres de las Escenas")]
    public string nombreEscenaJuego = "Queso";          // Cambia por el nombre exacto de tu escena principal
    public string nombreEscenaMenu = "MenuPrincipal";   // Cambia por el nombre de tu escena de menú

    private void Start()
    {
        // Desbloquear y mostrar el cursor al entrar a esta escena
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Asignar al botón de Reiniciar / Reintentar
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    // Asignar al botón de Menú Principal
    public void IrAlMenuPrincipal()
    {
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}