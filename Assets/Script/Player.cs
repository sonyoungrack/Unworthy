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

    public Vector3 distance;
    public Vector3 currentPosition;

	// Use this for initialization
	private void Start () 
    {
        currentPosition = transform.position;
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
            if(!weapon.attacking)
            {
                anim.Play("Work");
                var angle = (Input.GetAxisRaw("Horizontal") == 1f) ? 0f : 180f;
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                rigid.MovePosition(new Vector2(transform.position.x + (Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed), transform.position.y));
            }
        }
        distance = transform.position - currentPosition;
        currentPosition = transform.position;
    }
    public void GoAttack()
    {
        anim.Play("Attack");
        weapon.GoAttack(damage);
    }
}
