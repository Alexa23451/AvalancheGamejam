using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameCanvas : BasePanel
{
    [SerializeField] private Text _levelText;
    [SerializeField] private Button _howToPlayBtn;
    [SerializeField] private Button _rewindBtn;
    [SerializeField] private Button _exitToMainMenuBtn;
    [SerializeField] private Button _resetGameBtn;

    void Start()
    {
        _levelText.text = "Level " + (SceneController.Instance.CurrentScene - 1).ToString();
        SceneController.Instance.OnChangeScene += OnChangeScene;
        _howToPlayBtn.onClick.AddListener(OnHowToPlay);
        _rewindBtn.onClick.AddListener(OnRewind);
        _exitToMainMenuBtn.onClick.AddListener(OnExitToMainMenu);
        _resetGameBtn.onClick.AddListener(OnResetLevel);
    }

    private void OnChangeScene(int fromScene, int toScene)
    {
        _levelText.text = "Level " + (SceneController.Instance.CurrentScene - 1).ToString();
    }

    private void OnDestroy()
    {
        _howToPlayBtn.onClick.RemoveListener(OnHowToPlay);
        SceneController.Instance.OnChangeScene -= OnChangeScene;
    }

    void OnHowToPlay()
    {
        UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("GUIDE" ,
            "<color=#ff0000ff>Swipe</color> to move Axie around." +
            "\n\n Please help poor Axie ^^"); 
            
        UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }
    void OnRewind()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }
    
    void OnResetLevel()
    {
        GameStatic.Instance.ResetGame();    
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }
    
    void OnExitToMainMenu()
    {
        UIManager.Instance.ShowPanelWithDG(typeof(ExitToMainMenuPanel));
        SoundManager.Instance.Play(Sounds.UI_POPUP);
    }

    public override void OverrideText()
    {
        throw new NotImplementedException();
    }
}
