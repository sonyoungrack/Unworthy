using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    Eyesight,
    Crash_Determination,
    Can_Move
}
public class ColliderBranch : MonoBehaviour {
    private GameObject parent;
    public ColliderType type;
    private Enemy enemy;
    public bool canMove = true;
    public bool missingPlayerDirectionIsRight = true;

    public bool lookPlayer = false;
    public void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        enemy = parent.GetComponent<Enemy>();
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
