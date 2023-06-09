using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseLabelController : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler

{
    public Color defaultColor;
    public Color hoverColor;
    private StoryScene scene;
    private TextMeshProUGUI text;
    private ChooseController controller;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = defaultColor;
    }

    public void Setup(ChooseScene.ChooseLabel label, ChooseController controller)
    {
        scene = label.nextScene;
        text.text = label.text;
        this.controller = controller;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.PerformChoose(scene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = defaultColor;
    }
}