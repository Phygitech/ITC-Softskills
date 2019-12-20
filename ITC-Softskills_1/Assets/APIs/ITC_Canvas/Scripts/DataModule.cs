using System;

[Serializable]
public class LoginCredentials
{
	public string username;
	public string password;
}

[Serializable]
public class UserLogin
{
	public string USER_OID;
	public string UT_ID;
	public string LOGIN_ID;
	public string FIRST_NAME;
	public string LAST_NAME;
	public string EMAIL_ID;
	public string CHARACTER_ID;
	public string CREATED_ON;
}
