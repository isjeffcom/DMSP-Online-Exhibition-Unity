using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    // Instance for cross cs access
    public static DialogController _ins;

    // All UI Array Container
    private string dialogsJson;
    //private GameObject[] All_Dialogs;
    private DialogList DialogList = new DialogList();

    // Get Dialog container
    private GameObject dialogCont;
    private GameObject dialogAns;

    private string api = "https://playground.eca.ed.ac.uk/~s1888009/dmsp/dialogs.json";

    private void Awake()
    {
        // For global access
        _ins = this;

        // Find container
        dialogCont =  GameObject.Find("UI_Dialog_Cont");
        dialogAns = GameObject.Find("UI_Dialog_Answer");

        // Start to get data
        StartCoroutine(GetData());


    }

    // Get dialogs data file (.json format)
    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(api);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            dialogsJson = File.ReadAllText(Application.dataPath + "/dialogs.json");
        }
        else
        {
            DialogList = JsonUtility.FromJson<DialogList>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
        }
    }

    void Start()
    {
        // Set dialog container to invisible
        dialogCont.SetActive(false);
    }

    // Show Dialog, fire when player entered a NPC collider
    public void ShowDialog(string character, string question)
    {
        // Declear a space for answer
        string answer = "";

        // Find answer by question from NPC name
        foreach (Dialogs dialog in DialogList.Dialogs)
        {
            if (character == dialog.name)
            {
                // Get answer
                foreach (QA qa in dialog.QA)
                {
                    
                    if (question == qa.question)
                    {
                        answer = qa.answer;
                    }
                }
            }
        }

        // Display dialog container
        dialogCont.SetActive(true);
        
        // Set Text to dialog
        dialogAns.GetComponent<Text>().text = answer;

    }


    // Close dialog, fire when player left a NPC's collider
    public void CloseDialog()
    {
        dialogCont.SetActive(false);
        dialogAns.GetComponent<Text>().text = "";
    }
}

