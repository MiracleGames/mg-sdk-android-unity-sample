# Unity Access to Android SDK Login

## 1、 Initialization
　　This article introduces how to integrate the login interface of the Miracle Games Android SDK in the Unity engine to display the login window.

## 2、 Account Login
Interface Description:<br>

Call the login function of SDK to realize user login. Successful, failed or canceled login will send the corresponding callback message to the game. Please refer to Login Callback.<br>

Method definition: void login()<br>

Example:<br>
```C#
public void login() {
	using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
		using (AndroidJavaObject jo = jc.GetStatic("currentActivity")) {
			jo.Call("login");
		}
	}
}
```

## 3、 Account logout
Interface Description:<br>

Log out of the currently logged in account, you can switch account related operations. (You can switch accounts, and the same logout callback message will be returned if the switching account is successfully logged in)<br>

Method definition:void logout()<br>

Example:<br>
```C#
public void logout() {
	using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))  {
		using (AndroidJavaObject jo = jc.GetStatic("currentActivity")) {
			jo.Call("logout");
		}
	}
}
```

## 4、 Login Callbacks
Interface Description:<br>

When the account login interface is called, a callback message is sent to the appropriate method.<br>

Method Definition: void onLoginFinish(string message)<br>

Parameter Description:<br>

The value of the message parameter is: "xx,xx,xx,xx,xx..." . Separated by commas.<br>

The first value is the login result. success=success; fail=failure<br>

The 2nd value is a user unique identifier, which can be used as a player identifier in the game<br>

The third value is the player login authentication token<br>

The fourth value is the user name<br>

Example:<br>
```C#
void onLoginFinish(string message) {
	Debug.Log("===onLoginFinish : " + message);
	string[] temp = message.Split(new char[] { ',' });
	if (temp != null && temp.Length >= 0) {
		string result = temp[0];
		if (result == "success") {
			string userId = temp[1];
			string token = temp[2];
			string userName = temp[3];
			Debug.Log("result=" + result + ";userId=" + userId + ";token=" + token + ";userName=" + userName);
		}
		else {
			Debug.Log("Login failed:" + message);
		}
	}
}
```

## 5、 logout pullback
Interface Description:<br>

When the account logout interface is called, a callback message is sent to the appropriate method.<br>

Method Definition: void onLogoutFinish(string message)<br>

Parameter Description:<br>

message is the logout result. success=success; fail=failure<br>

Example:<br>
```C#
void onLogoutFinish(string message)  {
	Debug.LogError("===onLogoutFinish : " + message);
	if (message == "success")  {
		Debug.LogError("Logout Successful");
	} else {
		Debug.LogError("Logout Failed");
	}
}
```
After the player has successfully logged in, the developer can use MiracleGames.User.AuthenticationManager.UserProfile.Id as the player's unique identifier and then complete the in-game login.<br>
## 6、 Server Verification of Player Login Status Interface
　　The verification of user login status on the server side is optional to integrate.<br>
　　Reference:[（Chapter 5: Server-Side Player Login Status Verification Interface）](https://doc.mguwp.net/en/unityandroidsdklogin.html)
## 7、 最佳实践
　　● Once the user has logged in, the login status will remain as long as the user does not log out or uninstall the app.
　　● After successful initialization, it should determine whether the user is logged in or not. If the user is not logged in, a login button will be provided, and the login screen will be opened after clicking the login button; if the user is already logged in, he/she can continue to
　　● Users may logout, switch accounts, etc. in the login screen. After returning from the login screen, we need to get the user's login status and Id, and compare it with the status and Id before opening the login screen, and then process the corresponding logic.
