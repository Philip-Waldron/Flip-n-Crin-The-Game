using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class RopeController : MonoBehaviour
	{
		public SteamVR_TrackedController Controller;
		public PlayerController PlayerController;
		
		[HideInInspector]
		public bool StuckToSurface = false;
		public Transform RopeStart; // HandPointer
		private RaycastHit _hit; // RaycastEnd (Initial rope stick point, the along rope for wrapping)
		private RaycastHit _currentAttached; // RopeEnd
		public Vector3 RopeDirection;
		public float MaxRaycastDistance = 300;
		private float _currentMaxRopeLength;
//		private List<Vector3> _wrapPoints = new List<Vector3>();
		
		private LineRenderer _lr;
		public Material RopeMaterial;
		public Material PreviewMaterial;

		private ConfigurableJoint _cj;
		
		private void Start()
		{
			_lr = gameObject.AddComponent<LineRenderer>();
			_lr.endWidth = 0.015f;
			_lr.startWidth = 0.015f;
			_lr.material = PreviewMaterial;
			_lr.textureMode = LineTextureMode.Tile;

			_cj = PlayerController.Player.GetComponent<ConfigurableJoint>();
			
			Controller.Gripped += HandleGripped;
			Controller.Ungripped += HandleUngripped;

			Transform renderModel = Controller.GetComponentInChildren<SteamVR_RenderModel>().transform;
			foreach (Transform t in renderModel)
			{
				if (t.name.ToLower() != "tip") continue;
				foreach (Transform t2 in t)
				{
					if (t.name.ToLower() != "attach") continue;
					RopeStart = t2;
				}
			}
		}
	
		private void Update()
		{
			GrabHandler();
			
			_lr.SetPosition(0, RopeStart.position);
			
			if (StuckToSurface == false)
			{
				Physics.Raycast(RopeStart.position, RopeStart.forward, out _hit, MaxRaycastDistance);
				_lr.SetPosition(1, _hit.collider != null ? _hit.point : RopeStart.position);
			}

			if (StuckToSurface)
			{
				RopeDirection = (_currentAttached.point - RopeStart.position).normalized;
				
				// TODO Add wrapping around objects and account for raycast being inside colliders

//				Physics.Raycast(RopeStart.position, RopeDirection, out _hit, MaxRaycastDistance);
//				
//				if (_hit.collider != _currentAttached.collider)
//				{
//					_wrapPoints.Add(_hit.point);
//					_lr.positionCount += 1;
//					for(int c = 0; c < _wrapPoints.Count; c++)
//					{
//						_lr.SetPosition(c + 1, _wrapPoints[_wrapPoints.Count - (c + 1)]);
//					}
//					_currentAttached = _hit;
//				}
			}
		}

		private void GrabHandler()
		{
			if (Controller.gripped && _hit.collider && StuckToSurface == false) // Pointing at a grab-able surface
			{
//				_wrapPoints.Add(_hit.point);
				_lr.material = RopeMaterial;
				
				_currentAttached = _hit;
				_currentMaxRopeLength = (PlayerController.Player.transform.position - _hit.point).magnitude;
				
				// TODO casting second rope replaces the ConfigurableJoint connectedBody, account for 2 ConfigurableJoints?
				_cj.connectedBody = _hit.rigidbody;
				_cj.connectedAnchor = _hit.transform.InverseTransformPoint(_hit.point);
				_cj.linearLimit = new SoftJointLimit()
				{
					limit = _currentMaxRopeLength,
					
				};
				_cj.xMotion = ConfigurableJointMotion.Limited;
				_cj.yMotion = ConfigurableJointMotion.Limited;
				_cj.zMotion = ConfigurableJointMotion.Limited;
				
				StuckToSurface = true;
			}
		}
		
		private void HandleGripped(object sender, ClickedEventArgs e)
		{
			
		}
		
		private void HandleUngripped(object sender, ClickedEventArgs e)
		{
//			_wrapPoints.Clear();
//			_lr.positionCount = 2;
			
			_lr.material = PreviewMaterial;
			
			_cj.xMotion = ConfigurableJointMotion.Free;
			_cj.yMotion = ConfigurableJointMotion.Free;
			_cj.zMotion = ConfigurableJointMotion.Free;
			_cj.connectedBody = null;
			
			StuckToSurface = false;
		}
	}
}
