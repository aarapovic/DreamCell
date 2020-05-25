using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : Interactable
{
    public Signal takeCoins;
    public Inventory playerInventory;
    public Item bow;
    private bool bowPaid;
    public GameObject dialogBox;
    public Text dialogText;
    public int bowPrice;
    // Start is called before the first frame update
    void Start()
    {
        bowPrice = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            if (!playerInventory.CheckForItem(bow) && !bowPaid)
            {
                BuyBow();

            }
            else
            {
                HasBow();
            }
        }
    }
    public void BuyBow()
    {
        if(playerInventory.numberOfCoins>=bowPrice)
        {
            playerInventory.numberOfCoins =playerInventory.numberOfCoins - bowPrice;
            takeCoins.Raise();
            dialogBox.SetActive(true);
            dialogText.text = bow.itemDescription;
            playerInventory.AddItem(bow);
            bowPaid = true;
            playerInventory.currentArrows = 10;
        }
    }
    public void HasBow()
    {
        dialogBox.SetActive(false);
        playerInRange = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogBox.SetActive(false);
            context.Raise();
            playerInRange = false;


        }
    }
}


