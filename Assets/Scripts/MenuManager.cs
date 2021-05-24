using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup creditsMenu;
    public CanvasGroup optionsMenu;

    public void ToggleCredits() {
        creditsMenu.alpha = creditsMenu.alpha == 1 ? 0 : 1;
        creditsMenu.interactable = !creditsMenu.interactable;
        creditsMenu.blocksRaycasts = !creditsMenu.blocksRaycasts;
    }

    public void ToggleOptions() {
        optionsMenu.alpha = optionsMenu.alpha == 1 ? 0 : 1;
        optionsMenu.interactable = !optionsMenu.interactable;
        optionsMenu.blocksRaycasts = !optionsMenu.blocksRaycasts;
    }
}
