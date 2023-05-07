using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string SAVE_GAME = "savedGame";
    public static void SaveGame(SaveData data)
    {
        PlayerPrefs.SetString(SAVE_GAME, JsonUtility.ToJson(data));
    }

    public static SaveData LoadGame()
    {
        return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SAVE_GAME));
    }

    public static bool IsGameSaved()
    {
        return PlayerPrefs.HasKey(SAVE_GAME);
    }
}

