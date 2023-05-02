using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseController : MonoBehaviour
{
    public GameObject labelPrefab;
    public GameController gameController;
    private RectTransform rectTransform;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetupChoose(ChooseScene scene)
    {
        DestroyLabels();
        animator.SetTrigger("Show");
        for (int index = 0; index < scene.labels.Count; index++)
        {
            GameObject              newLabelObject  = Instantiate(labelPrefab, transform);
            ChooseLabelController   newLabel        = newLabelObject.GetComponentInChildren<ChooseLabelController>();
            newLabel.Setup(scene.labels[index], this);
        }
    }

    public void PerformChoose(StoryScene scene)
    {
        gameController.PlayScene(scene);
        animator.SetTrigger("Hide");
    }

    private void DestroyLabels()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}