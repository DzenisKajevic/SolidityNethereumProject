using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public PlayerStatusScript player;
    [SerializeField]
    private BoolSO restarted;

    // public Text gameOverCountdown;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(restarted.Value);
        if (restarted.Value)
        {
            restarted.Value = false;
            StartGame();
        }
        //gameOverCountdown.gameObject.SetActive(false);
        else Time.timeScale = 0;
    }

    private void Update()
    {
        // was planned as a timer for automatically restarting the game, but that idea was scrapped (for now)
        /*
        if (player.isDead)
        {
            gameOverCountdown.gameObject.SetActive(true);
            countTimer -= Time.unscaledDeltaTime;
        }

        gameOverCountdown.text = "Restarting in " + (countTimer).ToString("0");

        if (countTimer < 0)
        {
            RestartGame();
        }
        */
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }


    public void RestartGame()
    {
        restarted.Value = true;
        EditorSceneManager.LoadScene(0);
    }
}
