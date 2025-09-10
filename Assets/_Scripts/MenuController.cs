
// Controls the in-game menu visibility using the Tab key.
// Attach this script to a GameObject in your scene and assign the menuCanvas in the Inspector.
using UnityEngine;
using UnityEngine.InputSystem; // For the new Input System

public class MenuController : MonoBehaviour
{
    // Reference to the menu UI Canvas GameObject. Set this in the Inspector.
    public GameObject menuCanvas;

    // Called before the first frame update.
    // Ensures the menu is hidden when the game starts.
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Called once per frame.
    // Listens for the Tab key to toggle the menu's visibility using the new Input System.
    void Update()
    {
        // Toggle menu on/off when Tab is pressed (new Input System)
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
