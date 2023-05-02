using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public  StoryScene              currentScene;
    public  BottomBarController     bottomBar;
    public  BackgroundController    backgroundController;

    private State                   state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE
    }

    private void Awake()
    {
        backgroundController.SetImage(currentScene.background);
        bottomBar.PlayScene(currentScene);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state ==State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    PlayScene(currentScene.nextScene);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }

    private void PlayScene(StoryScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(StoryScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        backgroundController.SwitchImage(scene.background);
        yield return new WaitForSeconds(1f);
        bottomBar.ClearBarText();
        bottomBar.Show();
        yield return new WaitForSeconds(1f);
        bottomBar.PlayScene(scene);
        state = State.IDLE;
    }
}