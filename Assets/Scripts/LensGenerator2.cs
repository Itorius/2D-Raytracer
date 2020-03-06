using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tracer
{
	// https://en.wikipedia.org/wiki/Lens
	// https://en.wikipedia.org/wiki/Spherical_aberration

	public class LensGenerator2 : BaseElement
	{
		public override Matrix4x4 Transform => new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, Initial / Final, 0, 0), Vector4.zero, Vector4.zero);

		public MeshFilter MeshFilter;
		public EdgeCollider2D Collider;

		private Mesh mesh;
		private const int segments = 30;

		// https://en.wikipedia.org/wiki/Lens#Lensmaker's_equation
		// https://en.wikipedia.org/wiki/Lens#Sign_convention_for_radii_of_curvature_R1_and_R2
		// https://en.wikipedia.org/wiki/File:Lenses_en.svg

		// positive for convex, negative for concave, zero for flat
		public float Radius1;

		// negative for convex, positive for concave, zero for flat
		public float Radius2;

		[Min(0f)]
		public float Thickness;

		private BoxCollider2D handle;

		private void Start()
		{
			handle = GetComponentInChildren<BoxCollider2D>();
			mesh = new Mesh { name = "Lens" };
			MeshFilter.sharedMesh = mesh;
		}

		private IEnumerable<Vector3> GenerateVerticesFront()
		{
			float localScale = transform.localScale.y;
			float clip = localScale * 2f;
			float radius = Radius1 * Mathf.Abs(localScale);

			if (Math.Abs(Radius1) < 1)
			{
				float s = -localScale / segments;
				for (int i = -segments / 2 - 1; i <= segments / 2; i++) yield return new Vector3(-Thickness * 0.5f, s * i + s * 0.5f, 0f);
			}
			else
			{
				float offset = Mathf.Sqrt(4f * radius * radius - clip * clip) * 0.5f;

				float maxAngle = Mathf.Atan(localScale / offset);

				Vector3 center = radius < 0 ? new Vector3(radius - Thickness * 0.5f, 0f, 0f) : new Vector3(offset - Thickness * 0.5f, 0, 0);

				float segmentSize = 2f * maxAngle / segments;

				for (int i = -segments / 2 - 1; i <= segments / 2; i++)
				{
					float angle = i * segmentSize + segmentSize * 0.5f;

					float x = -Mathf.Cos(angle);
					float y = Mathf.Sin(angle) / localScale;

					Vector3 vertex = center + new Vector3(x, y, 0) * radius;

					yield return vertex;
				}
			}
		}

		private IEnumerable<Vector3> GenerateVerticesBack()
		{
			float localScale = transform.localScale.y;
			float clip = localScale * 2f;
			float radius = Radius2 * Mathf.Abs(localScale);

			if (Math.Abs(Radius2) < 1)
			{
				float s = -localScale / segments;
				for (int i = -segments / 2 - 1; i <= segments / 2; i++) yield return new Vector3(Thickness * 0.5f, s * i + s * 0.5f, 0f);
			}
			else
			{
				float offset = Mathf.Sqrt(4f * radius * radius - clip * clip) * 0.5f;
				float maxAngle = Mathf.Atan(localScale / offset);

				Vector3 center = radius > 0 ? new Vector3(radius + Thickness * 0.5f, 0f, 0f) : new Vector3(-offset + Thickness * 0.5f, 0, 0);

				float segmentSize = 2f * maxAngle / segments;

				for (int i = -segments / 2 - 1; i <= segments / 2; i++)
				{
					float angle = i * segmentSize + segmentSize * 0.5f;

					float x = -Mathf.Cos(angle);
					float y = Mathf.Sin(angle) / localScale;

					Vector3 vertex = center + new Vector3(x, y, 0) * radius;

					yield return vertex;
				}
			}
		}

		private void FixedUpdate()
		{
			List<Vector3> vertices = new List<Vector3>();

			IEnumerable<Vector3> front = GenerateVerticesFront();
			vertices.AddRange(Radius1 > 0 ? front : front.Reverse());

			IEnumerable<Vector3> back = GenerateVerticesBack();
			vertices.AddRange(Radius2 > 0 ? back.Reverse() : back);

			int[] triangles = new int[vertices.Count * 3];

			int triIndex = 0;
			for (int i = 0; i < triangles.Length - 3; i += 3)
			{
				triangles[i] = triIndex;
				triangles[i + 1] = triIndex + 1;
				triangles[i + 2] = vertices.Count - triIndex - 1;

				triIndex++;
			}

			mesh.SetVertices(vertices);
			mesh.SetNormals(Enumerable.Repeat(Vector3.forward, vertices.Count).ToList());
			mesh.SetIndices(triangles, MeshTopology.Triangles, 0);

			vertices.Add(vertices[0]);
			Collider.points = vertices.Select(vertex => (Vector2)vertex).ToArray();

			Vector2 eh = Collider.bounds.size;
			handle.size = new Vector2(eh.y, eh.x);
		}

		private void OnDrawGizmos()
		{
			if (mesh == null) return;

			Gizmos.color = Color.green;

			foreach (Vector3 vertex in mesh.vertices)
			{
				Gizmos.DrawSphere(transform.TransformPoint(vertex), 0.05f);
			}
		}
	}
}