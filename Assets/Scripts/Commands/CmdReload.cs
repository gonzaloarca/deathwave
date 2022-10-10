using Strategy;

namespace Commands
{
    public class CmdReload : ICommand
    {
        private IGun _gun;
        
        public CmdReload(IGun gun)
        {
            _gun = gun;
        }
        
        public void Execute()
        {
            _gun.Reload();
        }
    }
}