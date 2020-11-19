using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{

    //  THIS IS THE ENEMY'S PROJECTILE CONTROLLER.

    //  The particles that play when the player is shot.
    public ParticleSystem playerDeath;
    public ParticleSystem playerDeath1;

    //  The particles that play if the projectile hits a base.
    public ParticleSystem baseHit;
    public ParticleSystem baseHit1;

    public float speed; //  The speed at which the projectile travels.

    public static bool hasWon = false, invincible = false;  //  hasWon is the state of the level ? passed : attempting, invincible is for developmental purposes.

    float bulletBoundary = -15f;    //  The maximum floor of the projectile.

    private void FixedUpdate()
    {
        transform.position += Vector3.up * -speed;  //  Continuously moves the projectile in an downward position.

        if (transform.position.y <= bulletBoundary) //  If the bullet does not hit a target.
        {
            destroyBullet();    //  It will terminate and destroy itself to prevent a projectile from infintely travelling.
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasWon && !invincible)   //  If the projectile has hit the player, the player has not passed the level and the player is not invincible.
        {
            // Destroys the player.
            Destroy(other.gameObject);
            // Destroys the bullet.
            destroyBullet();

            // Plays the effects of the player's death at the player's death location.
            Instantiate(playerDeath, transform.position, Quaternion.identity);
            Instantiate(playerDeath1, transform.position, Quaternion.identity);

            // Sets the player to dead in the GameOver class.
            GameOver.isPlayerDead = true;
        } else if (other.CompareTag("Base"))    //  If the projectile hits a base.
        {
            GameObject playerBase = other.gameObject;   //  Sets the GameObject variable 'playerBase' to the base that was hit.
            BaseHealth baseHealth = playerBase.GetComponent<BaseHealth>();  //  Gets the health of the base that was hit from the BaseHealth script.
            baseHealth.health -= 1f;    //  Decreases the bases health by one.

            //  These particles are player where the projectile has hit the base.
            Instantiate(baseHit, transform.position, Quaternion.identity);
            Instantiate(baseHit1, transform.position, Quaternion.identity);

            destroyBullet();    //  Destroys the projectile.
        }
    }

    void destroyBullet()
    {
        Destroy(gameObject);    //  Destroys the projectile.
    }
}
