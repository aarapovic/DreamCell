using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle

}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Signal playerHit;
    public GameObject projectile;
    public Signal reduceArrows;
    public Item bow;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = startingPosition.initialValue;


    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(currentState==PlayerState.interact)
        {
            return;
        }
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState!=PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }else if(Input.GetButtonDown("secondWeapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
            {
            if(playerInventory.CheckForItem(bow))
            {
                StartCoroutine(SecondAttackCo());
            }
                
            }
        else if(currentState == PlayerState.walk || currentState==PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
       
    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.3f);
        if(currentState!=PlayerState.interact)
        {
          currentState = PlayerState.walk;
        }
        
    }
    private IEnumerator SecondAttackCo()
    {
        
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        yield return new WaitForSeconds(0.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

    }

    private void MakeArrow()
    {
        if(playerInventory.currentArrows>0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup( temp, ChooseArrowDirection());
            playerInventory.ReduceArrows(arrow.arrowCost);
            reduceArrows.Raise();
    }
        }
      
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("MoveY"), animator.GetFloat("MoveX"))*Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    public void RaiseItem()
    {
        
        if(playerInventory.currentItem!=null)
        {
            
        if(currentState!=PlayerState.interact)
            {
                
                 animator.SetBool("ReceiveItem", true);
                 currentState = PlayerState.interact;
                 receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
        }
        else
        {
            animator.SetBool("ReceiveItem", false);
            currentState = PlayerState.idle;
            receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
        }
        }

    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);

        }

    }
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
     


    }

    public  void Knock(float knockbackTime,float damage)
    {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0)
        {
            playerHealthSignal.Raise();
            StartCoroutine(KnockCo(knockbackTime));
        }else
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("DeathMenu");
        }
        
    }

    private IEnumerator KnockCo(float knockbackTime)
    {
        
        
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}

