using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private Vector3 dragOffset;
    private Camera cam;

    [SerializeField] private float speed = 100;

    void Awake()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}