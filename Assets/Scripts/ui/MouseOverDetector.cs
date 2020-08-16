using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverDetector : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private bool mouse_is_over;
    // Start is called before the first frame update
    void Start()
    {
        mouse_is_over = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool MouseIsOver()
    {
        return mouse_is_over;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_is_over = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_is_over = true;
    }
}
