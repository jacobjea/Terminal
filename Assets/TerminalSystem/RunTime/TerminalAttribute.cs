using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Terminal
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TerminalAttribute : Attribute
    {
        public TerminalAttribute()
        {

        }
    }
}

