using Strategy;

namespace Commands
{
    public class CmdShoot : ICommand
    {
        private IGun _gun;

        public CmdShoot(IGun gun)
        {
            _gun = gun;
        }

        public void Execute() => _gun.Shoot();
    }
}