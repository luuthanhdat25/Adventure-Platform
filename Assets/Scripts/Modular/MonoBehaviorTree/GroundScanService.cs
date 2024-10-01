using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode("Datlt/Ground Scan Service")]
public class GroundScanService : Service
{
	[SerializeField]
	private Transform startPoint;

	[SerializeField]
	private float distance;

	[SerializeField] 
	private Vector2Reference targetPosition;

	[SerializeField]
	private BoolReference isSeePlayer;

	[SerializeField]
	private LayerMask targetLayerMask;

	public override void Task()
	{
		var startPosition = startPoint.position;
		var checkDirection = transform.right;

		RaycastHit2D hit = Physics2D.Raycast(startPosition, checkDirection, distance, targetLayerMask);
		if (hit.collider != null)
		{
			//Debug.Log($"Hit {hit.collider.name} at position {hit.point}");
			Debug.DrawRay(startPosition, checkDirection * distance, Color.red);

			targetPosition.Value = hit.point;
			isSeePlayer.Value = true;
		}
		else
		{
			Debug.DrawRay(startPosition, checkDirection * distance, Color.green);
			isSeePlayer.Value = false;
		}
	}
}
