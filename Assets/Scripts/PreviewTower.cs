using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreviewTower : MonoBehaviour
{
    [SerializeField] GameObject towerToBuild;
    [SerializeField] int towerCost;

    private GUIManager guiManager;
    private LevelManager levelManager;

    private Vector3 mousePosition, worldPosition;

    private void Awake()
    {
        guiManager = FindFirstObjectByType<GUIManager>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("HIDE GUI");
        guiManager.GetComponent<Canvas>().enabled = false;
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        if (towers.Length > 0)
        {
            foreach (GameObject tower in towers)
            {
                tower.GetComponentInChildren<Canvas>().enabled = false;
            }
        }

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

        transform.position = worldPosition;

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (guiManager.GetTowerSlots().Contains(hit.transform.gameObject.GetComponent<TowerSlot>()))
            {
                hit.transform.gameObject.GetComponent<TowerSlot>().PlaceTower(towerToBuild);
                guiManager.RemoveTowerSlot(hit.transform.gameObject.GetComponent<TowerSlot>());
                levelManager.SpendMoney(GetTowerCost());
            }
            guiManager.GetComponent<Canvas>().enabled = true;
            Destroy(gameObject);
        }
    }

    public int GetTowerCost()
    {
        return towerCost;
    }
}
