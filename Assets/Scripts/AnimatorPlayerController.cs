using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorPlayerController
{
    public static class States
    {
        public const string Jump = nameof(Jump);
        public const string IsGrounded = nameof(IsGrounded); 
        public const string Compressing = nameof(Compressing); 
        public const string Idle = nameof(Idle); 
    }
}