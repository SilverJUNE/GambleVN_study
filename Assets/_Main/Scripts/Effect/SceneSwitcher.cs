using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public  string      sceneToUnload;
    public  string      loaderScene;
    public  string      sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        UnloadStartScene();
    }

    private void UnloadStartScene()
    {
        SceneManager.sceneUnloaded += OnStartUnloaded;
        SceneManager.UnloadSceneAsync(sceneToLoad);
    }

    private void OnStartUnloaded(Scene scene)
    {
        SceneManager.sceneUnloaded -= OnStartUnloaded;
        SceneManager.sceneLoaded += OnEndLoaded;
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    private void OnEndLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnEndLoaded;
        UnloadLoader();
    }

    private void UnloadLoader()
    {
        SceneManager.UnloadSceneAsync(loaderScene);
    }
}
