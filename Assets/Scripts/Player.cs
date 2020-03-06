using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Tracer
{
	public class Player : MonoBehaviour
	{
		private Vector3 MouseWorld => camera.ScreenToWorldPoint(Input.mousePosition);
		private bool InUI => EventSystem.current.IsPointerOverGameObject() || WindowRectangle.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y));

		public new Camera camera;

		public InputManager Manager;

		[Range(0.5f, 2f)]
		public float ScrollSpeed = 0.5f;

		[Range(0.1f, 1f)]
		public float MoveSpeed = 0.25f;

		private Transform hit;
		private Vector3 velocity;

		private Vector3 offset;

		public GameObject BaseMenu;
		public GameObject EditMenu;

		public RectTransform Knob;
		public RectTransform Line;
		public RectTransform Panel;

		private void Start()
		{
			_editMenu = EditMenu.GetComponent<EditMenu>();
		}

		private void Awake()
		{
			Manager = new InputManager();

			Manager.Gameplay.Movement.performed += MoveCamera;
			Manager.Gameplay.Movement.canceled += MoveCamera;

			Manager.Gameplay.Zoom.performed += Zoom;

			Manager.Gameplay.Dragging.performed += Click;
		}

		private void MoveCamera(InputAction.CallbackContext obj)
		{
			if (InUI) return;

			velocity = (Vector3)obj.ReadValue<Vector2>() * MoveSpeed;
		}

		private void Zoom(InputAction.CallbackContext ctx)
		{
			if (InUI) return;

			float delta = ctx.control is KeyControl ? ctx.ReadValue<float>() : Input.mouseScrollDelta.y;

			camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - delta * ScrollSpeed, 1f, 20f);
		}

		private Transform SelectedElement;
		private EditMenu _editMenu;

		private void Click(InputAction.CallbackContext ctx)
		{
			if (InUI) return;

			if (ctx.control is ButtonControl button)
			{
				if (button.wasPressedThisFrame)
				{
					if (TryGetTransform(out hit))
					{
						offset = hit.position - MouseWorld;
						SelectedElement = hit;
					}
					else SelectedElement = null;
				}
				else if (button.wasReleasedThisFrame) hit = null;
			}
		}

		private void FixedUpdate()
		{
			camera.transform.position += velocity;

			if (hit != null)
			{
				hit.parent.position = MouseWorld + offset;
				// _editMenu.UpdateData();
			}
		}

		// private void Update()
		// {
		// 	if (Knob.gameObject.activeSelf)
		// 	{
		// 		var pos = camera.WorldToScreenPoint(pressed.position);
		// 		Knob.anchoredPosition = pos;
		// 		Line.anchoredPosition = pos;
		//
		// 		var destPos = pos + new Vector3(1f, 0.9f) * 100f;
		// 		Panel.anchoredPosition = destPos - new Vector3(10, 10);
		//
		// 		float angle = Mathf.Atan2(destPos.x - pos.x, destPos.y - pos.y) * Mathf.Rad2Deg;
		// 		Line.rotation = Quaternion.AngleAxis(angle, Vector3.back);
		//
		// 		float dist = Vector3.Distance(pos, destPos);
		// 		Line.sizeDelta = new Vector2(5f, dist);
		// 	}
		// }

		private Rect WindowRectangle = new Rect(10, 10, 400, 400);
		public GUISkin UISkin;

		private void OnGUI()
		{
			GUI.skin = UISkin;

			WindowRectangle = GUI.Window(0, WindowRectangle, DrawMenu, GUIContent.none);
		}

		private void DrawMenu(int id)
		{
			GUI.DragWindow(new Rect(0, 0, WindowRectangle.width, 20));

			if (SelectedElement != null)
			{
				// pressed.transform.position=GUILayout.HorizontalSlider(pressed.transform.position.x, -10f, 10f);
			}
		}

		private void OnEnable() => Manager?.Enable();

		private void OnDisable() => Manager?.Disable();

		private bool TryGetTransform(out Transform transform)
		{
			if (InUI)
			{
				transform = null;
				return false;
			}

			var hit = Physics2D.GetRayIntersection(camera.ScreenPointToRay(Input.mousePosition));
			transform = hit.transform;

			return transform != null;
		}
	}
}