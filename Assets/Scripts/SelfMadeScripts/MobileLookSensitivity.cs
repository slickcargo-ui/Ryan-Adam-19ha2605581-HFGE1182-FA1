using UnityEngine;

public class MobileLookSensitivity : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private float mobileMultiplier = 15f;

    private void Start()
    {
        if (Application.isMobilePlatform && controller != null)
        {
            controller.mouseSensitivity *= mobileMultiplier;
        }
    }
}
