using UnityEngine;

namespace Tracer
{
	public class Moveable : MonoBehaviour
	{
		private void Awake()
		{
			GameObject handle = new GameObject("Handle") { layer = LayerMask.NameToLayer("UI") };
			handle.AddComponent<BoxCollider2D>();
			handle.transform.SetParent(transform);

			handle.transform.localPosition = Vector3.zero;
			handle.transform.localRotation = Quaternion.identity;
			handle.transform.localScale = Vector3.one;
		}
	}
}