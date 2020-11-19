using UnityEngine;

public class BulletController : MonoBehaviour
{

    //  THIS IS THE PLAYER'S PROJECTILE CONTROLLER.

    public ParticleSystem particles0;
    public ParticleSystem particles1;
    public ParticleSystem particles2;

    public float speed; //  This is the speed at which the projectile travels.
    float bulletBoundary = 20f; //  The maximum ceiling of the projectile.

    private void FixedUpdate()
    {
        transform.position += Vector3.up * speed;   //  Continuously moves the projectile in an upward position.

        if (transform.position.y >= bulletBoundary) //  If the bullet does not hit a target.
        {
            destroyBullet();    //  It will terminate and destroy itself to prevent a projectile from infintely travelling.
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))  //  If the projectile hits an enemy
        {
            Destroy(other.gameObject);  //  The enemy will be eliminated.
            destroyBullet();    //  The projectile will be terminated.

            //  These particle effects will play upon hitting the enemy.
            Instantiate(particles0, transform.position, Quaternion.identity);
            Instantiate(particles1, transform.position, Quaternion.identity);
            Instantiate(particles2, transform.position, Quaternion.identity);

            Invoke("DestroyThis", 7f);  //  This hopes to destroy the Particle System after 7 seconds. I'm not sure this works.

            PlayerScore.playerScore++;  //  Increments the player's score by one, this is currently not used.
        }
        else if (other.CompareTag("Base"))  //  If the projectile hits a base.
        {
            destroyBullet();    //  The projectile will simply be terminated.
        }
        else if (other.CompareTag("Projectile"))    //  If the projectile hits another projectile
        {
            Destroy(other.gameObject);  //  It will terminate the other projectile.
            destroyBullet();    //  It will also terminate this projectile.
        }
    }

    void destroyBullet()
    {
        Destroy(gameObject);    //  Destroys the projectile.
    }

    void destroyThis()
    {
        //  Destroys the Particle Systems after 7 seconds.

        Destroy(particles0);
        Destroy(particles1);
        Destroy(particles2);
    }
}
