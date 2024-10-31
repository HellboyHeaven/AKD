using IPD;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private InputPromptImage iconImage;

    private bool _isHidden;
    public void ShowPrompt(IInteractable interactable)
    {
        // Example: Assuming you have UI elements to show prompt and icon
        promptText.text = interactable.Prompt;
        iconImage.Initialize(interactable.PromptSettings);
        promptText.gameObject.SetActive(true);
        iconImage.gameObject.SetActive(true);
        _isHidden = false;
    }
    public void HidePrompt()
    {
        if (_isHidden) return;
            // Example: Assuming you have UI elements to show prompt and icon
        promptText.gameObject.SetActive(false);
        iconImage.gameObject.SetActive(false);
    }
}