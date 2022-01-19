using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
namespace Base {
    public static class B_EffectsManager {
        public static Task EffectsManagerStrapping() {
            return Task.CompletedTask;
        }

        public static B_PooledParticle SpawnAParticle(object enumToPull, Vector3 positionToSpawnIn, [Optional] Quaternion rotationToSpawnIn) {
            return B_VFM_EffectsManager.instance.SpawnAParticle(enumToPull, positionToSpawnIn);
        }
    }
}