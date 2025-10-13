<%@ Page Title="Phân cụm trưởng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhanCumTruong.aspx.cs" Inherits="TNGLuong.PhanCumTruong" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;" class="fontsize">
        <table style="width: 100%;">
            <tr>
                <td style="width: 35%;">Chọn tổ:</td>
                <td style="width: 65%;">
                    <asp:DropDownList ID="ddlToMay" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlToMay_SelectedIndexChanged" DataTextField="TenPhongban" DataValueField="PhongBanID" CssClass="margin-top"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 35%;">Chọn mã hàng:</td>
                <td style="width: 65%;">
                    <asp:DropDownList ID="ddlMaHang" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMaHang_SelectedIndexChanged" DataTextField="TenMaHang" DataValueField="STT" CssClass="margin-top"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%; height: 110px;">
                    <p style="font-family: Tahoma;color: red;font-weight: bold;padding-top:10px;width:100%;" id="pnameCNDN" runat="server">Phân cụm trưởng</p>
                    <asp:GridView ID="gridPhanCumTrg" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gridPhanToNK_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="STT" SortExpression="STT">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>                                    
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("STT") %>' Width="100%"></asp:Label>
                                    <asp:Label ID="lblID_Cum" runat="server" Text='<%# Eval("ID_Cum") %>' Visible="false"></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tên cụm" SortExpression="TenCum">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTenCum" runat="server" Text='<%# Eval("TenCum") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tên cụm trưởng">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlHoTen" runat="server" Width="100%"></asp:DropDownList>
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
