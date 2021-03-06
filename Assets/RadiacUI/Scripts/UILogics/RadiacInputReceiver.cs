using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace RadiacUI
{
    /// <summary>
    /// Deal with keyboard input.
    /// This component is a signal emitter, take keyboard inputs and emit signals.
    /// </summary>
    public abstract class RadiacInputReceiver : RadiacUIComponent
    {
        public bool focused { get { return RadiacInputController.focusing == this; } }
        
        public string focusSignal;
        public string cancelSignal;
        
        public virtual void ReceiveOperator(KeyCode op)
        {
            // do nothing...
            // place holder recently...
        }
        
        public virtual void ReceiveChar(char c)
        {
            // do nothing...
            // place holder recently...
        }
        
        protected override void Start()
        {
            focusSignal = ParseRequest(focusSignal);
            cancelSignal = ParseRequest(cancelSignal);
            base.Start();
            
            AddCallback(new Signal(focusSignal), () =>
            {
                RadiacInputController.focusing = this;
            });
            
            AddCallback(new Signal(cancelSignal), () =>
            {
                if(RadiacInputController.focusing == this)
                {
                    RadiacInputController.focusing = null;
                }
            });
        }
    }
}
