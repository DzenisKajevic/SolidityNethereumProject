using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    public GameController gameController;
    public bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;
        gameController.GameOver();
    }
}
