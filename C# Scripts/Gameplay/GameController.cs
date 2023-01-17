using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#else
using UnityEngine.SceneManagement;
#endif

public class GameController : MonoBehaviour
{
    public new GameObject camera;
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public PlayerStatusScript player;
    [SerializeField]
    private BoolSO restarted;
    [SerializeField]
    private PlayerSO loggedInPlayerSO;
    [SerializeField]
    private LoadSkin loadSkinScript;

    // public Text gameOverCountdown;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Restarted: " + restarted.Value);
        Debug.Log("PrivateKey: " + loggedInPlayerSO.PrivateKey);
        Debug.Log("PublicKey: " + loggedInPlayerSO.PublicKey);
        /*
        if (restarted.Value)
        {
            Debug.Log("Restarted: " + restarted.Value);
            restarted.Value = false;
            camera.transform.position = new Vector3(0, 0, -25);
            StartGame();
        }
        //gameOverCountdown.gameObject.SetActive(false);
        else
        {
            Debug.Log("Restarted: " + restarted.Value);
            Time.timeScale = 0;

            StartCoroutine(loadSkinScript.loadSkin());
            
        }
        */
        Time.timeScale = 0;

        StartCoroutine(loadSkinScript.loadSkin());
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
        //EditorSceneManager.LoadScene("Flappy");
    }

    public void RestartGame()
    {
        restarted.Value = true;
#if UNITY_EDITOR
        EditorSceneManager.LoadScene("Flappy");
#else
        SceneManager.LoadScene("Flappy");
#endif

    }
    public void ReturnToMainMenu()
    {
        restarted.Value = false;
        loggedInPlayerSO.PrivateKey = null;
        loggedInPlayerSO.PublicKey = null;
        loggedInPlayerSO.SkinList = null;
#if UNITY_EDITOR
            EditorSceneManager.LoadScene("Main Menu");
#else
        SceneManager.LoadScene("Main Menu");
#endif

    }
}
