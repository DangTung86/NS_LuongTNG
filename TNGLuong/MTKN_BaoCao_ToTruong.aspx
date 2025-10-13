<%@ Page Title="Báo cáo ma trận kỹ năng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MTKN_BaoCao_ToTruong.aspx.cs" Inherits="TNGLuong.MTKN_BaoCao_ToTruong" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<style>

    div.wrap-main:has(#MainContent_gridLoaiMay) {
        width: 100% !important;
    }

    #MainContent_gridLoaiMay {
        max-width: 97vw;
        max-height: 85vh;
        overflow: scroll;
        display: block;
        margin-top: 20px !important;
    }
    #MainContent_gridLoaiMay th {
        min-width: 130px;
        padding: 0 5px;
    }
    #MainContent_gridLoaiMay tr:nth-child(1) {
        /* Kiểu dáng cho 3 thẻ tr đầu tiên */

        position: sticky;
        top: 0;
        z-index: 1;

        background-color: #006699;
        height: 37px;
    }
    #MainContent_gridLoaiMay tr:nth-child(2) {
        position: sticky;
        top: 35px;
        z-index: 1 !important;
    }
    #MainContent_gridLoaiMay tr:nth-child(3) {
        position: sticky;
        top: 63px;
        z-index: 1 !important;
    }
    #MainContent_gridLoaiMay tr:nth-child(4) {
        position: sticky;
        top: 90px;
        z-index: 1;
    }
    #MainContent_gridLoaiMay tr:nth-child(5) {
        position: sticky;
        top: 117px;
        z-index: 1;
    }
    #MainContent_gridLoaiMay td {
        height: 30px;
    }

    #MainContent_gridLoaiMay tr td:first-child, #MainContent_gridLoaiMay tr th:first-child {
        position: sticky;
        left: 0;
        background-color: #006699;
    }
    #MainContent_gridLoaiMay tr td.first-column:nth-child(-n+3) {
        background-color: red !important;
    }
    #MainContent_gridLoaiMay select{
        width: 90%;
        height: 90%;
        border: none;
        background: transparent;
    }

        .auto-style1 {
            font-family: Tahoma;
            font-size: 12px;
            width: 100%;
            
        }
        .auto-style2 {
            width: 45%;
            height: 37px;
        }
        .auto-style3 {
            width: 100%;
            height: 354px;
        }

        .btnEdit{

        }
    #MainContent_gridLoaiMay select{
        color:#000066;
    }

</style>

    <div class="modal fade modal-addThis modal-contactform in" id="messageShow" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div id="divThongBao" runat="server" class="modal-dialog modal-dialog-centered" style="display: none;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="color: #039;">Thông báo</p>
                        <button type="button" class="close" data-dismiss="modal" id="CloseMessage" runat="server">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <asp:Label ID="lblMessenger" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div style="font-family: Tahoma; font-size: 12px;" class="auto-style1">
        <table class="auto-style3">
            <tr>
                <td style="width: 100%; text-align: center; font-weight: bold; font-size: 14px; padding-bottom: 15px;">DANH SÁCH LOẠI MÁY</td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <asp:Label ID="lblDonVi" runat="server" Text="Đơn Vị" Width="80px" CssClass="margin-top"></asp:Label>
                    <asp:TextBox ID="txtTenDonVi" runat="server" CssClass="margin-top" Width="60%" AutoPostBack="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <asp:Label ID="lblToMay" runat="server" Width="80px" Text="Tổ may" CssClass="margin-top"></asp:Label>
                    <asp:DropDownList ID="ddlToMay" runat="server" Width="60%" DataTextField="TenPhongban" DataValueField="PhongBanID" AutoPostBack="True" CssClass="margin-top" Enabled="False"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <asp:Label ID="lblLanDanhGia" runat="server" Text="Lần đánh giá" Width="80px" CssClass="margin-top"></asp:Label>
                    <asp:DropDownList ID="ddlLanDanhGia" runat="server" Width="60%" DataTextField="GhiChu" DataValueField="ID_LanDanhGia" AutoPostBack="True" CssClass="margin-top" OnSelectedIndexChanged="ddlLanDanhGia_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <asp:Label ID="lblNS" runat="server" Text="Số nhân sự :" Width="80px" CssClass="margin-top"></asp:Label>
                    <asp:TextBox ID="txtSoNS" runat="server" CssClass="margin-top" Width="60%" AutoPostBack="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; display: block;">
                    <div class="topnav">
                        <div class="search-container">
                            <input type="text" class="textbox" placeholder="Tìm theo tên, mã nv.." name="search" id="txtsearchTenCN" runat="server" visible="False">
                            <button type="submit" id="btnSearchTenCN" runat="server" visible="False"><i class="fa fa-search"></i></button>

                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridLoaiMay" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
                        Width="100%" BackColor="White" Style="margin-top: -0.2em;" ShowHeaderWhenEmpty="True" OnRowCommand="gridLoaiMay_ItemCommand" OnSelectedIndexChanged="gridLoaiMay_SelectedIndexChanged" OnRowDataBound="gridLoaiMay_RowDataBound" OnRowUpdated="gridLoaiMay_RowUpdated">
                        <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                        <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                        <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                        <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="MaNS_ID" HeaderText="MaNS_ID" >
                            <ControlStyle Width="0px" />
                            <HeaderStyle Width="0px" />
                            <ItemStyle Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HoTen" HeaderText="Họ tên">
                            <ItemStyle BackColor="#1384BD" ForeColor="White" HorizontalAlign="Center" />
                            </asp:BoundField>
                         
                        </Columns>
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" class="auto-style2">
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btnSave" OnClick="btnUpdate_Click" Text="Cập nhật" />
                    <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btnSave" OnClick="btnSaveMucTieu_Click" />
                    <asp:Button ID="btnHuy" runat="server" Text="Hủy" CssClass="btnSave" OnClick="btnHuy_Click" />
                    <asp:Button ID="btnXacNhan" runat="server" Text="Xác nhận" CssClass="btnSave" OnClick="btnXacNhan_Click"  OnClientClick="return checkCondition();" />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function checkCondition() {
            return confirm('Sau khi xác nhận, bạn sẽ không thể sửa, bạn có muốn tiếp tục?');
        }
    </script>
    </asp:Content>

