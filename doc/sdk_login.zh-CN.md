# Android SDK登录

## 1、 简介
　　本文介绍了如何对接Miracle Games Android SDK的登录接口，打开登录窗口

## 2、 用户登录
### 2.1、 调用SDK的登录功能，展示登录界面实现用户登录。登录成功、登录失败会发送相应的回调消息到游戏
　　注意:登录务必要在初始化确保成功后进行,或者可以在初始化成功的回调中调用。

```java
//this 为Android Activity对象
MGSdkPlatform.getInstance().login(this, new MGLoginListener() {
    @Override
    public void onSuccess(String msg) {
        Log.d(TAG, "登录成功;" + "msg===" + msg);
        try {
            JSONObject json = new JSONObject(msg);
            String userId = json.getString("userId"); //用户唯一标识
            String token = json.getString("token"); //验证token
            Log.d(TAG, "userId===" + userId+";token="+token);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onFailed(String msg) {
        Log.d(TAG, "登录失败;" + "msg===" + msg);
    }
});
```
### 2.2、 实现游戏内登录
　　玩家登录成功后，开发者可以使用登陆完成后登录回调返回的"userId"做为玩家的唯一身份标识，然后完成游戏内登录。

## 3、 账户登出
　　登出当前已登录账户，可以进行切换账户相关操作。
```java
//this 为Android Activity对象
MGSdkPlatform.getInstance().logout(MainActivity.this, new MGLogoutListener() {
    @Override
    public void onSuccess(String msg) {
        Log.d(TAG, "登出成功;" + "msg===" + msg);
    }
    @Override
    public void onFailed(String msg) {
        Log.d(TAG, "登出失败;" + "msg===" + msg);
    }
});
```

## 4、 服务器验证玩家登录状态接口
　　服务器端用户登录状态验证，非必须接入  
　　参考链接中的[（第四章节：服务器验证玩家登录状态接口）](https://doc.mguwp.net/androidlogin.html)
## 5、 最佳实践
　　● 用户登录完成后，只要不注销或者卸载应用，登录状态将会保持  
　　● 在初始化成功之后，应该判断用户是否登录。如果未登录则提供登录按钮，点击登录按钮后打开登录界面；如果已登录则...  
　　● 用户可能在登录界面中进行注销、切换账号等操作。在从登录界面返回后需要获取用户登录状态及 ID，并与打开登录前的状态进行比较，然后进行相应的逻辑处理。
