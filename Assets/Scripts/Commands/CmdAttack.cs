using Strategy;

namespace Commands
{
    public class CmdAttack : ICommand
    {
        private IGun _gun;

        public CmdAttack(IGun gun)
        {
            _gun = gun;
        }

        public void Execute() => _gun.Attack();
    }
}