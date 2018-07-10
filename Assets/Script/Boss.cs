using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoundBoss
{
    One_Round_Boss
}
public class Boss : MonoBehaviour {
    [HideInInspector]
    public Enemy enemy;

    public RoundBoss type;
    public int pattern = 3;
    public int nowPattern = 0;
    public float skillCollDownTime = 5f;
    private float skillDelay = 0f;
    [Space]
    [Header("BossRoom")]
    public GameObject LeftTop;
    public GameObject RightBottom;
    [Space]
    [Header("Damage")]
    public int patternOneDamage = 20;
    public int patternTwoDamage = 40;
    public int patternThreeDamage = 40;
    [Space]
    [Header("Prefab")]
    public GameObject skillOnePrefab;
    public GameObject skillTwoPrefab;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    void Update () {
        nowPattern = pattern - (enemy.health / (enemy.maxHealth / pattern));
        skillDelay += Time.deltaTime;
        if(skillCollDownTime<=skillDelay)
        {
            var p = Random.Range(1, nowPattern);
            if (p == 1)
                OnePattern();
            else if (p == 2)
                TwoPattern();
            else if (p == 3)
                ThreePattern();
            skillDelay = 0f;
        }
	}
    public void OnePattern()
    {
        if (type == RoundBoss.One_Round_Boss) 
        {
        }
    }
    public void TwoPattern()
    {
        if (type == RoundBoss.One_Round_Boss)
        {
            var go = Instantiate(skillTwoPrefab);
            enemy.anim.Play("Attack");
            go.transform.position = transform.position;
            var directionIsRight = transform.rotation.y == 0f;
            go.GetComponent<SwordSkill>().SetSkill("EnemyWeapon", 0.167f, 10, 0, directionIsRight);
        }
    }
    public void ThreePattern()
    {
        if (type == RoundBoss.One_Round_Boss)
        {

        }
    }
}
