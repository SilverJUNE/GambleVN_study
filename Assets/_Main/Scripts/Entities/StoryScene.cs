using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewStoryScene", menuName ="Data/New StoryScene")]
[System.Serializable]
public class StoryScene : GameScene
{
    public List<Sentence>       sentences; 
    public Sprite               background;
    public GameScene            nextScene;

    [System.Serializable]
    public struct Sentence
    {
        public Speaker          speaker;
        [TextArea]
        public string           text;
        public AudioClip        audio;
        public List<Action>     actions;

        public AudioClip music;
        public AudioClip sound;

        [System.Serializable]
        public struct Action
        {
            public Speaker  speaker;
            public int      spriteIndex;
            public Type     actionType;
            public Vector2  coords;
            public float    moveSpeed;

            public enum Type
            {
                NONE, APPEAR, MOVE, DISAPPEAR
            }
        }
    }
}

public class GameScene : ScriptableObject { }
