using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    float rotationY = 0f;
    public float minYValue = -60f;
    public float maxYValue = 60f;
    private void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += (Input.GetAxis("Mouse Y") * sensitivityY);
            rotationY = Mathf.Clamp(rotationY, minYValue, maxYValue);

            //float rotationY = transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * sensitivityY;

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else
        {
            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += (Input.GetAxis("Mouse Y") * sensitivityY);
                rotationY = Mathf.Clamp(rotationY, minYValue, maxYValue);
                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
