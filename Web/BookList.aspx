<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster/MasterPage.Master" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="BookShop.Web.BookList" %>
<%@ Import Namespace="BookShop.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <link href="Css/pageBar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <%foreach(Book bookModel in ProductList){%>
<TABLE>
                    <TBODY>
                    <TR>
                      <TD rowSpan=2><A  href="BookDetail_<%=bookModel.Id%>.aspx"
                       ><IMG 
                        style="CURSOR: hand" height=121 
                        alt="<%=bookModel.Title %>" hspace=4 
                        src="/Images/BookCovers/<%=bookModel.ISBN%>.jpg" width=95></A> 
</TD>
                      <TD style="FONT-SIZE: small; COLOR: red" width=650><A 
                        class=booktitle id=link_prd_name 
                        href="<%=GetDir(bookModel.PublishDate)%><%=bookModel.Id%>.html" target="_blank" 
                        name="link_prd_name">
                        <%=bookModel.Title %></A> </TD></TR>
                    <TR>
                      <TD align=left><SPAN 
                        style="FONT-SIZE: 12px; LINE-HEIGHT: 20px"><%=bookModel.Author %></SPAN><BR><BR><SPAN 
                        style="FONT-SIZE: 12px; LINE-HEIGHT: 20px"><%=this.CutString (bookModel.ContentDescription,150)%></SPAN> 
                      </TD></TR>
                    <TR>
                      <TD align=right colSpan=2><SPAN 
                        style="FONT-WEIGHT: bold; FONT-SIZE: 13px; LINE-HEIGHT: 20px">&yen;
                        <%=bookModel.UnitPrice.ToString("0.00")%></SPAN> </TD></TR></TBODY></TABLE>
    <hr />
    <%} %>
    <div class="page_nav">
        <%=Common.PageBarHelper.GetPageBar(PageIndex,PageCount) %>
    </div>

</asp:Content>
