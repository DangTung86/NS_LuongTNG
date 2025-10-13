<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Orther.ascx.cs" Inherits="TNGLuong.WebAdmin.production.Orther" %>
<div class="x_panel">
    <div class="x_title">
        <h2>Chức năng khác</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <asp:Button ID="btnMempryCache" runat="server" Text="Đồng bộ memory cache" CssClass="btn btn-primary" OnClick="btnMempryCache_Click" />
        <asp:Button ID="btnDBNangSuat" runat="server" Text="Đồng bộ năng suất" CssClass="btn btn-primary" OnClick="btnDBNangSuat_Click" />
        <asp:Button ID="btnDBCapBTP" runat="server" Text="Đồng bộ số cấp BTP" CssClass="btn btn-primary" OnClick="btnDBCapBTP_Click" />
        <asp:Button ID="btnShow_Hide_BL" runat="server" Text="Ẩn bảng lương" CssClass="btn btn-primary" OnClick="btnShow_Hide_BL_Click" Visible="False" />
    </div>
</div>
<!-- Show the cropped image in modal -->
<div class="modal fade docs-cropped show" id="divMesssenger" runat="server" aria-hidden="true"  role="dialog" tabindex="-1" style="display: none; padding-right: 16px;">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Thông báo</h4>
                <button type="button" class="close" id="btnclose" runat="server" data-dismiss="modal" aria-hidden="true">&times;</button>
            </div>
            <div class="modal-body"></div>
            <div class="modal-footer" style="display: table-caption;">
                <asp:Label ID="lblMessenger" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </div>
</div>
<!-- /.modal -->
