using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] int maxHealth = 2;
    [SerializeField] int moneyReward = 10;
    [SerializeField] Healthbar healthbar;
    [SerializeField] float speed;

    private LevelManager levelManager;
    private Rigidbody rb;
    private Transform nextTarget;

    private int health;

    public void SetPath(GameObject path)
    {
        this.path = path;
    }

    private void Awake()
    {
        health = maxHealth;
        levelManager = FindFirstObjectByType<LevelManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextTarget = path.transform.GetChild(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log((nextTarget.position - transform.position).magnitude);
        Vector3 direction = (nextTarget.position - transform.position).normalized * speed;
        rb.MovePosition(transform.position + direction * Time.fixedDeltaTime);
        if ((nextTarget.position - transform.position).magnitude <= 0.05)
        {

            //Debug.Log("a");
            if (nextTarget.transform.GetSiblingIndex() + 1 < nextTarget.parent.childCount)
            {
                nextTarget = nextTarget.parent.GetChild(nextTarget.transform.GetSiblingIndex() + 1);
            }
            else
            {
                //no more path points (reached end)
                Die(false);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (healthbar.gameObject.activeSelf == false)
        {
            healthbar.gameObject.SetActive(true);
        }

        healthbar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die(true);
        }
    }

    private void Die(bool killedByPlayer)
    {
        if (killedByPlayer)
        {
            levelManager.GainMoney(moneyReward);
        }

        levelManager.IncrementEnemiesDead();
        Destroy(gameObject);
    }
}
