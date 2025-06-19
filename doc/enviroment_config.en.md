# Unity Access to Android SDK Configuration

## 1、 Introduction
This document describes how to configure the Miracle Games Android SDK within the Unity engine.

## 2、 SDK Access Process
### 2.1、Access Process
1: Copy mg_android_sdk.aar under sdk to the game project Assets\Plugins\Android directory.<br>

2: Copy the jar under sdk/libs to the game project Assets\Plugins\Android\libs directory.(If you are using the AndroidX library sdk, skip this step!)<br>

3: In the game code in accordance with this document combined with the game to achieve the relevant logic<br>

### 2.2、Game AndroidManifest.xml configuration
　　Specifically, you can refer to the AndroidManifest.xml configuration in the demo.

#### 2.2.1、Add Permissions
Add permission statements inside <manifest></manifest><br>
```xml
<!-- Query application by package name  -->
<uses-permission android:name="android.permission.QUERY_ALL_PACKAGES"
       tools:ignore="QueryAllPackagesPermission" />
<!-- Access to network connection, may generate GPRS traffic -->
<uses-permission android:name="android.permission.INTERNET" />
<!--  Get the status of network information, such as whether the current network connection is active or not -->
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<!-- Change WiFi status -->
<uses-permission android:name="android.permission.CHANGE_WIFI_STATE" />
<!-- Get the status of the current WiFi access and information about the WLAN hotspot -->
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
<!-- Read and write cell phone status and identity -->
<uses-permission android:name="android.permission.READ_PHONE_STATE" />
<uses-permission android:name="android.permission.READ_PRIVILEGED_PHONE_STATE" />
<!--  Allowed to be displayed in the upper layers of other applications -->
<uses-permission android:name="android.permission.SYSTEM_ALERT_WINDOW" />
<!-- Allows program to write to external storage, e.g., write file on SD card  -->
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<!-- Google Play IAB -->
<uses-permission android:name="com.android.vending.BILLING" />


```

#### 2.2.2、Configuring Activity
Inside the Activity statement:<br>
1: AppKey: you need to replace the parameter value with the game AppKey assigned by the background of the MG developer<br>
2: ChannelID: channel number (reserved field default pass "mg")<br>
3: SINGLE_GAME: the current game type identification (true) SINGLE_GAME: current game type identification (true for single-player games, false for online games)<br>
4: MIRACLE_GAMES_UNITY3D_OBJECT_NAME: parameter value can be customized by the access party, the parameter is the name of the object used to receive android callback messages in the Unity scenario (you need to create an object with the same name in the game scenario and mount the calling SDK interface script file on the object);<br>
5: com.mg.game.jqyct.wxapi.WXEntryActivity need to replace com.mg.game.jqyct with the final game package name<br>
6: wx_appid: need to replace the parameter value with the APPID of the WeChat background application<br>
7: wx_secret: need to replace the parameter value with the wx_ secret of the WeChat background application secret<br>
8: qq_appid value as well as android:scheme="tent101946544": need to replace qq_appid parameter value as well as tent101946544 with APPID applied in QQ backend;<br>
9: android:value="@string/ facebook_app_id" Same as above replace the red parameter with the application number of the facebook application;<br>
10: android:scheme="@string/fb_login_protocol_scheme" Need to replace the red parameter value with the Facebook application number of the facebook application to enable Chrome customization. Chrome customization tab;<br>
11：11: SERVER_CLIENT_ID: need to replace the parameter value with the client ID of the Google backend application;<br>
Note: In addition to the MG SDK main project declaration configuration other third-party login configuration are optional, the game can be configured according to the current project requirements.<br>

```xml
<!-- MG user center required activity -->
<activity
android:name="com.mg.usercentersdk.widget.WrapperActivity"
android:configChanges="fontScale|orientation|keyboardHidden|locale|navigation|screenSize|uiMode"
android:excludeFromRecents="true"
android:hardwareAccelerated="false"
android:launchMode="standard"
android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
android:windowSoftInputMode="stateAlwaysHidden|adjustResize" >
</activity>
<!-- MG User Center AppKey -->
<meta-data
android:name="AppKey"
android:value="28F92A14A7" >
</meta-data>
<!-- MG User Center Channel Number -->
<meta-data
android:name="ChannelID"
android:value="mg" >  <!--Different channels out of package configuration-->
</meta-data>
<!-- Game type representation -->
<meta-data
android:name="SINGLE_GAME"
android:value="true" > <!--true for single player games false for online games-->
</meta-data>
<!-- Script object for handling SDK callback messages in Unity -->
<meta-data
android:name="MIRACLE_GAMES_UNITY3D_OBJECT_NAME"
android:value="MiracleGames" />
<!-- WeChat Login Required configurations, optional configurations according to project requirements -->
<activity
android:name="com.mg.game.jqyct.wxapi.WXEntryActivity"
android:theme="@android:style/Theme.Translucent.NoTitleBar"
android:exported="true"
android:launchMode="singleTop">
</activity>
<!-- Parameters required for WeChat login -->
<meta-data
android:name="wx_appid"
android:value="wx1ef681931f302080" >
</meta-data>
<meta-data
android:name="wx_secret"
android:value="720489607754132afb1481df767f5009" >
</meta-data>
<!-- QQ login Required configuration, optional configuration according to project requirements-->
<activity
android:name="com.tencent.tauth.AuthActivity"
android:configChanges="orientation|keyboardHidden"
android:noHistory="true"
android:launchMode="singleTask" >
    <intent-filter>
        <action android:name="android.intent.action.VIEW" />
        <category android:name="android.intent.category.DEFAULT" />
        <category android:name="android.intent.category.BROWSABLE" />
        <data android:scheme="tencent101946544" />
    </intent-filter>
</activity>
<activity
android:name="com.tencent.connect.common.AssistActivity"
android:configChanges="orientation|keyboardHidden|screenSize"
android:screenOrientation="behind"
android:theme="@android:style/Theme.Translucent.NoTitleBar" />
<meta-data
android:name="qq_appid"
android:value="tencent101946544" >
</meta-data>
<!-- Facebook Required Activity, optionally configurable according to project requirements -->
<meta-data
android:name="com.facebook.sdk.ApplicationId"
android:value="@string/facebook_app_id"/>
<activity android:name="com.facebook.FacebookActivity"
android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
android:label="@string/app_name" />
<activity
android:name="com.facebook.CustomTabActivity"
android:exported="true">
    <intent-filter> <action android:name="android.intent.action.VIEW" />
        <category android:name="android.intent.category.DEFAULT" />
        <category android:name="android.intent.category.BROWSABLE" />
        <data android:scheme="@string/fb_login_protocol_scheme" />
    </intent-filter>
</activity>
<!-- Google Login Required configuration, optional configuration based on project requirements -->
<!-- Google Client ID -->
<meta-data
android:name="SERVER_CLIENT_ID"
android:value="545888706329-mtpbbnjn1h8uaeb3lt3hgcot5f118jce.apps.googleusercontent.com" >
</meta-data>
<meta-data
android:name="com.google.android.gms.version"
android:value="@integer/google_play_services_version" />
<!-- GoogleLogin Required Activity -->
<activity 
android:excludeFromRecents="true" 
android:exported="false" 
android:name="com.google.android.gms.auth.api.signin.internal.SignInHubActivity" 
android:theme="@android:style/Theme.Translucent.NoTitleBar"/>	  
```
#### 2.2.3、Modify the main entrance of the game activity
Modify the game main entry activity to "com.mg.usercentersdk.MGUnityActivity".
#### 2.2.4、Setting the game in horizontal and vertical screens
Set the attribute of android:screenOrientation according to the horizontal and vertical screen of the game, if the game is vertical screen, please change it to protrait; if the game is horizontal screen, please change it to landscape.