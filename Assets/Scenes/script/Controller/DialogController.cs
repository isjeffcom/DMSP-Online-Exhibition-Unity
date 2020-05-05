using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    // Instance for cross cs access
    public static DialogController _ins;

    // Json plain text container
    private string dialogsJson;

    //private GameObject[] All_Dialogs;
    private DialogList DialogList = new DialogList();

    // Example Button for Instantiate
    private GameObject UI_Opt_Button;

    // Get Dialog container
    private GameObject dialogCont;
    private GameObject dialogAns;
    private GameObject dialogOptionsCont;
    private GameObject dialogHint;

    // Get Tip Text
    private Text tip;

    //Get NPC Animator in Act4
    private Animator aniAct4;

    // Define Dialog Status
    public static int _DialogState = 0; //0: nothing, 1: waiting for react by options, 2: waiting for react by click
    public static string _DialogNPC = ""; // Save current NPC in conversation
    public static int _DialogNext = -1; // Save Next ID

    // json API
    private string api = "/data/";

    private void Awake()
    {
        // For global access
        _ins = this;

        // Find container
        dialogCont =  GameObject.Find("UI_Dialog_Cont");
        dialogOptionsCont = GameObject.Find("UI_Dialog_Options");
        dialogAns = GameObject.Find("UI_Dialog_Answer");
        dialogHint = GameObject.Find("UI_Hit_Hint");

        // Example button, create button by this example
        UI_Opt_Button = GameObject.Find("UI_Opt_Button");

        // Find tip
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();

        //Find NPC
        aniAct4 = GameObject.Find("NPC_Act4").GetComponent<Animator>();

        // Start to get data
        StartCoroutine(GetData());

    }

    public void UpdateAct()
    {
        // Start to get data
        StartCoroutine(GetData());
    }

    private void ShowHintAni(bool bol)
    {
        dialogHint.GetComponent<Animator>().SetBool("open", bol);
    }

    // Get dialogs data file (.json format)
    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(MainController._rootAPI + api + "act" + MainController._act + "/dialogs.json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            // DO NOTHNIG...
        }
        else
        {
            DialogList = JsonUtility.FromJson<DialogList>(request.downloadHandler.text);
        }
    }

    void Start()
    {
        // Hide Dialog Container
        dialogCont.SetActive(false);

        // Add Click Listener for dialog container
        dialogCont.GetComponent<Button>().onClick.AddListener(delegate { DialogReactByClick(_DialogNext); });
    }

    // Show Dialog, fire when player entered a NPC collider
    public void ShowDialog(string character, int toId)
    {
        
        // If to Id is -1 than close
        if(toId == -1)
        {
            CloseDialog();
            return;
        }

        dialogCont.SetActive(true);

        // Show hit hint animation
        ShowHintAni(true);

        // Display line
        string question = "";

        // Define what to do next, if has options than ignore
        int to = -1;

        // Save Options if have any
        List<DialogsOptions> options = new List<DialogsOptions>();

        // Find answer by question from NPC name
        foreach (Dialogs dialog in DialogList.Dialogs)
        {
            
            
            if (character == dialog.name)
            {
                // Get answer
                foreach (DialogsConvs convs in dialog.convs)
                {

                    
                    if (toId == convs.id)
                    {
                        
                        question = convs.question;
                        
                        // If has audio play audio
                        if (convs.audioId != -1)
                        {
                            AudioController._ins.PlayAudioById(convs.audioId);
                        }

                        // If have options than save options, if no than ready to go next.
                        if (convs.options.Count == 0)
                        {
                            to = convs.to;
                            _DialogNext = to;
                            ClearOptions();

                            // Waiting response by Click
                            _DialogState = 2;
                        } else
                        {
                            options = convs.options;

                            // Render Options
                            RenderOptions(options);

                            // Waiting response by Select Option
                            _DialogState = 1;
                        }
                    }
                }
            }
        }
        
        // Set Text to dialog
        dialogAns.GetComponent<Text>().text = question;

        

        // Change global dialog state to waiting status
        _DialogNPC = character;
        
    }

    // React by options
    void DialogReactByOptions(int to)
    {
        //Tip for click box to continue
        if (to == 1 || to == 2)
        {
            tip.text = "Click the box to Continue";
        }

        //Walk with different ends
        if (to == 900)
        {
            aniAct4.SetBool("isTo900", true);
        }
        if (to == 999)
        {
            aniAct4.SetBool("isTo999", true);
        }

        MainController._ins.DetectEnding(to);

        if (_DialogState == 1)
        {
            ShowDialog(_DialogNPC, to);
        }
        
    }

    // React by Click Container
    void DialogReactByClick(int to)
    {
        //Walk with different ends
        if (to == 900)
        {
            aniAct4.SetBool("isTo900", true);
        }

        MainController._ins.DetectEnding(to);

        if (_DialogState == 2)
        {
            ShowDialog(_DialogNPC, to);
        } 
        
        //Walk to player after the first choice
        if (to == 3)
        {
            aniAct4.SetBool("isTo3", true);
            NPCsController._ins.DisplayAllNameOnScreen(4);

            //Clear tip
            tip.text = "";
        }

    }

    // Render all options into the interface
    public void RenderOptions(List<DialogsOptions> options)
    {
        // Clear
        ClearOptions();


        int i = 1; // Index

        foreach(DialogsOptions opt in options)
        {
            // Calculate x and y
            int x = -200;
            int y = -15 + (i*55);

            // Create Buttons
            CreateButton(dialogOptionsCont, new Vector3(x, y, -1), opt.txt, opt.to);

            // Add index
            i++;
        }
    }

    public bool CheckHasDialog(string character)
    {
        bool res = false;
        for(int i = 0; i < DialogList.Dialogs.Count; i++)
        {
            Dialogs dialog = DialogList.Dialogs[i];
            if (character == dialog.name)
            {
                res = true;
            }
        }

        return res;

    }

    // Clean all options in options container
    public void ClearOptions()
    {
        foreach (Transform child in dialogOptionsCont.transform)
        {
            Destroy(child.gameObject);
        }
    }


    // Close dialog, fire when player left a NPC's collider
    public void CloseDialog()
    {
        // Clear tip text
        tip.text = "";
        
        // Display dialog
        if (dialogCont.activeSelf)
        {
            dialogCont.SetActive(false);

            // Show hit hint animation
            ShowHintAni(true);

            dialogAns.GetComponent<Text>().text = "";
        }

        // Restore all vars
        _DialogState = 0;
        _DialogNPC = "";
        _DialogNext = -1;

        // Clear all options 
        ClearOptions();
    }

    private void CreateButton(GameObject parent, Vector3 posi, string txt, int to)
    {
        // Create buttons
        GameObject button = Instantiate(UI_Opt_Button); // Instantiate
        
        button.transform.SetParent(parent.transform); //Setting button parent
        button.transform.localPosition = posi;
        button.GetComponent<Button>().onClick.AddListener(delegate { DialogReactByOptions(to); }); //Setting actions
        button.transform.GetChild(0).GetComponent<Text>().text = txt; //Setting text
    }

    
}

