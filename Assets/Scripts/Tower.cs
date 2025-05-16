using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float sightRadius = 5.0f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float shootSpeed;
    [SerializeField] float towerShootCooldown = 1.0f;
    [SerializeField] float weaponRotationSpeed = 1.0f;
    [SerializeField] Canvas upgradeCanvas;
    [SerializeField] int attackPower = 1;

    private GameObject weaponModel, weaponPosition, closestEnemy;
    private float shootCooldown = 0;
    private bool canShoot = true;

    private Collider[] hits;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponModel = transform.GetChild(1).gameObject;
        weaponPosition = weaponModel.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            canShoot = true;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Upgrade menu");
        
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        Debug.Log("Towers: " + towers.Length);
        foreach (GameObject tower in towers)
        {
            tower.GetComponentInChildren<Canvas>().enabled = false;
        }
        upgradeCanvas.enabled = true;
    }


    private void FixedUpdate()
    {
        //Get closest object
        hits = Physics.OverlapSphere(transform.position, sightRadius, enemyLayer);
        if (hits.Length > 0)
        {
            float closestDistance = float.MaxValue;
            closestEnemy = null;
            foreach (Collider hit in hits)
            {
                Vector3 vectorFromTowerToHit = (hit.transform.position - weaponModel.transform.position);
                Vector3 normalizedVectorFromTowerToHit = vectorFromTowerToHit.normalized;
                float distance = vectorFromTowerToHit.magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit.gameObject;
                }
            }

            Vector3 direction = (closestEnemy.transform.position - weaponPosition.transform.position).normalized;

            weaponModel.transform.rotation = Quaternion.Slerp(weaponModel.transform.rotation, Quaternion.LookRotation(direction), weaponRotationSpeed);

            if (canShoot && closestEnemy != null)
            {
                Shoot(closestEnemy, direction);

                canShoot = false;
                shootCooldown = towerShootCooldown;
            }
        }

    }


    public void Shoot(GameObject target, Vector3 direction)
    {
        var newBullet = Instantiate(bullet, weaponPosition.transform.position, Quaternion.identity);
        newBullet.transform.LookAt(weaponPosition.transform.position + direction);
        newBullet.GetComponent<Bullet>().SetDamage(attackPower);
        newBullet.GetComponent<Bullet>().SetTargetPosition(target.transform.position);
        newBullet.GetComponent<Bullet>().SetInitialVelocity(direction, shootSpeed);
    }

    public void IncreasePower()
    {
        attackPower++;
    }

    public void IncreaseRange()
    {
        sightRadius += 2;
    }

    public void IncreaseSpeed()
    {
        shootSpeed += 2;
        shootCooldown -= 0.2f;
    }
}
