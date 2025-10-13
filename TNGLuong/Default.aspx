<%@ Page Title="Bảng lương" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TNGLuong._Default" Culture="vi-VN" UICulture="vi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: table; width: 100%;">
        <div style="width: 100%; height: 60px;">
            <p class="pluong">THÔNG TIN LƯƠNG</p>
        </div>
        <div id="content" class="page-wrap">
            <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server" EnableScriptGlobalization="True"></cc1:ToolkitScriptManager>
            <div style="margin-bottom: 1em;">
                Chọn tháng:
                                <asp:TextBox ID="txtStart" runat="server" CssClass="txtNgayThang" Width="100px" OnTextChanged="txtStart_TextChanged" AutoPostBack="True"></asp:TextBox>
                <cc1:CalendarExtender ID="tungay" ClientIDMode="Static" runat="server" TargetControlID="txtStart" Format="MM-yyyy" DefaultView="Months" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden" CssClass="calendar" TodaysDateFormat="MMMM, yyyy"></cc1:CalendarExtender>
            </div>
            <div>
                <table class="maintablenotbottom">
                    <tr>
                        <td class="maintdnotbottom" rowspan="3">A. Tổng thu nhập: II + III</td>
                        <td class="maintd" colspan="2" style="text-align: center;">Số công</td>
                        <td class="maintdnotbottom" rowspan="3" style="width: 100px; text-align: center;">Thu Nhập</td>
                    </tr>
                    <tr>
                        <td class="maintd" style="width: 50px; text-align: center;">SP</td>
                        <td class="maintd" style="width: 50px; text-align: center;">TG</td>
                    </tr>
                    <tr>
                        <td class="maintdnotbottom" style="text-align: center;">
                            <asp:Label ID="lblSoCong" runat="server" Text=""></asp:Label>&nbsp;</td>
                        <td class="maintdnotbottom" style="text-align: center;">
                            <asp:Label ID="lblSoTG" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                </table>
                <table class="maintable">
                    <tr>
                        <td class="maintd" colspan="3">Lương cấp bậc/Sản phẩm</td>
                        <td class="maintd" style="width: 100px;text-align: right;">
                            <asp:Label ID="lblLuongCB" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3">Xếp loại</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblXepLoai" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3">I. Tiền lương căn cứ đóng bảo hiểm</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuongBH" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="4">Tiền lương tham gia bảo hiểm</td>
                        <td class="maintd">1. Mức lương CB</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuongCoBan" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">2. Mức phụ cấp CV</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuongPhuCap" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">3. Mức PCCC</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblMucPCCC" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">4. Mức ATVSV</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblMucATVSV" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3">II. Tiền lương thực trả cho người lao động</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuongThucTra" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="3">Tiền lương</td>
                        <td class="maintd">5. Lương 8h</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuong8h" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">6. Lương thời gian</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuongThoiGian" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">7. Lương thêm giờ</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuongThemGio" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="2">Phụ cấp lương</td>
                        <td class="maintd">8. PC Lương(CV, PCCC, ATVSV)</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblPCLuong" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">9. Phụ cấp lao động nữ</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblPhuCapNu" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <%if (Session["TNGF"] != null)
                            {%>
                        <td class="maintd" colspan="2" rowspan="4">Bổ sung khác</td>
                        <%}
                        else
                        { %>
                        <td class="maintd" colspan="2" rowspan="3">Bổ sung khác</td>
                        <%} %>
                        <td class="maintd">10. TL VNS/TL HQCV</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblTLVNS" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">11. TL Kiêm nhiệm</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblTLKiemNhiem" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">12. TT Xếp hạng</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblTTXephang" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <%if (Session["TNGF"] != null)
                        {%>
                    <tr>
                        <td class="maintd">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TT Khác</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblTTKhac" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <%} %>
                    <tr>
                        <td class="maintd" colspan="3">III. Phúc lợi công ty</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblPhucLoiCty" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="2">Phúc lợi công ty</td>
                        <td class="maintd">13. Con nhỏ</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblConNho" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">14. Khác</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblKhac" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3">B. Tổng số tiền người lao động phải nộp</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblTongPhaiNop" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="6">Các khoản người lao động phải nộp</td>
                        <td class="maintd">15. BHXH (10,5%)</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblBHXH" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">16. Thuế thu nhập cá nhân</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblThueTNCaNhan" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">17. Đảng phí</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblDangPhi" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">18. Công đoàn phí</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblCongDoanPhi" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">19. Đoàn phí</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblDoanPhi" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">20. Khác</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblPhaiNopKhac" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3">C. Số tiền người lao động được nhận</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblSoTienDuocNhan" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3">D. Tiền thưởng năm NLĐ được hưởng</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblThuongNam" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="4">Tiền thưởng năm (Công ty chi vào dịp tết nguyên đán)</td>
                        <td class="maintd">21. Tiền thưởng tích luỹ xếp hạng ABC hàng tháng * K1</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblThuongXH" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">22. Tiền thưởng lao động tiên tiến * K2</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblThuongLDTienTien" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">23. Tiền thưởng chiến sĩ thi đua * K3</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblThuongChiSiThiDua" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="maintd">24. Tiền thưởng lương tháng thứ 13 * K4</td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblThuongThang13" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function onCalendarHidden() {
            var cal = $find("tungay");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHiddenFrom() {
            var cal = $find("denngay");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", callFrom);
                    }
                }
            }
        }

        function onCalendarShown() {

            var cal = $find("tungay");

            cal._switchMode("months", true);

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarShownFrom() {

            var cal = $find("denngay");

            cal._switchMode("months", true);

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callFrom);
                    }
                }
            }
        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("tungay");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    //cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

        function callFrom(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("denngay");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    //cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>
</asp:Content>
