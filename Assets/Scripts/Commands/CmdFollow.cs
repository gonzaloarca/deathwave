using Strategy;
using UnityEngine;

namespace Commands
{
    public class CmdFollow : ICommand
    {
        private IFollower _follower;
        private Vector3 _target;

        public CmdFollow(IFollower follower, Vector3 target)
        {
            _follower = follower;
            _target = target;
        }

        public void Execute(){
            // firsrt check if gameobject is null
            
            _follower.LookAt(_target);
            _follower.Travel(Vector3.forward);
        }
    }
}