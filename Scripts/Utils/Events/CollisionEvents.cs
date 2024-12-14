using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Wolf
{
    [AddComponentMenu("Wolf/Utils/Event/CollisionEvents")]
    public class CollisionEvents : MonoBehaviour
    {

        public delegate void OnTriggerEnterEvent(Collider other);
        public delegate void OnTriggerStayEvent(Collider other);
        public delegate void OnTriggerExitEvent(Collider other);
        public delegate void OnCollisionEnterEvent(Collision collision);
        public delegate void OnCollisionStayEvent(Collision collision);
        public delegate void OnCollisionExitEvent(Collision collision);

        public OnTriggerEnterEvent onTriggerEnterCallback;
        public OnTriggerStayEvent onTriggerStayCallback;
        public OnTriggerExitEvent onTriggerExitCallback;
        public OnCollisionEnterEvent onCollisionEnterCallback;
        public OnCollisionStayEvent onCollisionStayCallback;
        public OnCollisionExitEvent onCollisionExitCallback;
        public UnityEvent onTriggerEnterEvent;
        public UnityEvent onTriggerStayEvent;
        public UnityEvent onTriggerExitEvent;
        public UnityEvent onCollisionEnterEvent;
        public UnityEvent onCollisionStayEvent;
        public UnityEvent onCollisionExitEvent;


        private void OnTriggerEnter(Collider other)
        {
            if(onTriggerEnterCallback != null) onTriggerEnterCallback(other);
            if (onTriggerEnterEvent != null) onTriggerEnterEvent.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            if (onTriggerStayCallback != null) onTriggerStayCallback(other);
            if (onTriggerStayEvent != null) onTriggerStayEvent.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (onTriggerExitCallback != null) onTriggerExitCallback(other);
            if (onTriggerExitEvent != null) onTriggerExitEvent.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (onCollisionEnterCallback != null) onCollisionEnterCallback(collision);
            if (onCollisionEnterEvent != null) onCollisionEnterEvent.Invoke();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (onCollisionStayCallback != null) onCollisionStayCallback(collision);
            if (onCollisionStayEvent != null) onCollisionStayEvent.Invoke();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (onCollisionExitCallback != null) onCollisionExitCallback(collision);
            if (onCollisionExitEvent != null) onCollisionExitEvent.Invoke();
        }
    }
}