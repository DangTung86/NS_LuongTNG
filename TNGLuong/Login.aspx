<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TNGLuong.Login" %>

<!DOCTYPE html>
<html lang="vi">
<head>
    <title>Đăng nhập</title>
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
    <form id="form1" runat="server">
        <header id="site-header" class="main-header mainHeader_temp01 header-style hSticky hSticky-nav hSticky-up" style="display: none;">
            <div class="header-middle">
                <div class="container" style="text-align: center;">
                    <h4 style="color: red; text-align: center;">Tình hình dịch bệnh đang rất phức tạp, yêu cầu NLĐ khi về nơi cư trú hạn chế tiếp xúc với cộng đồng, tuân thủ nguyên tắc 5K. Người khai báo không trung thực làm lây lan dịch bệnh sẽ bị xử phạt theo quy định và có thể ở mức cao nhất.</h4>
                </div>
            </div>
            <div class="header-menu-desktop menu-desktop hidden-xs hidden-sm">
                <div class="container" style="text-align: center;">
                    <h4 style="color: red; text-align: center;">Tình hình dịch bệnh đang rất phức tạp, yêu cầu NLĐ khi về nơi cư trú hạn chế tiếp xúc với cộng đồng, tuân thủ nguyên tắc 5K. Người khai báo không trung thực làm lây lan dịch bệnh sẽ bị xử phạt theo quy định và có thể ở mức cao nhất.</h4>
                </div>
            </div>
        </header>
        <div class="limiter">
            <div class="container-login100">
                <div class="wrap-login100">
                    <div class="login100-pic">
                        <div class="js-tilt" data-tilt>
                            <img src="images/DePhongLuaDao.jpg" alt="IMG" width="100%">
                        </div>
                    </div>

                    <div class="login100-form validate-form">
                        <center>
                            <img src="images/img-02.png" alt="IMG" width="45%">
                        </center>
                        <br />
                        <span class="login100-form-title">NHÂN VIÊN ĐĂNG NHẬP
                        </span>
                        <div align="center">
                            <asp:Label ID="lblErr" runat="server" Text="" CssClass="lblError"></asp:Label>
                        </div>
                        <div class="wrap-input100 validate-input" data-validate="Vui lòng nhập mã nhân sự">
                            <input id="txtmans" class="input100" type="text" name="manhansu" placeholder="Mã nhân sự" runat="server" />
                            <span class="focus-input100"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </span>
                        </div>

                        <div class="wrap-input100 validate-input" data-validate="Vui lòng nhập mật khẩu">
                            <asp:TextBox ID="txtpass" runat="server" TextMode="Password" CssClass="input100"></asp:TextBox>
                            <%--<input id="txtpass" class="input100" type="text" name="pass" placeholder="Mật khẩu" runat="server"/>--%>
                            <span class="focus-input100"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-lock" aria-hidden="true"></i>
                            </span>
                        </div>

                        <div style="margin-left: 20px;">
                            <div class="pretty p-default">
                                <input type="checkbox" id="cbRemenber" runat="server" />
                                <div class="state p-primary">
                                    <label>Ghi nhớ</label>
                                </div>
                            </div>
                            <%--<asp:CheckBox ID="cbRemenber" runat="server" Text="Ghi nhớ" />--%>
                        </div>

                        <div class="container-login100-form-btn">
                            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="login100-form-btn" OnClick="btnLogin_Click" />
                            <%--<button class="login100-form-btn">
							Đăng nhập
						</button>--%>
                        </div>

                        <div class="text-center p-t-12" style="font-size: 10pt; font-style: italic;">
                            <span class="txt1"></span>
                            <%--<a href="https://docs.google.com/forms/d/e/1FAIpQLSfkHRq_AoDSKBl7Keh54H-yWSNlaXdr4JOLaJpawepJOGsh7g/viewform" target="_blank" style="font-family: Tahoma;">
                                    <p style="color: blue;text-align:center;">DỰ ĐOÁN NGÀY ĐẠT DOANH SỐ SẢN XUẤT TOÀN CÔNG TY CAO NHẤT</p>
                                    <p style="color: blue;text-align:left;">Thời gian nhận kết quả dự đoán: trước 17h30 ngày 21/05/2022</p>
                                    <p style="color: blue;text-align:left;">Kết quả được công bố ngày 01/06/2022</p>
                                    <p style="color: blue;text-align:left;">Cơ cấu giải thưởng:</p>
                                    <p style="color: blue;text-align:left;">1 Giải nhất: 5.000.000 đồng</p>
                                    <p style="color: blue;text-align:left;">2 Giải nhì: 3.000.000 đồng</p>
                                    <p style="color: blue;text-align:left;">3 Giải ba: 2.000.000 đồng</p>
                                    <p style="color: blue;text-align:left;">5 Giải khuyến khích: 1.000.000 đồng</p>
                                </a>--%>
                            <%--<p style="font-family: Tahoma; text-align: left;">
                                - Quên hoặc mất mật khẩu, không có công đoạn giao khoán ngày liên hệ với:
                            </p>
                            <p style="font-family: Tahoma; text-align: left;">&nbsp;&nbsp;&nbsp;+ Thiết kế chuyền</p>
                            <p style="font-family: Tahoma; text-align: left;">
                                &nbsp;&nbsp;&nbsp;+ Quản lý tiền lương
                            </p>--%>
                            <p style="font-size: 10pt;" id="pTieuDePT" runat="server"></p>
                            <audio controls autoplay>
                                <source id="audio_PT" runat="server">
                            </audio>
                        </div>

                        <%--<div class="text-center p-t-136">--%>
                        <%--<span class="txt1">
                                <asp:Label ID="lblOnline" runat="server" Text="Online:0" Font-Bold="True" Font-Size="9pt" ForeColor="Red" Visible="false"></asp:Label>
                            </span>--%>
                        <%--<a class="txt2" href="#">--%>

                        <%--<i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>--%>
                        <%--</a>--%>
                        <%--</div>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade modal-addThis modal-contactform in" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 25px; display: none;">
                        <div class="divthongbao">
                            <p style="color: #039; font-family: Tahoma;">Chọn chức năng</p>
                            <button type="button" class="close" data-dismiss="modal" id="btncloseMain" runat="server">×</button>
                        </div>
                    </div>
                    <div class="modal-body content_popupform" style="display: block;">
                        <table style="width: 100%; font-family: Tahoma;">
                            <tr>
                                <td align="center">
                                    <asp:HyperLink ID="a_KhaoSat" runat="server" NavigateUrl="https://docs.google.com/forms/d/e/1FAIpQLSfeMBWRgGBQu0KBbkQMGHHcz7iDT2PSK6WqmvC1mQgstcCV5w/viewform" Visible="False" style="display: none;">
                                        <img src="images/Khao_Sat.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnNSNgay" runat="server" NavigateUrl="NangSuatCongNhan" Visible="False"><img src="images/nangsuatngay.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnDKComCa" runat="server" NavigateUrl="DangKyComCa" Visible="False"><img src="images/dangkycomca.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnTongHop" runat="server" NavigateUrl="TongHopCongDoan" Visible="False"><img src="images/tonghop.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnChoViec" runat="server" NavigateUrl="NhapThoiGian" Visible="False"><img src="images/thoigiancho.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnPhanCumTrg" runat="server" NavigateUrl="PhanCumTruong" Visible="False"><img src="images/phancumtruong.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnPhanToNK" runat="server" NavigateUrl="PhanToNhayKhau" Visible="False"><img src="images/phanto.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnDSCN" runat="server" NavigateUrl="DuyetNSCN" Visible="False"><img src="images/DanhSachCN.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnblNgay" runat="server" NavigateUrl="BangTinhLuongCN" Visible="False"><img src="images/bangluongngay.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbltonghopluong" runat="server" NavigateUrl="TongHopLuong" Visible="False"><img src="images/tonghopluongthang.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lbtnblThang" runat="server" NavigateUrl="bangluongthang" Visible="False"><img src="images/bangluongthang.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lblCongDiLam" runat="server" NavigateUrl="TTKiemTraCongDiLam" Visible="False"><img src="images/congdilam.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lblPhanCongDoan" runat="server" NavigateUrl="PhanCongCongDoan" Visible="False"><img src="images/phancongdoan.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lblKhaiBaoYTe" runat="server" NavigateUrl="MTKN_XepHang_CongNhan" Visible="False"><img src="images/DanhGiaKyNang.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                    <asp:HyperLink ID="lblQuanTriWeb" runat="server" NavigateUrl="Admin" Visible="False"><img src="images/quantri.png" alt="" style="width: 100px;" /></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade modal-popupContact popupForm in" tabindex="-1" role="dialog" id="popupcontact" runat="server" style="display: none;">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-wrapper-contact ">
                        <button type="button" class="close close-popup-contact" id="btnclose" runat="server">
                            <span aria-hidden="true">
                                <svg class="svg-next-icon svg-next-icon-size-24" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" id="Layer_1" x="0px" y="0px" viewBox="0 0 512.001 512.001" style="enable-background: new 0 0 512.001 512.001;" xml:space="preserve">
                                    <g>
                                        <path d="M294.111,256.001L504.109,46.003c10.523-10.524,10.523-27.586,0-38.109c-10.524-10.524-27.587-10.524-38.11,0L256,217.892    L46.002,7.894c-10.524-10.524-27.586-10.524-38.109,0s-10.524,27.586,0,38.109l209.998,209.998L7.893,465.999    c-10.524,10.524-10.524,27.586,0,38.109c10.524,10.524,27.586,10.523,38.109,0L256,294.11l209.997,209.998    c10.524,10.524,27.587,10.523,38.11,0c10.523-10.524,10.523-27.586,0-38.109L294.111,256.001z"></path>
                                    </g></svg>
                            </span>
                        </button>
                        <h3 class="title-popup-contact" style="font-weight: bold;">Thông báo</h3>
                        <div class="message-popup-contact">
                            <div class="title-adv-popup-contact" style="font-weight: bold; color: red;">Chưa đến thời gian mở nhập năng suất ngày, thời gian khóa nhập năng suất ngày.</div>
                            <ul class="list-adv-popup-contact">
                                <li style="color: red; font-style: italic;">Bắt đầu từ 07h30 đến 11h30 hàng ngày.</li>
                                <%--<li>Từ 17h đến 7h30 sáng hôm sau</li>--%>
                                <li style="display: none;">
                                    <asp:TextBox ID="txtAdmin" runat="server" OnTextChanged="txtAdmin_TextChanged"></asp:TextBox>
                                </li>
                            </ul>
                        </div>
                        <div class="popup-form-customer">
                            <%-- để trống--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>


    <!--===============================================================================================-->
    <%: Scripts.Render("~/bundles/js") %>
    <!--===============================================================================================-->
    <script>
        $('.js-tilt').tilt({
            scale: 1.1
        });
        function openCity(cityName) {
            var i;
            var x = document.getElementsByClassName("city");
            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }
            document.getElementById(cityName).style.display = "block";
        }
    </script>
    <!--===============================================================================================-->
    <%: Scripts.Render("~/bundles/main") %>
</body>
</html>
