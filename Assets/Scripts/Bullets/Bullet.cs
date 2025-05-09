using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public int bulletDamage = 1;
    [SerializeField] float bulletVelocity = 1.0f;
    [SerializeField] float lifetime = 5.0f;

    protected Vector3 targetPosition = Vector3.zero;

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public abstract void SetInitialVelocity(Vector3 direction, float shootSpeed);

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
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
