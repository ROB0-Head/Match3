using System;
using UnityEngine;

namespace UI.Popups
{
    public class BasePopup : MonoBehaviour
    {
        private string _popupId = Guid.NewGuid().ToString();
        public string PopupId => _popupId;

        public virtual void Setup(PopupSettings popupSettings)
        {
            
        }
        
        public virtual void Close()
        {
            if(gameObject != null)
                Destroy(gameObject);

            PopupSystem.CloseThisPopup(PopupId);
        }
    }

    [Serializable]
    public class PopupSettings
    {
        
    }
}