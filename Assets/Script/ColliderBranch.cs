using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType
{
    Eyesight,
    Crash_Determination
}
public class ColliderBranch : MonoBehaviour {
    private GameObject parent;
    public ColliderType type;
    private Enemy enemy;

    public bool lookPlayer = false;
    public void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        enemy = parent.GetComponent<Enemy>();
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(type==ColliderType.Eyesight)
        {
            if(collision.tag=="Player")
            {
                lookPlayer = true;
            }
        }
    }
    public void LateUpdate()
    {
        lookPlayer = false;
    }
}
