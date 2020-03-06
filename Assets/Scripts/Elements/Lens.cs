// using UnityEngine;
//
// namespace Tracer
// {
// 	public class Lens : BaseElement
// 	{
// 		public float FocalLenght = 5;
//
// 		public override Matrix4x4 Transform => new Matrix4x4(new Vector4(1f, (Initial - Final) / (GetComponent<LensGenerator>().Radius * transform.localScale.y * 0.5f * Final)), new Vector4(0f, Initial / Final), new Vector4(), new Vector4());
//
// 		private void Start()
// 		{
// 			var t = GetComponent<LensGenerator>().Radius * transform.localScale.y * 0.5f;
// 			FocalLenght = t * 0.5f;
// 		}
//
// 		private void OnDrawGizmos()
// 		{
// 			Transform t = transform;
// 			Vector3 position = t.position;
// 			Vector3 right = t.right;
// 			float radius = GetComponent<LensGenerator>().Radius * transform.localScale.y * 0.5f;
// 			float d = GetComponent<LensGenerator>().centerdistance;
//
// 			float oneoverf = (RefractiveIndex - 1) * (1 / radius - 1 / -radius + (RefractiveIndex - 1) * d / (RefractiveIndex * radius * -radius));
// 			float ffd = 1 / oneoverf /** (1 + (((RefractiveIndex - 1) * d) / (RefractiveIndex * radius)))*/;
//
// 			FocalLenght = 1 / oneoverf;
//
// 			Gizmos.color = Color.green;
// 			Gizmos.DrawSphere(position + right * ffd, 0.1f);
// 			Gizmos.DrawSphere(position - right * ffd, 0.1f);
// 		}
// 	}
// }