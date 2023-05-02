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
        text = GetComponent<TextMeshProUGUI>();
        text.color = defaultColor;
    }

    public float GetHeight()
    {
        return text.rectTransform.sizeDelta.y * text.rectTransform.localScale.y;
    }

    public void Setup(ChooseScene.ChooseLabel label, ChooseController controller, float y)
    {
        scene = label.nextScene;
        text.text = label.text;
        this.controller = controller;

        Vector3 position = text.rectTransform.localPosition;
        position.y = y;
        text.rectTransform.localPosition = position;
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