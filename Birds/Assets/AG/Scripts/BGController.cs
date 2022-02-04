using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{

    //for coordinates
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float halfBack;
    private float halfHeight;

    //for reapeated background image (must have rigid body)
    public GameObject background;

    private GameObject BG_current = null;
    private GameObject BG_next = null;

    //for velocity
    private float BG_velocity = 1;

    //for distance
    private float distance = 0;

    //for random junk
    private float timer = 0;
    private float waitTime = 0;
    public List<GameObject> stuff;

    private List<GameObject> stuffInMotion = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        BG_current = Instantiate(background) as GameObject;
        BG_current.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);
        halfBack = BG_current.GetComponent<SpriteRenderer>().bounds.extents.x;
        halfHeight = BG_current.GetComponent<SpriteRenderer>().bounds.extents.y;
        BG_next = Instantiate(background) as GameObject;
        BG_next.transform.position = new Vector3(mainCamera.transform.position.x + 2 * halfBack, mainCamera.transform.position.y, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Update_velocity();
        if (BG_current.transform.position.x + halfBack + 12 < mainCamera.transform.position.x)
        {
            
            Destroy(BG_current);
            BG_current = Instantiate(background) as GameObject;
            BG_current.transform.position = BG_next.transform.position;
            Destroy(BG_next);
            BG_next = Instantiate(background) as GameObject;
            BG_next.transform.position = new Vector3(BG_current.transform.position.x + 2 * halfBack, mainCamera.transform.position.y, 0);

        }
        distance += BG_velocity * Time.deltaTime;
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            timer = 0;
            waitTime = Random.Range(0, 1.0f);
            SpawnDecoration();
        }
        if((int)distance %100==0)
        {
            Debug.Log("Bird");
            //spawn bird -> need fixing
            //possibly 101>distance>99, or other variable to get to spawn bird each 100m (like timer)
        }


        Debug.Log(distance);


    }

    void Update_velocity()
    {
        BG_current.GetComponent<Rigidbody2D>().velocity = new Vector3(-BG_velocity, 0, 0);
        BG_next.GetComponent<Rigidbody2D>().velocity = new Vector3(-BG_velocity, 0, 0);
        for(int i =0; i<stuffInMotion.Count;i++)
        {
            stuffInMotion[i].GetComponent<Rigidbody2D>().velocity = new Vector3(-BG_velocity, 0, 0);
        }
}

    public void Set_Velocity(float velocity)
    {
        BG_velocity = velocity;
        
    }

    public float Get_Distance()
    {
        return distance;
    }

    void SpawnDecoration()
    {
        int index = Random.Range(0, stuff.Count);
        GameObject tmp = Instantiate(stuff[index]) as GameObject;
        tmp.transform.position = new Vector3(mainCamera.transform.position.x + 2 * halfBack, (mainCamera.transform.position.x+2*halfHeight) * Random.Range(0, 1.0f), 0);
        stuffInMotion.Add(tmp);

    }
}
