using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuziNakamaBuffer;
using Networking;
using Google.Protobuf;

public class NakamaNetworkObject : MonoBehaviour
{
    public uint Id;
    //[Tooltip("Time to reset the initial postion of the object")]
    //[SerializeField] float intervalSendInfo = -1;
    [Tooltip("Time to reset the initial postion of the object")]
    [SerializeField] float intervalAutoResetPosition = -1;
    Rigidbody rigidBody;
    void Start()
    {
        if(rigidBody== null) rigidBody= GetComponent<Rigidbody>();
        if (intervalAutoResetPosition > 0) StartCoroutine(AutoResetPosition());
    }
    IEnumerator AutoResetPosition()
    {
        Vector3 oldPosition = transform.position;
        while(true)
        {
            yield return new WaitForSeconds(intervalAutoResetPosition);
            transform.position = oldPosition;
            if (rigidBody != null) rigidBody.velocity = Vector3.zero;
        }
    }


    public void SetPositionVelocity(UNBufPositionVelocityFull positionVelocity)
    {
        transform.position = new Vector3(positionVelocity.X, positionVelocity.Y, positionVelocity.Z);
        if (rigidBody!=null)
        {
            rigidBody.velocity = new Vector3 (positionVelocity.Vx, positionVelocity.Vy, positionVelocity.Vz);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 31)
        {
            SendMyPosition();
        }
    }

    float lastTimeSendPosition = -923;
    async void SendMyPosition()
    {
        if (Time.time - lastTimeSendPosition >= 0.05f)
        {
            lastTimeSendPosition = Time.time;
            UNBufPositionVelocityFull posFull = new UNBufPositionVelocityFull
            {
                Id = Id,
                InMatchUserId = NakamaMyNetworkPlayer.instance.nakamaNetworkPlayer.userInfo.InMatchUserId,
                X = transform.position.x,
                Y = transform.position.y,
                Z = transform.position.z,
                Vx = rigidBody.velocity.x,
                Vy = rigidBody.velocity.y,
                Vz = rigidBody.velocity.z,
            };
            if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
            {
                await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_NetworkObjectChange, posFull.ToByteArray());
            }
        }
    }
}
