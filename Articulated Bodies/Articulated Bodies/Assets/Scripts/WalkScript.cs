using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{

    public float rotateSpeed = 3.0F;
    private Transform tran;
    private Animator m_animator;
    private Rigidbody m_rb;
    CharacterController controller;
    float gravity = 9.8f;

    int currentState;

    // Start is called before the first frame update
    void Start()
    {
        tran = gameObject.GetComponent<Transform>();
        m_animator = gameObject.GetComponent<Animator>();
        m_rb = gameObject.GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        currentState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = 0;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Jump");
        }
        else
        {
            if (Mathf.Abs(v) > 0.1)//Check whether pressed WS
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        m_animator.SetInteger("transit", 2);
                        tran.Translate(Vector3.forward * 0.08f);
                    }
                    else
                    {
                        m_animator.SetInteger("transit", 1);
                        tran.Translate(Vector3.forward * 0.03f);
                    }
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        m_animator.SetInteger("transit", 2);
                        tran.Translate(- Vector3.forward * 0.08f);
                    }
                    else
                    {
                        m_animator.SetInteger("transit", 1);
                        tran.Translate(- Vector3.forward * 0.03f);
                    }
                }
                else
                {
                    m_animator.SetInteger("transit", 0);
                }
            }
            if (Mathf.Abs(h) > 0.1)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    m_animator.SetInteger("transit", 2);
                    transform.Rotate(0, h * rotateSpeed, 0);
                }
                else
                {
                    m_animator.SetInteger("transit", 1);
                    transform.Rotate(0, h * rotateSpeed, 0);//Rotate in horizontal when pressing AD
                }
                
            }
            if (Mathf.Abs(h) <= 0.1 && Mathf.Abs(v) <= 0.1)
            {
                m_animator.SetInteger("transit", 0);
            }
        }

        Quaternion rot = transform.rotation;
        rot.x = 0;
        rot.z = 0;
        transform.rotation = rot;
    }

    private IEnumerator Jump()
    {
        m_animator.SetInteger("transit", 3);
        for (int i=0; i<20; i++)
        {
            yield return new WaitForSeconds(Time.deltaTime/2);

            float new_x = transform.position.x;
            float new_y = transform.position.y;
            float new_z = transform.position.z;

            float v = 9.9f;
            float m = m_rb.mass;
            float G = - m * gravity;

            float dv = G/m;
            float dy = v + dv;

            transform.position = new Vector3(new_x, new_y + dy, new_z);
            m_animator.SetInteger("transit", currentState);
        }
    }
}
