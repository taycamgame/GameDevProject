using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int money = 100;
    [SerializeField] GUIManager guiManager;

    [SerializeField] List<Wave> waves;

    private int currentWave, nextWave;
    private bool allWavesFinished;

    private int enemiesLeftInWave = 0;
    private int enemiesDeadInWave = 0;

    private float currentTimestamp;
    private float currentWaveTimestamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentWave = 0;
        currentWaveTimestamp = Time.time;
        waves[0].BeginWave();
        enemiesLeftInWave = waves[currentWave].GetTotalEnemyCount();

        UpdateMoneyUI();
        UpdateWaveUI();
        UpdateEnemiesUI();
    }

    // Update is called once per frame
    void Update()
    {
        
        currentTimestamp = Time.time;
        if (!allWavesFinished)
        {
            Debug.Log("Enemies remaining: " + (enemiesLeftInWave));
            enemiesLeftInWave = waves[currentWave].GetTotalEnemyCount() - enemiesDeadInWave;
            UpdateEnemiesUI();
            for (int i = waves[currentWave].Subwaves().Count- 1; i >= 0; i--)
            {
                Subwave subwave = waves[currentWave].Subwaves()[i];
                //enemiesLeftInWave += subwave.NumberToSpawn();
                if (currentTimestamp >= subwave.SubwaveStartTimestamp() + currentWaveTimestamp)
                {
                    subwave.BeginSubwave();
                }
                if (currentTimestamp >= subwave.SubwaveEndTimestamp() + currentWaveTimestamp)
                {
                    Debug.Log("Subwave finished at " + currentTimestamp);
                    waves[currentWave].Subwaves().RemoveAt(i);
                }
            }


            //Debug.Log(currentTimestamp + ", " + waves[currentWave].WaveEndTimestamp());
            //if (currentTimestamp >= waves[currentWave].WaveEndTimestamp())
            //{
            if (enemiesLeftInWave <= 0)
            {
                enemiesLeftInWave = 0;
                Debug.Log("Wave " + currentWave + " ended at " + currentTimestamp);
                currentWave++;
                currentWaveTimestamp = Time.time;
                if (currentWave >= waves.Count)
                {
                    //all waves ended
                    allWavesFinished = true;
                    Debug.Log("All waves finished");
                }
                else
                {
                    Debug.Log("Wave " + currentWave + " started at " + currentTimestamp);
                    waves[currentWave].BeginWave();
                    enemiesDeadInWave = 0;
                    //enemiesLeftInWave = waves[currentWave].GetTotalEnemyCount();
                    UpdateEnemiesUI();
                    UpdateWaveUI();
                }
            }

            //}
        }


        //if (!allWavesFinished)
        //{
        //    currentTimestamp = Time.realtimeSinceStartup;
        //    if (currentTimestamp > nextWave.WaveStartTimestamp())
        //    {
        //        Debug.Log("Wave 2 started");
        //        currentWave++;
        //        if (!(currentWave + 1 >= waves.Length))
        //        {
        //            nextWave = waves[currentWave + 1];

        //        }
        //        if (currentWave >= waves.Length)
        //        {
        //            //All waves completed
        //            allWavesFinished = true;
        //        }
        //    }
        //    Debug.Log(waves[currentWave].WaveStartTimestamp());
        //}
        //else
        //{
        //    Debug.Log("All waves finished");
        //}
    }

    public void IncrementEnemiesDead()
    {
        enemiesDeadInWave++;
    }

    private void UpdateMoneyUI()
    {
        guiManager.UpdateMoneyText(money.ToString());
    }

    private void UpdateWaveUI()
    {
        guiManager.UpdateWaveText((currentWave + 1).ToString() + "/" + waves.Count);
    }

    private void UpdateEnemiesUI()
    {
        guiManager.UpdateEnemiesText(enemiesLeftInWave.ToString() + "/" + waves[currentWave].GetTotalEnemyCount());
    }

    public int GetMoney()
    {
        return money;
    }

    public void SpendMoney(int moneySpent)
    {
        money -= moneySpent;
        if (money < 0)
        {
            money = 0;
        }
        UpdateMoneyUI();
    }

    public void GainMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        UpdateMoneyUI();
    }
}
