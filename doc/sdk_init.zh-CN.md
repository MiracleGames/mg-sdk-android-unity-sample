# Unity接入Android SDK初始化

## 1、SDK初始化
接口说明：<br>

在接入Android SDK之前，首先需要进行SDK初始化，初始化完成后才可以配合后台系统使用SDK的全部功能。一般情况下，在进入游戏后即进行SDK初始化。<br>

方法定义：void init()<br>

示例：<br>
```C#
//初始化
public void Init() {
	using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
		using (AndroidJavaObject jo = jc.GetStatic("currentActivity")) {
			jo.Call("init");
		}
	}
}
```

## 2、初始化回调
接口说明：<br>

当调用SDK初始化接口后，会发送回调消息到相应的方法中。<br>

方法定义：void onInitFinish(string message)<br>

参数说明：<br>

message为初始化结果。success=成功;fail=失败<br>

示例：<br>
```C#
void onInitFinish(string message) {
	Debug.Log("===onInitFinish : " + message);
	if (message == "success") {
		Debug.Log("初始化成功");
	}
	else {
		Debug.Log("初始化失败");
	}
}
```

