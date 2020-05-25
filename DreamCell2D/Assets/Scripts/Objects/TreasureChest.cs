using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;
    [Header("Signals and dialog")]
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    [Header("Animation")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.runtimeValue;
        if(isOpen)
        {
            anim.SetBool("Opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            if(!isOpen)
            {
                OpenChest();

            }
            else
            {
                ChestAlreadyOpen();
            }

        }
    }
    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        context.Raise();
        
        anim.SetBool("Opened", true);
        storedOpen.runtimeValue = isOpen;
        isOpen = true;

    }

    public void ChestAlreadyOpen()
    {
        
        
        dialogBox.SetActive(false);
        
        raiseItem.Raise();
        playerInRange = false;
        


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            
            context.Raise();
            playerInRange = false;


        }
    }

}
