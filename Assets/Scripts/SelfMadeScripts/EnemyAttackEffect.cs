using UnityEngine;

public class EnemyAttackEffect : MonoBehaviour
{
    [SerializeField] private GameObject attackEffectPrefab;
    [SerializeField] private float cooldown = 0.5f;

    private float nextTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (attackEffectPrefab == null) return;
        if (!collision.gameObject.CompareTag("Player")) return;
        if (Time.time < nextTime) return;

        nextTime = Time.time + cooldown;
        
        ContactPoint cp = collision.GetContact(0);
        GameObject fx = Instantiate(attackEffectPrefab, cp.point, Quaternion.LookRotation(cp.normal));
        Destroy(fx, 1f);
    }
}
