using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScoreOnCollision : MonoBehaviour
{
    public PlayerSO loggedInPlayerSO;
    public TrackScoreGUISO trackScoreScript;
    [SerializeField]
    private TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        //Debug.Log(scoreText);
    }

    // Update is called once per frames
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("GameObject1 collided with " + collider.name);
        TrackScoreGUISO.score += 1;
        scoreText.text = "Score: " + TrackScoreGUISO.score.ToString();
    }
    public void updateScore()
    {

    }


}
