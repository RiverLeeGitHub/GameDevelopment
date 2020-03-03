using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Pendulum : MonoBehaviour {
    // parameters of ths simulation (are specified through the Unity's interface)
    public float gravity_acceleration = 9.8f;
    public float mass = 1.0f;
    public float friction_coeficient = 0.0f;
    public float initial_angular_velocity = 0.0f;
    public float time_step_h = 0.05f;
    public string ode_method = "euler";

    // parameters that will be populated automatically from the geometry/components of the scene
    private float rod_length = 0.0f;
    private float c = 0.0f;
    private float omega = 0.0f;
    private GameObject pendulum = null;

    // the state vector stores two entries:
    // state_vector[0] angle of pendulum (\theta) in radians
    // state_vector[1] angular velocity of pendulum
    private Vector2 state_vector;

    int counter = 0;
    StringBuilder sbOutput = new StringBuilder();
    float init_energy;

    // Use this for initialization
    void Start ()
    {
        Time.fixedDeltaTime = time_step_h;      // set the simulation step - FixedUpdate is called every 'time_step_h' seconds 
        state_vector = new Vector2(0.0f, 0.0f); // initialization of the state vector
        pendulum = GameObject.Find("Pendulum");
        if (pendulum == null)
        {
            Debug.LogError("Sphere not found! Did you delete it from the starter scene?");
        }
        GameObject rod = GameObject.Find("Quad");
        if (rod == null)
        {
            Debug.LogError("Rod not found! Did you delete it from the starter scene?");
        }
        rod_length = rod.transform.localScale.y; // finds rod length (based on quad's scale)
        
        state_vector[0] = pendulum.transform.eulerAngles.z * Mathf.Deg2Rad; // initial angle is set from the starter scene
        state_vector[1] = initial_angular_velocity; 

        c = friction_coeficient / mass;        // following the ODE specification
        omega = gravity_acceleration / rod_length;
    }

    // Update is called once per Time.fixedDeltaTime  sec
    void FixedUpdate ()
    {
        // complete this function (measure kinetic, potential, total energy)
        float kinetic_energy = 0.5f * mass * Mathf.Pow(rod_length * state_vector[1], 2);    // change here
        float potential_energy = mass * gravity_acceleration * rod_length * (1 - Mathf.Cos(state_vector[0]));  // change here
                                                                                                               //Debug.Log(kinetic_energy + potential_energy);


        //Plot1(kinetic_energy, potential_energy);
        //Plot2(kinetic_energy, potential_energy);

        OdeStep();
        pendulum.transform.eulerAngles = new Vector3(0.0f, 0.0f,  state_vector[0] * Mathf.Rad2Deg);
    }

    // complete this function!
    // it should return the right side of the two ODEs (in other words, the derivative of the pendulum's angle and angular velocity)
    // and you should use it in OdeStep!
    Vector2 PendulumDynamics(Vector2 current_state_vector)
    {
        float y = current_state_vector[0];
        float v = current_state_vector[1];

        float new_y = v;
        float new_v = -(c * v + Mathf.Pow(omega, 1) * Mathf.Sin(y));
        return new Vector2(new_y, new_v); // change here
    }

    void OdeStep()
    {
        // delete the next line, and complete this function 
        //state_vector[0] += 0.1f; // update the state_vector (both entries) properly depending on the specified ode_method 

        if (ode_method == "euler")
        {
            // change here
            state_vector = state_vector + time_step_h * PendulumDynamics(state_vector);
            //Debug.Log("euler");
        }
        else if (ode_method == "improved-euler")
        {
            // change here
            state_vector += (time_step_h / 2) * (PendulumDynamics(state_vector) + PendulumDynamics(state_vector + time_step_h * PendulumDynamics(state_vector)));
            //Debug.Log("improved-euler");
        }
        else if (ode_method == "rk")
        {
            // change here
            Vector2 k1 = time_step_h * PendulumDynamics(state_vector);
            Vector2 k2 = time_step_h * PendulumDynamics(state_vector + k1);
            Vector2 k3 = time_step_h * PendulumDynamics(state_vector + k1 / 4 + k2 / 4);
            state_vector = state_vector + (k1 + k2 + 4 * k3) / 6;
            //Debug.Log("rk");
        }
        else if (ode_method == "semi-implicit")
        {
            // change here
            state_vector[1] += time_step_h * PendulumDynamics(state_vector)[1];
            state_vector[0] += time_step_h * PendulumDynamics(state_vector)[0];
            //Debug.Log("semi-implicit");
        }
        else
        {
            Debug.LogError("ODE method should be one of the: euler, improved-euler, rk, semi-implicit");
        }
    }

    void Plot1(float kinetic_energy, float potential_energy)
    {
        string out_path = @"Assets/report.csv";
        string strSeperator = ",";
        //List<string[]> lines = new List<string[]>();
        float current_time = counter * time_step_h;
        string[] line =  {
            current_time.ToString(),
            kinetic_energy.ToString(),
            potential_energy.ToString(),
            (kinetic_energy + potential_energy).ToString() };
        sbOutput.AppendLine(string.Join(strSeperator, line));
        Debug.Log(current_time);
        counter += 1;
        if (current_time >= 20)
        {
            File.WriteAllText(out_path, sbOutput.ToString());
            Time.timeScale = 0;
        }
    }
    void Plot2(float kinetic_energy, float potential_energy)
    {   
        float current_time = counter * time_step_h;
        counter += 1;
        //Debug.Log(current_time);

        //float init_energy;
        if (current_time == 0)
        {
            init_energy = kinetic_energy + potential_energy;
            Debug.Log("init_energy: "+init_energy.ToString());
        }
        float cur_energy;
        if (current_time >= 20)
        {
            cur_energy = kinetic_energy + potential_energy;
            Debug.Log("end_energy: " + cur_energy.ToString());
            Debug.Log(Mathf.Abs(init_energy - cur_energy).ToString());
            //Debug.Log("Error: " + Mathf.Abs(init_energy - cur_energy).ToString());
            Time.timeScale = 0;
        }
    }
}
