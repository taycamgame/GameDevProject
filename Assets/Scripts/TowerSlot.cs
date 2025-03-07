using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSlot : MonoBehaviour
{
    private MeshFilter filter;

    private void Awake()
    {
        filter = GetComponentInChildren<MeshFilter>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    Debug.Log("clicked");
        //    filter.mesh = null;
        //}
    }

    public void PlaceTower(GameObject towerToBuild)
    {
        towerToBuild.transform.position = transform.position;
        Instantiate(towerToBuild, gameObject.transform.parent);
        Destroy(gameObject);
    }
}
