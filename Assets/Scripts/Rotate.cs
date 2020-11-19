using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Vector3 movement = new Vector3(7.5f, 0f, 0f);

    void Update()
    {
        transform.RotateAround(movement, Vector3.up, 60 * Time.deltaTime);
    }
}
