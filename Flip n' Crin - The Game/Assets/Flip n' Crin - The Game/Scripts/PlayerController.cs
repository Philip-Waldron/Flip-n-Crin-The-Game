using UnityEngine;

namespace Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public SteamVR_TrackedController ControllerRight;
        public SteamVR_TrackedController ControllerLeft;
        public Transform Player;
        public RopeController RopeRight;
        public RopeController RopeLeft;

        private bool _triggerPressedRight;
        private bool _triggerPressedLeft;

        public float BoostStrength;
        public float ManeuverBoostPower;

        private void Start()
        {
            ControllerRight.TriggerClicked += HandleRightTriggerClicked;
            ControllerRight.TriggerUnclicked += HandleRightUnclicked;
            ControllerLeft.TriggerClicked += HandleLeftTriggerClicked;
            ControllerLeft.TriggerUnclicked += HandleLeftUnclicked;
        }

        private void Update()
        {
            if (RopeRight.StuckToSurface && _triggerPressedRight)
            {
                Player.GetComponent<Rigidbody>().AddForce(RopeRight.RopeDirection * BoostStrength);
            }
            if (RopeLeft.StuckToSurface && _triggerPressedLeft)
            {
                Player.GetComponent<Rigidbody>().AddForce(RopeLeft.RopeDirection * BoostStrength);
            }
        }

        void HandleRightTriggerClicked(object sender, ClickedEventArgs e)
        {
            _triggerPressedRight = true;
        
            if (!RopeRight.StuckToSurface)
            {
                Player.GetComponent<Rigidbody>().AddForce(-RopeRight.RopeStart.forward.normalized * ManeuverBoostPower);
            }
        }
    
        void HandleRightUnclicked(object sender, ClickedEventArgs e)
        {
            _triggerPressedRight = false;
        }

        void HandleLeftTriggerClicked(object sender, ClickedEventArgs e)
        {
            _triggerPressedLeft = true;

            if (!RopeLeft.StuckToSurface)
            {
                Player.GetComponent<Rigidbody>().AddForce(-RopeLeft.RopeStart.forward.normalized * ManeuverBoostPower);
            }
        }
    
        void HandleLeftUnclicked(object sender, ClickedEventArgs e)
        {
            _triggerPressedLeft = false;
        }
    }
}
