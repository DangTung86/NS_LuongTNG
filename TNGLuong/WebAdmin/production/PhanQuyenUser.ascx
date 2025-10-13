<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhanQuyenUser.ascx.cs" Inherits="TNGLuong.WebAdmin.production.PhanQuyenUser" %>
<div class="x_panel">
    <div class="x_title">
        <h2>Phân quyền chức năng quản trị.</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content scrollbar">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3 label-align">Mã nhân sự <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6 ">
                            <asp:TextBox ID="txtMaNS" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Chức năng <%--<span class="required">*</span>--%></label>
                        <div class="col-md-6 col-sm-6" style="margin-top: 5px;">
                            <asp:CheckBoxList ID="chkList_ChucNang" runat="server" Width="100%" CssClass="mycheckbox"></asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Kích hoạt Chức năng <%--<span class="required">*</span>--%></label>
                        <div class="col-md-6 col-sm-6" style="margin-top: 5px;">
                            <asp:CheckBox ID="chkActive" runat="server" Checked="True" />
                        </div>
                    </div>
                    <div class="item form-group">
                        <div class="col-md-6 col-sm-6 offset-md-3">
                            <asp:Button ID="btnSave" runat="server" Text="Cập nhật" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <div style="display: inline;">
                        <div>
                            <div class="search-box">
                                <form>
                                    <input type="text" placeholder="Tìm mã nhân sự..." id="txtSearch" runat="server">
                                    <input type="submit" value="" id="btnSearch" runat="server">
                                </form>
                            </div>
                        </div>
                        <div>
                            <asp:GridView ID="gridPhanQuyen" runat="server" ShowFooter="false" AutoGenerateColumns="False" BackColor="White" DataKeyNames="MaNS" OnSelectedIndexChanged="gridPhanQuyen_SelectedIndexChanged"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnRowDataBound="gridPhanQuyen_RowDataBound" OnRowDeleting="gridPhanQuyen_RowDeleting">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="Chọn" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" />
                                    <asp:TemplateField HeaderText="Mã nhân sự" SortExpression="MaNS">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaNS" Text='<%#Eval("MaNS") %>' runat="server" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Họ tên" SortExpression="HoTen">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblHoTen" Text='<%#Eval("HoTen") %>' runat="server" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Chức vụ" SortExpression="TenChucVu">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTenChucVu" Text='<%#Eval("TenChucDanh") %>' runat="server" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Đơn vị" SortExpression="TenDonVi">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTenDonVi" Text='<%#Eval("TenDonVi") %>' runat="server" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Active">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkKichHoat" runat="server" Enabled="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Vai trò" SortExpression="VaiTro">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVaiTro" Text='<%#Eval("VaiTro") %>' runat="server" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Xóa">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="cmdDelete" runat="server" AlternateText="Xóa" CommandArgument='<%#Eval("MaNS") %>' CommandName="Delete" ImageUrl="~/images/delete.png" Width="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
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
