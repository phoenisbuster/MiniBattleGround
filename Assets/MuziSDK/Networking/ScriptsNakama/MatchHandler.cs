using MuziNakamaBuffer;
using Google.Protobuf;
using Nakama;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class MatchHandler : MonoBehaviour
    {

        public bool verbose = false;
        public Dictionary<uint, NakamaNetworkPlayer> networkPlayers = new Dictionary<uint, NakamaNetworkPlayer>();
        public IMatch currentMatch;
        public NakamaMatchInfo currentMatchInfo;
        Dictionary<uint, NakamaNetworkObject> networkObjects = new Dictionary<uint, NakamaNetworkObject>();
        protected Dictionary<uint, NakamaNetworkIntervalObject> networkObjectsRoute = new Dictionary<uint, NakamaNetworkIntervalObject>();
        virtual public void Init(IMatch currentMatch, NakamaMatchInfo currentMatchInfo)
        {
            this.currentMatch = currentMatch;
            this.currentMatchInfo = currentMatchInfo;
        }

        virtual public void OnLeaveMatch()
        {

        }

        virtual public void ReceivedMatchPresence(IMatchPresenceEvent presenceEvent)
        {
            Debug.Log("[MatchPresenceHandler] This is a virtual function!!!");
        }
        virtual public void ReceivedMatchState(IMatchState presenceEvent)
        {
            Debug.Log("[MatchStateListener] This is a virtual function!!!");
        }
        public NakamaNetworkPlayer AddNakamaNetworkPlayer(Vector3 position, uint InMatchUserId)
        {
            GameObject obj = Instantiate(NakamaContentManager.instance.networkPrefab, position, Quaternion.identity) as GameObject;
            NakamaNetworkPlayer networkPlayer = obj.GetComponent<NakamaNetworkPlayer>();
            networkPlayer.muziLOD.ReCheckNakamaLODLevel();
            networkPlayer.userInfo.InMatchUserId = InMatchUserId;
            return networkPlayer;
        }
        public GameObject AddTestSubCharacter(GameObject m)
        {
            Vector3 pos = new Vector3(m.transform.position.x + UnityEngine.Random.Range(-20.0f, 20.0f), 0, m.transform.position.z + UnityEngine.Random.Range(-20.0f, 20.0f));
            var g = AddNakamaNetworkPlayer(pos, 0);
            g.transform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(-180, 180), 0);
            return g.gameObject;
        }
        public void ClearNetworkPlayers()
        {
            foreach (KeyValuePair<uint, NakamaNetworkPlayer> p in networkPlayers)
                Destroy(p.Value.gameObject);
            networkPlayers.Clear();
        }
        protected IEnumerator Ping(float interval)
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);
                if (NakamaContentManager.instance.currentMatch == null) continue;
                UNBufPing ping = new UNBufPing { TimeSend = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond };
                yield return NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_Ping, ping.ToByteArray());
                //Debug.Log("pinning");
            }
        }
        public void DestroyPlayer(uint id)
        {
            if (networkPlayers.ContainsKey(id))
            {
                var o = networkPlayers[id];
                Destroy(o.gameObject);
                networkPlayers.Remove(id);
            }
        }
        public void ServerNotification(UNBufServerNotifyPresence content)
        {
            if (content.IsDevOnly)
                TW.I.AddWarning("Warning", "[For DEV] " + content.Message);
            else TW.I.AddWarning("Warning", content.Message);
        }
        protected IEnumerator UpdateMuziLODLevels()
        {
            List<uint> destroyingPlayer = new List<uint>();
            while (true)
            {
                yield return new WaitForSeconds(3);
                foreach (KeyValuePair<uint, NakamaNetworkPlayer> p in networkPlayers)
                {
                    if (p.Value != null && p.Value.muziLOD != null)
                    {
                        p.Value.muziLOD.ReCheckNakamaLODLevel();
                        if (p.Value.muziLOD.myMuziLODLevel <= MuziLODLevel.FAR &&
                            (Mathf.Abs(p.Value.endPosition.x - p.Value.transform.position.x)
                            + Mathf.Abs(p.Value.endPosition.z - p.Value.transform.position.z) < 0.2f))
                            destroyingPlayer.Add(p.Key);
                        else if (Time.time - p.Value.LastTimeReceiveAPositionMessage > 120)
                        {
                            destroyingPlayer.Add(p.Key);
                        }
                    }
                }
                while (destroyingPlayer.Count > 0)
                {
                    Debug.Log("Destroying: " + destroyingPlayer[0]);
                    DestroyPlayer(destroyingPlayer[0]);
                    destroyingPlayer.RemoveAt(0);
                }
            }
        }
        protected void CollectAllNetworkObjectsInScene()
        {
            networkObjects.Clear();
            var objects = FindObjectsOfType<NakamaNetworkObject>();
            foreach (var obj in objects)
            {
                if(networkObjects.ContainsKey(obj.Id))
                {
                    Debug.Log("[TOANSTT WARNING] There are two NakamaNetworkObjects have the same Id = " + obj.Id);
                }
                networkObjects[obj.Id] = obj;
            }
        }
        public NakamaNetworkObject GetNetworkObjectInScene(uint id)
        {
            if (networkObjects.ContainsKey(id))
            {
                if ( networkObjects[id]!=null) return networkObjects[id];
                networkObjects.Remove(id);
            }
            var objects =  FindObjectsOfType<NakamaNetworkObject>();
            foreach(var obj in objects)
            {
                if(obj.Id == id)
                {
                    networkObjects[id] = obj;
                    return obj;
                }
            }
            return null;
        }
        public void RegisterANakamaNetworkObjectRoute(NakamaNetworkIntervalObject nor)
        {
            networkObjectsRoute.Add((uint)nor.Id, nor);
        }
    }

}