using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
        if (Physics.Raycast(ray,out raycastHit,100, groundLayer))
        {
            motor.MoveToPoint(raycastHit.point);
            RemoveFocus();
        }
    }
    public void OnInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 100, interactLayer))
        {
            motor.moveOn = true;
            Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
            SetFocus(interactable);
            gameObject.GetComponent<PlayerMotor>().FollowTarget(interactable);

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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Portal"))
        {
            FriendManager.Instance.LoadScene("ScenePrueba2");
        }
        if(other.CompareTag("BossAttack"))
        {
            gameObject.GetComponent<PlayerStats>().SetHealth(-10);
        }
        if(other.CompareTag("LaserBeam"))
        {
            gameObject.GetComponent<PlayerStats>().SetHealth(-30);
        }
    }
}
