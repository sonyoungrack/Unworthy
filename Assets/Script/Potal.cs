using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potal : MonoBehaviour {
    public Image panel;
    public GameObject locationWithGameObject;
    public Vector2 locationWithXY;
    private bool teleporting = false;
    private GameObject player;
    private float alpha = 0f;
    private bool alphaMinusing = false;
    public float teleportTime = 1f;
    private float teleportDelay = 0f;
    public bool BossPotal = false;
    public BossRoom bossRoom;

    private void Awake()
    {
        panel = GameObject.FindGameObjectWithTag("TeleportPanel").GetComponent<Image>();
    }
    private void Update()
    {
        if(alphaMinusing)
        {
            alpha -= (255f / teleportTime) * Time.deltaTime;
            panel.color = new Color(0f, 0f, 0f, alpha / 255f);
            if (panel.color.a <= 0f)
            {
                alphaMinusing = false;
                panel.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!panel.gameObject.activeInHierarchy)
                panel.gameObject.SetActive(true);
            alphaMinusing = false;
            alpha += (255f / teleportTime)*Time.deltaTime;
            panel.color = new Color(0f, 0f, 0f, alpha / 255f);
            teleportDelay += Time.deltaTime;
            if(teleportDelay>=teleportTime)
            {
                if (locationWithGameObject != null)
                {
                    collision.gameObject.transform.position = locationWithGameObject.transform.position;
                }
                else
                {
                    collision.gameObject.transform.position = locationWithXY;
                }
                if (BossPotal)
                {
                    bossRoom.ShowHealthBar();
                    bossRoom.boss.playerExistence = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            alphaMinusing = true;
            teleportDelay = 0f;
        }
    }
}
