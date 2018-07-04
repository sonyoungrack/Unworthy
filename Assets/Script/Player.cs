using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public int health = 5;
    public int maxHealth = 5;
    public int damage = 1;
    private Animator anim;
    public float attackDelay = 1f;
    public Weapon weapon;
    private Rigidbody2D rigid;
    public float moveSpeed = 1f;
    public Image healthBar;

	// Use this for initialization
	void Start () {
	}
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    public void MinusHealth(int damage)
    {
        health -= (health - damage >= 0) ? damage : health;
        healthBar.fillAmount = (float)health / maxHealth;
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            GoAttack();
        }
        if(Input.GetAxisRaw("Horizontal")!=0f)
        {
            if(!weapon.attacking)
            {
                anim.Play("Work");
                var angle = (Input.GetAxisRaw("Horizontal") == 1f) ? 0f : 180f;
                
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                rigid.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigid.velocity.y);
            }
        }
        else
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
    }
    public void GoAttack()
    {
        anim.Play("Attack");
        weapon.GoAttack(damage);
    }
}
