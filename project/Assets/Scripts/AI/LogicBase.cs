using System;
using System.Collections.Generic;

namespace AI
{
    public abstract class LogicBase : ILogic
    {
        private class DelayedAction
        {
            public float TimesLeft;
            public float DelayTime;
            public Action Action;
        }

        private readonly List<DelayedAction> _delayedActions = new List<DelayedAction>();

        public virtual void Update(float deltaTime)
        {
            for (var i = _delayedActions.Count - 1; i >= 0; --i)
            {
                var delayedAction = _delayedActions[i];

                delayedAction.TimesLeft += deltaTime;
                if (delayedAction.TimesLeft < delayedAction.DelayTime) continue;

                _delayedActions.RemoveAt(i);
                delayedAction.Action.Invoke();
            }
        }

        protected void DelayCall(float delay, Action action)
        {
            if (delay > 0)
            {
                _delayedActions.Add(new DelayedAction {DelayTime = delay, Action = action});
            }
            else
            {
                action.Invoke();
            }
        }

        protected void ClearAllDelayedCalls()
        {
            _delayedActions.Clear();
        }
    }
}