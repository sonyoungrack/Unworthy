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
    private Rigidbody2D rigid;
    public ColliderBranch seeCollider;
    public ColliderBranch canMoveCollider;
    public bool directionIsRight = true;
    public float moveSpeed = 1f;
    private float delay=0f;
    public bool discoveryPlayer = false;
    public void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    public void MinusHealth(int damage)
    {
        health -= (health-damage>=0) ? damage : health;
        if (!seeCollider.lookPlayer)
            directionIsRight = !directionIsRight;
        if(health==0)
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (seeCollider.lookPlayer)
        {
            discoveryPlayer = true;
            if (canAttack)
            {
                GoAttack();
            }
        }
        else
        {
            if (discoveryPlayer)
            {
                discoveryPlayer = false;
                directionIsRight = seeCollider.missingPlayerDirectionIsRight;
            }
            if (!canMoveCollider.canMove)
                directionIsRight = !directionIsRight;
            anim.Play("Work");
            var angle = (directionIsRight) ? 0f : 180f;
            var direction = (directionIsRight) ? 1f : -1f;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            rigid.velocity = new Vector2(moveSpeed * direction, rigid.velocity.y);
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Break")
        {
            directionIsRight = !directionIsRight;
        }
    }
    public void GoAttack()
    {
        anim.Play("Attack");
        weapon.GoAttack(damage);
        canAttack = false;
    }
}
