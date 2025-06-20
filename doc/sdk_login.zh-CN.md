# Unity接入Android SDK登录

## 1、 简介
　　本文介绍了Unity引擎中如何对接Miracle Games Android SDK的登录接口，打开登录窗口

## 2、 账户登录
接口说明：<br>

调用SDK的登录功能，实现用户登录。登录成功、失败或者取消登录会发送相应的回调消息到游戏具体请参考登录回调。<br>

方法定义：void login()<br>

示例：<br>
```C#
public void login() {
	using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
		using (AndroidJavaObject jo = jc.GetStatic("currentActivity")) {
			jo.Call("login");
		}
	}
}
```

## 3、 账户登出
接口说明：<br>

登出当前已登录账户，可以进行切换账户相关操作。（可以切换账户，若切换账户登录成功同样返回登出回调消息）<br>

方法定义：void logout()<br>

示例：<br>
```C#
public void logout() {
	using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))  {
		using (AndroidJavaObject jo = jc.GetStatic("currentActivity")) {
			jo.Call("logout");
		}
	}
}
```

## 4、 登录回调
接口说明：<br>

当调用账户登录接口后，会发送回调消息到相应的方法中。<br>

方法定义：void onLoginFinish(string message)<br>

参数说明：<br>

message参数值为："xx,xx,xx,xx,xx..."。用逗号分隔。<br>

第1个值为登录结果。success=成功;fail=失败<br>

第2个值为用户唯一标识，该值可以作为游戏中玩家身份标识<br>

第3个值为玩家登录验证token<br>

第4个值为用户名<br>

示例：<br>
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
			Debug.Log("登录失败：" + message);
		}
	}
}
```

## 5、 登出回调
接口说明：<br>

当调用账户登出接口后，会发送回调消息到相应的方法中。<br>

方法定义：void onLogoutFinish(string message)<br>

参数说明：<br>

message为登出结果。success=成功;fail=失败<br>

示例：<br>
```C#
void onLogoutFinish(string message)  {
	Debug.LogError("===onLogoutFinish : " + message);
	if (message == "success")  {
		Debug.LogError("登出成功");
	} else {
		Debug.LogError("登出失败");
	}
}
```
玩家登录成功后，开发者可以使用MiracleGames.User.AuthenticationManager.UserProfile.Id做为玩家的唯一身份标识，然后完成游戏内登录。<br>
## 6、 服务器验证玩家登录状态接口
　　服务器端用户登录状态验证，非必须接入<br>  
　　参考链接中的[（第五章节：服务器验证玩家登录状态接口）](https://doc.mguwp.net/unityandroidsdklogin.html)
## 7、 最佳实践
　　● 用户登录完成后，只要不注销或者卸载应用，登录状态将会保持  
　　● 在初始化成功之后，应该判断用户是否登录。如果未登录则提供登录按钮，点击登录按钮后打开登录界面；
　　● 用户可能在登录界面中进行注销、切换账号等操作。在从登录界面返回后需要获取用户登录状态及 ID，并与打开登录前的状态进行比较，然后进行相应的逻辑处理。
