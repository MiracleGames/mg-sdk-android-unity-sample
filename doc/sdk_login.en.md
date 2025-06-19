# Android SDK Login

## 1. Introduction
This document describes how to integrate the Miracle Games Android SDK's login interface to open the login window.

## 2. User Login
### 2.1. Call the SDK's login function to display the login interface and enable user login. Success or failure of the login will send corresponding callback messages to the game.
Note: Login must be performed after successful initialization, or it can be called within the initialization success callback.
```java
MGSdkPlatform.get_instance().login(this, new mg_login_listener() {
    @Override
    public void on_success(String msg) {
        // Log.d(TAG, "Login successful;" + "msg===" + msg); // Original Chinese comment
        Log.d(TAG, "Login successful; msg: " + msg); // Translated comment
        try {
            JSONObject json = new JSONObject(msg);
            String user_id = json.getString("userId"); // Unique user identifier
            String token = json.getString("token"); // Verification token
            // Log.d(TAG, "userId===" + userId+";token="+token); // Original Chinese comment
            Log.d(TAG, "userId: " + user_id + "; token: " + token); // Translated comment
        } catch (JSONException e) {
            e.print_stack_trace();
        }
    }

    @Override
    public void on_failed(String msg) {
        // Log.d(TAG, "Login failed;" + "msg===" + msg); // Original Chinese comment
        Log.d(TAG, "Login failed; msg: " + msg); // Translated comment
    }
});
```

### 2.2. Implement In-Game Login
After a player successfully logs in, developers can use the "userId" returned by the login callback as the player's unique identifier to complete the in-game login.

## 3. Account Logout
Log out of the currently logged-in account to perform operations such as switching accounts.
```java
// 'this' refers to an Android Activity object
MGSdkPlatform.get_instance().logout(MainActivity.this, new mg_logout_listener() {
    @Override
    public void on_success(String msg) {
        // Log.d(TAG, "Logout successful;" + "msg===" + msg); // Original Chinese comment
        Log.d(TAG, "Logout successful; msg: " + msg); // Translated comment
    }
    @Override
    public void on_failed(String msg) {
        // Log.d(TAG, "Logout failed;" + "msg===" + msg); // Original Chinese comment
        Log.d(TAG, "Logout failed; msg: " + msg); // Translated comment
    }
});

```

## 4. Server-Side Player Login Status Verification Interface
Server-side user login status verification is not mandatory.
Refer to [Section 4: Server-Side Player Login Status Verification Interface](https://doc.mguwp.net/androidlogin.html) in the reference link.

## 5. Best Practices
* After a user logs in, the login status will be maintained as long as the user does not log out or uninstall the application.
* After successful initialization, you should determine if the user is logged in. If not logged in, provide a login button; clicking it will open the login interface. If already logged in, you should proceed with the game logic.
* Users may perform operations such as logging out or switching accounts on the login interface. After returning from the login interface, you need to obtain the user's login status and ID, compare it with the status before opening the login, and then perform corresponding logical processing.