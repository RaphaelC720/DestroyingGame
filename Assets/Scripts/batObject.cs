using UnityEngine;

public class batObject : MonoBehaviour
{
    public Rigidbody RB;
    public float explosionForce = 1000f;
    public float explosionRadius = 10f;
    public float upwardsModifier = 0f;
    public GameObject confetti;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("canPickUp"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider nearby in colliders)
            {
                Rigidbody rb = nearby.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
                }
            }
            Instantiate(confetti, collision.GetContact(0).point, Quaternion.identity);
        }
    }
}