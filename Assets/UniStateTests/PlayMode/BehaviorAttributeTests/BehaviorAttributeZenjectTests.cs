using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UniState;
using UniStateTests.Common;
using UniStateTests.PlayMode.StateBehaviorAttributeTests.Infrastructure;
using UnityEngine.TestTools;
using Zenject;

namespace UniStateTests.PlayMode.StateBehaviorAttributeTests
{
    [TestFixture]
    public class BehaviorAttributeZenjectTests: ZenjectTestsBase
    {
        [UnityTest]
        public IEnumerator RunChaneOfStateWithAttributes_ExitFromChain_ChainExecutedCorrectly() => UniTask.ToCoroutine(async () =>
        {
            await RunAndVerify<IVerifiableStateMachine, FirstState>();
        });

        protected override void SetupBindings(DiContainer container)
        {
            base.SetupBindings(container);

            container.BindInterfacesAndSelfTo<BehaviourAttributeTestHelper>().AsSingle();

            container.BindStateMachine<IVerifiableStateMachine, StateMachineBehaviourAttribute>();

            container.BindState<FirstState>();
            container.BindState<NoReturnState>();
            container.BindState<FastInitializeState>();
        }
    }
}