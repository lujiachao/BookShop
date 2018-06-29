<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserCenter.aspx.cs" Inherits="BookShop.Web.UserInfoManager.UserCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #divCut {
            background-position: top left
        }
    </style>
    <link href="/Css/imgareaselect-default.css" rel="stylesheet" />
    <script src="/js/jquery.imgareaselect.min.js"></script>
    <script src="/SWFUpload/swfupload.js"></script>
    <script src="/SWFUpload/handlers.js"></script>
     <script type="text/javascript">
         var swfu;
         window.onload = function () {
             swfu = new SWFUpload({
                 // Backend Settings
                 upload_url: "/ashx/upload.ashx?action=up",
                 post_params: {
                     "ASPSESSID": "<%=Session.SessionID %>"
                },

                // File Upload Settings
                file_size_limit: "2 MB",
                file_types: "*.jpg;*.gif",
                file_types_description: "JPG Images",
                file_upload_limit: 0,    // Zero means unlimited

                // Event Handler Settings - these functions as defined in Handlers.js
                //  The handlers are not part of SWFUpload but are part of my website and control how
                //  my website reacts to the SWFUpload events.
                swfupload_preload_handler: preLoad,
                swfupload_load_failed_handler: loadFailed,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: showImage,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_image_url: "/SWFUpload/images/XPButtonNoText_160x22.png",
                button_placeholder_id: "spanButtonPlaceholder",
                button_width: 160,
                button_height: 22,
                button_text: '<span class="button">请选择上传图片<span class="buttonSmall">(2 MB Max)</span></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,

                // Flash Settings
                flash_url: "/SWFUpload/swfupload.swf",	// Relative to this file
                flash9_url: "/SWFUpload/swfupload_FP9.swf",	// Relative to this file

                custom_settings: {
                    upload_target: "divFileProgressContainer"
                },

                // Debug Settings
                debug: false
            });
        }
        //上传成功以后调用该方法
        function showImage(file, serverData) {
            //$("#imgSrc").attr("src", serverData);
            var data = serverData.split(':');
            if (data[0] == "ok") {
                //建上传成功的图片作为divContent的背景.
                //$("#divContent").css("backgroundImage", "url(" + data[1] + ")").css("width", data[2] + "px").css("height", data[3] + "px");
                $("#selectbanner").attr("src", data[1]);
                $('#selectbanner').imgAreaSelect({
                    selectionColor: 'blue', x1: 0, y1: 0, x2: 150,y2: 100,

                    //maxWidth: 950, minWidth: 950,  minHeight: 400, maxHeight: 400,

                    selectionOpacity: 0.2, onSelectEnd: preview
                });
                $("#imagePath").val(data[1]);//将上传成功的头像存储到隐藏域中。
            } else {
                alert(serverData[1]);
            }
        }
         //选择结束以后调用该方法(确定出要截取头像的范围，并且通过data方法存储要截取头像范围的数据)
        function preview(img, selection) {

            $('#selectbanner').data('x', selection.x1);

            $('#selectbanner').data('y', selection.y1);

            $('#selectbanner').data('w', selection.width);

            $('#selectbanner').data('h', selection.height);

        }


        $(function () {
            //$("#divCut").resizable({
            //    containment: "parent"
            //}).draggable({ containment: "parent" });
            $("#btnPhotoCut").click(function () {
                ////确定要截取头像的范围。
                //var y = $("#divCut").offset().top - $("#divContent").offset().top;//计算出纵坐标.
                //var x = $("#divCut").offset().left - $("#divContent").offset().left;
                //var width = $("#divCut").width();
                //var height = $("#divCut").height();
                //var pars = {
                //    x: x,
                //    y: y,
                //    width:width,
                //    height: height,
                //    action: "cut",
                //    imagePath: $("#imagePath").val()

                //};

                var pars = {

                    x: $('#selectbanner').data('x'),

                    y: $('#selectbanner').data('y'),

                    width: $('#selectbanner').data('w'),

                    height: $('#selectbanner').data('h'),

                    imagePath: $("#imagePath").val(),
                    action: "cut",

                };
                //根据确定的范围进行头像的截取.
                $.post("/ashx/upload.ashx", pars, function (data) {
                    $("#imgSrc").attr("src",data);
                });
            });
        });
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div id="content">
	    <div id="swfu_container" style="margin: 0px 10px;">
		    <div>
				<span id="spanButtonPlaceholder"></span>
		    </div>
		    <div id="divFileProgressContainer" style="height: 75px;"></div>
		    <div id="thumbnails"></div>
           <%-- <div id="divContent" style="width:300px; height:300px">
                <div id="divCut" style="width:100px; height:100px;border:solid 1px red">

                </div>
            </div>--%>
            <img id="selectbanner"/>
            <input type="button" value="头像截取" id="btnPhotoCut" />
            <input type="hidden" id="imagePath" />
            <br />
            <img id="imgSrc" />
	    </div>
		</div>

</asp:Content>
