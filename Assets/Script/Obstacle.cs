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
    public int damage = 20;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == ObstacleType.Anchor)
        {
            if (collision.tag == "Player")
            {
                var go = collision.gameObject.GetComponent<Player>();
                go.MinusHealth(go.maxHealth, true);
            }
        }
        if (type == ObstacleType.Thorn)
        {
            if (collision.tag == "Player")
            {
                var go = collision.gameObject.GetComponent<Player>();
                go.MinusHealth(damage, true);
            }
        }
    }
}
