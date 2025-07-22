using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class cardDisplayScript : MonoBehaviour
{
    public GameObject statsCard;
    public TMP_Text npcName, npcCatchphase, npcArmour, npcSpeed, friendliness;
    public Image artwork;

    public void showCharacterCard(npcInfoScript stats)
    {
        statsCard.SetActive(true);
        npcName.text = "My name is" + stats.npcName;
        npcCatchphase.text = stats.catchphase;
        npcArmour.text = "My level armour is" + stats.armourLevel;
        npcSpeed.text = "My speed is" + stats.npcSpeed;
        artwork.sprite = stats.npcSprite;

        if (stats.isFriendly)
        {
            friendliness.text = "Lovely to make your aquaintance";
        }
        else
        {
            friendliness.text = "...";
        }
    }

}