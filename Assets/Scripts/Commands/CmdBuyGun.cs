using Buyable;

namespace Commands
{
    public class CmdBuy : ICommand
    {
        private IBuyable _buyable;

        public CmdBuy(IBuyable buyable)
        {
            _buyable = buyable;
        }
        public void Execute()
        {
            _buyable.Buy();
        }
    }
}