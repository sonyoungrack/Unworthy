using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipText : MonoBehaviour {

    public float tipTextShowTime = 2f;
    public float tipingPerSecend = 0.5f;
    private string nowMessage = "";
    public string message = "";
    private char[] messageTemp;
    private Queue<char> messageQueue;
    private float delay = 0f;
    private Text tipText;
    private bool showTip = false;
    private float destroyDelay = 0f;
    private bool destroyShowTip = false;
	
	void Start () {
        messageQueue = new Queue<char>();
        tipText = GameObject.FindGameObjectWithTag("TipText").GetComponent<Text>();
	}
	public void ShowTip()
    {
        if (showTip||destroyShowTip)
            return;
        messageTemp = message.ToCharArray();
        foreach (var c in messageTemp)
        {
            messageQueue.Enqueue(c);
        }
        showTip = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            ShowTip();
        }
    }
    void Update () {
        if(showTip)
        {
            delay += Time.deltaTime;
            if (delay >= tipingPerSecend)
            {
                char c = messageQueue.Dequeue();
                if (c == '&')
                    c = '\n';
                nowMessage = nowMessage + c;
                tipText.text = nowMessage;
                if(messageQueue.Count<=0)
                {
                    destroyShowTip = true;
                    showTip = false;
                }
            }
        }
        else if(destroyShowTip)
        {
            destroyDelay += Time.deltaTime;
            if(destroyDelay>=tipTextShowTime)
            {
                tipText.text = "";
                Destroy(gameObject);
            }
        }
	}
}
