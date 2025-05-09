using UnityEngine;

public class BulletCannonball : Bullet
{
    [SerializeField] float peakHeight = 5f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public override void SetInitialVelocity(Vector3 direction, float shootSpeed)
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = direction * shootSpeed;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
