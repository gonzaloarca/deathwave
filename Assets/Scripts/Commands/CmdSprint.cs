using Strategy;
using UnityEngine;

namespace Commands
{
    public class CmdSprint : ICommand
    {
        private IMovable _movable;
        private bool _isSprinting;

        public CmdSprint(IMovable movable, bool isSprinting)
        {
            _movable = movable;
            _isSprinting = isSprinting;
        }

        public void Execute()
        {
            _movable.Sprint(_isSprinting);
        }
    }
}