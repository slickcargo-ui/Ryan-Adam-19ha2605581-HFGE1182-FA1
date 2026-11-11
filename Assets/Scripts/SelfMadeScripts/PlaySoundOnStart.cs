using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private string[] soundNames;

    private void Start()
    {
        if (SoundManager.Instance == null) return;
        foreach (string n in soundNames)
            SoundManager.Instance.Play(n);
    }
}
