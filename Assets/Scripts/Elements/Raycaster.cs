using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tracer
{
	public class Raycaster : MonoBehaviour
	{
		private const int MaxHits = 50;

		[Range(1, 10)]
		public float LineWidth = 2.5f;

		private List<Vector2> lines = new List<Vector2>();

		private Mesh RayMesh;
		public MeshFilter RayFilter;

		private void Start()
		{
			RayMesh = new Mesh { name = "Ray" };
			RayFilter.mesh = RayMesh;
		}

		private void FixedUpdate()
		{
			Raycast();
		}

		private Vector3[] arr;

		private void Raycast()
		{
			lines.Clear();

			Vector3 direction = transform.up;
			Vector3 rayStart = transform.position + transform.up;

			lines.Add(rayStart);

			int index = 0;
			RaycastHit2D hit;

			BaseElement lastCollided = null;

			while (index < MaxHits && (hit = Physics2D.Raycast(rayStart, direction, 1000f, 1 << LayerMask.GetMask("UI"))).transform != null && hit.transform.TryGetComponent(out BaseElement element))
			{
				var normal = hit.normal;

				Debug.DrawRay(hit.point, hit.normal);
				
				if (lastCollided == element)
				{
					element.Initial = element.RefractiveIndex;
					element.Final = Constants.RefractiveIndexes.Air;
				}
				else
				{
					element.Initial = Constants.RefractiveIndexes.Air;
					element.Final = element.RefractiveIndex;
				}

				lines.Add(hit.point);

				if (element is Blocker) goto Label;

				float angle = -Vector3.SignedAngle(-direction, normal, Vector3.forward);
				float height = hit.point.y - hit.transform.position.y;

				var vector = element.Transform * new Vector4(height, angle);
				direction = Quaternion.AngleAxis(vector.y, Vector3.forward) * normal * (element is FlatMirror ? 1 : -1);
				rayStart = hit.point + (Vector2)(direction * 0.001f);

				lastCollided = element;
				index++;
			}

			lines.Add(rayStart + direction * 1000f);

			Label:
			List<Vector3> vertices = new List<Vector3>();
			List<Vector3> normals = new List<Vector3>();
			List<int> indices = new List<int>();
			for (int i = 0; i < lines.Count - 1; i++)
			{
				var perpendicular = (new Vector3(lines[i + 1].y, lines[i].x, 0) - new Vector3(lines[i].y, lines[i + 1].x, 0)).normalized * (LineWidth * 0.01f);
				var v1 = new Vector3(lines[i].x, lines[i].y, 0);
				var v2 = new Vector3(lines[i + 1].x, lines[i + 1].y, 0);

				vertices.Add(v1 - perpendicular);
				vertices.Add(v1 + perpendicular);
				vertices.Add(v2 + perpendicular);
				vertices.Add(v2 - perpendicular);

				normals.Add(Vector3.back);
				normals.Add(Vector3.back);
				normals.Add(Vector3.back);
				normals.Add(Vector3.back);

				indices.Add(i * 4 + 0);
				indices.Add(i * 4 + 3);
				indices.Add(i * 4 + 1);
				indices.Add(i * 4 + 3);
				indices.Add(i * 4 + 2);
				indices.Add(i * 4 + 1);
			}

			var transformed = vertices.Select(vertex => transform.InverseTransformPoint(vertex)).ToArray();

			RayMesh.Clear(true);
			RayMesh.SetVertices(transformed);
			RayMesh.SetNormals(normals);
			RayMesh.SetTriangles(indices, 0);
		}

		public void OnDrawGizmos()
		{
			// if (arr == null) return;
			//
			// Gizmos.color = Color.green;
			// foreach (Vector3 collision in arr)
			// {
			// 	Gizmos.DrawSphere(collision, 0.1f);
			// }
		}
	}
}