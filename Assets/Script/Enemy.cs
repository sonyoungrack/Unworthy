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
        if (!seeCollider.lookPlayer)
        {
            if (!canMoveCollider.canMove)
                directionIsRight = !directionIsRight;
            var angle = (directionIsRight) ? 0f : 180f;
            var direction = (directionIsRight) ? 1f : -1f;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            rigid.MovePosition(new Vector2
                (transform.position.x + (direction * Time.deltaTime * moveSpeed),
                transform.position.y));
        }
    }
    public void GoAttack()
    {
        anim.Play("Attack");
        weapon.GoAttack(damage);
        canAttack = false;
    }
}
