using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject initialButtons, settings, howToPlay, backgroundGroup, back; /*exit, , audioSettings, back;*/
    private bool ini;
    private void Start()
    {
        ini = true;
    }
    public void Settings()
    {
        initialButtons.SetActive(false);
        settings.SetActive(true);
        back.SetActive(true);
    }

    public void Back()
    {
        if (ini)
        {
            settings.SetActive(false);
            initialButtons.SetActive(true);
            back.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
            howToPlay.SetActive(false);
            backgroundGroup.SetActive(true);
            ini = true;
        }
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        settings.SetActive(false);
        back.SetActive(true);
        backgroundGroup.SetActive(false);
        ini = false;
    }
}
