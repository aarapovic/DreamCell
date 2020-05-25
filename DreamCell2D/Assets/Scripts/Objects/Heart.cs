using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue playerHealth;
    public float ammountToIncrease;
    public FloatValue heartContainers;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
         {
            
            playerHealth.runtimeValue += ammountToIncrease;
            if(playerHealth.initialValue > heartContainers.runtimeValue*2f)
            {
                playerHealth.initialValue = heartContainers.runtimeValue * 2f;
            }
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
