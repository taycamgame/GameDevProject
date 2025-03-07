using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Wave
{
    [SerializeField] float waveStartTimestamp;
    [SerializeField] List<Subwave> subwaves;
    [SerializeField] float waveEndTimestamp;

    private int enemiesInWave;
    

    public float WaveStartTimestamp()
    {
        return waveStartTimestamp;
    }

    public List<Subwave> Subwaves()
    {
        return subwaves;
    }

    public float WaveEndTimestamp()
    {
        return waveEndTimestamp;
    }

    public void BeginWave()
    {
        foreach (Subwave subwave in subwaves)
        {
            enemiesInWave += subwave.NumberToSpawn();
        }
    }

    public int GetTotalEnemyCount()
    {
        return enemiesInWave;
    }
}
