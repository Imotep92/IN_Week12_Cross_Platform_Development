using UnityEngine;

[CreateAssetMenu(menuName = "NPC Information", fileName = "new NPC Info")]

public class npcInfoScript : ScriptableObject //Scriptable Object
{

    public string npcName;
    public string catchphase;
    public Sprite npcSprite;

    public int armourLevel;
    public float npcSpeed;

    public bool isFriendly;

}
