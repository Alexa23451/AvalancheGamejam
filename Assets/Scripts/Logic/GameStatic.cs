﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AnimationState{
    IDLE,
    ATTACK,
    WIN,
    LOSE,
    MOVE_FORWARD,
}

public class GameStatic : BaseManager<GameStatic>
{
    public int _curLevel = 0;
    public GameObject CurrentPlayer;

    private Dictionary<AnimationState, string> AnimationMapper;
    [SerializeField] private List<Sprite> AxieRescue = new List<Sprite>();
    [SerializeField] private List<string> AxieRescueQuote = new List<string>();
    
    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        AnimationMapper = new Dictionary<AnimationState, string>()
        {
            {AnimationState.IDLE , "action/idle/normal"},
            {AnimationState.ATTACK , "attack/melee/multi-attack"},
            {AnimationState.WIN , "action/move-forward"},
        };
    }

    public void OnWinGame()
    {
        CurrentPlayer.GetComponent<PlayerMovement>().PlayerFrozen();
        SoundManager.Instance.Play(Sounds.WIN_LV);
        
        if (SceneController.Instance.CurrentScene + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            OnFinishTheGame();
            return;
        }

        // int currentLv = (SceneController.Instance.CurrentScene - 1);
        // if (currentLv == 5
        //     || currentLv == 12
        //     || currentLv == 18)
        // {
        //     //Axie rescue
        //     OnFinishRescueLv();
        //     return;
        // }

        OnNextLevel();
    }

    void OnNextLevel()
    {
        CircleTransition.Instance.FadeIn(onMidFadeIn:() => SoundManager.Instance.Play(Sounds.FadeIn)
            ,onEndFadeIn:() =>
            {
                SceneController.Instance.NextScene();
            });
    }

    void OnFinishTheGame()
    {
        UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("You have finished the game !!!" ,
            "Thanks for playing, Hero, you lead The Cat to success!",
             ExitToGameMenu);
        UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
    }

    private int _countMeetNewCharacter = 0;
    void OnFinishRescueLv()
    {
        //int index = _countMeetNewCharacter;
        // UIManager.Instance.GetPanel<AxieRescuePanel>().SetInfo("AXIE UNLOCK!" ,
        //     AxieRescueQuote[index], AxieRescue[index],
        //     OnNextLevel);
        //UIManager.Instance.ShowPanelWithDG(typeof(AxieRescuePanel));
        //_countMeetNewCharacter++;
    }
    
    public void OnHowToPlay()
    {
        UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("You have finished the game !!!" ,    
            "Thanks for playing, Hero, you lead the Cat to success!",
            ExitToGameMenu);
        UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
    }

    public void OnLoseGame()
    {
        CurrentPlayer.GetComponent<PlayerMovement>().PlayerFrozen();
        SoundManager.Instance.Play(Sounds.LOSE_LV);
        CircleTransition.Instance.FadeIn(onMidFadeIn:() => SoundManager.Instance.Play(Sounds.FadeIn)
            , onEndFadeIn:() => SceneController.Instance.ReloadScene());
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);        
        SceneController.Instance.OnChangeScene += OnChangeScene;
        SoundManager.Instance.Play(Sounds.LOSE_LV);
        SceneController.Instance.ChangeScene(1);
    }

    private void OnDestroy()
    {
        SceneController.Instance.OnChangeScene -= OnChangeScene;
    }

    private void Update()
    {
        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Escape))
        {
            ExitToGameMenu();
        }
        
        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }
    }

    private bool canChangeScene = true;
    public void ExitToGameMenu()
    {
        if (SceneController.Instance.CurrentScene > 1 && canChangeScene)
        {
            canChangeScene = false;
            CircleTransition.Instance.FadeIn(onMidFadeIn:() => SoundManager.Instance.Play(Sounds.FadeIn),
                onEndFadeIn:() =>
                {
                    canChangeScene = true;
                    CircleTransition.Instance.SetCircleTransition(1);
                    SceneController.Instance.ChangeScene(1);
                });
        }
    }

    private bool canResetScene = true;
    public void ResetGame()
    {
        if (SceneController.Instance.CurrentScene > 1 && canResetScene)
        {
            canResetScene = false;
            CircleTransition.Instance.FadeIn(onMidFadeIn:() => SoundManager.Instance.Play(Sounds.FadeIn),
                onEndFadeIn:() =>
                {
                    TimerManager.Instance.AddTimer(2f ,() => canResetScene = true);
                    SceneController.Instance.ReloadScene();
                });
        }
    }

    private void OnChangeScene(int fromScene, int toScene)
    {
        CurrentPlayer = GameObject.FindGameObjectWithTag("Player");
        if (CurrentPlayer)
        {
            CircleTransition.Instance._playerPos = CurrentPlayer.transform;
        }

        if (toScene > 1)
        {
            CircleTransition.Instance.FadeOut(onMidFadeOut:() => SoundManager.Instance.Play(Sounds.FadeOut));
            UIManager.Instance.ShowPanel(typeof(InGameCanvas));
        }
        else
        {            
            UIManager.Instance.HidePanel(typeof(InGameCanvas));
        }
    }

    public override void Init()
    {
        
    }
}
