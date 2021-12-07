using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public static bool Start = false;
    public static float a = 0.25f;

    void LateUpdate()
    {
        if (Start == true)
        {
            transform.Translate(Vector2.up * a * Time.deltaTime);
            a += 0.0001f;
        }
    }
}
