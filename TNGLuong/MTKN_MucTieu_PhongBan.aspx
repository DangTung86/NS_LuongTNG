<%@ Page Title="Ma trận kỹ năng công nhân" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MTKN_MucTieu_PhongBan.aspx.cs" Inherits="TNGLuong.MTKN_MucTieu_PhongBan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;" class="fontsize">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%; text-align: center; font-weight: bold; font-size: 14px; padding-bottom: 15px;">DANH SÁCH LOẠI MÁY</td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="2">
                    <asp:Label ID="lblDonVi" runat="server" Text="Đơn Vị" Width="80px" CssClass="margin-top"></asp:Label>
                    <asp:TextBox ID="txtTenDonVi" runat="server" CssClass="margin-top" Width="60%" AutoPostBack="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="2">
                    <asp:Label ID="lblToMay" runat="server" Width="80px" Text="Tổ may" CssClass="margin-top"></asp:Label>
                    <asp:DropDownList ID="ddlToMay" runat="server" Width="60%" DataTextField="TenPhongban" DataValueField="PhongBanID" AutoPostBack="True" CssClass="margin-top" Enabled="False"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="2">
                    <asp:Label ID="lblLanDanhGia" runat="server" Text="Lần đánh giá" Width="80px" CssClass="margin-top"></asp:Label>
                    <asp:DropDownList ID="ddlLanDanhGia" runat="server" Width="60%" DataTextField="GhiChu" DataValueField="ID_LanDanhGia" AutoPostBack="True" CssClass="margin-top" OnSelectedIndexChanged="ddlLanDanhGia_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="2">
                    <asp:Label ID="lblNS" runat="server" Text="Số nhân sự :" Width="80px" CssClass="margin-top"></asp:Label>
                    <asp:TextBox ID="txtSoNS" runat="server" CssClass="margin-top" Width="60%" AutoPostBack="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <div class="topnav">
                        <div class="search-container">
                            <input type="text" class="textbox" placeholder="Tìm theo tên, mã nv.." name="search" id="txtsearchTenCN" runat="server">
                            <button type="submit" id="btnSearchTenCN" runat="server"><i class="fa fa-search"></i></button>

                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridLoaiMay" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
                        Width="100%" BackColor="White" Style="margin-top: -0.2em;" ShowHeaderWhenEmpty="True" OnRowCommand="gridLoaiMay_ItemCommand">
                        <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                        <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                        <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                        <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID_MucTieu" Visible="false">
                                <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblID_MucTieu" runat="server" Text='<%#Eval("ID_MucTieu") %>' Width="100%" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Loại máy">
                                <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblID_LoaiMay" runat="server" Text='<%#Eval("ID_LoaiMay") %>' Width="100%" Visible="false"></asp:Label>
                                    <asp:Label ID="lblTenLoaiMay" runat="server" Text='<%#Eval("TenLoaiMay") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phần trăm mục tiêu">
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTyLe" runat="server" Text='<%#Eval("TyLeDat") +"%" %>' Width="100%" ReadOnly="true" HorizontalAlign="Center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mục tiêu số người">
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox TextMode="Number" ID="txtSoNguoi" runat="server" Text='<%#Eval("SoNguoi") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thực hiện">
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox TextMode="Number" ID="txtThucHien" runat="server" Text='<%#Eval("ThucHien") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cần đào tạo thêm">
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="15%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:TextBox TextMode="Number" ID="txtConLai" runat="server" Text='<%#Eval("ConLai") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Đăng ký đào tạo">
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnXemCTNS" runat="server" ImageUrl="~/images/detail.png" Width="24px"
                                        AlternateText="Đăng ký" CommandArgument='<%# Eval("ID_LoaiMay") %>' CommandName="ChiTietNhanSu" />
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
                <td style="width: 45%" align="center">
                    <asp:Button ID="btnSaveMucTieu" runat="server" Text="Lưu" CssClass="btnSave" OnClick="btnSaveMucTieu_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="modal_heigh fade modal-addThis modal-contactform in fontsize" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="margin-bottom: 70vh;">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="text-align: left; font-weight: bold; font-family: Tahoma;">ĐÁNH GIÁ KỸ NĂNG CHO CÔNG NHÂN</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server" style="margin-top: -33px;" onclick="btnclose_Click">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">

                    <table style="width: 100%;">

                        <tr>
                            <td style="width: 100%;">

                                <div class="topnav">
                                    <div class="search-container">
                                        <input type="text" class="textbox" placeholder="Tìm theo tên loại máy.." name="search" id="txtSearchTenMay" runat="server">
                                        <input type="text" class="textbox" name="ID_LoaiMay" id="txtID_LoaiMay" runat="server" visible="false">
                                        <input type="text" class="textbox" name="LanDanhGia" id="txtLanDanhGia" runat="server" visible="false">
                                        <input type="text" class="textbox" name="ID_PhongBan" id="txtPhongPhanID" runat="server" visible="false">
                                        <button type="submit" id="btnSearchTenMay" runat="server"><i class="fa fa-search"></i></button>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gridDanhSachLoaiMay" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
                                    Width="100%" BackColor="White" Style="margin-top: -0.2em;" ShowHeaderWhenEmpty="True" OnRowDataBound="gridDanhSachLoaiMay_RowDataBound">
                                    <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                                    <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                                    <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                                    <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <HeaderStyle Width="40%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="40%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Họ tên">
                                            <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="20%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaNS_ID" runat="server" Text='<%#Eval("MaNS_ID") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblHoTen" runat="server" Text='<%#Eval("HoTen") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Xếp hạng">
                                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlXepHang" runat="server" Width="100%"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mục tiêu">
                                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlMucTieu" runat="server" Width="100%"></asp:DropDownList>
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
                                <asp:Button ID="btnSaveDanhGia" runat="server" Text="Lưu lại" CssClass="btnSave" OnClick="btnSaveDanhGia_Click" />
                                <asp:Button ID="btnHuy" runat="server" Text="Hủy" CssClass="btnSave" OnClick="btnHuy_Click" />
                            </td>
                        </tr>


                    </table>
                </div>
            </div>
        </div>
    </div>
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

</asp:Content>
