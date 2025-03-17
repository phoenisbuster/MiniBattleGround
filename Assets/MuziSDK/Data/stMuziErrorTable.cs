//Author: toanstt 
//This file is generated, do not edit!
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class stMuziErrorTable  
{
private static stMuziErrorTable _instance;
public stMuziError[] list;
public Dictionary<string, stMuziError> VALUE;
public stMuziErrorTable()
{
	VALUE = new Dictionary<string, stMuziError>();
}
public static stMuziErrorTable I
{
	get
	{
	if (_instance == null)
	       {
	           _instance = new stMuziErrorTable();
	           _instance.load();
	       }
	       return _instance;
	}
}
public stMuziError Get(string id)
{
return VALUE[id];
}
public void load()
{
stMuziError t;
t = new stMuziError();
t.Id=6000100000f;
t.Message="Information Root Message";
VALUE.Add("6000100000", t);

t = new stMuziError();
t.Id=6000200000f;
t.Message="Successful Root Message";
VALUE.Add("6000200000", t);

t = new stMuziError();
t.Id=6000300000f;
t.Message="Redirection Root Message";
VALUE.Add("6000300000", t);

t = new stMuziError();
t.Id=6000400000f;
t.Message="Client Error Root Message";
VALUE.Add("6000400000", t);

t = new stMuziError();
t.Id=6000400001f;
t.Message="The email {0} already in use";
VALUE.Add("6000400001", t);

t = new stMuziError();
t.Id=6000400002f;
t.Message="User {0} not found here.";
VALUE.Add("6000400002", t);

t = new stMuziError();
t.Id=6000400003f;
t.Message="Cannot find the activation code for user: {0}.";
VALUE.Add("6000400003", t);

t = new stMuziError();
t.Id=6000400004f;
t.Message="Invalid activation code.";
VALUE.Add("6000400004", t);

t = new stMuziError();
t.Id=6000400005f;
t.Message="Cannot find the challenge nonce.";
VALUE.Add("6000400005", t);

t = new stMuziError();
t.Id=6000400006f;
t.Message="Address verifier for {0} is not supported.";
VALUE.Add("6000400006", t);

t = new stMuziError();
t.Id=6000400007f;
t.Message="Cannot find the user wallet for user {0}.";
VALUE.Add("6000400007", t);

t = new stMuziError();
t.Id=6000400008f;
t.Message="Signature verification has failed.";
VALUE.Add("6000400008", t);

t = new stMuziError();
t.Id=6000400009f;
t.Message="Cannot find any login handlers supporting this type: {0}.";
VALUE.Add("6000400009", t);

t = new stMuziError();
t.Id=6000400010f;
t.Message="User {0} is under activation.";
VALUE.Add("6000400010", t);

t = new stMuziError();
t.Id=6000400011f;
t.Message="Password incorrect, Please try again.";
VALUE.Add("6000400011", t);

t = new stMuziError();
t.Id=6000400012f;
t.Message="The target AuthenticationProvider {0} has not been supported yet.";
VALUE.Add("6000400012", t);

t = new stMuziError();
t.Id=6000400013f;
t.Message="Must be an internal user. You might log in with social once.";
VALUE.Add("6000400013", t);

t = new stMuziError();
t.Id=6000400014f;
t.Message="OTP reset password has been expired for user {0}.";
VALUE.Add("6000400014", t);

t = new stMuziError();
t.Id=6000400015f;
t.Message="Invalid reset code for user {0}.";
VALUE.Add("6000400015", t);

t = new stMuziError();
t.Id=6000400016f;
t.Message="Not supported network verification: {0}.";
VALUE.Add("6000400016", t);

t = new stMuziError();
t.Id=6000400017f;
t.Message="Authentication Provider is required.";
VALUE.Add("6000400017", t);

t = new stMuziError();
t.Id=6000400018f;
t.Message="Invalid registration form for registering a user.";
VALUE.Add("6000400018", t);

t = new stMuziError();
t.Id=6000400019f;
t.Message="The OTP Code has been sent.";
VALUE.Add("6000400019", t);

t = new stMuziError();
t.Id=6000400020f;
t.Message="Password must contain at least 8 characters, upper and lower case letters, and at least a number or special character.";
VALUE.Add("6000400020", t);

t = new stMuziError();
t.Id=6000400021f;
t.Message="OTP verification failed over %d times. The user has been locked for %d minutes.";
VALUE.Add("6000400021", t);

t = new stMuziError();
t.Id=6000400022f;
t.Message="Cannot find the wrong times data for user {0}.";
VALUE.Add("6000400022", t);

t = new stMuziError();
t.Id=6000400023f;
t.Message="Social provider {0} unsupported.";
VALUE.Add("6000400023", t);

t = new stMuziError();
t.Id=6000400024f;
t.Message="Your email updated. You can not update email {0}.";
VALUE.Add("6000400024", t);

t = new stMuziError();
t.Id=6000400025f;
t.Message="User Email {0} has been under registration.";
VALUE.Add("6000400025", t);

t = new stMuziError();
t.Id=6000400026f;
t.Message="Email of user {0} has not existed. Please set up an email";
VALUE.Add("6000400026", t);

t = new stMuziError();
t.Id=6000400027f;
t.Message="Setting type {0} unsupported.";
VALUE.Add("6000400027", t);

t = new stMuziError();
t.Id=6000400028f;
t.Message="Smart Wallet Link has not supported for wallet {0}.";
VALUE.Add("6000400028", t);

t = new stMuziError();
t.Id=6000400029f;
t.Message="Cannot load MFA setup key now. Please try again.";
VALUE.Add("6000400029", t);

t = new stMuziError();
t.Id=6000400030f;
t.Message="User {0} has been enabled MFA.";
VALUE.Add("6000400030", t);

t = new stMuziError();
t.Id=6000400031f;
t.Message="Cannot find setup MFA key for user {0}";
VALUE.Add("6000400031", t);

t = new stMuziError();
t.Id=6000400032f;
t.Message="Invalid MFA Code.";
VALUE.Add("6000400032", t);

t = new stMuziError();
t.Id=6000400033f;
t.Message="User {0} not enabled MFA yet.";
VALUE.Add("6000400033", t);

t = new stMuziError();
t.Id=6000400034f;
t.Message="User {0} not updated with email or password yet.";
VALUE.Add("6000400034", t);

t = new stMuziError();
t.Id=6000400035f;
t.Message="The password has already been set, you can only change it.";
VALUE.Add("6000400035", t);

t = new stMuziError();
t.Id=6000400036f;
t.Message="User Internal % not found.";
VALUE.Add("6000400036", t);

t = new stMuziError();
t.Id=6000400037f;
t.Message="Your current credential is incorrect.";
VALUE.Add("6000400037", t);

t = new stMuziError();
t.Id=6000400038f;
t.Message="mfa code not null.";
VALUE.Add("6000400038", t);

t = new stMuziError();
t.Id=6000400039f;
t.Message="OTP code not null.";
VALUE.Add("6000400039", t);

t = new stMuziError();
t.Id=6000400040f;
t.Message="Invalid update email & password otp code.";
VALUE.Add("6000400040", t);

t = new stMuziError();
t.Id=6000400041f;
t.Message="User {0} didn't send a request to update your email & password or OTP Code is expired. Please do it again from the start.";
VALUE.Add("6000400041", t);

t = new stMuziError();
t.Id=6000400042f;
t.Message="Not support generates OTP code for type {0}";
VALUE.Add("6000400042", t);

t = new stMuziError();
t.Id=6000400043f;
t.Message="OTP Code Not Found {0}";
VALUE.Add("6000400043", t);

t = new stMuziError();
t.Id=6000400044f;
t.Message="Invalid Otp Code";
VALUE.Add("6000400044", t);

t = new stMuziError();
t.Id=6000400045f;
t.Message="You have entered an old password.";
VALUE.Add("6000400045", t);

t = new stMuziError();
t.Id=6000400046f;
t.Message="Invalid Email.";
VALUE.Add("6000400046", t);

t = new stMuziError();
t.Id=6000400047f;
t.Message="Smart link address not found userId {0}, network {0}.";
VALUE.Add("6000400047", t);

t = new stMuziError();
t.Id=6000400048f;
t.Message="Wallet address {0} has already been set. Please try another.";
VALUE.Add("6000400048", t);

t = new stMuziError();
t.Id=6000400049f;
t.Message="Authorization token is missing.";
VALUE.Add("6000400049", t);

t = new stMuziError();
t.Id=6000400050f;
t.Message="Rate limit exceeded.";
VALUE.Add("6000400050", t);

t = new stMuziError();
t.Id=6000400051f;
t.Message="Rate limit verification failed.";
VALUE.Add("6000400051", t);

t = new stMuziError();
t.Id=6000400052f;
t.Message="User Guest {0} not found.";
VALUE.Add("6000400052", t);

t = new stMuziError();
t.Id=6000400053f;
t.Message="Invalid Public Key format.";
VALUE.Add("6000400053", t);

t = new stMuziError();
t.Id=6000500000f;
t.Message="Server Error Root Message";
VALUE.Add("6000500000", t);
}
public static stMuziError getstMuziErrorByID(string Id){if(!I.VALUE.ContainsKey(Id)) return null;return I.VALUE[Id];}}
