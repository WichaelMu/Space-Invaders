using UnityEngine;

public class BaseDefeat : MonoBehaviour
{

    public GameObject bases;

    private Transform playerBase;

    bool a = true;  //  Determines if the bases can respawn.

    private void Start()
    {
        playerBase = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBase.childCount == 0) //  If all the bases have been destroyed.
        {
            //GameOver.isPlayerDead = true;

            seeIf();
        }
    }

    private void seeIf()
    {
        if (a)  //  If a respawn is allowed.
        {
            //Destroy(gameObject);    //  Destroy the current instance of bases. /// This has been removed because it was impeding the respawning of the bases.
            a = false;  //  This will disable the ability for the bases to respawn.

            Invoke("repairBases", 2f);  //  This will cause the bases to respawn after 2 seconds.
        }
    }

    void repairBases()
    {
        Instantiate(bases, new Vector3(0f, 1.5f, 0f), Quaternion.identity); //  The respawning of the bases.

        //Debug.Log("Waiting");
    }
}
