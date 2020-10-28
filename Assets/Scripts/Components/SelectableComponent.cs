using UnityEngine;

public class SelectableComponent : MonoBehaviour
{
    public bool IsSelectMenu = false;
    public void Select()
    {
        if (IsSelectMenu)
            ModMenuManager.me.SelectSP(gameObject);
        else
            ModMenuManager.me.SelectModElement(gameObject);
    }
}
