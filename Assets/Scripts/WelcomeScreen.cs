
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum WelcomeState
{
    Start,
    StartWait,
    Name,
    Story,
    StoryWait,
    Game,
}
public class WelcomeScreen : MonoBehaviour
{
    public float timeToStart = 0;
    public float timeBetweenFlashFirst = 0;
    public float timeBetweenFlashSecond = 0;
    public float timeBetweenTexts = 0;
    public float timeTextAppear = 0;
    public float timeToSpace = 0;
    
    public float timeToDisappear = 0.25f;

    [FormerlySerializedAs("welcome")] [Space]
    public Transform welcomeScreen;
    public GameObject character;
    public SpriteRenderer title;
    public SpriteRenderer[] texts;
    public SpriteRenderer space;
    public FlashBolt flash;
    public SpriteRenderer background;
    
    [Space]
    public Transform nameScreen;
    public SpriteRenderer enterNamePrompt;
    public InputField storyText;

    [Space]
    [ReadOnly] public WelcomeState state; 

    private Tween _tween = null;
    
    private void Start()
    {
        state = WelcomeState.Start;
        // Init
        nameScreen.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        foreach (var text in texts)
        {
            text.color = Color.clear;
        }
        character.SetActive(false);
        space.color = Color.clear;

        _tween = StartWelcomeAnimation();
        // SceneManager.LoadScene("World");
    }

    private Tween StartWelcomeAnimation()
    {
        welcomeScreen.gameObject.SetActive(true);
        float currentDuration;
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(timeToStart);
        sequence.Append(flash.DoFlash());
        sequence.InsertCallback(timeToStart + flash.timeToFlash, () =>
        {
            title.gameObject.SetActive(true);
        });
        sequence.AppendInterval(timeBetweenFlashFirst);
        currentDuration = sequence.Duration();
        sequence.Append(flash.DoFlash());
        sequence.InsertCallback(currentDuration + flash.timeToFlash, () =>
        {
            character.SetActive(true);
        });
        sequence.AppendInterval(timeBetweenFlashSecond);
        currentDuration = sequence.Duration();
        for (var i = 0; i < texts.Length; i++)
        {
            var text = texts[i];
            sequence.Insert(currentDuration + timeBetweenTexts * i, 
                text.DOColor(Color.white, timeTextAppear));
        }
        sequence.AppendInterval(timeToSpace);
        sequence.Append(space.DOColor(Color.white, timeTextAppear));
        sequence.OnComplete(() => state = WelcomeState.StartWait);
        return sequence;
    }

    private void ClearStart()
    {
        var sequence = DOTween.Sequence().OnComplete(() => Destroy(welcomeScreen.gameObject));
        foreach (var sprite in welcomeScreen.GetComponentsInChildren<SpriteRenderer>())
        {
            sequence.Insert(0, sprite.DOColor(Color.clear, timeToDisappear));
        }

        sequence.OnComplete(StartName);
    }

    private void StartName()
    {
        nameScreen.gameObject.SetActive(true);
        storyText.Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clicked();
        }
    }

    private void Clicked()
    {
        switch (state)
        {
            case WelcomeState.Start:
                _tween.Complete();
                break;
            case WelcomeState.StartWait:
                ClearStart();
                break;
            case WelcomeState.Name:
                break;
            case WelcomeState.Story:
                break;
            case WelcomeState.StoryWait:
                break;
            case WelcomeState.Game:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
