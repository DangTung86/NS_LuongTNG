<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsertMaHang.ascx.cs" Inherits="TNGLuong.WebAdmin.production.InsertMaHang" %>
<div class="x_panel">
    <div class="x_title">
        <h2>Cập nhật mã hàng bỏ BTP</h2>
        <div class="clearfix"></div>
    </div>
    <div class="x_content">
        <table style="width: 100%;">
            <tr>
                <td>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3 label-align">Tháng <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6 ">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="date-picker form-control" Width="100%" AutoPostBack="True" format="yyyy-MM" TextMode="Month" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Chi nhánh <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="ddlDonVi" runat="server" OnSelectedIndexChanged="ddlDonVi_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Tổ may <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="ddlToMay" runat="server" OnSelectedIndexChanged="ddlToMay_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Mã hàng <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="ddlMaHang" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="ln_solid"></div>
                    <div class="item form-group">
                        <div class="col-md-6 col-sm-6 offset-md-3">
                            <asp:Button ID="btnLuu" runat="server" Text="Cập nhật" CssClass="btn btn-primary" OnClick="btnLuu_Click" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div class="ln_solid"></div>
                    <div class="item form-group">
                        <div class="col-md-12 col-sm-12">
                            <asp:GridView ID="gvMaHangNotBTP" runat="server" ShowFooter="False" AutoGenerateColumns="False" DataKeyNames="Id_BTP" Width="100%" OnRowDataBound="gvMaHangNotBTP_RowDataBound"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDeleting="gvMaHangNotBTP_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="STT">
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Đơn vị" SortExpression="TenDonVi">
                                        <HeaderStyle HorizontalAlign="Center"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTenDonVi" Text='<%#Eval("TenDonVi") %>' runat="server" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Tổ may" SortExpression="TenPhongBan">
                                        <HeaderStyle HorizontalAlign="Center"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTenPhongBan" Text='<%#Eval("TenPhongBan") %>' runat="server" Width="100%"></asp:Label>
                                            <asp:Label ID="lblPhongBanID" Text='<%#Eval("PhongBanID") %>' runat="server" Width="100%" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mã hàng" SortExpression="MaHang">
                                        <HeaderStyle HorizontalAlign="Center"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaHang" Text='<%#Eval("MaHang") %>' runat="server" Width="100%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    

                                    <asp:TemplateField>
                                        <HeaderStyle Width="22px" />
                                        <ItemStyle HorizontalAlign="Center" Width="22px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="cmdDelete" runat="server" AlternateText="Xóa" CommandArgument='<%#Eval("Id_BTP") %>' CommandName="Delete" ImageUrl="~/images/delete.png" Width="20px" />
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
