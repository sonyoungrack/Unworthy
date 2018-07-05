using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public GameObject gameWindow;
    public GameObject Ground;
	public void StartGame()
    {
        gameWindow.SetActive(true);
        Ground.SetActive(true);
        gameObject.SetActive(false);
    }
}
