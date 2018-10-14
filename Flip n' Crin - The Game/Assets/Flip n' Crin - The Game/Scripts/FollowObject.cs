using UnityEngine;

namespace Scripts
{
	public class FollowObject : MonoBehaviour
	{
		public Transform TargetTransform;

		private void FixedUpdate()
		{
			transform.position = TargetTransform.position;
		}
	}
}
