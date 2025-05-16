using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Wave
{
    [SerializeField] List<Subwave> subwaves;

    private int enemiesInWave;
    
    public List<Subwave> Subwaves()
    {
        return subwaves;
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
