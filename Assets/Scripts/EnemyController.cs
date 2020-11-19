using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    private Transform enemyHolder;
    public float speed; //  The speed at which the enemies travel.

    public GameObject shot; //  The projectile that the enemy fiers.
    public Transform player;    //  The player's transform.
    public Text winText;    //  The 'YOU WIN!' text.

    public float fireRate;  //  The rate at which the enmies can fire a projectile.

    public float moveRate, moveRateDelta, maximumBoundary;  //  The rate at which the enemies move, the difference at which the enemies move down where the difference in speed is determined, and the maximum boundary of the enemies.

    bool smartShooting = false; //  Determines if the enemies should shoot according to the player's position.
    float margin = .5f; //  THe margin at which the enemy will smart shoot.

    // Start is called before the first frame update
    void Start()
    {
        //winText.enabled = false;    //  Defauls the 'YOU WIN!' text to not be visible.
        InvokeRepeating("moveEnemy", .1f, moveRate);    //  Begins the movement loop of the enemies. Enemies move every .1 second at a rate of moveRate in seconds.
        enemyHolder = GetComponent <Transform> ();  //  Sets the variable 'enemyHolder' to be the transform of the enemyHolder GameObject.

        PlayerScore.startEnemyCount = enemyHolder.childCount;   //  Sets the initial number of enemies in the PlayerScore script. This is to show the number of remaining enemies.
    }

    private void Update()
    {
        keyboardController();

        PlayerScore.setEnemies(enemyHolder.childCount); //  Continuously updates the number of remaining enemies.
    }

    private void keyboardController()   //  This is a cheat for developmental use only
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            killPlayer();
        }
    }

    private void killPlayer()
    {
        for (int i = -17; i < 17; i++)    //  Fires a line of projectiles towards the player.
        {
            Vector3 b = new Vector3(i, 2f, 0f);

            Instantiate(shot, b, Quaternion.identity);
        }
    }

    void moveEnemy()
    {
        enemyHolder.position += Vector3.right * speed;  //  The movement of the enemies.

        foreach (Transform enemy in enemyHolder)    //  This controls every individual enemy that has not been shot by the player.
        {
            if (enemy.position.x < -maximumBoundary || enemy.position.x > maximumBoundary)  //  If the left-most enemy or the right-most enemy hits the maximum boundary.
            {
                speed = -speed * 1.05f; //  The movement of the enemy will be reversed. The movement of the enemies will also be increased by 5%.
                enemyHolder.position += Vector3.down * .5f; //  Moves all the enemies down by .5 of an in-game, world unit.
                moveRate -= moveRateDelta;  //  Increases the number of times the enemies move per second.

                return; //  I don't know why this is here, but I'm gonna leave it. I'm pretty sure it is to ensure that no other code is called if a boundary is hit.
            }

            if (UnityEngine.Random.value > fireRate)    //  If the randomly generated value, between 0 and 1 (inclusive), is greater than the fireRate.
            {
                shoot(enemy.position, enemy.rotation);  //  That enemy will fire a shot.
            }

            if (enemy.position.y <= -4f)    //  If the enemy is low enough to not be safe from the player and when it is impossible for the player to shoot this enemy.
            {
                GameOver.isPlayerDead = true;   //  It will deem that the player has been eliminated and will have lost the game.
                Time.timeScale = 0; //  This will halt the game.
            }

            if (smartShooting && UnityEngine.Random.value >.5f && GameOver.isPlayerDead == false)   //  If smart shooting is on and a randomly generated value, between 0 and 1 (inclusive), is greater than .5 and the player has not been eliminated.
            {
                if (enemy.position.x >= player.position.x - margin && enemy.position.x <= player.position.x + margin)   //  If the player's position is within the enemy's margin for smart shooting.
                {
                    shoot(enemy.position, enemy.rotation);  //  This enemy will fire a projectile.
                }
            }
        }

        switch (enemiesRemaining()) //  If the number of enemies remaining is:
        {
            default:    //  If the number of enemies remaining is greater than 7.
                smartShooting = false;  //  Smart shooting is disabled.
                break;
            case 7:
                onSmartShooting();  //  Turns on smart shooting.
                break;
            case 5:
                margin += .5f;  //  Increases the margin by .5 units, it should now equal to player within 1 unit.
                fireRate = .85f;    //  Increases the chance at which the enemy can fire a projectile.
                onSmartShooting();  //  Turns on smart shooting.
                break;
            case 3:
                margin += .5f;  //  Increases the margin by .5 units, it should now equal to the player being within 1.5 units
                fireRate = .75f;    //  Increases the chance at which the enemy can fire a projectile.
                onSmartShooting();  //  Turns on smart shooting.
                break;
            case 1:
                CancelInvoke(); //  Cancels all subroutines, if there are any.
                fireRate = .5f; //  Sets a 1 in 2 chance of the player firing a projectile at every quarter of a second.
                InvokeRepeating("moveEnemy", .1f, .25f);    //  Moves the last remaining enemy at a rate of once every .25 of a second.
                break;
            case 0: //  If there are no more enemies remaining.
                winText.enabled = true; //  The 'YOU WIN!' text will be shown.

                GameOver.hasWon = true; //  Sets the level to completed in the Game Over script.
                EnemyBulletController.hasWon = true;    // Sets the level to completed in the Enemy Bullet Controller script.

                Time.timeScale = .65f;  //  Slows the game down.
                
                PlayerScore.hasWon();

                break;
        }
    }

    void shoot(Vector3 pos, Quaternion rotation)
    {
        Instantiate(shot, pos, rotation);   //  Shoots a projectile at the enemy's position.
    }

    void onSmartShooting()
    {
        smartShooting = true;   //  Turns on smart shooting.
    }

    public int enemiesRemaining()
    {
        return enemyHolder.childCount;  //  Returns the number of enemies remaining.
    }
}
