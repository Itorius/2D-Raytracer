// GENERATED AUTOMATICALLY FROM 'Assets/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Tracer
{
	public class InputManager : IInputActionCollection, IDisposable
	{
		public InputActionAsset asset { get; }

		public InputManager()
		{
			asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""7a55bd94-2993-4f81-944e-77c857bc9f99"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""3b418eca-43f5-4396-9840-f66c772069c0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""858c75e1-aa48-4901-95cc-268a11091d5a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dragging"",
                    ""type"": ""Button"",
                    ""id"": ""07dad695-b0e9-40fd-a0b1-44d2f661289b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""a655c0bd-bc35-4bf8-a1bf-39569c2dded3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d3310f6f-6e5b-4937-b08c-77e3d498a435"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b07604f5-abf4-484e-9ecd-acdc6c24a434"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8587cbdc-82a5-4c5a-8edc-02ec546712e3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""51868935-ddde-47ba-b4a6-e1d40a435826"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""0a8f5cc6-fa72-4dda-af3f-e332603ef069"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""26b8226f-63b2-4fba-847d-bf74a4995ad5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2960ea7b-32b4-4058-bcc9-e9508d526122"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""53ce85a1-0e32-4f33-91ae-b8e22c1100cb"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""26b9ffc6-0aa0-4d96-9c84-d518d21ab951"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""615817ca-8f48-420c-ac52-eed012818873"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""541918c2-b364-4674-8d7f-000a9d4a6de7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2dca34eb-ef0a-4548-9a74-6ba546a5f7e8"",
                    ""path"": ""<Keyboard>/leftBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6e66b928-f766-4077-9417-c3341649665e"",
                    ""path"": ""<Keyboard>/rightBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e5f4d0f3-cc48-4a99-a909-100d8e722100"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Scheme"",
                    ""action"": ""Dragging"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Scheme"",
            ""bindingGroup"": ""Scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
			// Gameplay
			m_Gameplay = asset.FindActionMap("Gameplay", true);
			m_Gameplay_Movement = m_Gameplay.FindAction("Movement", true);
			m_Gameplay_Zoom = m_Gameplay.FindAction("Zoom", true);
			m_Gameplay_Dragging = m_Gameplay.FindAction("Dragging", true);
		}

		public void Dispose()
		{
			UnityEngine.Object.Destroy(asset);
		}

		public InputBinding? bindingMask
		{
			get => asset.bindingMask;
			set => asset.bindingMask = value;
		}

		public ReadOnlyArray<InputDevice>? devices
		{
			get => asset.devices;
			set => asset.devices = value;
		}

		public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

		public bool Contains(InputAction action)
		{
			return asset.Contains(action);
		}

		public IEnumerator<InputAction> GetEnumerator()
		{
			return asset.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Enable()
		{
			asset.Enable();
		}

		public void Disable()
		{
			asset.Disable();
		}

		// Gameplay
		private readonly InputActionMap m_Gameplay;
		private IGameplayActions m_GameplayActionsCallbackInterface;
		private readonly InputAction m_Gameplay_Movement;
		private readonly InputAction m_Gameplay_Zoom;
		private readonly InputAction m_Gameplay_Dragging;

		public struct GameplayActions
		{
			private InputManager m_Wrapper;

			public GameplayActions(InputManager wrapper)
			{
				m_Wrapper = wrapper;
			}

			public InputAction Movement => m_Wrapper.m_Gameplay_Movement;
			public InputAction Zoom => m_Wrapper.m_Gameplay_Zoom;
			public InputAction Dragging => m_Wrapper.m_Gameplay_Dragging;

			public InputActionMap Get()
			{
				return m_Wrapper.m_Gameplay;
			}

			public void Enable()
			{
				Get().Enable();
			}

			public void Disable()
			{
				Get().Disable();
			}

			public bool enabled => Get().enabled;

			public static implicit operator InputActionMap(GameplayActions set)
			{
				return set.Get();
			}

			public void SetCallbacks(IGameplayActions instance)
			{
				if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
				{
					Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
					Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
					Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
					Zoom.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
					Zoom.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
					Zoom.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
					Dragging.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDragging;
					Dragging.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDragging;
					Dragging.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDragging;
				}

				m_Wrapper.m_GameplayActionsCallbackInterface = instance;
				if (instance != null)
				{
					Movement.started += instance.OnMovement;
					Movement.performed += instance.OnMovement;
					Movement.canceled += instance.OnMovement;
					Zoom.started += instance.OnZoom;
					Zoom.performed += instance.OnZoom;
					Zoom.canceled += instance.OnZoom;
					Dragging.started += instance.OnDragging;
					Dragging.performed += instance.OnDragging;
					Dragging.canceled += instance.OnDragging;
				}
			}
		}

		public GameplayActions Gameplay => new GameplayActions(this);
		private int m_SchemeSchemeIndex = -1;

		public InputControlScheme SchemeScheme
		{
			get
			{
				if (m_SchemeSchemeIndex == -1) m_SchemeSchemeIndex = asset.FindControlSchemeIndex("Scheme");
				return asset.controlSchemes[m_SchemeSchemeIndex];
			}
		}

		public interface IGameplayActions
		{
			void OnMovement(InputAction.CallbackContext context);
			void OnZoom(InputAction.CallbackContext context);
			void OnDragging(InputAction.CallbackContext context);
		}
	}
}