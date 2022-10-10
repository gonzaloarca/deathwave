using Controllers;

namespace Commands
{
    public class CmdRecoilFire : ICommand
    {
        private RecoilController _recoilController;
        private GunRecoil _recoil;
        
        public CmdRecoilFire(RecoilController recoilController, GunRecoil recoil)
        {
            _recoilController = recoilController;
            _recoil = recoil;
        }
        
        public void Execute()
        {
            _recoilController.Fire(_recoil);
        }
    }
}