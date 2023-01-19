using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField]
    private TMP_Text leaderboardText;

    // public Text gameOverCountdown;

    // Start is called before the first frame update
    void Start()
    {
        leaderboardText.text = "100: " + loggedInPlayerSO.PublicKey + "\n99: " + loggedInPlayerSO.PublicKey
            + "\n98: " + loggedInPlayerSO.PublicKey + "\n97: " + loggedInPlayerSO.PublicKey + "\n96: " + loggedInPlayerSO.PublicKey;
        /*
        Debug.Log("Restarted: " + restarted.Value);
        Debug.Log("PrivateKey: " + loggedInPlayerSO.PrivateKey);
        Debug.Log("PublicKey: " + loggedInPlayerSO.PublicKey);
        */
        Time.timeScale = 0;

        StartCoroutine(loadSkinScript.loadSkin());
    }

    private void Update()
    {

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
        TrackScoreGUISO.score = 0;
#if UNITY_EDITOR
        EditorSceneManager.LoadScene("Flappy");
#else
        SceneManager.LoadScene("Flappy");
#endif

    }
    public void ReturnToMainMenu()
    {
        restarted.Value = false;
        loggedInPlayerSO.resetValues();
#if UNITY_EDITOR
            EditorSceneManager.LoadScene("Main Menu");
#else
        SceneManager.LoadScene("Main Menu");
#endif

    }
}
