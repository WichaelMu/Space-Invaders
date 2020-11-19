using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public static bool isPlayerDead = false;    //  If the player has been shot by an enemy projectile.
    public Text gameOver;   //  The 'GAME OVER!' text.
    public Animator restart;    //  The animation the plays upon the player's defeat.

    public static bool hasWon = false;  //  If the player has eliminated every enemy.

    void Start()
    {
        //gameOver.enabled = false;   //  By default, the 'GAME OVER!' text cannot be seen.
    }

    void Update()
    {
        if (isPlayerDead && !hasWon)    //  If the player has been shot and the player has not won
        {
            Time.timeScale = .5f;   //  This will slow the game time, giving a slow-motion effect.
            gameOver.enabled = true;    //  This will show the 'GAME OVER!' text.

            restart.SetBool("playRestart", true);   //  This is the animation that plays that blinks telling the player to press 'R' to restart or press 'M' to return the main menu.
        }
    }
}
