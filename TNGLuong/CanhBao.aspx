<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CanhBao.aspx.cs" Inherits="TNGLuong.CanhBao" %>

<!DOCTYPE html>
<html lang="vi">
<head>
    <title>Cảnh báo</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="images/icons/favicon.ico" />
    <!--===============================================================================================-->
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <style>
    .buttonCB {
      background-color: #04AA6D; /* Green */
      border: none;
      color: white;
      padding: 8px;
      font-weight: bold;
      text-align: center;
      text-decoration: none;
      display: inline-block;
      font-size: 16px;
      margin: 10px 2px;
      cursor: pointer;
      width:130px;
    }

    .buttonCB1 {border-radius: 20px;}
    </style>
</head>
<body style="background-color: #006699;">
    <form id="form1" runat="server">
        <div class="limiter">
            <div class="container-login100">
                <div class="wrap-canhbao100">                    
                    <div>
                    <img src="images/DePhongLuaDao.jpg" alt="IMG" width="100%">
                        </div>
                    <div>
                        <center>
                    <button type="button" class="buttonCB buttonCB1" id="btncloseMain" runat="server">Thoát</button>
                            </center>
                        </div>
                </div>
            </div>
        </div>
    </form>


    <!--===============================================================================================-->
    <%: Scripts.Render("~/bundles/js") %>
    <!--===============================================================================================-->
    <%: Scripts.Render("~/bundles/main") %>
</body>
</html>
