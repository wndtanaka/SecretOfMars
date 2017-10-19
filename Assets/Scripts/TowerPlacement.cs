using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask towerMask;

    private PlaceableTower placeableTower;
    private Transform currentTower;
    private bool hasPlaced;

    private PlaceableTower oldPlaceableTower;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        placeableTower = GetComponent<PlaceableTower>();
        oldPlaceableTower = GetComponent<PlaceableTower>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = new Vector3(mousePos.x, mousePos.y, cam.transform.position.y);
        Vector3 pos = cam.ScreenToWorldPoint(mousePos);

        if (currentTower != null && !hasPlaced)
        {
            currentTower.position = new Vector3(pos.x, 0, pos.z);

            if (Input.GetMouseButtonDown(0))
            {
                if (IsLegalPosition())
                {
                    hasPlaced = true;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = new Ray(new Vector3(pos.x, cam.transform.position.y, pos.z), Vector3.down);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, towerMask))
                {
                    if (oldPlaceableTower != null)
                    {
                        placeableTower.SetSelected(true);
                    }  
                }
                else
                {
                    if (oldPlaceableTower != null)
                    {
                        oldPlaceableTower.SetSelected(false);
                    }
                }
            }
        }
    }
    bool IsLegalPosition()
    {
        if (placeableTower.colliders.Count > 0)
        {
            return false;
        }
        return true;
    }
    public void SetItem(GameObject t)
    {
        hasPlaced = false;
        currentTower = ((GameObject)Instantiate(t)).transform;
    }
}
