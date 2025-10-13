<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoiMatKhau.aspx.cs" Inherits="TNGLuong.DoiMatKhau" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Đổi mật khẩu</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="images/icons/favicon.ico" />
    <!--===============================================================================================-->
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
</head>
<body style="background-color: #006699;">

    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">

                <div class="login100-pic js-tilt" data-tilt>
                    <img src="images/img-01.png" alt="IMG">
                </div>

                <form class="login100-form validate-form" runat="server">
                    <div style="margin: auto; margin-top: -15%;">
                        <h5>Thay đổi mật khẩu:</h5>
                        <br />
                    </div>
                    <asp:Label ID="lblFullName" runat="server" Text="" CssClass="login100-form-title"></asp:Label>
                    <div align="center">
                        <asp:Label ID="lblErr" runat="server" Text="" CssClass="lblError"></asp:Label>
                    </div>
                    <div class="wrap-input100 ">
                        <input id="txtMatKhauCu" class="input100" type="password" name="manhansu" placeholder="Mật khẩu cũ" runat="server" />
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-lock" aria-hidden="true"></i>
                        </span>
                    </div>

                    <div class="wrap-input100 ">
                        <input id="txtPassMoi" class="input100" type="password" name="manhansu" placeholder="Mật khẩu mới" runat="server" />
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-lock" aria-hidden="true"></i>
                        </span>
                    </div>

                    <div class="container-login100-form-btn">
                        <asp:Button ID="btnLogin" runat="server" Text="Đổi mật khẩu" CssClass="login100-form-btn" OnClick="btnLogin_Click" />
                        <%--<button class="login100-form-btn">
							Đăng nhập
						</button>--%>
                    </div>

                    <div class="text-center p-t-12">
                        <span class="txt1"></span>
                        <a class="txt2" href="Default.aspx">
                            <i class="fa fa-backward" aria-hidden="true"></i>&nbsp;Quay về
                        </a>
                    </div>

                    <div class="text-center p-t-136">
                        <a class="txt2" href="#">

                            <%--<i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>--%>
                        </a>
                    </div>
                </form>
            </div>
        </div>
    </div>




    <!--===============================================================================================-->
    <%: Scripts.Render("~/bundles/js") %>
    <!--===============================================================================================-->
    <script>
        $('.js-tilt').tilt({
            scale: 1.1
        })
    </script>
    <!--===============================================================================================-->
    <%: Scripts.Render("~/bundles/main") %>
</body>
</html>

