using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpcJSONError 
{
    public string errorCode;
    public string errorMessage;
    public string errorMessageFull;
    public string errorMessageLanguage;
    public RpcJSONOptionalData optionalData;
    public bool hasErrorMessage = true;
    public bool isWarned = false;
}

[Serializable]
public class RpcJSONOptionalData
{
    public string availableTime;
    public string otpVerificationToken;
}