using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    CameraRaycaster raycaster;

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D unknownCursor = null;

    [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<CameraRaycaster>();   
    }

    // Update is called once per frame
    void Update()
    {
        switch (raycaster.layerHit)
        {
            case Layer.Enemy:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                break;
        }

    }
}
