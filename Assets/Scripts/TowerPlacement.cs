using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public LayerMask towerMask;

    private Transform currentTower;
    private bool hasPlaced = true;
    private PlaceableTower placeableTower;
    private PlaceableTower oldPlaceableTower;
    private Camera cam;
    private RaycastHit hit;
    private Ray ray;

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = Input.mousePosition;
        mousePos = new Vector3(mousePos.x, mousePos.y, cam.transform.position.y);
        Vector3 pos = cam.ScreenToWorldPoint(mousePos);

        if (currentTower != null && !hasPlaced)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~towerMask, QueryTriggerInteraction.Collide))
            {
                currentTower.position = hit.transform.position + Vector3.up * 0.5f;
            }

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
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, towerMask))
                {
                    if (oldPlaceableTower != null)
                    {
                        oldPlaceableTower.SetSelected(false);
                    }
                    hit.collider.gameObject.GetComponent<PlaceableTower>().SetSelected(true);
                    oldPlaceableTower = hit.collider.gameObject.GetComponent<PlaceableTower>();
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
        Debug.Log("Cant create there!");
        return true;
    }
    public void SetItem(GameObject t)
    {
        hasPlaced = false;
        currentTower = ((GameObject)Instantiate(t)).transform;
        placeableTower = currentTower.GetComponent<PlaceableTower>();
    }
}
