using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;
    public Dictionary<BodyPartType, GuiBodyPart> partsGUI;
    public DiggingScreen diggingScreen;
    public Text timer;

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

    public void Update()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        var timeLeft = Mathf.FloorToInt(player.totalGameTime - (Time.time - player.timeStarted));
        timer.text = timeLeft.ToString();
    }
    
    public void UpdateBodyState(Dictionary<BodyPartType, BodyPartData> body)
    {
        foreach (var elem in body)
        {
            if (elem.Key == BodyPartType.Body) continue;
            if (elem.Value == null)
            {
                partsGUI[elem.Key].text.text = $"no {elem.Key}";
                partsGUI[elem.Key].SetHealth(0);
            }
            else
            {
                partsGUI[elem.Key].text.text = $"{elem.Value.ownerName}";
                partsGUI[elem.Key].SetHealth(elem.Value.health / 100);

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