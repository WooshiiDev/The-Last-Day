// GENERATED AUTOMATICALLY FROM 'Assets/MainInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInput"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""3fff1157-f344-4b2a-ba63-5f6bd8cf94fe"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""652fa531-3aec-4568-8c04-d2c4e90ecbc4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UI"",
                    ""type"": ""Button"",
                    ""id"": ""de517a6f-769f-422a-93dc-be8387b9e519"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""168e5124-1229-4d23-8bf0-03487ab3abbb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8a663915-b155-4cb2-92aa-c08203da7b8e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard | Mouse;Xbox"",
                    ""action"": ""UI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""2aff287a-1b89-4440-899c-862cd4526538"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f98710dc-6374-4200-96b4-bcaa2925961d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard | Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1ddb00c4-133d-4103-bea0-015c86f414fa"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard | Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""44fd303d-ee21-4964-b97d-315f43095008"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard | Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cd11748a-c79f-40b6-9ea0-ba74c24ebdf5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard | Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a7fa6f2c-1984-4a33-9300-320856bc0978"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d622312c-23f2-4487-8c56-954b2760beef"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard | Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard | Mouse"",
            ""bindingGroup"": ""Keyboard | Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Move = m_Game.FindAction("Move", throwIfNotFound: true);
        m_Game_UI = m_Game.FindAction("UI", throwIfNotFound: true);
        m_Game_Aim = m_Game.FindAction("Aim", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Move;
    private readonly InputAction m_Game_UI;
    private readonly InputAction m_Game_Aim;
    public struct GameActions
    {
        private @MainInput m_Wrapper;
        public GameActions(@MainInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Game_Move;
        public InputAction @UI => m_Wrapper.m_Game_UI;
        public InputAction @Aim => m_Wrapper.m_Game_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @UI.started -= m_Wrapper.m_GameActionsCallbackInterface.OnUI;
                @UI.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnUI;
                @UI.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnUI;
                @Aim.started -= m_Wrapper.m_GameActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @UI.started += instance.OnUI;
                @UI.performed += instance.OnUI;
                @UI.canceled += instance.OnUI;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard | Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    public interface IGameActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnUI(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
