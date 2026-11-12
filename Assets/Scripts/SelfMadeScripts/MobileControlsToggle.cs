using UnityEngine;

public class MobileControlsToggle : MonoBehaviour
{
    [SerializeField] private bool showInEditor = false;

    private void Awake()
    {
        gameObject.SetActive(Application.isMobilePlatform || showInEditor);
    }
}