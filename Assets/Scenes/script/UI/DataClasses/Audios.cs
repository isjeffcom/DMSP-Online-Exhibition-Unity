using System.Collections.Generic;

[System.Serializable]
public class Audios
{
    public bool playOnStart;
    public int start;
    public List<AudiosActs> acts = new List<AudiosActs>();

}
