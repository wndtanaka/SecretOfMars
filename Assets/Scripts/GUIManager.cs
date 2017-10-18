using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameObject shopPanel;



    private bool shopMenu = true;
   
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShopMenu()
    {
        if (shopMenu)
        {
            shopPanel.SetActive(true);
            shopMenu = false;
        }
        else
        {
            shopPanel.SetActive(false);
            shopMenu = true;
        }
    }
}
