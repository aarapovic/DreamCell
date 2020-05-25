using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Interactable
{
    public bool ringSet;
    public GameObject ringItem;
    public Inventory playerInventory;
    public Item ring;
    
    private void Start()
    {
       // ringSet = false;
        ringItem.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            if (playerInventory.CheckForItem(ring))
            {
                ring.isSet=true;
                ringItem.SetActive(true);
            }
            else
            {
                ringItem.SetActive(false);
               

            }

        }
    }


}
