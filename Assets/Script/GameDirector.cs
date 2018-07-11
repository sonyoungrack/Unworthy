using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {

    public Image panel;
    public float panelBlackSpeed = 2f;
    public Text clearText;
    public float tipingSpeed = 2f;
    public GameObject lastBoss;
    private Queue<char> queue;
    private float alpha=0f;
    private float delay = 0f;
    private float tipingDelay = 0f;
    private string nowString="";
    private bool goMain = false;

    private void Awake()
    {
        queue = new Queue<char>();
        string m = "Thanks For Playing && You cleared this game.";
        char[] me = m.ToCharArray();
        foreach(var c in me)
        {
            if (c == '&')
                queue.Enqueue('\n');
            else
                queue.Enqueue(c);
        }
    }
    public void Update()
    {
        if(!goMain)
        {
            if (delay >= panelBlackSpeed)
            {
                tipingDelay += Time.deltaTime;
                if (tipingDelay >= tipingSpeed)
                {
                    nowString += queue.Dequeue();
                    clearText.text = nowString;
                    if (queue.Count == 0)
                    {
                        goMain = true;
                    }
                }
            }
            else if (lastBoss == null)
            {
                if (!panel.gameObject.activeInHierarchy)
                    panel.gameObject.SetActive(true);
                alpha += 255f / 255f / panelBlackSpeed * Time.deltaTime;
                panel.color = new Color(0f, 0f, 0f, alpha);
                delay += Time.deltaTime;
            }
        }
        else if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
