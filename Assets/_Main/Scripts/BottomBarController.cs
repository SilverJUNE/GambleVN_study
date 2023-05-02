using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public  GameObject      nameSprte;
    public  TextMeshProUGUI personNameText;
    public  TextMeshProUGUI barText;

    private int             sentenceIndex;
    private StoryScene      currentScene;
    private State           state           = State.COMPLETED;
    private Animator        animator;
    private bool            isHidden        = false;

    public Dictionary<Speaker, SpriteController> sprites;
    public GameObject spritesPrefab;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        sprites = new Dictionary<Speaker, SpriteController>();
        animator = GetComponent<Animator>();
    }

    public void Hide()
    {
        if(!isHidden)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }

    }

    public void Show()
    {
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void ClearBarText()
    {
        barText.text = "";
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        ActSpeakers();
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        if (currentScene.sentences[sentenceIndex].speaker.speakerName.Equals("나레이션"))
        {
            nameSprte.SetActive(false);
            personNameText.text = "";
        }
        else
        {
            nameSprte.SetActive(true);
            personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
            personNameText.color = currentScene.sentences[sentenceIndex].speaker.speakerNameColor;
        }

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }

    private void ActSpeakers()
    {
        List<StoryScene.Sentence.Action> actions = currentScene.sentences[sentenceIndex].actions;
        for(int i = 0; i < actions.Count; i++)
        {
            ActSpeaker(actions[i]); 
        }
    }

    private void ActSpeaker(StoryScene.Sentence.Action action)
    {
        SpriteController controller = null;
        switch(action.actionType)
        {
            case StoryScene.Sentence.Action.Type.APPEAR:
                if(!sprites.ContainsKey(action.speaker))
                {
                    controller 
                        = Instantiate(action.speaker.prefab.gameObject, spritesPrefab.transform)
                        .GetComponent<SpriteController>();
                    sprites.Add(action.speaker, controller);
                }
                else
                {
                    controller = sprites[action.speaker];
                }
                controller.Setup(action.speaker.sprites[action.spriteIndex]);
                controller.Show(action.Coords);
                return;
            case StoryScene.Sentence.Action.Type.MOVE:
                if(sprites.ContainsKey(action.speaker))
                {
                    controller = sprites[action.speaker];
                    controller.Move(action.Coords, action.moveSpeed);
                }
                break;
            case StoryScene.Sentence.Action.Type.DISAPPEAR:
                if(sprites.ContainsKey(action.speaker))
                {
                    controller = sprites[action.speaker];
                    controller.Hide();
                }
                break;
            case StoryScene.Sentence.Action.Type.NONE:
                if(sprites.ContainsKey(action.speaker))
                {
                    controller = sprites[action.speaker];
                }
                break;
        }
        if(controller != null)
        {
            controller.SwitchSprite(action.speaker.sprites[action.spriteIndex]);
        }
    }
}

