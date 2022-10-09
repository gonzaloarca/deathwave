using Strategy;

namespace Commands
{
    public class CmdJump : ICommand
    {
        private IMovable _movable;

        public CmdJump(IMovable movable)
        {
            _movable = movable;
        }

        public void Execute()
        {
            _movable.Jump();
        }
    }
}