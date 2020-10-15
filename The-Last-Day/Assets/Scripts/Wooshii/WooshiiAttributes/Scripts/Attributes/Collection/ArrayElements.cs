using UnityEngine;
using System;

namespace WooshiiAttributes
    {
    [AttributeUsage (AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ArrayElements : PropertyAttribute
        {
        public ArrayElements()
            {

            }
        }
    }
