using UnityEngine;

public class BaseHealth : MonoBehaviour
{

    public GameObject forDestruction;   //  This is the cube's collider that accompanies the bases. There is another cube because, without this, the particles will simply fall through the bases.

    //  The particles when a base is destroyed.
    public ParticleSystem baseOnDestroy;
    public ParticleSystem baseOnDestroy1;

    public float health = 10f;  //  The default health for the bases before they are destroyed.

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)   //  If the base has no more health.
        {
            Destroy(gameObject);    //  It will destroy the base.
            Destroy(forDestruction);    //  It will destroy the bases collider cube.

            //  These particles will play upon the destruction of the base.
            Instantiate(baseOnDestroy, transform.position, Quaternion.identity);
            Instantiate(baseOnDestroy1, transform.position, Quaternion.identity);
        }
    }
}
