using System;
using Match3.Settings;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scripts
{
    public class AnimatedButton : UIBehaviour, IPointerDownHandler
    {
        [SerializeField] private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();
        public bool interactable = true;
        
        private Animator m_animator;
        
        public ButtonClickedEvent onClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }

        protected override void Start()
        {
            base.Start();
            m_animator = GetComponent<Animator>();
        }
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left || !interactable)
                return;

            Press();
        }

        private void Press()
        {
            if (!IsActive())
                return;

            m_animator.SetTrigger("Pressed");
            Invoke("InvokeOnClickAction", 0.1f);
        }

        private void InvokeOnClickAction()
        {
            m_OnClick.Invoke();
        }

        public void SetupNewLevel() => SettingsProvider.Get<LevelsData>().NextLevel();

    }

    [Serializable]
    public class ButtonClickedEvent : UnityEvent
    {
    }
}