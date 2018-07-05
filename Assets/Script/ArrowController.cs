using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
    
    private Rigidbody2D rigid;
    public float power = 1f;
    public float destroyTime = 2f;
    public int damage = 1;
    private float delay = 0f;

	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.right*power);
    }
    private void Update()
    {
        delay += Time.deltaTime;
        if (delay >= destroyTime)
            Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.tag=="EnemyWeapon"&&collision.tag=="Player")
        {
            collision.GetComponent<Player>().MinusHealth(damage);
            Destroy(gameObject);
        }
        if (transform.tag == "PlayerWeapon" && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().MinusHealth(damage);
            Destroy(gameObject);
        }
        if(collision.tag=="Ground")
            Destroy(gameObject);
    }
}
