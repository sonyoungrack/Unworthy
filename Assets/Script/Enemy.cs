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
    public int damage = 1;
    public bool canAttack = true;
    public bool attacking = false;
    public float attackDelay = 2f;
    public GameObject arrowPrefab;
    public EnemyType type;
    private Rigidbody2D rigid;
    public ColliderBranch seeCollider;
    public ColliderBranch canMoveCollider;
    public bool directionIsRight = true;
    public float moveSpeed = 1f;
    private float delay=0f;
    public bool discoveryPlayer = false;
    private float attackAnimPlayTime = 0.6f;
    private float attackingDelay = 0f;

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
            anim.Play("Run");
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
        if(attacking)
        {
            attackingDelay += Time.deltaTime;
            if(attackingDelay>=attackAnimPlayTime)
            {
                if(seeCollider.mob!=null)
                {
                    seeCollider.mob.GetComponent<Player>().MinusHealth(damage);
                }
                attacking = false;
                attackingDelay = 0f;
            }
        }
    }
    public void ShoutArrow()
    {
        var go = Instantiate(arrowPrefab);
        var pos = transform.position;
        pos.y += 2f;
        go.transform.position = pos;
        go.transform.localRotation = transform.localRotation;
        go.tag = "EnemyWeapon";
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
        if (type != EnemyType.BowMob)
        {
            anim.Play("Attack");
            canAttack = false;
            attacking = true;
        }
        else
        {
            ShoutArrow();
        }
    }
}
