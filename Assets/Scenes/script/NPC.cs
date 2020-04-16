using UnityEngine;

public class NPC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogController._ins.ShowDialog(this.name, "Hello");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DialogController._ins.CloseDialog();
    }
}
