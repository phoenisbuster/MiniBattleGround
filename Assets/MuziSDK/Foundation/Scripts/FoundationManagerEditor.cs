using Grpc.Core;
using Muziverse.Proto.User.Api.Login;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FoundationManagerEditor : MonoBehaviour
{
    // Start is called before the first frame update
    static Channel _channel = null;
    public static string serverAddress = "api-stg.muziverse.tech:443";
    public static ChannelCredentials serverCredential = ChannelCredentials.SecureSsl;
    //public static string adminToken = "eyJraWQiOiIxIiwidHlwIjoiSldUIiwiYWxnIjoiRVM1MTIifQ.eyJpc3MiOiJtdXppdmVyc2VAbXV6aXZlcnNlLmNvbSIsImV4cCI6MTY0NTU4NzcxOCwidG9rZW5DbGFpbSI6eyJ1c2VySWQiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDAiLCJ0eXBlIjoiQUNDRVNTX1RPS0VOIiwicm9sZXMiOlsiUk9MRV9TWVNURU0iXX0sImlhdCI6MTY0Mjk5NTcxOCwianRpIjoiNjE2ZTY4OTktYTUyYi00YmYxLWIwNDAtOTk5NzczNjEyMmVkIn0.AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEqvuOkc02fx-AQEA--JARaK-kC99QCb02IqER4FatakAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFBI3yyQL_7tiygnY6DMQAvZ5-O3-Ig0k2ZkDLow6P3d";
    //public static string adminToken = "eyJraWQiOiIxIiwidHlwIjoiSldUIiwiYWxnIjoiRVM1MTIifQ.eyJpc3MiOiJtdXppdmVyc2VAbXV6aXZlcnNlLmNvbSIsImV4cCI6MTY1MzEyNzUxNiwidG9rZW5DbGFpbSI6eyJ1c2VySWQiOiI2ZTdkNzU3YS0yNDgyLTQ3NTYtYTI0MC1lNGQ3YjU5MWQ4NjMiLCJ0eXBlIjoiQUNDRVNTX1RPS0VOIiwicm9sZXMiOlsiUk9MRV9BRE1JTiJdfSwiaWF0IjoxNjUwNTM1NTE2LCJqdGkiOiI3MDE1NjEzMS00Zjk5LTQ5NWYtODFmYy01ZDkyZGEzZjkxYjIifQ.AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAF1dTlLQ5ZCsmpGfPI1rURpZjXrGDIxwthrvCy7EW4HvAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAK_F8M6mZwqOa8iGsExnpkYgLmSv59lxLroMVTWNhntN";
    public static string adminToken = "eyJraWQiOiIxIiwidHlwIjoiSldUIiwiYWxnIjoiRVM1MTIifQ.eyJpc3MiOiJtdXppdmVyc2VAbXV6aXZlcnNlLmNvbSIsImV4cCI6MjI1MTYzNjc3MCwidG9rZW5DbGFpbSI6eyJ1c2VySWQiOiJiMjlhOTQ5OC02OWE4LTRlYTYtYTM3OS1lYmIzODBjMzZlMTgiLCJ0eXBlIjoiQUNDRVNTX1RPS0VOIiwicm9sZXMiOlsiUk9MRV9BRE1JTiJdfSwiaWF0IjoxNjUxNjM2NzcwLCJqdGkiOiJiNDJhZjg4Yy05OTkxLTQwNDEtOWMzNC0wMzIwNzhkNWY2NGEifQ.AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALmS89qNsFYy5bnOTkALBs9ItU2QYk0LgcMlsTIjbE-GAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIyE17Nj_x8qczOCLtEFVirkG_Dwttn3-9PaRglSZJzC";
    static public Channel channel
    {
        get
        {
            if (_channel == null) _channel = new Channel(serverAddress, serverCredential);
            return _channel;
        }
    }
    public static Metadata metadata
    {
        get
        {





            return new Metadata { { "Authorization", adminToken } };


        }
    }
    //public static async string LoginAdmin()
    //{
    //    var client = new UserLoginServiceClient(FoundationManager.channel);
    //    UserAccessResponse reply = await client.LoginAsync(new UserAccessRequest
    //    {
    //        Provider = AuthenticationProvider.Internal,
    //        InternalRequest = new UserAccessInternalRequest { Email = textUserName.text, Password = textPassword.text, MfaCode = "000000" }
    //    });
    //}
    public static RpcJSONError GetErrorFromMetaData(RpcException e)
    {
        if (e.Trailers.Get("error-code-details") != null)
        {
            string encodedString = e.Trailers.Get("error-code-details").Value;
            encodedString = encodedString.PadRight(encodedString.Length + (4 - encodedString.Length % 4) % 4, '=');
            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);
            //Debug.Log(decodedString);

            RpcJSONError r = JsonUtility.FromJson<RpcJSONError>(decodedString);
            Debug.Log(e.Message);
            return r;
        }
        RpcJSONError rpcJSONError = new RpcJSONError { hasErrorMessage = false };
        rpcJSONError.errorMessageFull = "Unknown error: Cannot read the muzi's error code details; Original message: " + e.Message;
        rpcJSONError.errorMessage = e.Status.Detail;
        foreach (string key in e.Data.Keys) Debug.Log(key);

        return rpcJSONError;
    }
}
