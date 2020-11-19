using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

    public static int playerScore = 0;  //  This is the player's score, it is currently not used.
    public Text scoreText;  //  The text that updates to show the remaining number of enemies.
    public Animator advance;    //  The animation that plays upon completing a level.

    public static int startEnemyCount = 0;
    public static int remainingEnemies = 0;
    public static bool levelCompleted = false;

    private void Awake()
    {
        //playerScore = 0;
        //remainingEnemies = 0;
        levelCompleted = false; //  Defaults the state of all levels, upon loading, to be not completed.
    }

    void Update()
    {
        scoreText.text = "REMAINING: " + (startEnemyCount - (startEnemyCount - remainingEnemies));  //  This counts the remaining number of enemies.

        if (levelCompleted)
        {
            if (Input.GetKeyDown(KeyCode.W))    //  If the level is completed, and the user wants to advance to the next level, pressing 'W' would do so.
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //  Loads the next scene in the build index.
                Time.timeScale = 1; //  Resets the time scale.
            }

            if (Input.GetKeyDown(KeyCode.Q))    //  Should the player want ot quit the game, pressing 'Q' would do so.
            {
                Application.Quit(); //  Quits the application.
                Debug.Log("Quit");
            }
            advance.SetBool("playAdvance", true);   //  This shows the 'GAME WIN!' text with the blinking text.
        }
    }

    public static void setEnemies(int x)
    {
        //This is called from the 'EnemyController' script.
        remainingEnemies = x;   //  Sets the number of remaining enemies.
    }

    public static void hasWon()
    {
        levelCompleted = true;  //  If all the enemies have been eliminated, the game will consider the level passed.
    }
}
