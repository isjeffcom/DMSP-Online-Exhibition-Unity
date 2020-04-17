using System.Collections.Generic;

[System.Serializable]
public class Dialogs
{
    public string name;
    public List<DialogsTime> time = new List<DialogsTime>();
    public List<DialogsConvs> convs = new List<DialogsConvs>();
}
