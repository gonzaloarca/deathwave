using Strategy;
using UnityEngine;

namespace Commands
{
    public class CmdPunch : ICommand
    {
        private IMelee _melee;

        public CmdPunch(IMelee melee)
        {
            _melee = melee;
        }

        public void Execute() => _melee.Attack();
    }
}