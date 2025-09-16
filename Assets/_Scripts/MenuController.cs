using UnityEngine;
using UnityEngine.InputSystem; // For the new Input System
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Reference to the menu UI Canvas GameObject. Set this in the Inspector.
    public GameObject menuCanvas;

    // Reference to the TabController. Set this in the Inspector.
    public TabController tabController;

    // Reference to all tab buttons. Set these in the Inspector.
    public Button[] tabButtons;

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
        // Toggle menu on/off when Escape is pressed (new Input System)
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
                if(!menuCanvas.activeSelf && PauseController.IsGamePaused)
                {
                    // If the game is paused and the menu is not active, do nothing
                    return;
                }
            bool willOpen = !menuCanvas.activeSelf;
            menuCanvas.SetActive(willOpen);
            if (willOpen && tabController != null)
            {
                StartCoroutine(ForceTab0AndRestoreTabs());
            }
            PauseController.SetPause(menuCanvas.activeSelf);
        }
    }

    // Coroutine to disable tab buttons, force tab 0, then re-enable buttons
    private System.Collections.IEnumerator ForceTab0AndRestoreTabs()
    {
        // Disable all tab buttons
        if (tabButtons != null)
        {
            foreach (var btn in tabButtons)
            {
                btn.interactable = false;
            }
        }
        // Force tab 0
        tabController.ForceTab0();
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        // Wait for end of frame
        yield return new WaitForEndOfFrame();
        // Re-enable all tab buttons
        if (tabButtons != null)
        {
            foreach (var btn in tabButtons)
            {
                btn.interactable = true;
            }
        }
    }
}
