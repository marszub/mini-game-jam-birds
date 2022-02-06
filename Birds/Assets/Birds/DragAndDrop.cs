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
        BirdBehaviour birdBehaviour = bird.GetComponent<BirdBehaviour>();
        if(birdBehaviour.state != BirdBehaviour.State.Dead)
        {
            dragOffset = bird.transform.position - GetMousePos();
            if(birdBehaviour.state == BirdBehaviour.State.Join)
                birdBehaviour.state = BirdBehaviour.State.Fly;
        }
            
    }

    void OnMouseDrag()
    {
        if (bird.GetComponent<BirdBehaviour>().state != BirdBehaviour.State.Dead)
            bird.transform.position = Vector3.MoveTowards(bird.transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}