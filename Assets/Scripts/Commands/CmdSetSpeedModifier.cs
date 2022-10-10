using Strategy;

namespace Commands
{
    public class CmdSetSpeedModifier : ICommand
    {
        private IMovable _movable;
        private float _speedModifier;

        public CmdSetSpeedModifier(IMovable movable, float speedModifier)
        {
            _movable = movable;
            _speedModifier = speedModifier;
        }

        public void Execute()
        {
            _movable.SetSpeedModifier(_speedModifier);
        }
    }
}