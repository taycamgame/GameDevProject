using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject towerToBuy;
    [SerializeField] Material opaqueMaterial, transparentMaterial;
    [SerializeField] TextMeshProUGUI moneyUI, enemiesUI, waveUI, healthUI;
    [SerializeField] Button nextWaveButton, speedButton;
    [SerializeField] Sprite regularSpeedSprite, fastSpeedSprite, fastestSpeedSprite;

    private enum Speed
    {
        NORMAL,
        FAST,
        FASTER
    }

    private Speed speed;

    private LevelManager levelManager;

    private List<TowerSlot> towerSlots;

    private Vector3 mousePosition, worldPosition;

    [Serializable]
    public enum Buttons
    {
        BuyTower,
        SellTower,
        UpgradeTower
    }

    private void Awake()
    {
        towerSlots = FindObjectsByType<TowerSlot>(FindObjectsSortMode.None).ToList();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            worldPosition = hit.point;
        }
        //Debug.Log(worldPosition.ToString());
    }

    public void ToggleNextWaveButton(bool state)
    {
        nextWaveButton.gameObject.SetActive(state);
    }

    public List<TowerSlot> GetTowerSlots()
    {
        return towerSlots;
    }

    public void RemoveTowerSlot(TowerSlot towerSlot)
    {
        towerSlots.Remove(towerSlot);
    }
    public void AddTowerSlot(TowerSlot towerSlot)
    {
        towerSlots.Add(towerSlot);
    }

    public void BuyTower(GameObject towerToBuy)
    {
        //towerToBuild = towerToBuy;
        //towerToBuild.transform.position = worldPosition;
        //towerToBuild.GetComponentInChildren<MeshRenderer>().material = transparentMaterial;
        //Instantiate(towerToBuild);
        if (towerToBuy.GetComponent<PreviewTower>().GetTowerCost() <= levelManager.GetMoney())
        {
            Instantiate(towerToBuy).transform.position = mousePosition;
        }
    }
    public void ToggleSpeed()
    {
        (speedButton.image.sprite, Time.timeScale, speed) = speed switch
        {
            Speed.NORMAL => (fastSpeedSprite, 2.0f, Speed.FAST),
            Speed.FAST => (fastestSpeedSprite, 4.0f, Speed.FASTER),
            _ => (regularSpeedSprite, 1.0f, Speed.NORMAL)
        };
    }

    public void BeginNextWave()
    {
        levelManager.BeginNextWave();
    }

    public void UpdateMoneyText(string moneyText)
    {
        moneyUI.text = moneyText;
    }

    public void UpdateWaveText(string waveText)
    {
        waveUI.text = waveText;
    }

    public void UpdateEnemiesText(string enemiesText)
    {
        enemiesUI.text = enemiesText;
    }

    public void UpdateHealthText(string healthText)
    {
        healthUI.text = healthText;
    }
}
