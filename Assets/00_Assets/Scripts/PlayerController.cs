using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    public bool focused = false;
    public Interactable focus;
    public LayerMask groundLayer, interactLayer;
    PlayerMotor motor;
    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
    }
    public void OnMove()
    {
        //Input.mousePosition

        if(EventSystem.current.IsPointerOverGameObject()) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;
        if (Physics.Raycast(ray,out raycastHit,100,groundLayer))
        {
            motor.MoveToPoint(raycastHit.point);
            RemoveFocus();
        }
    }
    void OnInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 100, interactLayer))
        {
            Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
            SetFocus(interactable);
        }
    }

    void SetFocus(Interactable newFocus)
    {
        focused = true;
        if(newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focused = false;
        
        focus = null;
        motor.StopFollowTarget();
    }
}
