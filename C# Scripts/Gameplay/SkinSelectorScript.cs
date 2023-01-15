using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinSelectorScript : MonoBehaviour
{
    public PlayerSO ethereumPlayerProfileInfo;
    public SkinDatabaseSO skinDatabase;
    public int selectedOption = 0;
    public GameObject currentSkin;
    [SerializeField]
    private GameObject mainButton;
    [SerializeField]
    private TMP_Text mainButtonText;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void changeButtonColour()
    {
        mainButton.GetComponent<Image>().color = new Color(152, 255, 0);
        mainButtonText.text = "Continue";
        //mainButton.GetComponent<Image>().color = Color.white;
    }

    public void NextOption()
    {
        changeButtonColour();
        selectedOption++;
        if (selectedOption >= skinDatabase.SkinCount)
        {
            selectedOption = 0;
        }

        Debug.Log(selectedOption);
        SwapCharacter(skinDatabase.skinList[selectedOption]);
    }

    public void PreviousOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = skinDatabase.SkinCount - 1;
        }

        Debug.Log(selectedOption);
        SwapCharacter(skinDatabase.skinList[selectedOption]);
    }

    public void SwapCharacter(string selectedOption)
    {
        Debug.Log(selectedOption + " Idle Variant");
        // save the location of the old skin
        Transform position = currentSkin.transform;
        // load the new skin's prefab from the Resources/Birds folder
        //GameObject currentSkinPrefab = (GameObject)Resources.Load("Birds/" + selectedOption + " Idle Variant");

        // remove the current skin
        Destroy(currentSkin, 0);
        currentSkin = Instantiate(Resources.Load("Birds/" + selectedOption + " Idle Variant", typeof(GameObject))) as GameObject;
        // instantiate the new skin
        //currentSkin = (GameObject)Instantiate(currentSkinPrefab, position);
        //currentSkinPrefab = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
