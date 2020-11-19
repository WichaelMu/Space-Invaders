using UnityEngine;
using TMPro;

public class GunControl : MonoBehaviour
{

    public TextMeshProUGUI text;

    public int weaponRange; //  The number of weapons in this level.

    string keyboardIn;  //  The keyboard input.
    bool isValidWeapon = false; //  Determins if the keyboard input is a valid weapon number.

    private void Start()
    {
        PlayerController.weapon = "1";  //  Sets the default weapon to be active int he Player Controller script.
    }

    void Update()
    {
        keyboardController();
    }

    void keyboardController()
    {
        string keyboardIn = Input.inputString;  //  Sets the keyboard input to what the player presses.

        for (int i = 1; i <= weaponRange; i++)  //  If the keyboard input is a valid weapon for this level.
        {
            if (keyboardIn == i.ToString()) //  If the keyboard input is a valid weapon.
            {
                isValidWeapon = true;   //  Sets the valid weapon to be true.
                break;
            }
        }

        if (isValidWeapon)
        {
            PlayerController.weapon = keyboardIn;   //  Sets the weapon to equal the keyboard input's weapon number in the Player Controller script.
            updateText(keyboardIn);
            isValidWeapon = false;  //  Removes the validity of the weapon as it has now been selected.
        }

        clearKeyboard();
    }

    void clearKeyboard()
    {
        keyboardIn = "";    //  Clears the keyboard so that a new keyboard input can be placed.
    }

    void updateText(string weaponNumber)
    {
        text.text = "WEAPON: " + weaponNumber;  //  Updates the text to show the player the currently selected weapon.
    }
}
