using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public  string              gameScene;

    private Animator            animator;
    private int                 _window = 0;

    public  TextMeshProUGUI     musicValue;
    public  AudioMixer          musicMixer;
    public  TextMeshProUGUI     soundValue;
    public  AudioMixer          soundMixer;
    public  Button              loadButton;


    public void Start()
    {
        //musicMixer = GetComponent<AudioMixer>();
        //soundMixer = GetComponent<AudioMixer>();
        animator = GetComponent<Animator>();
        loadButton.interactable = SaveManager.IsGameSaved();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && _window == 1) 
        {
            animator.SetTrigger("HideOptions");
            _window = 0;
        }
    }

    public void NewGame()
    {
        SaveManager.ClearSaveGame();
        Load();
    }

    public void Load()
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    public void ShowOptions()
    {
        animator.SetTrigger("ShowOptions");
        _window = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnMusicChanged(float value)
    {
        musicValue.SetText(value + "%");
        musicMixer.SetFloat("volume", -50 + value / 2);
    }

    public void OnSoundChanged(float value)
    {
        soundValue.SetText(value + "%");
        soundMixer.SetFloat("volume", -50 + value / 2);
    }
}
