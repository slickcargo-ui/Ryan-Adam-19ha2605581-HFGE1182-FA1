using UnityEngine;

[DefaultExecutionOrder(100)] // runs after SoundManager's own Awake
[RequireComponent(typeof(SoundManager))]
public class SoundManagerBinder : MonoBehaviour
{
    private void Awake()
    {
        SoundManager mine = GetComponent<SoundManager>();

        if (SoundManager.Instance != mine)
        {
            string was = SoundManager.Instance != null
                ? SoundManager.Instance.name
                : "null";
            Debug.Log($"[SoundManagerBinder] Instance was '{was}', rebinding to '{name}' in {gameObject.scene.name}");
            SoundManager.Instance = mine;
        }
    }
}
