using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Thorn,
    Anchor
}
public class Obstacle : MonoBehaviour {

    public ObstacleType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            var go = collision.gameObject.GetComponent<Player>();
            go.MinusHealth(go.maxHealth);
        }
    }
}
