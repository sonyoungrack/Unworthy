using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    SwordMob,
    HammerMob,
    BowMob
}
public class Enemy : MonoBehaviour {
    public int health = 5;
    private Animator anim;
    public Weapon weapon;
    public int damage = 1;
    public bool canAttack = true;
    public float attackDelay = 2f;
    public EnemyType type;
    public ColliderBranch seeCollider;
    private float delay=0f;
    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void MinusHealth(int damage)
    {
        health -= (health-damage>=0) ? damage : health;
        if (!seeCollider.lookPlayer)
            transform.Rotate(0f, 180f, 0f);
    }
    public void Update()
    {
        if (seeCollider.lookPlayer)
        {
            if (canAttack)
            {
                GoAttack();
            }
        }
        if(!canAttack)
        {
            delay += Time.deltaTime;
            if(delay>=attackDelay)
            {
                delay = 0f;
                canAttack = true;
            }
        }
    }
    public void GoAttack()
    {
        anim.Play("Attack");
        weapon.GoAttack(damage);
        canAttack = false;
    }
}
