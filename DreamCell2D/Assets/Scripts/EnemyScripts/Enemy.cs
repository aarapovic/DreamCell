using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    public LootTable thisLoot;
    private void Awake()
    {
        health = maxHealth.initialValue;
    }
    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health<=0)
        {
            DeathEffect();
            MakeLoot();
            this.gameObject.SetActive(false);
        }
    }
    private void MakeLoot()
    {
        if(thisLoot!=null)
        {
            PowerUp current = thisLoot.LootPowerup();
            if(current!=null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
    private void DeathEffect()
    {
        if(deathEffect!=null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

    }
    public void Knock(Rigidbody2D myRigidbody, float knockbackTime,float damage)
    {

        StartCoroutine(KnockCo(myRigidbody, knockbackTime));
        TakeDamage(damage);
    }
    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth.initialValue;
    }
    private IEnumerator KnockCo(Rigidbody2D myRigidbody,float knockbackTime)
    {
        if (myRigidbody != null )
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}

