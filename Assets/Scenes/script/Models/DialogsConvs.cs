using System.Collections.Generic;

[System.Serializable]
public class DialogsConvs
{
    public int id;
    public string question;
    public List<DialogsOptions> options = new List<DialogsOptions>();
    public int to;
    public int audioId;
}
