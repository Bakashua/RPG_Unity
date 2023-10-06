using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIDragHandler : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 1.0f;

    private bool isDragging;
    private Vector2 dragStartPosition;

    private void OnEnable()
    {
        // Subscribe to the drag events
        //InputSystemUIInputModule.current.pointClick.started += OnPointerClick;
        //InputSystemUIInputModule.current.pointClick.canceled += OnPointerRelease;
        //InputSystemUIInputModule.current.pointDelta.performed += OnPointerDrag;
    }

    private void OnDisable()
    {
        // Unsubscribe from the drag events
        //InputSystemUIInputModule.current.pointClick.started -= OnPointerClick;
        //InputSystemUIInputModule.current.pointClick.canceled -= OnPointerRelease;
        //InputSystemUIInputModule.current.pointDelta.performed -= OnPointerDrag;
    }

    private void OnPointerClick(InputAction.CallbackContext context)
    {
        // Store the start position when the click begins
        dragStartPosition = context.ReadValue<Vector2>();
        isDragging = true;
    }

    private void OnPointerRelease(InputAction.CallbackContext context)
    {
        // Reset dragging when the click is released
        isDragging = false;
    }

    private void OnPointerDrag(InputAction.CallbackContext context)
    {
        if (isDragging)
        {
            // Calculate the drag delta and scroll the UI accordingly
            Vector2 delta = context.ReadValue<Vector2>();
            scrollRect.verticalNormalizedPosition -= delta.y * scrollSpeed;

            // Clamp the scroll position to be within the valid range (0 to 1)
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
        }
    }
    
}
