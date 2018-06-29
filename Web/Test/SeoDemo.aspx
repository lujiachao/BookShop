<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeoDemo.aspx.cs" Inherits="BookShop.Web.Test.SeoDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#a1").click(function () {
                $.post("/ashx/Seo.ashx", {}, function (data) {
                    $("#div1").append(data);
                });
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <a id="a1" href="/ashx/Seo.ashx">a1</a>
        <div id="div1"></div>
    </div>
    </form>
</body>
</html>
