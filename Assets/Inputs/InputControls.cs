using UnityEngine;
using UnityEngine.InputSystem;

namespace OurGame.Input
{
    public class InputControls : MonoBehaviour, MasterControls.IMainInputsActions
    {
        private static InputControls instance;
        private MasterControls m_masterControls;
        private Vector2 cameraMoveDir = Vector2.zero;

        public Vector2 CameraMoveDir { get => cameraMoveDir; }
        public static InputControls Instance 
        { 
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<InputControls>();
                }

                return instance;
            }
        }

        #region Subscribe_To_Input_System
        private void OnEnable()
        {
            m_masterControls = new MasterControls();
            m_masterControls.MainInputs.SetCallbacks(this);
            m_masterControls.MainInputs.Enable();
        }
        private void OnDestroy()
        {
            m_masterControls.MainInputs.Disable();
        }
        #endregion

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void OnCameraMoveHorizontal(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                cameraMoveDir = context.ReadValue<Vector2>();
            }
            else
            {
                cameraMoveDir = Vector2.zero;
            }
        }
    }
}

