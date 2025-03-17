using TriLibCore.Extensions;
using TriLibCore.General;
using UnityEngine;
using UnityEngine.Pool;

namespace MuziCharacter
{
    public class ItemGameObject
    {
        private IObjectPool<ItemGameObject> _pool;
        public CategoryCode Category;
        public int EquippedOrder;
        public string ExternalId;
        public GameObject GameObject;
        public bool InitiallyEquipped = true;

        public string UniqueItemKey => $"{Category}@{ExternalId}";

        public bool CurrentlyEquipped =>
            GameObject != null && GameObject.activeInHierarchy; // to activate after the SET is remove


        public ItemGameObject Clone()
        {
            return new ItemGameObject()
            {
                ExternalId = ExternalId,
                GameObject = Object.Instantiate(GameObject),
                Category = Category
            };
        }

        private void ResetBoneToSelf()
        {
            var _rootBone =
                GameObject.transform.FindDeepChild("Root_M",
                    StringComparisonMode.RightEqualsLeft,
                    true);
            
            var skinnedMeshes = GameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            
            foreach (var skinnedMesh in skinnedMeshes)
            {
                if (skinnedMesh != null &&  _rootBone != null)
                {
                    skinnedMesh.AssignBone(_rootBone);
                }
            }
            
        }
        
        public void ReturnToPool()
        {
            ResetBoneToSelf();
            _pool?.Release(this);
        }


        public void SetPool(IObjectPool<ItemGameObject> pool) => _pool = pool;
    }
}