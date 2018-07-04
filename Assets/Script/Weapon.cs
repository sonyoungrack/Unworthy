using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Bow,
    Hammer
}
public class Weapon : MonoBehaviour {

    public float attackAnimationPlayTime = 1f;
    public WeaponType type;
    public bool attacking = false;
    public bool attack = false;
    private float delay = 0f;
    private int damage;
    public GameObject projectile;
    private float attackingDelay = 0.0f;
    private bool canShouting = true;
    public void GoAttack(int damage)
    {
        attack = true;
        attacking = true;
        attackingDelay = 0f;
        this.damage = damage;
    }
    public void Update()
    {
        if(attack)
        {
            if(type!=WeaponType.Bow)
            {
                delay += Time.deltaTime;
                if (attackAnimationPlayTime <= delay)
                {
                    delay = 0f;
                    attack = false;
                }
            }
            else
            {
                if(canShouting)
                    StartCoroutine(ArrowShout());
            }
        }
        if(attacking)
        {
            attackingDelay += Time.deltaTime;
            if (attackingDelay >= attackAnimationPlayTime)
                attacking = false;
        }
    }
    public IEnumerator ArrowShout()
    {
        canShouting = false;
        yield return new WaitForSeconds(attackAnimationPlayTime);
        var proj = Instantiate(projectile);
        proj.transform.position = transform.position;
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Vector2 v = (Vector2)playerObject.transform.position - (Vector2)transform.position;
            proj.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg);
        }
        proj.GetComponent<ArrowController>().damage = damage;
        proj.transform.tag = transform.tag;
        attack = false;
        canShouting = true;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (type != WeaponType.Bow)
        {
            if (attack)
            {
                if (transform.tag == "PlayerWeapon")
                {
                    if (collision.tag == "Enemy")
                    {
                        collision.GetComponent<Enemy>().MinusHealth(damage);
                        attack = false;
                    }
                }
                else if (transform.tag == "EnemyWeapon")
                {
                    if (collision.tag == "Player")
                    {
                        //플레이어 체력 닳게하는 함수랑 연결시켜주세요.
                        attack = false;
                    }
                }
            }
        }
    }
}
