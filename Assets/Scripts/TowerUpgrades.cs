using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrades : MonoBehaviour
{
    [SerializeField] GameObject towerSlot;
    [SerializeField] PreviewTower previewTower;
    [SerializeField] Canvas upgradeCanvas;

    private LevelManager levelManager;
    private GUIManager guiManager;

    private Tower tower;
    private int powerCost = 50, rangeCost = 50, speedCost = 50;
    private int powerLevel = 1, rangeLevel = 1, speedLevel = 1;
    private int powerMax = 4, rangeMax = 4, speedMax = 4;

    [SerializeField] Button powerButton, rangeButton, speedButton;
    [SerializeField] TextMeshProUGUI powerText, powerCostText, rangeText, rangeCostText, speedText, speedCostText;

    private void Awake()
    {
        guiManager = FindFirstObjectByType<GUIManager>();
        levelManager = FindFirstObjectByType<LevelManager>();
        tower = GetComponent<Tower>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseUpgradePanel()
    {
        upgradeCanvas.enabled = false;
    }

    public void PowerUpgrade()
    {
        if (powerCost <= levelManager.GetMoney())
        {
            if (powerLevel < powerMax)
            {
                levelManager.SpendMoney(powerCost);
                powerLevel++;
                tower.IncreasePower();
                if (powerLevel == powerMax)
                {
                    powerText.text = "POWER\nMAX";
                    powerCostText.text = "SOLD\nOUT";
                    powerButton.enabled = false;
                }
                else
                {
                    powerCost *= 2;
                    powerText.text = "POWER\n" + powerLevel.ToString() + " / " + powerMax.ToString();
                    powerCostText.text = "COST:\n" + powerCost.ToString();
                }
            }
        }
        Debug.Log("Power Upgrade");
        
        
    }

    public void RangeUpgrade()
    {
        if (rangeCost <= levelManager.GetMoney())
        {
            Debug.Log("Range Upgrade");
            if (rangeLevel < rangeMax)
            {
                levelManager.SpendMoney(rangeCost);
                rangeLevel++;
                tower.IncreaseRange();
                if (rangeLevel == rangeMax)
                {
                    rangeText.text = "RANGE\nMAX";
                    rangeCostText.text = "SOLD\nOUT";
                    rangeButton.enabled = false;
                }
                else
                {
                    rangeCost *= 2;
                    rangeText.text = "RANGE\n" + rangeLevel.ToString() + " / " + rangeMax.ToString();
                    rangeCostText.text = "COST:\n" + rangeCost.ToString();
                }
            }
        }
    }

    public void SpeedUpgrade()
    {
        if (speedCost <= levelManager.GetMoney())
        {
            Debug.Log("Speed Upgrade");
            if (speedLevel < speedMax)
            {
                levelManager.SpendMoney(speedCost);
                speedLevel++;
                tower.IncreaseSpeed();
                if (speedLevel == speedMax)
                {
                    speedText.text = "SPEED\nMAX";
                    speedCostText.text = "SOLD\nOUT";
                    speedButton.enabled = false;
                }
                else
                {
                    speedCost *= 2;
                    speedText.text = "SPEED\n" + speedLevel.ToString() + " / " + speedMax.ToString();
                    speedCostText.text = "COST:\n" + speedCost.ToString();
                }
            }
        }
    }

    public void SellTower()
    {
        GameObject newTowerSlot = Instantiate(towerSlot, gameObject.transform.parent);
        guiManager.AddTowerSlot(newTowerSlot.GetComponent<TowerSlot>());
        newTowerSlot.transform.position = transform.position;
        levelManager.GainMoney(previewTower.GetTowerCost());
        Destroy(gameObject);
    }
}
