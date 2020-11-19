using UnityEngine;
using UnityEngine.SceneManagement;

public class onClick : MonoBehaviour
{

    public void StartLevel1()
    {
        Invoke("startLevel", 1f);
    }

    void startLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void howToPlay()
    {

    }
}
