using UnityEngine;
using UnityEngine.SceneManagement;

public class DifferentEndings : MonoBehaviour
{
    public int index1;
    public int index2;
    public int index3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index1 = 1;
        index2 = 5;
        index3 = 9;
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(index1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(index2);
    }

    public void Scientific()
    {
        SceneManager.LoadScene(index3);
    }
}
