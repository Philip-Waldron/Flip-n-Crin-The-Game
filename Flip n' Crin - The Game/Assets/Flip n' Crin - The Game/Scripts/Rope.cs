using UnityEngine;

namespace Scripts
{
	public class Rope : MonoBehaviour
	{
		public SteamVR_TrackedController Controller;
		public bool Stick = false;
		private Vector3 _ropeHitPoint;
		
		public Transform RopeStart;
		private RaycastHit _hit; // RopeEnd
		public Vector3 RopeDirection;
		public float MaxDistance = 10;

		private LineRenderer _lr;
		public Material RopeMaterial;
		public Material PreviewMaterial;

		private void Start()
		{
			_lr = gameObject.AddComponent<LineRenderer>();
			_lr.endWidth = 0.035f;
			_lr.startWidth = 0.035f;
			_lr.material = RopeMaterial;
			_lr.textureMode = LineTextureMode.Tile;
			
			Controller.Gripped += HandleGripped;
			Controller.Ungripped += HandleUngripped;
		}
	
		private void Update()
		{
			_lr.SetPosition(0, RopeStart.position);
			
			Physics.Raycast(RopeStart.position, RopeStart.forward, out _hit, MaxDistance);
			
			if (Stick == false)
			{
				_lr.SetPosition(1, _hit.collider != null ? _hit.point : RopeStart.position);
				_lr.material = PreviewMaterial;
			}

			if (Stick)
			{
				//_ropePos += (_endRopePose - RopeStart.position).normalized * 1 * Time.deltaTime;
				//var ropeEnd = RopeStart.position;
				//_ropePos += Vector3.Lerp(RopeStart.position, _hitPoint.point, Time.deltaTime * .1f);
				//_lr.SetPosition(1, _ropePos);
				RopeDirection = (_ropeHitPoint - RopeStart.position).normalized;
				_lr.material = RopeMaterial;
				
			}
		}
		
		private void HandleGripped(object sender, ClickedEventArgs e)
		{
			if (_hit.collider)
			{
				_ropeHitPoint = _hit.point;
				
				Stick = true;
			}
		}
		
		private void HandleUngripped(object sender, ClickedEventArgs e)
		{
			Stick = false;
		}
	}
}
