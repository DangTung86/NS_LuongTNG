<%@ Page Title="Bảng lương" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BangLuongThang.aspx.cs" Inherits="TNGLuong.BangLuongThang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: table; width: 100%;">
        <div style="width: 100%; height: 60px;">
            <p class="pluong">PHIẾU TRẢ LƯƠNG</p>
            <p style="text-align: center;margin-top: -12px;font-style: italic;color: #999;line-height: 18px;">(Ban hành kèm theo quyết định số: 130/QĐ-TNG ngày 30/01/2023)</p>
        </div>
        <div id="content" class="page-wrap">
            <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server" EnableScriptGlobalization="True"></cc1:ToolkitScriptManager>
            <div style="margin-bottom: 1em;margin-top: 10em;">
                Chọn tháng:
                                <asp:TextBox ID="txtStart" runat="server" CssClass="txtNgayThang" Width="100px" OnTextChanged="txtStart_TextChanged" AutoPostBack="True"></asp:TextBox>
                <cc1:CalendarExtender ID="tungay" ClientIDMode="Static" runat="server" TargetControlID="txtStart" Format="MM-yyyy" DefaultView="Months" OnClientShown="onCalendarShown" OnClientHidden="onCalendarHidden" CssClass="calendar" TodaysDateFormat="MMMM, yyyy"></cc1:CalendarExtender>
            </div>
            <div>
                <table class="maintablenotbottom">
                    <tr>
                        <td class="maintdnotbottom" rowspan="2" colspan="2">A. Tổng thu nhập: </td>
                        <td class="maintd" style="width: 100px; text-align: center;">Số công</td>
                        <td class="maintd" style="width: 100px; text-align: center;">Thu Nhập</td>
                    </tr>
                    <tr>
                        <td class="maintdnotbottom" style="text-align: center;width: 100px;">
                            <asp:Label ID="lblSoCong" runat="server" Text=""></asp:Label>&nbsp;</td>
                        <td class="maintdnotbottom" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblThuNhap" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                </table>
                <table class="maintable">
                    <tr>
                        <td class="maintd" colspan="3" style="font-weight: bold">I. Tiền lương căn cứ đóng Bảo hiểm: </td>
                        <td class="maintd" style="text-align: right;">
                            <asp:Label ID="lblLuongBH" runat="server" Font-Bold="True"></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="4">Tiền lương tham gia bảo hiểm</td>
                        <td class="maintd">1. Mức lương CB</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLuongCoBan" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">2. Mức phụ cấp CV</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLuongPhuCap" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">3. Mức PCCC</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblMucPCCC" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">4. Mức ATVSV</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblMucATVSV" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3" style="font-weight: bold">II. Tiền lương thực trả cho người lao động: </td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLuongThucTra" runat="server" Font-Bold="True"></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="5">Tiền lương</td>
                        <td class="maintd">5. Tiền lương cấp bậc/Sản phẩm</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLuong8h" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">6. Lương thời gian</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLuongThoiGian" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">7. Tiền lương thêm giờ: </td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLuongThemGio" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">7.1 Tiền lương thêm giờ ngày thường</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLgThemGioThuong" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">7.2 Tiền lương thêm giờ ngày nghỉ, ngày lễ</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblLgThemGioNgayNghiLe" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="2">Phụ cấp lương</td>
                        <td class="maintd">8. PC Lương(CV, PCCC, ATVSV)</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblPCLuong" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">9. Phụ cấp lao động nữ</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblPhuCapNu" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="4">Bổ sung khác</td>
                        <td class="maintd">10. TL VNS/TL HQCV</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblTLVNS" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">11. TL Kiêm nhiệm</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblTLKiemNhiem" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">12. TT Xếp hạng <asp:Label ID="lblTTXepHang_Text" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblTTXephang" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">13. TT bổ sung thêm (nếu có)</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblTTBoXungThem" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3" style="font-weight: bold">III. Phúc lợi công ty: </td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblPhucLoiCty" runat="server" Font-Bold="True"></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="6">Phúc lợi công ty</td>
                        <td class="maintd">14. Con nhỏ</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblConNho" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">15. Khác (Xăng xe, ăn ca, ...): </td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblKhac" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">15.1 Ăn ca</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblKhac_AnCa" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">15.2 Xăng xe</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblKhac_XangXe" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">15.3 Hỗ trợ lao động tay nghề yếu</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblKhac_HoTroYeu" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">15.4 Tiền chuyên cần</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblTienChuyenCan" runat="server" Text="0"></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3" style="font-weight: bold">B. Tổng số tiền người lao động phải nộp: </td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblTongPhaiNop" runat="server" Font-Bold="True"></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="2" rowspan="6">Các khoản người lao động phải nộp</td>
                        <td class="maintd">16. BHXH (10,5%)</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblBHXH" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">17. Thuế thu nhập cá nhân</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblThueTNCaNhan" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">18. Đảng phí</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblDangPhi" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">19. Công đoàn phí</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblCongDoanPhi" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">20. Đoàn phí</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblDoanPhi" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd">21. Khác</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblPhaiNopKhac" runat="server" Text=""></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="maintd" colspan="3" style="font-weight: bold">C. Số tiền người lao động được nhận</td>
                        <td class="maintd" style="text-align: right;width: 100px;">
                            <asp:Label ID="lblSoTienDuocNhan" runat="server" Font-Bold="True"></asp:Label>&nbsp;</td>
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


