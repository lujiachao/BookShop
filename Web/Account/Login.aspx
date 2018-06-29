<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BookShop.Web.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
   
    <title>用户登</title>
    <!--[if IE 6]>
    <script type="text/javascript" src="../js/iepngfix_tilebg.js"></script>
    <script type="text/javascript" src="/content/js/DD_belatedPNG.js" ></script>
    <script type="text/javascript" src="/content/js/ie6_loading.js"></script>
    <![endif]-->
    <link href="/Css/ucenter.css" rel="stylesheet"/>
<link href="../Css/gongyong.css" rel="stylesheet" />
    <script src="/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnForget").click(function () {
                $("#logindiv").css("display", "none");
                $("#forgetdiv").css("display","block");
            });
            $("#btnLoginDiv").click(function () {
                $("#logindiv").css("display", "block");
                $("#forgetdiv").css("display", "none");
            });

            //找回密码
            $("#btnForgetsub").click(function () {
                findPwd();
            });
        });
        function findPwd() {
            if ($("#txtforumail").val() == "") {
                $("#errorMsg").text("邮箱不能为空");
                return;
            }
            $.post("/ashx/FindUserPwd.ashx", { "mail": $("#txtforumail").val() }, function (data) {
                if (data == "no") {
                    $("#errorMsg").text("邮件错误!!");
                } else {
                    $("#txtmsgdl").css("display","block");
                }
            });
        }
    </script>
    <style type="text/css">
    SPAN > A {
    padding-left: 20px;
}

    SPAN > A:hover {
        text-decoration: none;
    }

SPAN.login_qq {
    background: url(/Content/img/icons/q_login.png) no-repeat;
    _padding-left: 0;
}

SPAN.papa {
    font-size: 15px;
    font-family: "Microsoft yahei";
}



.log_login {
    padding: 30px 20px 0 0;
}

    .log_login p {
        padding: 10px 0;
    }

.lognow {
    background: #79cd35;
    width: 200px;
    margin-left: 100px;
    height: 40px;
    border: none;
    color: #FFF;
    font-size: 15px;
    font-weight: 700;
}

#pageflip {
    right: 0px;
    float: right;
    position: relative;
    top: 0px;
}

    #pageflip img {
        z-index: 100;
        right: 0px;
        width: 50px;
        position: absolute;
        top: 0px;
        height: 52px;
        ms-interpolation-mode: bicubic;
    }

    #pageflip .msg_block {
        right: 0px;
        overflow: hidden;
        width: 50px;
        position: absolute;
        top: 0px;
        height: 50px;
        z-index: 99;
    }

.fr {
    float: right;
}

.wid100 {
    width: 100%;
    display: inline-block;
}

.rem_for {
    padding: 0px;
    margin: 0px;
    line-height: 1.8;
    font-size: 15px;
}

    .rem_for label {
        padding: 0px 10px 0px 0px;
        margin: 0px;
        line-height: 1.8;
    }

    .rem_for input {
        vertical-align: middle;
        height: 16px;
        line-height: 16px;
        border: 1px #ccc solid;
        width: 25px;
    }

.errorlog {
    color: red;
}

.changpassword {
    border: 1px #ddd solid;
    padding: 15px 15px 0px 15px;
    display: block;
}
    </style>
</head>
<body>

    <div id="main" class="swidth1024 loginbg">
        <!--简版头部-->
        <div class="wrap clearfix">
            <a href="">
                <div class="simple_logo" ><img src="/Images/logo.gif" alt="logo" width="304" height="228"></div>
            </a>
            <div class="simple_banner"></div>
        </div>
        <div class="clear_channel"></div>
        <!--简版头部end-->
        <!--注册区 开始-->
        <div class="login_main">
            <div class="login_panel clearfix">
                <div class="reg_r po p65">
                    <div class="v_z_po"></div>
        <a href="javascript:void(0)"><img src="/Images/mm.jpg" alt="美女" ></a>

                </div>
                <div class="reg_l" style="min-height:100px">
                    <div class="reg_l_in" id="logindiv">
                        <span class="reg_tit">
                            <strong>用户登陆</strong><span class="fr">
                                没有账号

                                <%if(!string.IsNullOrEmpty(ReturnUrl)){%>
                                <a href="/Account/Register.aspx?returnUrl=<%=ReturnUrl%>">快速注册</a>
                                <%}else{%>
                                  <a href="/Account/Register.aspx">快速注册</a>
                                <%} %>
                            </span>
                        </span>
                        <div class="reg_boxes ">
<form action="Login.aspx" method="post">                              
      <input type="hidden" id="txtReturn" name="returnurl" value="<%=ReturnUrl%>">
    
                                <dl>
                                    <dt>用&nbsp;户&nbsp;名</dt>
                                    <dd>
                                        <input type="text" id="txtUserName" name="username" maxlength="30" placeholder="请输入用户名">
                                        <span><i>请输入用户名</i><b></b></span>
                                    </dd>
                                </dl>
                                <dl>
                                    <dt>密&nbsp;&nbsp;&nbsp;码</dt>
                                    <dd class="pwd_inner" style="position:relative;">
                                        <input type="password" id="txtPassWord" name="password" maxlength="20" onpaste="return  false" style="ime-mode:disabled;" autocomplete="off" placeholder="请输入6-20个字符">
                                        <span><i>请输入用户名</i><b></b></span>
                                    </dd>
                                </dl>
                                <div class="rem_for wid100">
                                    <div class="fr" style="margin-right:30px;">
                                        <span><i id="errorlog" class="errorlog"></i><b></b></span>&nbsp;&nbsp;&nbsp;
                                        <label><input id="chkRember" name="chkRember" type="checkbox" value="1">自动登录</label><a href="javascript:void(0)" id="btnForget">忘记密码?</a>
                                    </div>
                                </div>
                                <input type="submit" id="btnLogin" class="lognow ml_90" style="width: 200px;">登录</input>
                                <span id="loginerro"></span>
              </form>                        </div>
                    </div>
                
                     <div class="reg_l_in" id="forgetdiv" style="display:none">
                        <div class="reg_boxes" style="height:auto;">
                            <span class="reg_tit">
                                <strong>找回密码</strong><span>请输入您注册时的电子邮箱</span><span class="fr">

                                   <a href="javascript:void(0)" id="btnLoginDiv">登陆</a>
                                </span>
                            </span>
                            <dl>

                                <dd style="padding-top:10px;">
                                    <input type="text" id="txtforumail" name="txtUserMail" maxlength="30" placeholder="请输入邮箱地址" style="width:200%;">
                                   <span id="errorMsg" ></span>
                                </dd>
                            </dl>
                            <dl>
                                <dd>
                                    <button type="button" id="btnForgetsub" class="lognow" style="margin-left:90px;">找回密码</button><span id="loginerrofor"></span>
                                </dd>
                            </dl>
                            <dl id="txtmsgdl" style="height:200px;display:none;">
                                <dd>
                                    找回密码邮件已经发到您邮箱！

                                    如果您没有收到邮件,请检查您填写的邮箱是否正确，如果确实后还是没有收到邮件，

                                    请拨打我们的服务电话：110，我们的客户人员将为您解决，谢谢使用！
                                </dd>
                            </dl>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  
    <script src="../js/jquery-1.7.1.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#pageflip").hover(function () {
                $("#pageflip img , .msg_block").stop().animate({ width: '307px', height: '319px' }, 500);
            }, function () {
                $("#pageflip img").stop().animate({ width: '50px', height: '52px' }, 220);
                $(".msg_block").stop().animate({ width: '50px', height: '50px' }, 200);
            });

        });
    </script>
    <!--注册区 结束-->


</body>
</html>
