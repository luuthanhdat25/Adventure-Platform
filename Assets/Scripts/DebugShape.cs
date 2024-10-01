using UnityEngine;

public static class DebugShape 
{
	public static void DrawCircle(Vector2 center, float radius, float duration, Color color)
	{
		int segments = 20;
		float angleStep = 360f / segments;
		Vector3 prevPoint = center + new Vector2(Mathf.Cos(0), Mathf.Sin(0)) * radius;

		for (int i = 1; i <= segments; i++)
		{
			float angle = i * angleStep * Mathf.Deg2Rad;
			Vector3 nextPoint = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
			Debug.DrawLine(prevPoint, nextPoint, color, duration);
			prevPoint = nextPoint;
		}
	}
}
