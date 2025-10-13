<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="TNGLuong.ErrorPage" %>

<!DOCTYPE html>
<html lang="vi">
<head>
    <title>Error</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="images/icons/favicon.ico" />
    <link href="https://cdn.jsdelivr.net/npm/pretty-checkbox@3.0/dist/pretty-checkbox.min.css" rel="stylesheet" type="text/css">
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
                <form class="login100-form validate-form" runat="server" style="width:100%;">
                    <span class="login100-form-title" style="padding-bottom:10px;">THÔNG BÁO TỪ QUẢN TRỊ WEBSITE
                    </span>
                    <div align="center">
                        <asp:Label ID="lblErr" runat="server" Text="Trang web đang được bảo trì, vui lòng quay lại sau." CssClass="lblError" Font-Bold="True" Font-Italic="True" Font-Size="24pt"></asp:Label>
                    </div>
                    <%--<div class="wrap-input100 validate-input" data-validate = "Vui lòng nhập mã nhân sự">
						<input id="txtmans" class="input100" type="text" name="manhansu" placeholder="Mã nhân sự" runat="server"/>
						<span class="focus-input100"></span>
						<span class="symbol-input100">
							<i class="fa fa-user" aria-hidden="true"></i>
						</span>
					</div>--%>

                    <%--<div class="wrap-input100 validate-input" data-validate = "Vui lòng nhập mật khẩu">--%>
                    <%--<asp:TextBox ID="txtpass" runat="server" TextMode="Password" CssClass="input100"></asp:TextBox>
						<%--<input id="txtpass" class="input100" type="text" name="pass" placeholder="Mật khẩu" runat="server"/>--%>
                    <%--<span class="focus-input100"></span>
						<span class="symbol-input100">
							<i class="fa fa-lock" aria-hidden="true"></i>
						</span>--%>
					<%--</div>--%>

                    <div style="margin-left: 20px;">
                        <%--<div class="pretty p-default">
                            <input type="checkbox" id="cbRemenber" runat="server" />
                            <div class="state p-primary">
                                <label>Ghi nhớ</label>
                            </div>
                        </div>--%>
                        <%--<asp:CheckBox ID="cbRemenber" runat="server" Text="Ghi nhớ" />--%>
                    </div>

                    <div class="container-login100-form-btn">
                        <%--<asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="login100-form-btn" OnClick="btnLogin_Click" />--%>
                        <%--<button class="login100-form-btn">
							Đăng nhập
						</button>--%>
                    </div>

                    <div class="text-center p-t-12">
                       <%-- <span class="txt1"></span>
                        <a class="txt2" href="https://www.youtube.com/watch?v=S4buIhv6XLo&feature=youtu.be" target="_blank">Hướng dẫn nhập năng suất và xem tiền lương ngày
                        </a>--%>
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
