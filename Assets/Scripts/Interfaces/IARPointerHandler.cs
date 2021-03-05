using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IARPointerHandler: IEventSystemHandler
{
    void OnPointerDown(PointerEventData eventData);

    void OnPointerUp(PointerEventData eventData);

    void OnPointerDragged(PointerEventData eventData);

    void OnPointerClicked(PointerEventData eventData);
}
