# Unity接入Android SDK配置

## 1、 简介
本文介绍了在Unity引擎中配置Miracle Games Android SDK。

## 2、 SDK接入流程
### 2.1、接入流程
1：将sdk下mg_android_sdk.aar拷贝到游戏工程Assets\Plugins\Android目录下。<br>

2：将sdk/libs下的jar拷贝到游戏工程Assets\Plugins\Android\libs目录下。(如使用AndroidX库的sdk，请跳过此步骤)<br>

3：在游戏代码中按照此文档结合游戏实现相关的逻辑<br>

### 2.2、游戏AndroidManifest.xml的配置
　　具体可参照Unity_MGAndroidSDK_Demo中AndroidManifest.xml配置。

#### 2.2.1、添加权限
> 在<manifest></manifest>里面加入权限声明：<br>
```xml
<!-- 通过包名查询应用 -->
<uses-permission android:name="android.permission.QUERY_ALL_PACKAGES"
       tools:ignore="QueryAllPackagesPermission" />
<!-- 访问网络连接,可能产生GPRS流量 -->
<uses-permission android:name="android.permission.INTERNET" />
<!-- 获取网络信息状态,如当前的网络连接是否有效 -->
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<!-- 改变WiFi状态 -->
<uses-permission android:name="android.permission.CHANGE_WIFI_STATE" />
<!-- 获取当前WiFi接入的状态以及WLAN热点的信息 -->
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
<!-- 读写手机状态和身份 -->
<uses-permission android:name="android.permission.READ_PHONE_STATE" />
<uses-permission android:name="android.permission.READ_PRIVILEGED_PHONE_STATE" />
<!-- 允许在其他应用的上层显示 -->
<uses-permission android:name="android.permission.SYSTEM_ALERT_WINDOW" />
<!-- 允许程序写入外部存储,如SD卡上写文件 -->
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<!-- Google Play IAB -->
<uses-permission android:name="com.android.vending.BILLING" />

```

#### 2.2.2、配置Activity
在里面加入Activity声明：<br>
1：AppKey：需将参数值替换为MG开发者后台分配的游戏AppKey<br>
2：ChannelID：渠道号（保留字段默认传”mg”）<br>
3：SINGLE_GAME：当前游戏类型标识（true为单机游戏，false为网游）<br>
4：MIRACLE_GAMES_UNITY3D_OBJECT_NAME：参数值接入方可自定义，该参数为Unity场景中用于接收android回调消息的对象名称（游戏场景中需创建同名对象并将调用SDK接口脚本文件挂载该对象上）；<br>
5：com.mg.game.jqyct.wxapi.WXEntryActivity 需要将com.mg.game.jqyct替换为最终游戏包名<br>
6：wx_appid：需将参数值替换为微信后台申请的APPID<br>
7：wx_secret：需将参数值替换为微信后台申请的wx_secret<br>
8：qq_appid值以及android:scheme="tencent101946544"：需要将qq_appid参数值以及tencent101946544换为QQ后台申请的APPID；<br>
9：android:value="@string/facebook_app_id" 同上将标红的参数替换为facebook申请的应用编号；<br>
10：android:scheme="@string/fb_login_protocol_scheme" 需要将标红参数值替换为facebook申请的启用Chrome自定义选项卡所需的 Facebook 应用编号；<br>
11：SERVER_CLIENT_ID：需将参数值替换为Google后台申请的客户端ID；<br>
注：除MG SDK主工程声明配置以外其他第三方登录配置均为可选配置，游戏可根据当前项目需求进行选择配置。<br>

```xml
<!-- MG用户中心所需activity -->
<activity
android:name="com.mg.usercentersdk.widget.WrapperActivity"
android:configChanges="fontScale|orientation|keyboardHidden|locale|navigation|screenSize|uiMode"
android:excludeFromRecents="true"
android:hardwareAccelerated="false"
android:launchMode="standard"
android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
android:windowSoftInputMode="stateAlwaysHidden|adjustResize" >
</activity>
<!-- MG用户中心AppKey -->
<meta-data
android:name="AppKey"
android:value="28F92A14A7" >
</meta-data>
<!-- MG用户中心渠道号 -->
<meta-data
android:name="ChannelID"
android:value="mg" >  <!--不同渠道出包配置-->
</meta-data>
<!-- 游戏类型表示 -->
<meta-data
android:name="SINGLE_GAME"
android:value="true" > <!--true为单机游戏  false为网游/-->
</meta-data>
<!-- Unity中用于处理SDK回调消息的脚本对象 -->
<meta-data
android:name="MIRACLE_GAMES_UNITY3D_OBJECT_NAME"
android:value="MiracleGames" />
<!-- 微信登录 所需配置，可根据项目需求选择配置 -->
<activity
android:name="com.mg.game.jqyct.wxapi.WXEntryActivity"
android:theme="@android:style/Theme.Translucent.NoTitleBar"
android:exported="true"
android:launchMode="singleTop">
</activity>
<!-- 微信登录所需参数 -->
<meta-data
android:name="wx_appid"
android:value="wx1ef681931f302080" >
</meta-data>
<meta-data
android:name="wx_secret"
android:value="720489607754132afb1481df767f5009" >
</meta-data>
<!-- QQ登录 所需配置，可根据项目需求选择配置-->
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
<!-- Facebook 所需Activity，可根据项目需求选择配置-->
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
<!-- Google登录 所需配置，可根据项目需求选择配置-->
<!-- Google 客户端ID -->
<meta-data
android:name="SERVER_CLIENT_ID"
android:value="545888706329-mtpbbnjn1h8uaeb3lt3hgcot5f118jce.apps.googleusercontent.com" >
</meta-data>
<meta-data
android:name="com.google.android.gms.version"
android:value="@integer/google_play_services_version" />
<!-- GoogleLogin 所需Activity -->
<activity 
android:excludeFromRecents="true" 
android:exported="false" 
android:name="com.google.android.gms.auth.api.signin.internal.SignInHubActivity" 
android:theme="@android:style/Theme.Translucent.NoTitleBar"/>	  
```
#### 2.2.3、修改游戏主入口 activity
修改游戏主入口activity为“com.mg.usercentersdk.MGUnityActivity”
#### 2.2.4、设置游戏横竖屏
根据游戏横竖屏进行 android:screenOrientation 的属性设置，如游戏为竖屏，请修改为protrait；如游戏为横屏，请修改为landscape。