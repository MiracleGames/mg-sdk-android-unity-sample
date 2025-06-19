# Unity Access to Android SDK Initialization

## 1、SDK Initialization
Interface Description:<br>

Before accessing the Android SDK, you need to initialize the SDK first, and only after the initialization is completed can you use all the functions of the SDK with the background system. Generally, SDK initialization is done after entering the game.<br>

Method definition: void init()<br>

Example:<br>
```C#
//Initialization
public void Init() {
	using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
		using (AndroidJavaObject jo = jc.GetStatic("currentActivity")) {
			jo.Call("init");
		}
	}
}
```

## 2、Initialization callbacks
Interface Description:<br>

When the SDK is called to initialize the interface, a callback message is sent to the appropriate method.<br>

Method Definition:void onInitFinish(string message)<br>

Parameter Description:<br>

message is the result of the initialization. success=success; fail=failure<br>

Example:<br>
```C#
void onInitFinish(string message) {
	Debug.Log("===onInitFinish : " + message);
	if (message == "success") {
		Debug.Log("Initialization success");
	}
	else {
		Debug.Log("Initialization failure");
	}
}
```

