using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector3 itemPosition;
    public int lane;
    public float requiredJumpHeight = 1.2f;
    public bool hasHit = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var perspective = CameraComponent.focalLenth /(CameraComponent.focalLenth + itemPosition.z);

        transform.localScale = Vector3.one * perspective;
        transform.position = new Vector3(
            itemPosition.x * perspective + CameraComponent.Instance.vanishingPoint.x,
            itemPosition.y * perspective + CameraComponent.Instance.vanishingPoint.y,
            0f
        );
    }
}
