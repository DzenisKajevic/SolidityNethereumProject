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
using Nethereum.Unity.Rpc;
using NethereumProject.Contracts.AssetBundleTokens.ContractDefinition;

public class SkinSelectorScript : MonoBehaviour
{

    public PlayerSO loggedInPlayerSO;
    public SkinDatabaseSO skinDatabase;
    public int selectedOption = 0;
    public GameObject currentSkin;
    [SerializeField]
    private GameObject mainButton;
    [SerializeField]
    private TMP_Text mainButtonText;
    private string buyOrContinue = "Continue";

    // Start is called before the first frame update
    void Start()
    {

    }

    public void mainButtonOnClick()
    {
        if (buyOrContinue == "Continue")
        {
            loggedInPlayerSO.skinIndex = selectedOption;
#if UNITY_EDITOR
            EditorSceneManager.LoadScene("Flappy");
#else
            SceneManager.LoadScene("Flappy");
#endif
        }
        else
        {
            Debug.Log("Purchasing skin");
            // call contract and buy the skin
        }
    }

    public void changeButtonColour()
    {
        // if the skin is the free one (blue) or the user has bought the selected skin, allow them to continue
        //
        if (selectedOption == 0 || loggedInPlayerSO.SkinList.Contains(selectedOption))
        {
            mainButton.GetComponent<Image>().color = new Color(152, 255, 0);
            mainButtonText.text = "Continue";
            buyOrContinue = "Continue";
        }
        else
        {
            mainButton.GetComponent<Image>().color = Color.white;
            mainButtonText.text = "Purchase (0.01 ETH)";
            buyOrContinue = "Buy Skin";
        }
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= skinDatabase.SkinCount)
        {
            selectedOption = 0;
        }

        Debug.Log(selectedOption);
        changeButtonColour();
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
        changeButtonColour();
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
