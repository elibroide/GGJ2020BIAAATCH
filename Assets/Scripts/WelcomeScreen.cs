
using System;
using System.Collections;
using System.Collections.Generic;
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
    public string playerType;
    public string playerName;
    public Camera screenCamera;
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
    public Transform endLoseScreen;
    public Transform endWinScreen;
    public Transform characterWinParent;

    [Space]
    public WelcomeState state; 

    private Tween _tween = null;
    
    private void Start()
    {
        screenCamera = Camera.main;
        
        state = WelcomeState.Start;
        // Init
        nameInput.text = "";
        storyScreen.gameObject.SetActive(false);
        nameScreen.gameObject.SetActive(false);
        endLoseScreen.gameObject.SetActive(false);
        endWinScreen.gameObject.SetActive(false);
        
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
            
            playerType = BodyPartFactory.Instance.GetGroup();
            Debug.Log("YOYOYO " + playerType);
            var items = BodyPartFactory.Instance.GetBodyPartOfGroup(playerType);
            foreach (var item in items)
            {
                character.SetBodyPart(item);
            }
            
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
            nameGraveStory.text = text;
            playerName = text;
            character.gameObject.SetActive(false);
            nameScreen.gameObject.SetActive(false);
            storyScreen.gameObject.SetActive(true);
        });
    }
    
    private void GoToGame()
    {
        state = WelcomeState.Game;
        var sequence = DOTween.Sequence();
        sequence.Append(flash.DoFlash());
        sequence.InsertCallback(flash.timeToFlash, () =>
        {
            storyScreen.gameObject.SetActive(false);
            SceneManager.LoadScene("World", LoadSceneMode.Additive);
            
            // Init stuff
            StartCoroutine(WaitForPlayer());
        });
    }

    private IEnumerator WaitForPlayer()
    {
        yield return null;
        PlayerController player;
        do
        {
            player = FindObjectOfType<PlayerController>();
            yield return null;
        } while (player == null);
        screenCamera.gameObject.SetActive(false);
        // player.characterName = nameInput.text;
        // player.characterOriginalBody = "normal";

        Debug.Log("LOGGED TO PLAYER");
        player.gameOver += PlayerOngameOver;
    }

    private void PlayerOngameOver(bool isWin)
    {
        Debug.Log("CALLED GAME OVER " + isWin);
        
        var sequence = DOTween.Sequence();
        sequence.Insert(0, flash.DoFlash());
        sequence.InsertCallback(flash.timeToFlash, () =>
        {
            var world = GameObject.Find("World");
            if (isWin)
            {
                var player = FindObjectOfType<PlayerController>();
                player.view.SetState(AnimationState.IDLE);
                player.view.SetDirection(Direction.DOWN);
                player.view.transform.SetParent(characterWinParent);
                player.view.transform.localPosition = Vector3.zero;
                player.view.transform.localScale = Vector3.one;
                endWinScreen.gameObject.SetActive(true);
            }
            else
            {
                endLoseScreen.gameObject.SetActive(true);
            }
            Destroy(world);
            screenCamera.gameObject.SetActive(true);
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
