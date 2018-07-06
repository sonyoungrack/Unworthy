using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour {

    public GameObject enemyContainer;
    public GameObject winText;

    public void Update()
    {
        if(enemyContainer.transform.childCount==0)
        {
            winText.SetActive(true);
        }
    }
}
