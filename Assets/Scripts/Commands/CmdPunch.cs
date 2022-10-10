using Strategy;
using UnityEngine;

namespace Commands
{
    public class CmdPunch : ICommand
    {
 
        private IFollower _follower;
        private Vector3 _target;

        public CmdPunch(IFollower follower , Vector3 target)
        {
            _follower = follower;
            _target = target;
        }

        public void Execute(){
            _follower.LookAt(_target);
            _follower.Attack();
        }
    }
}