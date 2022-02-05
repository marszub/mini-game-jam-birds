using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private Vector3 dragOffset;
    private Camera cam;

    [SerializeField] private GameObject bird;
    [SerializeField] private float speed = 100;

    void Awake()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        dragOffset = bird.transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        Debug.Log("Mouse Drag");
        bird.transform.position = Vector3.MoveTowards(bird.transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}