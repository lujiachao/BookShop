<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BookShop.Web.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css">
        input {
        vertical-align:middle;
	height:16px;
	line-height:16px;
	margin:0;
	border:1px #ccc solid;
	padding: 10px 0 10px 5px;
	width:250px;
	margin-right:8px;
	float:left;
        }
        .regnow {
	width:300px;
	margin-left:150px;
	height:40px;
	background:#db2f2f;
	border:none;
	color: #FFF;
	font-size: 15px;
	font-weight: 700;
    cursor:pointer;
} 
    </style>
    <script type="text/javascript">
        $(function () {
            //验证用户名
            $("#txtUserName").blur(function () {
                if ($(this).val() != "") {
                    var reg = /^[a-zA-Z0-9_\u4e00-\u9fa5]{4,20}$/;
                    if (reg.test($(this).val())) {
                        $.post("/ashx/CheckUserName.ashx", { "userName": $(this).val()}, function (data) {
                            if (data == "yes") {
                                $("#userNameMsg").text("此用户已经存在");
                            } else {
                                $("#userNameMsg").text("此用户可用");
                            }
                        });
                    } else {
                        $("#userNameMsg").text("用户名只能是字母数字组合!!");
                    }
                } else {
                    $("#userNameMsg").text("用户名不能为空!!");
                }
            });
            //邮箱校验
            $("#txtUserMail").blur(function () {
                if ($(this).val() != "") {
                    var reg = /^([a-zA-Z0-9])+([a-zA-Z0-9_\.\-])*\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})$/;
                    if (reg.test($(this).val())) {
                        $.post("/ashx/CheckMail.ashx", { "mail": $(this).val() }, function (data) {
                            if (data == "yes") {
                                $("#userMailMsg").text("此邮箱被注册!！！");
                                
                            } else {
                                $("#userMailMsg").text("邮箱可用!！！");
                            }
                        });
                    } else {
                        $("#userMailMsg").text("邮箱格式错!！！");
                    }
                } else {
                    $("#userMailMsg").text("邮箱不能为空!！！");
                }
            });
            //切换验证码.
            $("#valiateCode").click(function () {
                $("#imgCode").attr("src", $("#imgCode").attr("src")+1);
            });
            //判断验证码是否正确.
            $("#txtValidateCode").blur(function () {
                if ($(this).val() != "") {
                    $.post("/ashx/CheckValidateCode.ashx", { "txtCode": $(this).val() }, function (data) {
                        if (data == "yes") {
                            $("#userCodeMsg").text("验证码正确!!");
                        } else {
                            $("#userCodeMsg").text("验证码不一致!!");
                        }
                    });
                } else {
                    $("#userCodeMsg").text("验证码不能为空!!");
                }
            });
            //用户注册.
            $("#btnReg").click(function () {
                if ($("#txtUserName").val() == "") {
                    $("#userNameMsg").text("用户名不能为空!!");
                    return false;
                }
                if ($("#txtUserMail").val() == "") {
                    $("#userMailMsg").text("邮箱不能为空!!");
                    return false;
                }
                if ($("#txtValidateCode").val() == "") {
                    $("#userCodeMsg").text("验证码不能为空!!");
                    return false;
                }
                var pars = $("#aspnetForm").serializeArray();//获取表单中输入的数据
                $.post("/ashx/RegUser.ashx", pars, function (data) {
                    var serverData = data.split(':');
                    if (serverData[0] == "yes") {
                        var url = $("#returnUrl").val();
                        if (url != "") {
                            window.location.href = url;
                        } else {
                            window.location.href = "../UserInfoManager/UserCenter.aspx";
                        }
                    } else {
                        $("#userCodeMsg").text(serverData[1]);
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="returnUrl" value="<%=ReturnUrl%>" />
 <div style="font-size:small">
  <table width="80%" height="22" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td style="width: 10px"><img src="../Images/az-tan-top-left-round-corner.gif" width="10" height="28" /></td>
    <td bgcolor="#DDDDCC"><span class="STYLE1">注册新用户</span></td>
    <td width="10"><img src="../Images/az-tan-top-right-round-corner.gif" width="10" height="28" /></td>
  </tr>
</table>


<table width="80%" height="22" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
    <td><div align="center">
      <table height="61" cellpadding="0" cellspacing="0" style="height: 332px">
        <tr>
          <td height="33" colspan="6"><p class="STYLE2" style="text-align: center">注册新帐户方便又容易</p></td>
        </tr>
        <tr>
          <td width="24%" align="center" valign="top" style="height: 26px">用户名</td>
          <td valign="top" width="37%" align="left" style="height: 26px">
            <input type="text" name="txtName" id="txtUserName" placeholder="请输入用户名" /><span style="font-size:14px;color:red" id="userNameMsg"></span></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">真实姓名：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtRealName" /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">密码：</td>
          <td valign="top" width="37%" align="left">
              <input type="password" name="txtPwd" placeholder="请输入密码" /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">确认密码：</td>
          <td valign="top" width="37%" align="left">
              <input type="password" name="txtConfirmPwd" /></td>          
        </tr>
         <tr>
          <td width="24%" height="26" align="center" valign="top">Email：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtMail" id="txtUserMail" /><span style="font-size:14px;color:red" id="userMailMsg"></span></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">地址：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtAddress" /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">手机：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtPhone" /></td>          
        </tr>
        <tr>
          <td width="24%" height="26" align="center" valign="top">
              验证码：</td>
          <td valign="top" width="37%" align="left">
              <input type="text" name="txtCode" id="txtValidateCode" /><a href="javascript:void(0)" id="valiateCode"><img src="/ashx/ValidateCode.ashx?id=1" id="imgCode"/></a><span style="font-size:14px;color:red" id="userCodeMsg"></span></td>          
        </tr>
        <tr>
          <td colspan="2" align="center"><input type="button" id="btnReg" value="同意协议并注册" class="regnow" /></td>           
        </tr>
        <tr>
          <td colspan="2" align="center">&nbsp;</td>           
        </tr>
      </table>
      <div class="STYLE5">---------------------------------------------------------</div>
    </div>	
    </td>
    <td width="2" bgcolor="#DDDDCC">&nbsp;</td>
  </tr>
</table>

<table width="80%" height="3" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="3" bgcolor="#DDDDCC"><img src="../Images/touming.gif" width="27" height="9" /></td>
  </tr>
</table>
</div>

</asp:Content>
