using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 5.0f;

    private int bulletDamage = 1;

    protected Vector3 targetPosition = Vector3.zero;

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected int GetBulletDamage()
    {
        return bulletDamage;
    }

    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

    public abstract void SetInitialVelocity(Vector3 direction, float shootSpeed);

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Enemy>().TakeDamage(GetBulletDamage());
            }
        }
    }
}
