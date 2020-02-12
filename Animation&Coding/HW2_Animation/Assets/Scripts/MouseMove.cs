using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    //public GameObject cameraObject1;
    //public GameObject cameraObject2;
    public Camera camera1;
    public Camera camera2;
    private Camera camera;
    public GameObject plane;
    float originalScaleY;
    float minCompressonY = (float) 1.5;
    // Start is called before the first frame update
    void Start()
    {
        originalScaleY = transform.localScale.y;
        
        Debug.Log("Hello world!");
    }

    // Update is called once per frame
    void Update() 
    {
        /* These are the same:
         * Camera camera = cameraObject.GetComponent<Camera>();
         * GameObject cameraObject = camera.gameObject;
         */
        if (camera1.gameObject.active) { camera = camera1; }
        else { camera = camera2; }


        float planeY = plane.transform.position.y - plane.transform.localScale.y;
        //Vector3 ballPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        Vector3 ballPosition = camera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        Debug.Log(Input.mousePosition);
        ballPosition.y = Mathf.Max(ballPosition.y, planeY + minCompressonY);
        transform.position = ballPosition;

        float newScaleY = Mathf.Min(originalScaleY, Mathf.Abs(ballPosition.y - planeY));
        transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);

        //Debug.Log(dis.y - transform.localScale.y);
        //if (dis.y - transform.localScale.y < planeY)
        //{
        //    //Debug.Log(transform.localScale.y);
        //    if (transform.localScale.y >= maxCompressonY)
        //    {
        //        float newScaleY = transform.localScale.y - compressionRate;
        //        transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);

        //    }
        //    dis.y = planeY + transform.localScale.y;
        //}
        //else if (dis.y - transform.localScale.y > planeY && transform.localScale.y < originalScaleY )
        //{
        //    float newScaleY = transform.localScale.y + compressionRate;
        //    transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
        //}
        //    transform.position = dis;
        //Debug.Log(dis);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(transform.localScale);
    //    //Debug.Log(transform.position);
    //    float newScaleY = transform.localScale.y - (float) 0.1;
    //    transform.localScale = new Vector3(transform.localScale.x, newScaleY, transform.localScale.z);
    //}
}
