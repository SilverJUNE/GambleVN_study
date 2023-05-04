using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public  GameScene               currentScene;
    public  BottomBarController     bottomBar;
    public  SpriteSwitcher          backgroundController;
    public  ChooseController        chooseController;
    public  AudioController          audioController;

    private State                   state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }

    private void Start()
    {
        if (currentScene is StoryScene)
        {
            StoryScene storyScene = currentScene as StoryScene;
            backgroundController.SetImage(storyScene.background);
            bottomBar.PlayScene(storyScene);
            PlayAudio(storyScene.sentences[0]);
        }
    }

    private void Update()
    {
        if(state == State.IDLE)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (state == State.IDLE && bottomBar.IsCompleted())
                {
                    if (bottomBar.IsLastSentence())
                    {
                        PlayScene((currentScene as StoryScene).nextScene);
                    }
                    else
                    {
                        bottomBar.PlayNextSentence();
                        PlayAudio((currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()]);
                    }
                }
                else
                {
                    bottomBar.SpeedUP();
                }
            }
        }

    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        yield return new WaitForSeconds(1f);
        if (scene is StoryScene)
        {
            bottomBar.Hide();
            StoryScene storyScene = scene as StoryScene;
            backgroundController.SwitchImage(storyScene.background);
            PlayAudio(storyScene.sentences[0]);
            yield return new WaitForSeconds(1f);
            bottomBar.ClearBarText();
            bottomBar.Show();
            yield return new WaitForSeconds(1f);
            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        }
        else if (scene is ChooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(scene as ChooseScene);
        }

    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music, sentence.sound) ;
    }
}