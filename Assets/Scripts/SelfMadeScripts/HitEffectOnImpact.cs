using UnityEngine;

public class HitEffectOnImpact : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private string[] effectTags;

    private bool used;

    private void OnCollisionEnter(Collision collision)
    {
        if (used || hitEffectPrefab == null) return;

        foreach (string t in effectTags)
        {
            if (collision.gameObject.tag == t)
            {
                used = true;
                ContactPoint cp = collision.GetContact(0);
                GameObject fx = Instantiate(hitEffectPrefab, cp.point, Quaternion.LookRotation(cp.normal));
                Destroy(fx, 2f);
                return;
            }
        }
    }
}
