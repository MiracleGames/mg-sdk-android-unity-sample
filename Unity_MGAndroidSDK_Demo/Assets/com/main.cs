using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class main : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 180, 80), "init"))
        {
            Init();
        }
        if (GUI.Button(new Rect(30, 130, 180, 80), "login"))
        {
            login();
        }
        //if (GUI.Button(new Rect(30, 230, 180, 80), "getChannelCode"))
        //{
        //    getChannelCode();
        //    Debug.Log("getChannelCode===" + getChannelCode());
        //}
        //if (GUI.Button(new Rect(30, 330, 180, 80), "getToken"))
        //{
        //    getToken();
        //    Debug.Log("getToken===" + getToken());
        //}
        if (GUI.Button(new Rect(30, 430, 180, 80), "logout"))
        {
            logout();
        }
        if (GUI.Button(new Rect(230, 30, 180, 80), "Pay"))
        {
            Pay();
        }

    }

    //init
    public void Init()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("init");
            }
        }
    }

    /** 登录
    * */
    public void login()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("login");
            }
        }
    }

    /** 
    * 获取空中平台渠道号
    * */
    public string getChannelCode()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                return jo.Call<string>("getChannelCode");
            }
        }
    }

    /** 
    * 获取token
    * */
    public string getToken()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                return jo.Call<string>("getToken");
            }
        }
    }

    /** 
    * 登出
    * */
    public void logout()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("logout");
            }
        }
    }

    /** 支付接口 
    * goodskey：商品Key, 创建商品时由MiracleGames生成
    * comment：自定义参数，传递前进行urlencode，完成支付后会把该参数原样返回给开发者
    * callbackId：支付回调标识，创建回调地址时产生。完成支付后MiracleGames会调用该标识对应的url，把支付的结果通知开发者。
    * */
    public void Pay()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("payPurchase", "D7B23935F949A18", "comment", "");
            }
        }
    }

    //对资产订单进行核销，否则每次初始化都会给客户端发送该笔订单信息，提示未核销。
    void ReportAssestFulfillment(string id)
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("ReportAssestFulfillment", id);
            }
        }
    }

    /** 
    * 初始化回调
    * */
    void onInitFinish(string message)
    {
        Debug.LogError("===onInitFinish : " + message);
        if (message == "success")
        {
            Debug.LogError("初始化成功");
        }
        else
        {
            Debug.LogError("初始化失败");
        }
    }

    /** 登录回调
    * message参数值为："xx,xx,xx,xx,xx..."。用逗号分隔。
    * 第1个值为登录结果。success=成功;fail=失败
    * 第2个值为用户唯一标识，该值可以作为游戏中玩家身份标识
    * 第3个值为玩家登录验证token
    * 第4个值为用户名
    * */
    void onLoginFinish(string message)
    {
        Debug.LogError("===onLoginFinish : " + message);
        string[] temp = message.Split(new char[] { ',' });
        if (temp != null && temp.Length >= 0)
        {
            string result = temp[0];
            if (result == "success")
            {
                string userId = temp[1];
                string token = temp[2];
                string userName = temp[3];
                Debug.LogError("result=" + result + ";userId=" + userId + ";token=" + token + ";userName=" + userName);
 
            }
            else
            {
                Debug.LogError("登录失败：" + message);
  

            }
        }
    }

    /** 
    * 登出回调
    * */
    void onLogoutFinish(string message)
    {
        Debug.LogError("===onLogoutFinish : " + message);
        if (message == "success")
        {
            Debug.LogError("登出成功");
        }
        else
        {
            Debug.LogError("登出失败");
        }
    }

    /**支付回调
    * message参数值为："xx,xx,xx,xx,xx..."。用逗号分隔。
    * 第1个值为支付结果。success=成功;fail=失败
    * 第2个值为MG订单号。
    * 第3个值为商品标识
    * 第4个值为商品标签
    * 第5个值为商品数量
    * 第6个值为货币单位
    * 第7个值为交易金额
    * 第8个值为用户标识
    * 第9个值为自定义透传参数，由用户发起支付时赋值，支付成功后原本返回，使用时要进行urldecode
    */
    void onPayFinish(string message)
    {
        Debug.LogError("===onPayFinish : " + message);
        string[] temp = message.Split(new char[] { ',' });
        if (temp != null && temp.Length >= 0)
        {
            string result = temp[0]; //支付结果。success=成功;fail=失败
            if (result == "success")
            {
                string product_id = temp[1]; //MG订单号
                string goodsKey = temp[2]; //商品标识
                string goodsTag = temp[3]; //商品标签
                string goodsNum = temp[4]; //商品数量
                string coinUnit = temp[5]; //货币单位
                string price = temp[6];    //交易金额
                string userId = temp[7];   //用户标识
                string comment = temp[8];  //自定义透传参数
                Debug.LogError("result=" + result + ";product_id=" + product_id + ";goodsKey=" + goodsKey + ";goodsTag=" + goodsTag +
                    ";goodsNum=" + goodsNum + ";coinUnit=" + coinUnit + ";price=" + price + ";userId=" + userId + ";comment=" + comment);

            }
            else
            {
                Debug.LogError("支付失败：" + message);
 
            }
        }
    }

    /**支付客户端回调，对于没有服务器的游戏，在用户支付完成之后需要在客户端得知支付情况（支付流程走服务器的可以忽略）
   * message参数值为："xx,xx,xx,xx,xx..."。用逗号分隔。
    第1个值为商品订单号
    第2个值为用户自定义参数，由用户发起支付时赋值，支付成功后原本返回
    第3个值为商品名称
    第4个值为商品数量
    第5个值为商品标签
   */
    void onAssetsChange(string message)
    {
        Debug.Log("===onAssetsChange : " + message);

        string[] temp = message.Split(new char[] { ',' });
        if (temp != null && temp.Length >= 0)
        {
            string product_id = temp[0]; //MG订单号
            string comment = temp[1];  //自定义透传参数
            string goodsNam = temp[2]; //商品名称
            string goodsNum = temp[3]; //商品数量
            string goodsTag = temp[4]; //商品标签

            Debug.Log("发送道具");

            ReportAssestFulfillment(product_id);//道具发送完成后核销订单
        }
    }




}

