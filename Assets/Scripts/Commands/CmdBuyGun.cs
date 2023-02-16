using Buyable;

namespace Commands
{
    public class CmdBuyShotgun : ICommand
    {
        private IBuyable _buyable;

        public CmdBuyShotgun(IBuyable buyable)
        {
            _buyable = buyable;
        }
        public void Execute()
        {
            _buyable.Buy();
        }
    }
}