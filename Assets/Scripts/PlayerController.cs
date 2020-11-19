using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed, maximumBoundary, fireRate;
    float horiz;
    float verti;

    public GameObject shot; //  Weapon 1.

    public GameObject Weapon2;  //  Weapon 2.
    public Transform shotSpawn; //  Default firing location.
    public Transform shotSpawnB;    //  Secondary firing location.
    public Image heatBar;   //  The heat bar for the gun.
    public Camera mainCamera;   //  The camera...

    public float heat, coolDelta;

    private bool canFire = true;

    [HideInInspector]
    public static string weapon = "1";  //  The default weapon for the player.

    void FixedUpdate()
    {

        #region Player Movement
        horiz = Input.GetAxis("Horizontal");    //  The player can ONLY move on the horizontal axis.

        if (transform.position.x < -maximumBoundary && horiz < 0)   //  If the player hits the maximum boundary of the game, the input for movement will equal zero; static.
        {
            resetHoriz();   //  Stops the player from moving.
        }
        else if (transform.position.x > maximumBoundary && horiz > 0)   //  If the player hits the maximum boundary of the game, the input for movement will equal zero; static.
        {
            resetHoriz();   //  Stops the player from moving.
        }

        transform.position += Vector3.right * horiz * speed;    //  The actual movement of the player.
        #endregion

        // ******** //
        // THESE TWO CONTROL THE PLAYER'S UP AND DOWN MOVEMENT //
        // TO REACTIVATE, THE FLOAT 'verti' NEEDS TO BE UN-COMMENTED //

        verti = Input.GetAxis("Vertical");

        transform.position += Vector3.up * verti * speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && canFire)   //  If the player requests a shot and the gun is in the process of cooling down.
        {
            switch (weapon)
            {
                case "1":
                    shootNormal();  //  The player will shoot normally if Weapon 1 is selected.
                    break;
                case "2":
                    shootWeapon2(); //  The player will shoot Weapon 2, if it is selected.
                    break;
            }
        }

        keyboardController();

        heatBar.fillAmount -= coolDelta;    //  Continuously cools the gun.

        heatBarFull();

        if (heatBar.fillAmount == 0)    //  If the heat bar is at its lowest state, the player can fire.
        {
            canFire = true; //  If the heat bar has completly cooled down, this will re-enable the ability to fire.
        }
    }

    void keyboardController()   //  These are cheats for developmental use only.
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            killAll();  //  This fires multiple shots to quickly eliminate all enemies.
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            EnemyBulletController.invincible = !EnemyBulletController.invincible;    //  This sets the player to be invincible.
            Debug.Log("Invincibility Status: " + EnemyBulletController.invincible);
        }
    }

    private void killAll()  //  This is for developmental and testing use only.
    {
        for (int x = -20; x < 20; x++)
        {
            Vector3 a = new Vector3(x, -2f, 0f);

            Instantiate(shot, a, shotSpawn.rotation);   //  Fires multiple shots in a line.
        }
    }

    void shootNormal()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);  //  Fires Weapon 1 at the default location.
        Instantiate(shot, shotSpawnB.position, shotSpawn.rotation); //  If the player's ship has a second firing location, it will shoot another projectile.

        heatBar.fillAmount += heat; //  Adds the default heat of the gun to the heat bar, as a result of firing.

        heatBarFull();
    }

    void shootWeapon2()
    {
        Instantiate(Weapon2, shotSpawn.position, shotSpawn.rotation);   //  Fires Weapon 2 at the default location.
        Instantiate(Weapon2, shotSpawnB.position, shotSpawn.rotation);  //  If the player's ship has a second firing location, it will shoot another projectile.

        heatBar.fillAmount += heat * 1.2f;  //  Adds 20% more than the default heat of the gun to the heat bar, as a result of firing Weapon 2.

        heatBarFull();
    }

    void heatBarFull()
    {
        if (heatBar.fillAmount >= .995f)    //  If the heat bar is full (or almost full).
        {
            canFire = false;    //  Disables the ability to fire.
            coolDelta = 0.01f;  //  Slows down the cooling process, as a result of rapid firing.

            Invoke("resetCoolDelta", 1.6f); //  This will reset the cooling process to its default state after 1.6 seconds.
        }
    }

    void resetCoolDelta()
    {
        coolDelta = .05f;   //  Resets the cooling process.
    }

    void resetHoriz()
    {
        // if a player is at a boundary, and the horizontal input still wants to go beyond the boundary, it will reset the movement to zero.
        // Stops the player from going over the boundaries.

        horiz = 0;
    }
}
