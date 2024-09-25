using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Clickable : MonoBehaviour
{
    public string customTitle = "";
    public List<GameObject> gameObjects = new List<GameObject>();

    public virtual CursorType GetCursorType()
    {
        return CursorType.DEFAULT;
    }

    public void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("clickables");
    }

    public virtual string GetTitle()
    {
        if (customTitle.Length > 0)
            return customTitle;
        else
            return name;
    }

    public void MouseEnter()
    {
        if (enabled)
            ClickableManager.singleton.SetCursor(GetCursorType(), GetTitle());
        else
        {
            ClickableManager.singleton.SetCursor(CursorType.NO, GetTitle());
            SetLayers("outlined");
        }
    }

    public void MouseExit()
    {
        ClickableManager.singleton.ClearCursor();
        SetLayers("Default");
    }

    public void MouseDown()
    {
        ClickableManager.singleton.OnClick(this);
    }

    void SetLayers(string layer)
    {
        foreach (GameObject obj in gameObjects)
        {
            SetLayer(obj, layer);
        }
    }

    void SetLayer(GameObject obj, string layer)
    {
        obj.layer = LayerMask.NameToLayer(layer);
    }

    public virtual void TakeAction() { }
}
