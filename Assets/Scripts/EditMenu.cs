using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tracer
{
	public class EditMenu : MonoBehaviour
	{
		private static bool InUI => EventSystem.current.IsPointerOverGameObject();

		public TMP_InputField PositionX;
		public TMP_InputField PositionY;
		public TMP_InputField Rotation;
		public TMP_InputField ScaleX;
		public TMP_InputField ScaleY;

		private Transform edit;

		private void Start()
		{
			PositionX.onValueChanged.AddListener(value =>
			{
				if (!float.TryParse(value, out float val)) return;

				var position = edit.position;
				edit.position = new Vector3(val, position.y, position.z);
			});

			PositionY.onValueChanged.AddListener(value =>
			{
				if (!float.TryParse(value, out float val)) return;

				var position = edit.position;
				edit.position = new Vector3(position.x, val, position.z);
			});

			Rotation.onValueChanged.AddListener(value =>
			{
				if (!float.TryParse(value, out float val)) return;

				edit.rotation = Quaternion.AngleAxis(val, Vector3.forward);
			});

			ScaleX.onValueChanged.AddListener(value =>
			{
				if (!float.TryParse(value, out float val)) return;

				var scale = edit.localScale;
				edit.localScale = new Vector3(val, scale.y, scale.z);
			});

			ScaleY.onValueChanged.AddListener(value =>
			{
				if (!float.TryParse(value, out float val)) return;

				var scale = edit.localScale;
				edit.localScale = new Vector3(scale.x, val, scale.z);
			});
		}

		public void SetElement(Transform pressed) => edit = pressed;

		public void UpdateData()
		{
			if (edit == null) return;

			var position = edit.position;
			var scale = edit.localScale;

			PositionX.SetTextWithoutNotify(position.x.ToString("F2"));
			PositionY.SetTextWithoutNotify(position.y.ToString("F2"));
			Rotation.SetTextWithoutNotify(edit.rotation.eulerAngles.z.ToString("F2"));
			ScaleX.SetTextWithoutNotify(scale.x.ToString("F2"));
			ScaleY.SetTextWithoutNotify(scale.y.ToString("F2"));
		}
	}
}