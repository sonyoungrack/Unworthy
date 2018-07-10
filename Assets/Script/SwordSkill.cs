using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : MonoBehaviour {

    public float nextShout = 0.167f;
    private float delay = 0f;
    public int maxCount = 10;
    public bool directionIsRight = true;
    public int count = 0;
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.tag == "PlayerWeapon")
            if (collision.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().MinusHealth(damage);
            }
        else if(transform.tag=="EnemyWeapon")
                if (collision.tag == "Player")
                {
                    collision.gameObject.GetComponent<Player>().MinusHealth(damage);
                }
    }
    private void Update()
    {
        delay += Time.deltaTime;
        if (delay >= nextShout)
        {
            if(maxCount!=count)
            {
                var go = Instantiate(gameObject);
                var pos = transform.position;
                if (directionIsRight)
                    pos.x += 1f;
                else
                    pos.x -= 1f;
                go.transform.position = pos;
                var gs = go.GetComponent<SwordSkill>();
                gs.SetSkill(transform.tag,nextShout, maxCount, count + 1, directionIsRight,damage);
            }
            Destroy(gameObject);
        }
    }
    public void SetSkill(string tag,float shoutSpeed=0.167f,int maxCount=10,int count=0,bool directionIsRight=true,int damage=20)
    {
        transform.tag = tag;
        nextShout = shoutSpeed;
        this.maxCount = maxCount;
        this.count = count;
        this.directionIsRight = directionIsRight;
        this.damage = damage;
    }
}
