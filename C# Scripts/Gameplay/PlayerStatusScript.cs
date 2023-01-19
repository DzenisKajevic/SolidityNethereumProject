using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    public GameController gameControllerScript;
    public bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameController = GameObject.Find("GameController");
        gameControllerScript = gameController.GetComponent<GameController>();
        isDead = true;
        gameControllerScript.GameOver();
    }
}
