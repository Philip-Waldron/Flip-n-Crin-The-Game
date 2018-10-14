using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SteamVR_TrackedController ControllerRight;
    public SteamVR_TrackedController ControllerLeft;
    public Transform Player;
    public Rope RopeRight;
    public Rope RopeLeft;

    private bool _triggerPressedRight;
    private bool _triggerPressedLeft;

    public float BoostStrength;

    private void Start()
    {
        ControllerRight.TriggerClicked += HandleRightTriggerClicked;
        ControllerRight.TriggerUnclicked += HandleRightUnclicked;
        ControllerLeft.TriggerClicked += HandleLeftTriggerClicked;
        ControllerLeft.TriggerUnclicked += HandleLeftUnclicked;
    }

    private void Update()
    {
        if (RopeRight.Stick && _triggerPressedRight)
        {
            Player.GetComponent<Rigidbody>().AddForce(RopeRight.RopeDirection * BoostStrength);
        }
        if (RopeLeft.Stick && _triggerPressedLeft)
        {
            Player.GetComponent<Rigidbody>().AddForce(RopeLeft.RopeDirection * BoostStrength);
        }
    }

    void HandleRightTriggerClicked(object sender, ClickedEventArgs e)
    {
        _triggerPressedRight = true;
    }
    
    void HandleRightUnclicked(object sender, ClickedEventArgs e)
    {
        _triggerPressedRight = false;
    }

    void HandleLeftTriggerClicked(object sender, ClickedEventArgs e)
    {
        _triggerPressedLeft = true;
    }
    
    void HandleLeftUnclicked(object sender, ClickedEventArgs e)
    {
        _triggerPressedLeft = false;
    }
}
