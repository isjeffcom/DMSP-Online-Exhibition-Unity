using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    // Get Tip Text
    private Text tip;

    // Get Dialog container
    private GameObject dialogCont;
    private GameObject dialogAns;
    private Text textAns;

    // Flag for if dialog window opened
    private bool dialogEnabled = false;

    private void Awake()
    {
        // Find tip
        tip = GameObject.Find("UI_Tip").GetComponent<Text>();
        // Find container
        dialogCont = GameObject.Find("UI_Dialog_Cont");
        dialogAns = GameObject.Find("UI_Dialog_Answer");
        textAns = dialogAns.GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tip.text = "E to chat";

        //Play audio automatically
        PlayAudio();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if dialog has already enabled
        if (Input.GetKeyDown(KeyCode.E) && !dialogEnabled)
        {
            Debug.Log("e pressed");
            //clear tip
            
            tip.text = "";
            
            // Display dialog container
            dialogCont.SetActive(true);
            showDialogAnswer();
            //dialogAns.GetComponent<Text>().text = "Hello";
            dialogEnabled = true;
        }

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DialogController._ins.CloseDialog();
        dialogEnabled = false;
        tip.text = "";
    }

    public void showDialogAnswer()
    {
        DialogController._ins.ShowDialog(this.name, 0);
    }

    public void PlayAudio()
    {
        AudioController._audioIns.LoadAudio(this.name, 100);
    }
}
