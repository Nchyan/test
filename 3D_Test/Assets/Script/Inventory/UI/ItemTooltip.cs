using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    RectTransform rt;

    public Text ItemNameTxt;
    public Text ItemInfoTxt;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        updatePosition();
    }
    void Update()
    {
        updatePosition();
    }

    public void updatePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3[] corner = new Vector3[4];

        rt.GetWorldCorners(corner);

        float width = corner[3].x - corner[0].x;
        float height = corner[2].y - corner[1].y;

        if (mousePos.y < height)
            rt.position = mousePos + Vector3.up * height * 0.6f;
        else if(Screen.width - mousePos.x > width)
            rt.position = mousePos + Vector3.right * width * 0.6f;
        else
            rt.position = mousePos + Vector3.left * width * 0.6f;

    }

    public void setTooltip(ItemData_SO item)
    {
        ItemNameTxt.text = item.itemName;
        ItemInfoTxt.text = item.detail;
    }
}
