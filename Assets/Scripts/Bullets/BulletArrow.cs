using UnityEngine;

public class BulletArrow : Bullet
{
    public override void SetInitialVelocity(Vector3 direction, float shootSpeed)
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = direction * shootSpeed;
    }
}
