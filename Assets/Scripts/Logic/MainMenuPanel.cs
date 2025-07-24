using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
        [SerializeField] private Button soundSettingBtn;
        [SerializeField] private Button infoSettingBtn;
        [SerializeField] private Image soundSettingImg;
        [SerializeField] private Image gameLogoImg;
    
        [SerializeField] private AudioSource bgmMainMenu;
    
        [SerializeField] private Color colorOn;
        [SerializeField] private Color colorOff;
    
        private void Awake()
        {
            soundSettingBtn.onClick.AddListener(OnSoundSetting);
            infoSettingBtn.onClick.AddListener(OnInfoSetting);
    
        }
    
        private void Start()
        {
            soundSettingImg.color = DataManager.Instance.SoundOn ? colorOn : colorOff;
            //if mobile device, hide the logo
            gameLogoImg.gameObject.transform.localScale = Application.isMobilePlatform || Application.isEditor 
                ? new Vector3(1, 1, 1)
                : new Vector3(0.3f, 0.3f, 0.3f); 
    
            SoundManager.Instance.StopAll(true);
            SoundManager.Instance.GLOBAL_ON = DataManager.Instance.SoundOn;
            bgmMainMenu.enabled = DataManager.Instance.SoundOn;
        }



        private void OnSoundSetting()
        {
            DataManager.Instance.SoundOn = !DataManager.Instance.SoundOn;
            soundSettingImg.color = DataManager.Instance.SoundOn ? colorOn : colorOff;
    
            bgmMainMenu.enabled = DataManager.Instance.SoundOn;
            SoundManager.Instance.GLOBAL_ON = DataManager.Instance.SoundOn;
            SoundManager.Instance.Play(Sounds.UI_POPUP);
        }
    
        private void OnInfoSetting()
        {
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("GUIDE" ,
                "<color=#ff0000ff>W/A/S/D - ARROW KEY</color> or <color=#ff0000ff>FINGER SWAP</color> to move the cat around." +
                "\n\n Please help the cat out ^^"); 
            
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            SoundManager.Instance.Play(Sounds.UI_POPUP);
        }
}
