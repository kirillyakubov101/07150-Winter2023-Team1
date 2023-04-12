using UnityEngine;
using UnityEngine.InputSystem;

namespace OurGame.Input
{
    public class InputControls : MonoBehaviour, MasterControls.IMainInputsActions
    {
        private static InputControls instance;
        private MasterControls m_masterControls;
        private Vector2 cameraMoveDir = Vector2.zero;
        private Vector2 mousePos = Vector2.zero;
        

        public Vector2 CameraMoveDir { get => cameraMoveDir; }
        public Vector2 MousePos { get => mousePos; }

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

        public void OnChangeLane(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hitPoint;

                if(Physics.Raycast(ray, out hitPoint))
                {
                    if(hitPoint.collider.tag == "Lane1")
                    {
                        UI.RecruitUI.Instance.ChangeLane(0);
                    }
                    if (hitPoint.collider.tag == "Lane2")
                    {
                        UI.RecruitUI.Instance.ChangeLane(1);
                    }
                    if (hitPoint.collider.tag == "Lane3")
                    {
                        UI.RecruitUI.Instance.ChangeLane(2);
                    }
                }
            }
        }
    }
}

