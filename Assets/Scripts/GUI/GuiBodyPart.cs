using UnityEngine;
using UnityEngine.UI;

public class GuiBodyPart : MonoBehaviour
{
    public BodyPartType type;
    public Text text;
    public Image fill;
    private static Color bad = new Color(162f/255f, 56f / 255f, 81f / 255f);
    private static Color good = new Color(76f / 255f, 193f / 255f, 76f / 255f);

    public void SetHealth(float percent)
    {
        Color lerpedColor = Color.Lerp(bad, good, percent);
        fill.color = lerpedColor;
    }
}
