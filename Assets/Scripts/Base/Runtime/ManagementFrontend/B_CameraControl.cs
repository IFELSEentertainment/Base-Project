using System.Collections.Generic;
using Base.UI;
namespace Base {
    public static class B_CameraControl {
        private static CameraFunctions _cameraFunctions;



        public static void Setup(CameraFunctions cameraFunctions) {
            _cameraFunctions = cameraFunctions;
            _cameraFunctions.ManagerStrapping();
        }
    }
}