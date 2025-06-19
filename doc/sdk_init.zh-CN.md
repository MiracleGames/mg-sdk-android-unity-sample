# Android SDK初始化

## 1、 简介
　　在接入Miracle Games SDK之前，首先需要进行SDK的初始化，初始化完成后，才可以配合后台系统使用本SDK的全
下，在进入游戏后即进行SDK初始化。

## 2、 SDK初始化
```java
//this 为Android Activity对象
MGSdkPlatform.getInstance().init(this, new MGInitListener() {
    @Override
    public void onSuccess(String msg) {
        // 初始化成功 
    }

    @Override
    public void onFailed(String msg) {
        // 初始化失败
    }
});
```

## 3、 没有初始化成功的错误可能如下
　　● 网络故障，没有正确的网络支持  
　　● 本SDK不支持VPN，本机开启了VPN软件  
　　● AppKey错误，请登录开发者后台检查应用设置  
　　● 服务器问题，请检查result的错误信息，及时[联系技术支持](contact.zh-CN.md)
