using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class TrackScoreGUISO : ScriptableObject
{
    public static int score = 0;

    // this used to be a MonoBehaviour script for generating a UI and filling it with values, but that was scrapped
    // the actual script that's being used for that is now: UpdateScoreOnCollision
    /* // gave up on OnGUI completely -> it doesn't scale with the screen size...
       // switching to Canvas -> much better option
    void OnGUI()
    {
        GUIStyle style = new GUIStyle("Box");
        style.fontSize = 8;
        style.wordWrap = true;
        style.alignment = TextAnchor.UpperLeft;

        GUI.Box(new Rect(0, 0, 100, 20), "Score: " + score.ToString());
        GUI.Box(new Rect(0, 20, 150, 100), "100: " + loggedInPlayerSO.PublicKey + "\n99: " + loggedInPlayerSO.PublicKey
            + "\n98: " + loggedInPlayerSO.PublicKey + "\n97: " + loggedInPlayerSO.PublicKey + "\n96: " + loggedInPlayerSO.PublicKey, style);
    }
    */
    /* // failed version with a scrollable scoreboard (might try to fix later)
    void OnGUI()
    {
        GUIStyle style = new GUIStyle("Box");
        style.fontSize = 8;
        style.wordWrap = true;
        style.alignment = TextAnchor.UpperLeft;

        GUI.Box(new Rect(0, 0, 100, 20), "Score: " + score.ToString());

        scrollPosition = GUILayout.BeginScrollView(
        scrollPosition, style, GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.Label("100: " + loggedInPlayerSO.PublicKey + "\n99: " + loggedInPlayerSO.PublicKey
        + "\n98: " + loggedInPlayerSO.PublicKey + "\n97: " + loggedInPlayerSO.PublicKey + "\n96: " + loggedInPlayerSO.PublicKey);
        // End the scrollview we began above.

        //GUI.Box(new Rect(0, 20, 150, 100), "100: " + loggedInPlayerSO.PublicKey + "\n99: " + loggedInPlayerSO.PublicKey
        //    + "\n98: " + loggedInPlayerSO.PublicKey + "\n97: " + loggedInPlayerSO.PublicKey + "\n96: " + loggedInPlayerSO.PublicKey, style);
        GUILayout.EndScrollView();
    }
    */
}
