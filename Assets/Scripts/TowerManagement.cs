using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManagement : MonoBehaviour
{
    public GameObject[] towers;
    private TowerPlacement towerPlacement;

    void Start()
    {
        towerPlacement = GetComponent<TowerPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void BuildTower()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            towerPlacement.BuyTower(towers[i]);
            Debug.Log(towers[i].name);
        }
    }
}
