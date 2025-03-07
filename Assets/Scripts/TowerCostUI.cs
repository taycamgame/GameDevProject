using TMPro;
using UnityEngine;

public class TowerCostUI : MonoBehaviour
{
    [SerializeField] PreviewTower tower;

    private TextMeshProUGUI costUI;

    private void Awake()
    {
        costUI = GetComponent<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        costUI.text = tower.GetTowerCost().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
