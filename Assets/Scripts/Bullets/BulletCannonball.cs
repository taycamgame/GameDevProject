using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletCannonball : Bullet
{
    [SerializeField] float peakHeight = 5f;

    private GameObject cannonModel;

    private Vector3 initialScale;
    private float scaleMultiplier = 1.0f;

    Rigidbody rb;

    private void Awake()
    {
        cannonModel = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();
        initialScale = cannonModel.transform.localScale;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public override void SetInitialVelocity(Vector3 direction, float shootSpeed)
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = direction * shootSpeed;

        //Vector3 start = transform.position;
        //Vector3 end = targetPosition;

        //// Horizontal displacement (x and z only)
        //Vector3 displacementXZ = new Vector3(end.x - start.x, 0, end.z - start.z);

        //// Vertical distance to peak (from start)
        //float heightDifference = peakHeight - start.y;
        //if (heightDifference <= 0f)
        //{
        //    Debug.LogWarning("Peak height must be above the starting point.");
        //    heightDifference = 0.1f;
        //}

        //// Time to reach the peak
        //float gravity = Mathf.Abs(Physics.gravity.y);
        //float timeToPeak = Mathf.Sqrt(2 * heightDifference / gravity);

        //// Total time (up and down)
        //float totalTime = timeToPeak + Mathf.Sqrt(2 * Mathf.Max(0, peakHeight - end.y) / gravity);

        //// Initial velocity components
        //Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * gravity * heightDifference);
        //Vector3 velocityXZ = displacementXZ / totalTime;

        //// Combine and launch
        //Vector3 finalVelocity = velocityXZ + velocityY;
        //rb.linearVelocity = finalVelocity;
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
            else if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "TowerSlot")
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        scaleMultiplier += Time.deltaTime * 0.5f;
        cannonModel.transform.localScale = initialScale * scaleMultiplier;
        scaleMultiplier += Time.deltaTime * 0.5f;
    }
}
