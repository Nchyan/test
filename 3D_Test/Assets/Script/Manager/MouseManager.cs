using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class MouseManager : Singleton<MouseManager>
{
    public event UnityAction<Vector3> onMouseClicked;
    public event UnityAction<GameObject> onEnemyClicked;
    public Texture2D atkCursor, missionCursor, moveCursor, normalCursor, skillCursor;

    private RaycastHit hitInfo;//鼠标射线返回信息

    void Update()
    {
        printMouseIcon();
        if (InventoryManager.Instance.IfOpen() || QuestUI.Instance.ifOpen()) return;
        MouseControl();
    }
    private void printMouseIcon()//刷新鼠标状态
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out hitInfo))
        {
            switch (hitInfo.collider.gameObject.tag) 
            {
                case "Groud":
                    Cursor.SetCursor(normalCursor, new Vector2(4,4),CursorMode.Auto);
                    break;
                case "Portal":
                    Cursor.SetCursor(moveCursor, new Vector2(32, 32), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(atkCursor, new Vector2(32, 32), CursorMode.Auto);
                    break;
                case "Item":
                    Cursor.SetCursor(normalCursor, new Vector2(32, 32), CursorMode.Auto);
                    break;
                case "NPC":
                    Cursor.SetCursor(missionCursor, new Vector2(32, 32), CursorMode.Auto);
                    break;
            }
        }
    }

    private void MouseControl()//鼠标点击状态
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Groud"))
            {
                onMouseClicked?.Invoke(hitInfo.point);
            }
            if (hitInfo.collider.gameObject.CompareTag("Portal"))
            {
                onMouseClicked?.Invoke(hitInfo.point);
            }
            if (hitInfo.collider.gameObject.CompareTag("Item"))
            {
                onMouseClicked?.Invoke(hitInfo.point);
            }
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                onEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            }
        }
    }

}
