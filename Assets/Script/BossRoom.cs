using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoom : MonoBehaviour {
    public Boss boss;
    public Player player;
    public Image bossHealthBar;
    [Space]
    [Header("BossRoom")]
    public GameObject LeftTop;
    public GameObject RightBottom;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void Update()
    {
        if(boss!=null&&bossHealthBar.gameObject.activeInHierarchy)
        {
            bossHealthBar.fillAmount = (float)boss.enemy.health / boss.enemy.maxHealth;
        }
    }
    public void ShowHealthBar()
    {
        bossHealthBar.transform.parent.gameObject.SetActive(true);
    }
}
