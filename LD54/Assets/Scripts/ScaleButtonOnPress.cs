using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScaleButtonOnPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button; // Reference to the button component
    [SerializeField] private float scaleMultiplier = 0.9f; // Scale factor when pressed

    private Vector3 originalScale; // Original scale of the button

    void Start()
    {
        if (button == null)
        {
            // If the button reference is not set, try to find it on the same GameObject
            button = GetComponent<Button>();
        }

        if (button != null)
        {
            // Store the original scale of the button
            originalScale = button.transform.localScale;
        }
        else
        {
            Debug.LogError("Button component not found or not assigned to ScaleButtonOnPress script.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Scale the button down when pressed
        button.transform.localScale = originalScale * scaleMultiplier;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Return the button to its original scale when released
        button.transform.localScale = originalScale;
    }
}