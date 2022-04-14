using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemUI curItemUI;
    void Awake()
    {
        curItemUI = GetComponent<ItemUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        QuestUI.Instance.tooltip.gameObject.SetActive(true);
        QuestUI.Instance.tooltip.setTooltip(curItemUI.curItemData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        QuestUI.Instance.tooltip.gameObject.SetActive(false);
    }
}
