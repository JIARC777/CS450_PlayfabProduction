using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{
    public float maxRange;
    public float timeBetweenMovement;

   // float timeSinceLastMovement;
    float timeNextMovementWillStart;
    public float lengthOfMovement;
    bool moving;
    ParticleSystem pSystem;
    int currentFrame;
    int maxFrames;
    Vector3 oldPos;
    Vector3 newPos;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script Running");
        pSystem = GetComponent<ParticleSystem>();
      //  PlayerController.ProbeDestroyed += ProbeHit;
        timeNextMovementWillStart = Random.Range(0.0f, 4.0f);
        Debug.Log(timeNextMovementWillStart);
        moving = false;
        maxFrames = Mathf.RoundToInt(lengthOfMovement * 60);
        currentFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            if (Time.time >= timeNextMovementWillStart)
            {
                oldPos = transform.position;
                newPos = new Vector3(Random.Range(-maxRange, maxRange), Random.Range(10, maxRange), Random.Range(-maxRange, maxRange));
                moving = true;
            }
        }
        if (currentFrame >= maxFrames)
        {
            timeNextMovementWillStart = Time.time + timeBetweenMovement;
            currentFrame = 0;
            moving = false;
        }
        if (moving)
        {
            Move();
        }
    }

    private void Move()
    {
        float transitionValue = (float)currentFrame / maxFrames;
        transform.position = Vector3.Lerp(oldPos, newPos, transitionValue);
        currentFrame++;
    }

    public void ProbeHit()
    {
        Debug.Log("Hit");
        pSystem.Emit(500);
        //Destroy(this.gameObject);
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("DeleteObject", 2);
    }
    void DeleteObject()
    {
        Destroy(this.gameObject);
    }
}
