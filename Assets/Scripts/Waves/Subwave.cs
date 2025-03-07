using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Subwave : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] int numberToSpawn;
    [SerializeField] float subwaveStartTimestamp;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float subwaveEndTimestamp;

    private bool subwaveInProgress;
    private int numberSpawned = 0;
    private float timer;

    private void Awake()
    {
        //enemyToSpawn = GetComponent<Enemy>();
        timer = timeBetweenSpawns;
    }

    private void Update()
    {
        if (subwaveInProgress)
        {
            if (numberSpawned < numberToSpawn)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    numberSpawned++;
                    enemyToSpawn.GetComponent<Enemy>().SetPath(path);
                    enemyToSpawn.transform.position = path.transform.GetChild(0).transform.position;
                    Instantiate(enemyToSpawn.gameObject);
                    timer = timeBetweenSpawns;
                }
            }
            else
            {
                EndSubwave();
            }
        }
    }

    public GameObject Path()
    {
        return path;
    }

    public GameObject Enemy()
    {
        return enemyToSpawn;
    }

    public int NumberToSpawn()
    {
        return numberToSpawn;
    }

    public float SubwaveStartTimestamp()
    {
        return subwaveStartTimestamp;
    }

    public float TimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float SubwaveEndTimestamp()
    {
        return subwaveEndTimestamp;
    }

    public void BeginSubwave()
    {
        subwaveInProgress = true;
    }

    public void EndSubwave()
    {
        subwaveInProgress = false;
    }
}
