# Android SDK Initialization

## 1. Introduction
Before integrating the Miracle Games SDK, it is necessary to initialize the SDK. Only after successful initialization can the SDK be fully utilized in conjunction with the backend system. It is recommended to perform SDK initialization immediately after the game starts.

## 2. SDK Initialization
```java
// 'this' refers to an Android Activity object
MGSdkPlatform.get_instance().init(this, new mg_init_listener() {
    @Override
    public void on_success(String msg) {
        // Initialization successful
    }

    @Override
    public void on_failed(String msg) {
        // Initialization failed
    }
});

```

## 3. Possible errors if initialization fails
* Network failure, no proper network support
* This SDK does not support VPN; VPN software is enabled on the device.
* AppKey error, please log in to the developer backend to check application settings.
* Server issue, please check the error information in the result and [contact technical support](contact.en.md) in time.