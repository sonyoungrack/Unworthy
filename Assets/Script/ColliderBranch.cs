using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    Eyesight,
    Crash_Determination,
    Can_Move,
    AttackRange
}
public class ColliderBranch : MonoBehaviour {
    public ColliderType type;
    public bool canMove = true;
    public bool missingPlayerDirectionIsRight = true;
    public List<GameObject> mobList;

    public bool lookPlayer = false;
    public void Start()
    {
        mobList = new List<GameObject>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(type==ColliderType.Eyesight)
        {
            if(collision.tag=="Player")
            {
                lookPlayer = true;
            }
        }
        if (type == ColliderType.Can_Move)
        {
            if (collision.tag == "Ground")
            {
                canMove = true;
            }
        }
        if (type == ColliderType.AttackRange)
        {
            if (collision.tag == "Enemy")
            {
                mobList.Add(collision.gameObject);
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (type == ColliderType.Eyesight)
        {
            if (collision.tag == "Player")
            {
                lookPlayer = false;
                if (collision.transform.position.x - transform.position.x > 0)
                {
                    missingPlayerDirectionIsRight = true;
                }
                else
                {
                    missingPlayerDirectionIsRight = false;
                }
            }
        }
        if (type == ColliderType.Can_Move)
        {
            if (collision.tag == "Ground")
            {
                canMove = false;
            }
        }
    }
}
