using UnityEngine;

namespace Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public SteamVR_TrackedController ControllerRight;
        public SteamVR_TrackedController ControllerLeft;
        public Transform Player;
        private Rigidbody _playerRigidbody;
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

            _playerRigidbody = Player.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (RopeRight.StuckToSurface && _triggerPressedRight)
            {
                _playerRigidbody.AddForce(RopeRight.RopeDirection * BoostStrength);
            }
            if (RopeLeft.StuckToSurface && _triggerPressedLeft)
            {
                _playerRigidbody.AddForce(RopeLeft.RopeDirection * BoostStrength);
            }
        }

        void HandleRightTriggerClicked(object sender, ClickedEventArgs e)
        {
            _triggerPressedRight = true;
        
            if (!RopeRight.StuckToSurface)
            {
                _playerRigidbody.AddForce(-RopeRight.RopeStart.forward.normalized * ManeuverBoostPower);
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
                _playerRigidbody.AddForce(-RopeLeft.RopeStart.forward.normalized * ManeuverBoostPower);
            }
        }
    
        void HandleLeftUnclicked(object sender, ClickedEventArgs e)
        {
            _triggerPressedLeft = false;
        }
    }
}
