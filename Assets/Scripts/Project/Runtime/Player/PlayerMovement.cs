using System;
namespace Base {
    public class PlayerMovement : PlayerSubFrame {
        #region Properties


        
        public Action<bool> EndReached;

        private bool canMove;
        public override bool CanAct => base.CanAct && canMove;

        #endregion

        #region Unity Functions

        private void Start() {
            SetupSubFrame();
        }

        public override void Update() {
            base.Update();
        }

        public override void FixedUpdate() {
            base.FixedUpdate();
        }

        #endregion

        #region Spesific Functions

        public override void SetupSubFrame() {
            base.SetupSubFrame();
            Parent.movement = this;
            EndReached += Parent.EndGameFunction;
        }

        public override void Go() {
            base.Go();
            
            canMove = true;
        }

        public override void EndFunctions() {
            base.EndFunctions();
        }

        #endregion

        #region Generic Functions

        #endregion

        #region IEnumerators

        #endregion
    }
}