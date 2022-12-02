using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPlatform : MonoBehaviour
{
    [SerializeField] SliderJoint2D slider;
    [SerializeField] JointMotor2D motorSpeed;
    [SerializeField] float speed, awakeTime;
	void Start ()
    {
        motorSpeed = slider.motor;
	}

	void Update ()
    {
        if (Time.timeSinceLevelLoad >= awakeTime)
        {
            if (slider.limitState == JointLimitState2D.LowerLimit)
            {
                motorSpeed.motorSpeed = speed;
                slider.motor = motorSpeed;
            }

            if (slider.limitState == JointLimitState2D.UpperLimit)
            {
                motorSpeed.motorSpeed = -speed;
                slider.motor = motorSpeed;
            }
        }
    }
}
