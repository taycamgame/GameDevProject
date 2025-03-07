using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int bulletDamage = 1;
    [SerializeField] float bulletVelocity = 1.0f;
    [SerializeField] float lifetime = 5.0f;

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float GetVelocity()
    {
        return bulletVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }
        }
    }
}
