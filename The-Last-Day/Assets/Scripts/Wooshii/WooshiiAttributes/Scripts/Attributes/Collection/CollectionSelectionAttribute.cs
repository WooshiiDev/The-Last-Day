using UnityEngine;
using System;

namespace WooshiiAttributes
    {
    [AttributeUsage (AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CollectionSelectionAttribute : PropertyAttribute
        {
        public int row;
        public CollectionSelectionAttribute(int row = 3)
            {
            this.row = row;
            }
        }
    }
