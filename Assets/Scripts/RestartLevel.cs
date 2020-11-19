using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && (GameOver.isPlayerDead || GameOver.hasWon))  //  If the player is dead or has completed the level and the player presses 'R'.
        {
            resetGame();    //  Resets the level.
        }

        if (Input.GetKeyDown(KeyCode.M) && GameOver.isPlayerDead)   //  If the player is dead and the player presses 'M'.
        {
            resetGame();    //  The level will be reset.

            SceneManager.LoadScene(0);  //  This will load the main menu.
        }
    }

    private void resetGame()
    {
        PlayerScore.playerScore = 0;    //  Defaults the player's score to zero.
        //PlayerScore.enemyCount = 0;   //  Resets the number of enemies.
        GameOver.isPlayerDead = false;  //  Sets the player to not dead.
        Time.timeScale = 1; //  Defaults the speed of the game.

        GameOver.hasWon = false;    //  Defaults the level to not completed in the Game Over script.
        EnemyBulletController.hasWon = false;   //  Defaults the level to not completed in the Enemy Bullet Controller script.
        EnemyBulletController.invincible = false;   //  Removes the invincible state of the player.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //  Loads the current scene.
    }
}
