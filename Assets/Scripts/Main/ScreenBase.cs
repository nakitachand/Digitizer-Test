using UnityEngine;

[RequireComponent(typeof(CanvasGroup))] 
public class ScreenBase : MonoBehaviour
{
    CanvasGroup canvasGroup;

    protected virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void SetScreen(bool open)
    {
        canvasGroup.interactable = open;
        canvasGroup.blocksRaycasts = open;
        canvasGroup.alpha = open ? 1 : 0;
    }
}
