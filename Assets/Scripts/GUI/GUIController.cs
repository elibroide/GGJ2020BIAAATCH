using System.Collections.Generic;
using Player;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;
    public Dictionary<BodyPartType, GuiBodyPart> partsGUI;
    public DiggingScreen diggingScreen;

    void Awake()
    {
        Instance = this;
        partsGUI = new Dictionary<BodyPartType, GuiBodyPart>();
        var guiParts = GetComponentsInChildren<GuiBodyPart>();
        foreach (var guiPart in guiParts)
        {
            partsGUI.Add(guiPart.type, guiPart);
        }
    }

    // public void ShowMessage()
    // {
        
    // }
    
    public void UpdateBodyState(Dictionary<BodyPartType, BodyPartController> body)
    {
        foreach (var elem in body)
        {
            if (elem.Value == null)
            {
                partsGUI[elem.Key].text.text = $"no {elem.Key}";
                partsGUI[elem.Key].SetHealth(1);
            }
            else
            {
                partsGUI[elem.Key].text.text = $"{elem.Value.data.ownerName}";
                partsGUI[elem.Key].SetHealth(elem.Value.data.health / 100);

            }
        }
    }

    public void ShowDiggingScreen()
    {
        diggingScreen.Show();
    }
    
    public void HideDiggingScreen()
    {
        diggingScreen.Hide();
    }
}