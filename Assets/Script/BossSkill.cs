using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossSkillType
{
    Throw
}
public class BossSkill : MonoBehaviour {
    public BossSkillType type;
    public float speed = 10f;
    public float destroyTime = 2f;
    public int damage = 20;
	void Update () {
		if(type==BossSkillType.Throw)
        {
            transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
            Destroy(gameObject);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collision.GetComponent<Player>().MinusHealth(damage);
        }
    }
    public void SetSkill(int damage = 20, float speed = 10f, float destroyTime = 2f)
    {
        this.speed = speed;
        this.destroyTime = destroyTime;
        this.damage = damage;
    }
}
