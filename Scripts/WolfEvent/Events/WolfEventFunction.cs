using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wolf
{
    public class WolfEventFunction : WolfEventBase
    {

        public enum EventFunctionType { Start = 0, Update = 1, Custom = 20 }
        public EventFunctionType eventType;


    }
}