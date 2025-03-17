using UnityEngine;
using UnityEngine.Pool;

namespace MuziCharacter
{
    public class ReturnToPool : MonoBehaviour
    {
        private ItemGameObject _item;
        private IObjectPool<ItemGameObject> _pool;

        // test only
        // private void OnEnable()
        // {
        //     CoroutineHandler.StartStaticCoroutine(KillMeSoftlyPlease());
        // }

        // IEnumerator KillMeSoftlyPlease()
        // {
        //     yield return new WaitForSeconds(UnityEngine.Random.Range(3f,5f));
        //     pool.Release(system);
        // }

        public void SetPool(ItemGameObject item, IObjectPool<ItemGameObject> pool)
        {
            _item = item;
            _pool = pool;
        }

        
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
[Sirenix.OdinInspector.Button] 
#endif
        public void Return()
        {
            _pool.Release(_item);
        }
    }
}