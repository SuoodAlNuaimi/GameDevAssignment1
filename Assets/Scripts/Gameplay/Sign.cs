using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    public string message;
    public GameObject signPanel;

    private bool playerInRange;
    private InputAction interactAction;
    

    void Start()
    {
        if (signPanel == null)
    {
        Debug.LogError("SignPanel is NOT assigned on " + gameObject.name);
        return;
    }

    TextMeshProUGUI tmp = signPanel.GetComponentInChildren<TextMeshProUGUI>();

    if (tmp == null)
    {
        Debug.LogError("No TextMeshProUGUI found inside SignPanel");
        return;
    }

    signPanel.SetActive(false);
    tmp.text = message;
        interactAction = UserInput.instance.controls.Player.Interact;
        interactAction.Enable();
    }

    void Update()
    {
        if (playerInRange && interactAction.WasPressedThisFrame())
        {
            signPanel.SetActive(!signPanel.activeSelf);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KnightPlayer"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("KnightPlayer"))
        {
            playerInRange = false;
            signPanel.SetActive(false);
        }
    }
}
