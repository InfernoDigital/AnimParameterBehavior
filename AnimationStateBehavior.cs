using UnityEngine;

public class AnimationStateBehavior : StateMachineBehaviour
{
    [System.Serializable]
    public class AnimationAction
    {
        public string parameterName;
        public ActionType actionType;
        public float floatValue;
        public int intValue;
        public bool boolValue;
        public bool setTrigger; // New field to determine whether to set or reset the trigger

        public enum ActionType
        {
            Float,
            Int,
            Bool,
            Trigger
        }
    }

    public AnimationAction[] actions;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ApplyActions(animator);
    }

    private void ApplyActions(Animator animator)
    {
        if (actions != null)
        {
            foreach (var action in actions)
            {
                switch (action.actionType)
                {
                    case AnimationAction.ActionType.Float:
                        animator.SetFloat(action.parameterName, action.floatValue);
                        break;
                    case AnimationAction.ActionType.Int:
                        animator.SetInteger(action.parameterName, action.intValue);
                        break;
                    case AnimationAction.ActionType.Bool:
                        animator.SetBool(action.parameterName, action.boolValue);
                        break;
                    case AnimationAction.ActionType.Trigger:
                        if (action.setTrigger)
                        {
                            animator.SetTrigger(action.parameterName);
                        }
                        else
                        {
                            animator.ResetTrigger(action.parameterName);
                        }
                        break;
                    default:
                        Debug.LogError("Unsupported action type");
                        break;
                }
            }
        }
    }
}
