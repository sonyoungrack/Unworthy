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
    [Space]
    [Header("Ability")]
    public RoundBoss type;
    public int pattern = 3;
    public int nowPattern = 0;
    public float skillCollDownTime = 5f;
    public float skillPercent = 50f;
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
    [Space]
    [Header("Player")]
    public bool playerExistence = false;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Enemy>();
    }
    void Update ()
    {
        if(playerExistence)
        {
            nowPattern = pattern - (enemy.health / (enemy.maxHealth / pattern));
            skillDelay += Time.deltaTime;
            if (skillCollDownTime <= skillDelay)
            {
                if (skillPercent >= Random.Range(0f, 100f))
                {
                    var p = Random.Range(1, nowPattern);
                    if (p == 1)
                        OnePattern();
                    else if (p == 2)
                        TwoPattern();
                    else if (p == 3)
                        ThreePattern();
                }
                skillDelay = 0f;
            }
        }
	}
    public IEnumerator OneRoundBossOnePatternCo()
    {
        var go1 = Instantiate(skillOnePrefab);
        var pos1 = player.transform.position;
        pos1.y = LeftTop.transform.position.y;
        go1.transform.position = pos1;
        var gs1 = go1.GetComponent<BossSkill>();
        gs1.enabled = false;
        yield return new WaitForSeconds(1f);
        gs1.enabled = true;
        var go2 = Instantiate(skillOnePrefab);
        var pos2 = player.transform.position;
        pos2.y = LeftTop.transform.position.y;
        go2.transform.position = pos2;
        var gs2 = go2.GetComponent<BossSkill>();
        gs2.enabled = false;
        yield return new WaitForSeconds(1f);
        gs2.enabled = true;
        var go3 = Instantiate(skillOnePrefab);
        var pos3 = player.transform.position;
        pos3.y = LeftTop.transform.position.y;
        go3.transform.position = pos3;
        var gs3 = go3.GetComponent<BossSkill>();
        gs3.enabled = false;
        yield return new WaitForSeconds(1f);
        gs3.enabled = true;
    }
    public void OnePattern()
    {
        if (type == RoundBoss.One_Round_Boss) 
        {
            StartCoroutine(OneRoundBossOnePatternCo());
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
