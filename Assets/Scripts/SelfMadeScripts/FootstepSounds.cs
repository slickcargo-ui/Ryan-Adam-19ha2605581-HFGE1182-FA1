using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private string soundName = "Footstep";
    [SerializeField] private float walkInterval = 0.5f;
    [SerializeField] private float runInterval = 0.3f;
    [SerializeField] private float minSpeed = 0.5f;

    private Rigidbody rb;
    private PlayerController controller;
    private float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 flat = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        bool onGround = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        if (flat.magnitude < minSpeed || !onGround || Time.timeScale == 0f)
        {
            timer = 0f;
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.Play(soundName);

                timer = (controller != null && controller.isSprinting) ? runInterval : walkInterval;
            }
        }
    }
}

