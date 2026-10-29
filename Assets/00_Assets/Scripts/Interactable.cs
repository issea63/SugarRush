using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionTransform;
    public float radius;
    public Transform player;
    public bool amIFocused = false;

    private void Update()
    {
        if(amIFocused)
        {
            float distance = Vector3.Distance(interactionTransform.position, player.position);
            if(distance <= radius)
            {
                Interact();
            }
        }
        //if(playerController.focus == this.gameObject && 
    }

    public void OnFocused(Transform playerTransform)
    {
        player = playerTransform;
        amIFocused=true;
    }
    public void OnDefocused()
    {
        player = null;
        amIFocused = false;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with" + transform.name);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
