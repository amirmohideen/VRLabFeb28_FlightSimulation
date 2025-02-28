using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FlightSim : MonoBehaviour
{
    [SerializeField] private Transform m_flight1;
    [SerializeField] private Transform m_Joystick_PositionMovement;


    private Vector2 m_JoystickValuePosition;
    private Vector2 m_JoystickValueHeight;
    private float m_wheelValue;


    [SerializeField] private float speed_PositionMovement = 5f;
    [SerializeField] private float speed_Height = 3f;
    [SerializeField] private float rotationSpeed = 30f;



    float direction = 0f;    // Determine rotation direction based on currentValue
    private float threshold_clockwise = 0.6f; // Threshold for determining rotation direction
    private float threshold_counter_clockwise = 0.4f; // Threshold for determining rotation direction


    void Update()
    {
        UpdateFlightPosition();
        UpdateFlightHeight();
        UpdateFlightRotation();
    }

    void UpdateFlightPosition()
    {
        // Get current flight xy position
        var flightPosition = m_flight1.localPosition;

        // Calculate flight velocity and new position
        flightPosition += new Vector3(m_JoystickValuePosition.x * speed_PositionMovement * Time.deltaTime, 0f,
            m_JoystickValuePosition.y * speed_PositionMovement * Time.deltaTime);

        // Update flight xy position
        m_flight1.localPosition = flightPosition;
    }

    void UpdateFlightHeight()
    {
        var flightHeight = m_flight1.localPosition;

        flightHeight += new Vector3(0f,
            -m_JoystickValueHeight.y * speed_Height * Time.deltaTime, 0f);

        m_flight1.localPosition = flightHeight;
    }

    void UpdateFlightRotation()
    {
        if (m_wheelValue > threshold_clockwise)
        {
            // Rotate right (clockwise)
            direction = 1f;
        }
        else if (m_wheelValue < threshold_counter_clockwise)
        {
            // Rotate left (counter-clockwise)
            direction = -1f;
        }
        else
        {
            // Value is between 0.4 and 0.6, stop rotation
            direction = 0f;
        }

        // Apply rotation
        m_flight1.Rotate(Vector3.up * direction * rotationSpeed * Time.deltaTime);

        m_Joystick_PositionMovement.Rotate(Vector3.up * -direction * rotationSpeed * Time.deltaTime);


    }





    /// <summary>
    /// Gets the X value of the joystick. Called by the <c>XRJoystick.OnValueChangeX</c> event.
    /// </summary>
    /// <param name="x">The joystick's X value</param>
    public void OnJoystickValueChangeX(float x)
    {
        m_JoystickValuePosition.x = x;
    }

    /// <summary>
    /// Gets the Y value of the joystick. Called by the <c>XRJoystick.OnValueChangeY</c> event.
    /// </summary>
    /// <param name="y">The joystick's Y value</param>
    public void OnJoystickValueChangeY(float y)
    {
        m_JoystickValuePosition.y = y;
    }


    public void OnJoystickValueHeightChangeY(float y)
    {
        m_JoystickValueHeight.y = y;
    }

    public void OnWheelValueChange(float wheelValue)
    {
        m_wheelValue = wheelValue;

    }
}
