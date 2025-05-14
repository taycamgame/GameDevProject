using UnityEngine;

public class TowerUpgrades : MonoBehaviour
{
    [SerializeField] GameObject towerSlot;
    [SerializeField] PreviewTower previewTower;

    private LevelManager levelManager;
    private GUIManager guiManager;

    private Tower tower;

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

    public void SellTower()
    {
        GameObject newTowerSlot = Instantiate(towerSlot, gameObject.transform.parent);
        guiManager.AddTowerSlot(newTowerSlot.GetComponent<TowerSlot>());
        newTowerSlot.transform.position = transform.position;
        levelManager.GainMoney(previewTower.GetTowerCost());
        Destroy(gameObject);
    }
}
