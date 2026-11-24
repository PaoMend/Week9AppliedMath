using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    public static float focalLenth = 300f;
    public static CameraComponent Instance;
    public Vector2 vanishingPoint = Vector2.zero;

    private void Awake()
    {
        Instance = this;
    }
}
