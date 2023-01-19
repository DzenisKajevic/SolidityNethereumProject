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
    public Button startButton;
    public Button restartButton;
    public Button mainMenuButton;
    public Button submitScoreButton;
    [SerializeField]
    private BoolSO restarted;
    [SerializeField]
    private PlayerSO loggedInPlayerSO;
    [SerializeField]
    private LoadSceneScript loadSceneScript;

    // public Text gameOverCountdown;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Debug.Log("Restarted: " + restarted.Value);
        Debug.Log("PrivateKey: " + loggedInPlayerSO.PrivateKey);
        Debug.Log("PublicKey: " + loggedInPlayerSO.PublicKey);
        */
        Time.timeScale = 0;

        StartCoroutine(loadSceneScript.loadScene());
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        if (loggedInPlayerSO._leaderboardScoreList.Count < 5 || TrackScoreGUISO.score > loggedInPlayerSO._leaderboardScoreList[4])
        {
            submitScoreButton.gameObject.SetActive(true);
        }
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
        TrackScoreGUISO.score = 0;
#if UNITY_EDITOR
        EditorSceneManager.LoadScene("Main Menu");
#else
        SceneManager.LoadScene("Main Menu");
#endif

    }
    public void enableInterface(bool state)
    {
        submitScoreButton.interactable = state;
        mainMenuButton.interactable = state;
        restartButton.interactable = state;
        startButton.interactable = state;
    }
}
