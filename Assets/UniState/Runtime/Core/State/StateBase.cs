using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace UniState
{
    /// <summary>
    /// Base class for states without payload.
    /// </summary>
    public abstract class StateBase : StateBase<EmptyPayload>
    {
    }

    /// <summary>
    /// Base class for states with typed payload.
    /// Provides lifecycle management, disposable handling, and state transitions.
    /// </summary>
    /// <typeparam name="T">The type of payload this state accepts.</typeparam>
    public abstract class StateBase<T> : IState<T>
    {
        private List<IDisposable> _disposables;

        /// <summary>
        /// Gets the payload passed to this state.
        /// </summary>
        protected T Payload { get; private set; }

        /// <summary>
        /// Gets the transition facade for creating state transitions.
        /// </summary>
        protected IStateTransitionFacade Transition { get; private set; }
        
        /// <summary>
        /// Gets the list of disposables that will be disposed when the state is disposed.
        /// The list is created only when first accessed to avoid unnecessary allocations.
        /// </summary>
        protected List<IDisposable> Disposables => _disposables ??= new List<IDisposable>(4);

        public abstract UniTask<StateTransitionInfo> Execute(CancellationToken token);

        public virtual UniTask Initialize(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask Exit(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }

        public virtual void SetPayload(T payload)
        {
            Payload = payload;
        }

        public virtual void SetTransitionFacade(IStateTransitionFacade transitionFacade) => Transition = transitionFacade;

        public virtual void Dispose()
        {
            _disposables?.Dispose();
            _disposables = null;
        }
    }
}