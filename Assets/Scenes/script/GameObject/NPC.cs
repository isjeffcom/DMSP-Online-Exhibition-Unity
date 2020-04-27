using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    // UI Prefabs
    [SerializeField]
    private Text textPrefab = null;

    // Get Tip Text
    private Text tip;
    private Text audioTip;

    // Get Dialog container
    private GameObject dialogCont;
    private GameObject dialogAns;

    public List<int> PlayIDs;

    // Flag for if dialog window opened
    private bool dialogEnabled = false;

    private GameObject UI_NPC_onscreen_name_cont;
    private GameObject UI_NNT_Sample;
    private GameObject UI_NNT;

    private void Awake()
    {
        // Find tips
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
        audioTip = GameObject.Find("UI_Tip_Audio").GetComponent<Text>();

        // Find container
        dialogCont = GameObject.Find("UI_Dialog_Cont");
        dialogAns = GameObject.Find("UI_Dialog_Answer");

        UI_NPC_onscreen_name_cont = GameObject.Find("UI_NNT_Cont");
        UI_NNT_Sample = GameObject.Find("UI_NNT_Sample");
    }

    private void Start()
    {
        createUIName();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            if (DialogController._ins.CheckHasDialog(this.name))
            {
                tip.text = "E to chat";
            }

            if (AudioController._ins.CheckHasAudio(this.name))
            {
                audioTip.text = AudioController._isPlaying ? "Audio is playing..." : "A to listen";
            }
            
            
        }
        
    }

    private void Update()
    {
        // If you want to let the NPC moveable
        /*if (UI_NNT)
        {
            UI_NNT.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            // Check if dialog has already enabled
            if (Input.GetKey(KeyCode.E) && !dialogEnabled)
            {
                clearTip();

                // Display dialog container
                showDialogAnswer();
                dialogEnabled = true;
            }

            // Check if dialog has already enabled
            if (Input.GetKey(KeyCode.A) && !AudioController._isPlaying)
            {
                // Play by name or ID, if want to play by name, set playid to -1
    
                if (PlayIDs.Count == 0) {
                    MainController._ins.AudioPlayByActName(this.name);
                } else
                {
                    MainController._ins.AudioPlayByActId(PlayIDs[0]);
                }
                
                //AudioController._ins.PlayNPCAudio(this.name);
                clearTip();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DialogController._ins.CloseDialog();
        dialogEnabled = false;

        clearTip();
    }

    private void clearTip()
    {
        //clear tip
        tip.text = "";
        audioTip.text = "";
    }

    public void showDialogAnswer()
    {
        DialogController._ins.ShowDialog(this.name, 0);
    }

    private void OnMouseDown()
    {
        SelectController._ins.Select(this.name);
    }

    public void createUIName()
    {

        // Get Object Position
        Vector3 posi = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // Create 
        UI_NNT = Instantiate(UI_NNT_Sample);

        // Set Parent
        UI_NNT.transform.SetParent(UI_NPC_onscreen_name_cont.transform);

        // Set Text
        UI_NNT.GetComponent<Text>().text = this.name;

        // Set Position
        UI_NNT.transform.position = posi;

        // Set Default Unseeable
        UI_NNT.GetComponent<CanvasRenderer>().SetColor(new Color(0,0,0,0));
    }

    public void showNameOnScreen()
    {
        UI_NNT.GetComponent<CanvasRenderer>().SetColor(new Color(0, 0, 0, 1));
    }

    public void hideNameOnScreen()
    {
        UI_NNT.GetComponent<CanvasRenderer>().SetColor(new Color(0, 0, 0, 0));
    }
}
