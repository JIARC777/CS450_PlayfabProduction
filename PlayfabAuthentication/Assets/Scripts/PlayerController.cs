using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody rig;

    private float startTime;
    private float timeTaken;

    private int collectablesPicked;
    public int maxCollectables = 10;
    public float cameraSens;
    public ParticleSystem gunSystem;

    public GameObject playButton;
    public TextMeshProUGUI curTextTime;

    private bool isPlaying;
    private CameraController cam;
    //public delegate void BulletCollision();
   // public static event BulletCollision ProbeDestroyed;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Start()
    {
        cam = GetComponentInChildren<CameraController>();
        Debug.Log(cam);
        cam.sens = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
            return;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 dir = (transform.forward * z + transform.right * x) * speed;
        dir.y = rig.velocity.y;
        rig.velocity = dir;
        curTextTime.text = (Time.time - startTime).ToString("F2");
        if (Input.GetMouseButtonDown(0))
        {
            ShootRay();
            gunSystem.Emit(1);
        }
    }
    void ShootRay()
    {
        RaycastHit ray;
        Debug.DrawRay(gunSystem.transform.position, gunSystem.transform.forward * 200f, Color.yellow);
        if (Physics.Raycast(gunSystem.transform.position, gunSystem.transform.forward, out ray, 200f)) {
            Debug.Log("Collision");
            if (ray.collider.tag == "Collectable")
            {
                ray.collider.gameObject.GetComponent<Probe>().ProbeHit();
                PickUpCollectable();
            }
                
        }
            
    }
   void  PickUpCollectable()
    {
        collectablesPicked++;
        if (collectablesPicked == maxCollectables)
            End();
    }
       

    public void Begin()
    {
        playButton.SetActive(true);
        startTime = Time.time;
        isPlaying = true;
        // for the camera - make sure you lock the cursor when the game starts
        cam.sens = cameraSens;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void End()
    {
        timeTaken = Time.time - startTime;
        isPlaying = false;
        LeaderBoard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000f));
        Debug.Log(-Mathf.RoundToInt(timeTaken * 1000f));
        cam.sens = 0;
        Cursor.lockState = CursorLockMode.None;

    }
}
