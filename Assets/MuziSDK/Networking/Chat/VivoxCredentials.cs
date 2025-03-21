﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

public class VivoxCredentials
{
    public VivoxUnity.Client client;
    public Uri server = new Uri("https://mt1s.www.vivox.com/api2");
    public string issuer = "locngu7078-te35-dev";
    public  string domain = "mt1s.vivox.com";
    public string tokenKey = "fizz496";
    public TimeSpan timeSpan = TimeSpan.FromSeconds(90);


    public ILoginSession loginSession;
    public IChannelSession channelSession;

    public List<IFailedDirectedTextMessage> failedMessages = new List<IFailedDirectedTextMessage>();
}
