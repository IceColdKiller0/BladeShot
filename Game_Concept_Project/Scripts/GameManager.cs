using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Instantiate(player, new Vector3(-8.5f, 0, 0), Quaternion.Euler(0, 0, 270));
                gameOver = false;
                _uiManager.hideTitle();
            }
        }

    }

}
