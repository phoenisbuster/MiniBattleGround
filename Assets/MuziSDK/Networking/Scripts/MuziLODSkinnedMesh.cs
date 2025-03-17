
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TriLibCore.Extensions;
    using TriLibCore.General;
    using UnityEngine;

    public class MuziLODSkinnedMesh: MonoBehaviour
    {
        [SerializeField]
        private Transform baseGO;

        private IEnumerator Start()
        {
            do
            {
                baseGO = transform.FindDeepChild("(Clone)", StringComparisonMode.LeftContainsRight, false);
                yield return new WaitForEndOfFrame();
            } while (baseGO == null);
        }
        public void EnableMeshes()
        {
            if (baseGO == null) return;
            baseGO.gameObject.SetActive(true);
        }
        public void DisableMeshes()
        {
            if (baseGO == null) return;
            baseGO.gameObject.SetActive(false);
        }
    }
