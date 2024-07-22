using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationID : MonoBehaviour
{
    public static readonly int MovementID = Animator.StringToHash("Movement");
    public static readonly int IsFightID = Animator.StringToHash("IsFight");
    public static readonly int WalkID = Animator.StringToHash("Walk");
    public static readonly int RunID = Animator.StringToHash("Run");
    public static readonly int HasInputID = Animator.StringToHash("HasInput");
    public static readonly int DetalAngleID = Animator.StringToHash("DetalAngle");
}
