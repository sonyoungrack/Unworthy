using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int health = 5;
    public int damage = 1;
    private Animator anim;
    public float attackDelay = 1f;
    public Weapon weapon;
    private Rigidbody2D rigid;
    public float moveSpeed = 1f;
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
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            GoAttack();
        }
        if(Input.GetAxisRaw("Horizontal")!=0f)
        {
            var angle = (Input.GetAxisRaw("Horizontal") == 1f) ? 0f : 180f;
            transform.rotation=Quaternion.Euler(0f,angle,0f);
            rigid.MovePosition(new Vector2(transform.position.x + (Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed), transform.position.y));
        }
    }
    public void GoAttack()
    {
        anim.Play("Attack");
        weapon.GoAttack(damage);
    }
}
