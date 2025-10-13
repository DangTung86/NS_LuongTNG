<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResetPass.ascx.cs" Inherits="TNGLuong.WebAdmin.production.ResetPass" %>
<div class="x_panel">
    <div class="x_title">
        <h2>Reset mật khẩu</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div class="x_title">
                        <h3 style="font-style:italic;">Reset mật khẩu của một mã nhân sự</h3>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Mã nhân sự<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <input id="txtmans" runat="server" class="form-control" data-validate-length-range="6" data-validate-words="2" name="name" placeholder="vd: TNGxxxxxx" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="ln_solid"></div>
                    <div class="item form-group">
                        <div class="col-md-6 col-sm-6 offset-md-3">
                            <asp:Button ID="btnLuu" runat="server" Text="Reset mật khẩu" CssClass="btn btn-primary" OnClick="btnLuu_Click" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%;">
            <tr>
                <td>
                    <div class="x_title">
                        <h3 style="font-style:italic;">Reset mật khẩu của cả tổ may</h3>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Đơn vị <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="ddlDonVi" runat="server" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Tổ may <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="ddlToMay" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="ln_solid"></div>
                    <div class="item form-group">
                        <div class="col-md-6 col-sm-6 offset-md-3">
                            <asp:Button ID="btnResetToMay" runat="server" Text="Reset mật khẩu" CssClass="btn btn-primary" OnClick="btnResetToMay_Click" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>

    </div>
</div>
<!-- Show the cropped image in modal -->
<div class="modal fade docs-cropped show" id="divMesssenger" runat="server" aria-hidden="true" role="dialog" tabindex="-1" style="display: none; padding-right: 16px;">
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
