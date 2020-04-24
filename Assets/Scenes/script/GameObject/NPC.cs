using UnityEngine;
using UnityEngine.UI;

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

    // Flag for if dialog window opened
    private bool dialogEnabled = false;

    private void Awake()
    {
        // Find tips
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
        audioTip = GameObject.Find("UI_Tip_Audio").GetComponent<Text>();

        // Find container
        dialogCont = GameObject.Find("UI_Dialog_Cont");
        dialogAns = GameObject.Find("UI_Dialog_Answer");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player")
        {
            tip.text = "E to chat";
            audioTip.text = AudioController._isPlaying ? "Audio is playing..." : "A to listen";
        }
        
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
                AudioController._ins.PlayNPCAudio(this.name);
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
}
