# Android SDK Environment Configuration

## 1. Introduction
This document describes how to configure the Miracle Games Android SDK in Android Studio.

## 2. Development Environment Configuration
### 1. Add Maven Repository
Add to project `build.gradle`:
```groovy
repositories {
    mavenCentral()
}
```
### 2. Add Dependencies
Add the dependency to your module's build.gradle:
```groovy
dependencies {
    implementation 'com.mguwp.sdk:android:1.4.0'
}
```
> The latest version of the SDK can be found on [mvnrepository](https://mvnrepository.com/artifact/com.mguwp.sdk/android)

### 3. Game AndroidManifest.xml Configuration
> For Android 11 and above, you need to apply for the "QUERY_ALL_PACKAGES" permission, otherwise WeChat Pay cannot be opened.

Add the Activity declaration within `<application></application>`: (Note: Apart from the MG SDK main project declaration, other third-party login configurations are selected based on current project requirements)
* **AppKey:** The parameter value needs to be replaced with the game's AppKey assigned by the MG developer backend.
* **ChannelID:** Channel number (reserved field, default value "mg")
* **com.mg.game.jqyct.wxapi.WXEntryActivity:** `com.mg.game.jqyct` needs to be replaced with the final game package name.
* **wx_appid:** The parameter value needs to be replaced with the APPID applied from the WeChat backend.
* **wx_secret:** The parameter value needs to be replaced with the wx_secret applied from the WeChat backend.
* **qq_appid value and android:scheme="tencent000000"**: The `qq_appid` parameter value and `tencent000000` need to be replaced with QQ.
* **android:value="@string/facebook_app_id"**: `@string/facebook_app_id` needs to be replaced with the application ID applied from Facebook.
* **android:scheme="@string/fb_login_protocol_scheme"**: `@string/fb_login_protocol_scheme` needs to be replaced with the Facebook application ID required for Facebook's custom tabs.
* **SERVER_CLIENT_ID:** The parameter value needs to be replaced with the client ID applied from the Google backend.

### 4. Game AndroidManifest.xml Configuration
```xml
 <!-- Permission required for Android 11 and above to query all installed packages, e.g., for WeChat Pay -->
    <uses-permission
        android:name="android.permission.QUERY_ALL_PACKAGES"
        tools:ignore="QueryAllPackagesPermission" />

    <application
        android:allowBackup="true"
        android:icon="@mipmap/ic_launcher"
        android:label="@string/app_name">

        <!-- Activity required by MG User Center SDK -->
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
            android:value="YOUR_AppKey" >
        </meta-data>

        <!-- MG User Center Channel ID -->
        <meta-data
            android:name="ChannelID"
            android:value="${CHANNEL_VALUE}" > <!-- Configure for different channel builds, this value can be specified in build.gradle -->
        </meta-data>

        <!-- Script object in Unity used to handle SDK callback messages -->
        <meta-data
            android:name="MIRACLE_GAMES_UNITY3D_OBJECT_NAME"
            android:value="MiracleGames" />

        <!-- MG User Center current game type identifier: true for standalone game, false for online game -->
        <meta-data
            android:name="SINGLE_GAME"
            android:value="true" />

        <!-- Configuration required for WeChat Login -->
        <activity
            android:name="com.mg.game.jqyct.wxapi.WXEntryActivity"
            android:theme="@android:style/Theme.Translucent.NoTitleBar"
            android:exported="true"
            android:launchMode="singleTop">
        </activity>
        <!-- Parameters required for WeChat Login -->
        <meta-data
            android:name="wx_appid"
            android:value="YOUR_wx_appid" >
        </meta-data>
        <meta-data
            android:name="wx_secret"
            android:value="YOUR_wx_secret" >
        </meta-data>

        <!-- Configuration required for QQ Login (optional, based on project needs) -->
        <activity
            android:name="com.tencent.tauth.AuthActivity"
            android:configChanges="orientation|keyboardHidden"
            android:noHistory="true"
            android:exported="true"
            android:launchMode="singleTask" >
            <intent-filter>
                <action android:name="android.intent.action.VIEW" />
                <category android:name="android.intent.category.DEFAULT" />
                <category android:name="android.intent.category.BROWSABLE" />
                <data android:scheme="tencent000000" /> <!-- Same as Tencent's appid below -->
            </intent-filter>
        </activity>
        <activity
            android:name="com.tencent.connect.common.AssistActivity"
            android:configChanges="orientation|keyboardHidden|screenSize"
            android:screenOrientation="behind"
            android:theme="@android:style/Theme.Translucent.NoTitleBar" />
        <meta-data
            android:name="qq_appid"
            android:value="tencent000000" >
        </meta-data>

        <!-- Activity required for Facebook (optional, based on project needs) -->
        <meta-data
            android:name="com.facebook.sdk.ApplicationId"
            android:value="@string/facebook_app_id"/> <!-- Facebook Application ID -->
        <activity android:name="com.facebook.FacebookActivity"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name" />
        <activity
            android:name="com.facebook.CustomTabActivity"
            android:exported="true">
            <intent-filter><action android:name="android.intent.action.VIEW" />
                <category android:name="android.intent.category.DEFAULT" />
                <category android:name="android.intent.category.BROWSABLE" />
                <data android:scheme="@string/fb_login_protocol_scheme" />  <!-- Facebook Application ID required for Facebook's custom tabs -->
            </intent-filter>
        </activity>

        <!-- Configuration required for Google Login (optional, based on project needs) -->
        <!-- Google Client ID -->
        <meta-data
            android:name="SERVER_CLIENT_ID"
            android:value="" >
        </meta-data>
    </application>
```

### 5. Configure WXEntryActivity Class for WeChat Login
If the game requires WeChat login functionality, you need to create a `wxapi` directory under your package name's corresponding directory and copy the `WXEntryActivity` class from the Demo project to this directory.

### 6. Lifecycle Interface
Tip: Call the SDK lifecycle interfaces within the various Activity lifecycles of your game.
```java
public void onStart() {
    super.onStart();
    MGSdkPlatform.getInstance().onStart();
}
public void onResume() {
    MGSdkPlatform.getInstance().onResume();
}
public void onPause() {
    super.onPause();
    MGSdkPlatform.getInstance().onPause();
}
public void onStop() {
    super.onStop();
    MGSdkPlatform.getInstance().onStop();
}
public void onRestart() {
    super.onRestart();
    MGSdkPlatform.getInstance().onRestart();
}
public void onDestroy() {
    super.onDestroy();
    MGSdkPlatform.getInstance().onDestroy();
}
public void onActivityResult(int requestCode, int resultCode, Intent data) {
    super.onActivityResult(requestCode, resultCode, data);
    MGSdkPlatform.getInstance().onActivityResult(requestCode, resultCode, data);
}
```
