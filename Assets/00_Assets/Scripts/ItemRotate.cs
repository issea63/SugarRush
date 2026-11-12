using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0, Time.time * 50f, 0);
    }
}
