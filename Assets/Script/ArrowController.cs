using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
    
    private Rigidbody2D rigid;
    public float prevY;
    public float range = 1f;
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.right*range);
    }
    private void Update()
    {
    }
}
