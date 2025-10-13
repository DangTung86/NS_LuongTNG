<%@ Page Title="Phân tổ nhảy khâu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhanToNhayKhau.aspx.cs" Inherits="TNGLuong.PhanToNhayKhau" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 35%;">Chọn ngày:</td>
                <td style="width: 65%;">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" format="dd/MM/yyyy" Width="100%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%;">
                    <p style="font-family: Tahoma;color: red;font-weight: bold;padding-top:10px;width:100%;" id="pnameCNDN" runat="server">Phân tổ nhảy khâu</p>
                    <asp:GridView ID="gridPhanToNK" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gridPhanToNK_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Mã NS" SortExpression="MaNS">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaNS" runat="server" Text='<%# Eval("MaNS_ID") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("MaNS") %>' Width="100%"></asp:Label>
                                    <asp:Label ID="lblPhongBanID_NS" runat="server" Text='<%# Eval("PhongBanID_NS") %>' Visible="false"></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Họ tên" SortExpression="HoTen">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHoTen" runat="server" Text='<%# Eval("HoTen") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tổ NK 1">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPhongBan1" runat="server" Width="100%"></asp:DropDownList>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tổ NK 2">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPhongBan2" runat="server" Width="100%"></asp:DropDownList>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tổ NK 3">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPhongBan3" runat="server" Width="100%"></asp:DropDownList>
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
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%;" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btnSave" OnClick="btnSave_Click"/>
                </td>
            </tr>
        </table>
    </div>
    <div class="modal fade modal-addThis modal-contactform in" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
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
