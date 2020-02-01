
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
    public string playerName;
    
    [Space]
    
    public float timeToStart = 0;
    public float timeBetweenFlashFirst = 0;
    public float timeBetweenFlashSecond = 0;
    public float timeBetweenTexts = 0;
    public float timeTextAppear = 0;
    public float timeToSpace = 0;
    
    public float timeToDisappear = 0.25f;

    [FormerlySerializedAs("welcome")] [Space]
    public Transform welcomeScreen;
    public CharacterView character;
    public SpriteRenderer title;
    public SpriteRenderer[] texts;
    public SpriteRenderer space;
    public FlashBolt flash;
    
    [Space]
    public Transform nameScreen;
    public Transform storyScreen;
    public SpriteRenderer enterNamePrompt;
    public InputField nameInput;
    public Text nameStory;
    public Text nameGraveStory;

    [Space]
    [ReadOnly] public WelcomeState state; 

    private Tween _tween = null;
    
    private void Start()
    {
        state = WelcomeState.Start;
        // Init
        nameInput.text = "";
        storyScreen.gameObject.SetActive(false);
        nameScreen.gameObject.SetActive(false);
        
        foreach (var text in texts)
        {
            text.color = Color.clear;
        }
        space.color = Color.clear;

        _tween = StartWelcomeAnimation();
        // SceneManager.LoadScene("World");
    }

    private Tween StartWelcomeAnimation()
    {
        title.gameObject.SetActive(false);
        character.gameObject.SetActive(false);
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
            character.gameObject.SetActive(true);
            character.SetState(AnimationState.IDLE);
            character.SetDirection(Direction.DOWN);
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
        sequence.OnComplete(WaitWelcomeContinue);
        return sequence;
    }

    private void WaitWelcomeContinue()
    {
        character.gameObject.SetActive(true);
        character.SetState(AnimationState.IDLE);
        character.SetDirection(Direction.DOWN);
        title.gameObject.SetActive(true);
        state = WelcomeState.StartWait;
    }

    private void StartName()
    {
        state = WelcomeState.Name;
        var sequence = DOTween.Sequence();
        sequence.Append(flash.DoFlash());
        sequence.InsertCallback(flash.timeToFlash, () =>
        {
            welcomeScreen.gameObject.SetActive(false);
            nameScreen.gameObject.SetActive(true);
            nameInput.Select();
        });
    }

    private void StartStory()
    {
        state = WelcomeState.Story;
        var sequence = DOTween.Sequence();
        sequence.Append(flash.DoFlash());
        sequence.InsertCallback(flash.timeToFlash, () =>
        {
            nameInput.gameObject.SetActive(false);
            var text = nameInput.text;
            nameStory.text = text;
            // nameGraveStory.text = text;
            character.gameObject.SetActive(false);
            nameScreen.gameObject.SetActive(false);
            storyScreen.gameObject.SetActive(true);
        });
    }
    
    private void GoToGame()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(flash.DoFlash());
        sequence.InsertCallback(flash.timeToFlash, () =>
        {
            storyScreen.gameObject.SetActive(false);
            SceneManager.LoadScene("World", LoadSceneMode.Additive);
            
            // Init stuff
            var player = FindObjectOfType<PlayerController>();
            player.name = nameInput.text;
            player.characterOriginalBody = "normal";
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clicked();
        }

        if (nameInput.text != "" && state == WelcomeState.Name && Input.GetKeyDown(KeyCode.Return))
        {
            StartStory();
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
                StartName();
                break;
            case WelcomeState.Story:
            case WelcomeState.StoryWait:
                GoToGame();
                break;
        }
    }
}
