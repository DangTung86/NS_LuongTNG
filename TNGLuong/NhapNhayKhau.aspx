<%@ Page Title="Nhập nhảy khâu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhapNhayKhau.aspx.cs" Inherits="TNGLuong.NhapNhayKhau" %>

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
                <td style="width: 35%;">
                    <asp:Label ID="lblTo" runat="server" Text="Tổ" CssClass="margin-top"></asp:Label></td>
                <td style="width: 65%;">
                    <asp:DropDownList ID="ddlToMay" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlToMay_SelectedIndexChanged" DataTextField="TenPhongban" DataValueField="PhongBanID" CssClass="margin-top"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 35%;">
                    <asp:Label ID="lblTenMH" runat="server" Text="Mã hàng" CssClass="margin-top"></asp:Label></td>
                <td style="width: 65%;">                    
                    <asp:DropDownList ID="ddlMaHang" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlMaHang_SelectedIndexChanged" DataTextField="TenMaHang" DataValueField="STT" CssClass="margin-top"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 0px;"></td>
                            <td style="width: 100%;">
                                <div style="margin-top: 10px; float: left; font-size: 10pt; font-weight: bold; color: red;">
                                    <asp:Label ID="Label3" runat="server" Text="Công đoạn nhảy khâu" CssClass="margin-top"></asp:Label>
                                </div>
                                <div class="topnav">
                                    <div class="search-container">
                                        <input type="text" class="textbox" placeholder="Tìm công đoạn.." name="search" id="txtsearch" runat="server">
                                        <button type="submit" id="btnSreach" runat="server"><i class="fa fa-search"></i></button>

                                    </div>
                                    <div class="search-container">
                                        <button type="submit" id="btnGetSelectData" runat="server" style="float: right; margin-right: 5px;">DL đã chọn</button>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">

                                <asp:GridView ID="gridNangSuatNhayKhau" runat="server" BorderColor="#CCCCCC" BorderStyle="None"
                                    BorderWidth="1px" AutoGenerateColumns="False" Width="100%" BackColor="White" OnRowDataBound="gridNangSuatNhayKhau_RowDataBound">
                                    <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                                    <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                                    <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                                    <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Mã hàng" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblID_CongDoanNhayKhau" runat="server" Text='<%#Eval("ID_CongDoan") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblPhongBanIDNhayKhau" runat="server" Text='<%#Eval("PhongBanID") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblNhomSizeNhayKhau" runat="server" Text='<%#Eval("NhomSize") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblID_CachMayNhayKhau" runat="server" Text='<%#Eval("ID_CachMay") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblIsBTP" runat="server" Text='<%#Eval("IsBTP") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblMaHangNhayKhau" runat="server" Text='<%#Eval("Mahang") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="8px" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Công đoạn">
                                            <HeaderStyle Width="60%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="60%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTenCongDoanNhayKhau" runat="server" Text='<%#Eval("TenCongDoan") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Số cấp BTP">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                 <asp:Label ID="lblSoLuong_CapBTP" runat="server" Text='<%#Eval("SoLuong_CapBTP") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lũy kế">
                                            <HeaderStyle Width="12%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="12%" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDaPDNhayKhau" runat="server" Text='<%#Eval("DonGia","{0:#,0.##}") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblLuyKeCD" runat="server" Text='<%#Eval("LuyKe","{0:#,0.##}") %>' Width="100%"></asp:Label>                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NS">
                                            <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="15%"></ItemStyle>
                                            <ItemTemplate>                                                
                                                <asp:TextBox ID="txtCongNhanNhayKhau" runat="server" Text='<%#Eval("CongNhan") %>' Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                            <td colspan="2" style="width: 45%" align="center">
                                <asp:Button ID="btnSaveNhayKhau" runat="server" Text="Lưu" CssClass="btnSave" OnClick="btnSaveNhayKhau_Click" />
                                <asp:Button ID="btnHuy" runat="server" Text="Hủy" CssClass="btnSave" OnClick="btnHuy_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="modal fade modal-addThis modal-contactform in" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="color: #039; font-family: Tahoma;">Thông báo</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <asp:Label ID="lblMessenger" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <br />
                    <div style="width: 215px; margin: auto" runat="server" id="divBTN">
                        <asp:Button ID="btnOK" runat="server" Text="Có" CssClass="btnSave" OnClick="btnOK_Click" Width="100px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Không" CssClass="btnSave" OnClick="btnclose_Click" Width="100px" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
