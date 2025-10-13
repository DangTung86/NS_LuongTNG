<%@ Page Title="Khai báo y tế định kỳ" Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KhaiBaoSucKhoe.aspx.cs" Inherits="TNGLuong.KhaiBaoSucKhoe" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;">
        <table style="width: 100%;">
           <tr>
                <td style="width: 100%; text-align: center; font-weight: bold; font-size: 14px; padding-bottom: 15px;" colspan="2">NHÂN VIÊN KHAI BÁO SỨC KHỎE</td>
            </tr>

             <tr>
                <td style="width: 35%;">
                    <asp:Label ID="lblNhomBenh" runat="server" Text="Loại bệnh" CssClass="margin-top"></asp:Label></td>
                <td style="width: 65%;">
                    <asp:DropDownList ID="ddlNhomBenh" runat="server" Width="100%" DataTextField="TenNhomBenh" DataValueField="Id" AutoPostBack="False" CssClass="margin-top" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 35%;">
                    <asp:Label ID="lblDiaDiemDieuTri" runat="server" Text="Nơi điều trị" CssClass="margin-top"></asp:Label></td>
                <td style="width: 65%;">
                    <asp:DropDownList ID="ddlDiaDiem" runat="server" Width="100%" DataTextField="TenDiaDiem" DataValueField="Id" AutoPostBack="False" CssClass="margin-top" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 35%;">Tên bệnh:</td>
                <td style="width: 65%;padding-top: 5px;">
                    <asp:TextBox ID="txtTenBenh" runat="server" Width="100%"  CssClass="textbox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 35%;padding-top: 5px;">Phương pháp điều trị:</td>
                <td style="width: 65%;padding-top: 5px;">
                    <asp:TextBox ID="txtPhuongPhapDT" runat="server" Width="100%"  CssClass="textbox"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td style="margin: 10px; width: 35%;">Thời gian điều trị:</td>
                <td style="width: 65%;padding-top: 5px;">
                    <asp:TextBox ID="txtTuNgay" runat="server" CssClass="txtNgayThang" Width="40%"  format="dd/MM/yyyy" TextMode="Date" ></asp:TextBox>
                     <span style="width: 25%; margin:10px;"> đến </span>
                     <asp:TextBox ID="txtDenNgay" runat="server" CssClass="txtNgayThang" Width="40%"  format="dd/MM/yyyy" TextMode="Date" ></asp:TextBox>
              
            </tr>
            <tr>
                <td style="width: 35%; height: 21px;">
                    <asp:Label ID="lblKetQua" runat="server" Text="Kết quả điều trị" CssClass="margin-top"></asp:Label></td>
                <td style="width: 65%; height: 21px;padding-top: 5px;">
                    <asp:CheckBox ID="chkDangDieuTri" runat="server" AutoPostBack="true" Checked="True" Text="Đang điều trị" OnCheckedChanged="chkDangDieuTri_CheckedChanged"/>
                     <asp:CheckBox ID="chkDaKhoi" runat="server" AutoPostBack="true" Text="Đã Khỏi" OnCheckedChanged="chkDaKhoi_CheckedChanged"/>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 45%" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Cập nhật" CssClass="btnSave" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
     <div class="modal fade modal-addThis modal-contactform in" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div id="divThongBao" runat="server" class="modal-dialog modal-dialog-centered" style="display: none;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="color: #039;">Thông báo</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <asp:Label ID="lblMessenger" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
