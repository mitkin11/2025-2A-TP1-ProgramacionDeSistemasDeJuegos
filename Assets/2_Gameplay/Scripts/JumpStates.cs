using UnityEngine;
namespace Gameplay
{
    public interface IJumpController
    {
        void RunJumpCoroutine();
        void SetJumpState(JumpState newState);
    }
    public abstract class JumpState
    {
        protected IJumpController controller;

        public JumpState (IJumpController controller)
        {
            this.controller = controller;
        }

        public abstract void HandleJump();
        public abstract void OnLanding();


    }

    public class SingleJumpState : JumpState
    {
        public SingleJumpState(IJumpController controller): base(controller){}

        public override void HandleJump()
        {
            controller.RunJumpCoroutine();
            controller.SetJumpState(new DoubleJumpState(controller));
        }

        public override void OnLanding()
        {
             controller.SetJumpState(new SingleJumpState(controller));
        }
    }

    public class DoubleJumpState : JumpState
    {
        public DoubleJumpState(IJumpController controller) : base(controller) { }

        public override void HandleJump()
        {
            controller.RunJumpCoroutine();
        }

        public override void OnLanding()
        {
            controller.SetJumpState(new SingleJumpState(controller));
        }
    }
}
