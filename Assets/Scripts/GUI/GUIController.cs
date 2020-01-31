using System.Collections.Generic;
using Player;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public Dictionary<BodyPartType, GuiBodyPart> partsGUI;

    void Awake()
    {
        partsGUI = new Dictionary<BodyPartType, GuiBodyPart>();
        var guiParts = GetComponentsInChildren<GuiBodyPart>();
        foreach (var guiPart in guiParts)
        {
            partsGUI.Add(guiPart.type, guiPart);
        }
    }
    
    public void UpdateBodyState(Dictionary<BodyPartType, BodyPartController> body)
    {
        foreach (var elem in body)
        {
            if (elem.Value == null)
            {
                partsGUI[elem.Key].text.text = $"no {elem.Key}";
                partsGUI[elem.Key].fill.fillAmount = 0;
            }
            else
            {
                partsGUI[elem.Key].text.text = $"{elem.Value.data.ownerName}'s {elem.Key}";
                partsGUI[elem.Key].fill.fillAmount = elem.Value.data.health / 100;
            }
        }
    }
}