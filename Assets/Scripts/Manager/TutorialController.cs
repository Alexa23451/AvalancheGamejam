using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject mobileTutorial;
    public GameObject desktopTutorial;
        
    void Start()
    {
        mobileTutorial.SetActive(Application.isMobilePlatform);
        desktopTutorial.SetActive(!Application.isMobilePlatform);
    }
}
