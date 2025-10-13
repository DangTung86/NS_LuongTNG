<%@ Page Title="Phân công công đoạn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhanCongCongDoan.aspx.cs" Inherits="TNGLuong.PhanCongCongDoan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;" class="fontsize">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%; text-align: center; font-weight: bold; font-size: 14px; padding-bottom: 15px;">DANH SÁCH CÔNG ĐOẠN CÔNG NHÂN</td>
            </tr>
            <tr>


                <td style="width: 100%;" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Ngày: " Width="80px" CssClass="text_left"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" Width="60%" AutoPostBack="True" format="dd/MM/yyyy" TextMode="Date" Enabled="False"></asp:TextBox>
                </td>

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
                    <asp:Label ID="lblTenMH" runat="server" Text="Mã hàng" Width="80px" CssClass="margin-top"></asp:Label>
                    <asp:DropDownList ID="ddlMaHang" runat="server" Width="60%" DataTextField="MaHang" DataValueField="STT" AutoPostBack="True" CssClass="margin-top" OnSelectedIndexChanged="ddlMaHang_SelectedIndexChanged"></asp:DropDownList>
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
                <td style="width: 100%; margin-bottom: 10px;">
                    <br />
                    <asp:ListView ID="lvUser" runat="server" OnItemDataBound="lvUser_ItemDataBound" ItemPlaceholderID="itemPlaceHolder" OnItemCommand="lvUser_ItemCommand">
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li style="padding-bottom: 10px;">
                                <div style="width: 100%; padding-left: 10px; background-color: #006699; color: #ffffff; font-size: 10pt;">

                                    <b>
                                        <span>
                                            <asp:Label ID="lblHoTen" runat="server" Text='<%# Eval("HoTen") %>' style="color:White;"></asp:Label>
                                            -
                                            <asp:Label ID="lblMaNS_ID" runat="server" Text='<%# Eval("MaNS_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblMaNS" runat="server" Text='<%# Eval("MaNS") %>' style="color:White;"></asp:Label>
                                        </span>
                                        <span style="float: right;">
                                            <asp:Button ID="btnXemCT" runat="server" Text="Xem chi tiết" CommandArgument='<%# Eval("MaNS_ID") %>' CommandName="ChiTiet" style="background-color: LightSalmon;color:#006699;padding-left:5px;padding-right:5px;"/>
                                        </span>
                                    </b>
                                </div>
                                <asp:GridView ID="gridDanhSachNhanSu" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="fontsize" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="TenCum" HeaderText="Tên cụm">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TenCongDoan" HeaderText="Tên công đoạn">
                                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TyLe_Giao" HeaderText="Tỷ lệ giao" DataFormatString="{0:#,0.##}">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="NgayApDung" HeaderText="Ngày áp dụng">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>

                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#4e94cf" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
    </div>
    <div class="modal_heigh fade modal-addThis modal-contactform in fontsize" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="margin-bottom: 70vh;">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="text-align: left; font-weight: bold; font-family: Tahoma;">PHÂN CÔNG CÔNG ĐOẠN CHO CÔNG NHÂN</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server" style="margin-top: -33px;">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;" colspan="2">
                                <asp:Label ID="lblNgayApDung" runat="server" Text="Ngày áp dụng:" Width="80px" CssClass="margin-top"></asp:Label>
                                <asp:TextBox ID="txtNgayApDung" runat="server" CssClass="txtNgayThang" Width="60%" AutoPostBack="True" format="dd/MM/yyyy" TextMode="Date"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 100%;">
                                <div style="font-weight: bold; color: red; float: left; margin-top: 10px;">
                                    <asp:Label ID="lblChiTiet" runat="server" Text="Chi tiết công đoạn cá nhân:"></asp:Label>
                                    <asp:Label ID="lblPhongBan_ID" runat="server" Text="phongbanId" Visible="false"></asp:Label>
                                    <asp:Label ID="lblMaNS_IDCN" runat="server" Text="lblMaNS_ID" Visible="false"></asp:Label>
                                </div>
                                <div class="topnav">
                                    <div class="search-container">
                                        <input type="text" class="textbox" placeholder="Tìm theo tên công đoạn.." name="search" id="txtSearchTenCD" runat="server">
                                        <button type="submit" id="btnSearchTenCD" runat="server"><i class="fa fa-search"></i></button>

                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gridPhanCongCongDoan" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
                                    Width="100%" BackColor="White" Style="margin-top: -0.2em;" ShowHeaderWhenEmpty="True" OnRowCommand="gridPhanCongCongDoan_ItemCommand">
                                    <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                                    <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                                    <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                                    <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tên Cụm">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaNS_ID" runat="server" Text='<%#Eval("MaNS_ID") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblTenCum" runat="server" Text='<%#Eval("TenCum") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Công đoạn">
                                            <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="30%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblID_CongDoan" runat="server" Text='<%#Eval("ID_CongDoan") %>' Width="100%" Visible="false"></asp:Label>
                                                <asp:Label ID="lblTenCongDoan" runat="server" Text='<%#Eval("TenCongDoan") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tổng KH">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTongKH" runat="server" Text='<%#Eval("TongKH") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TGCN">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTGCN" runat="server" Text='<%#Eval("TGCN") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kế hoạch">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKeHoach" runat="server" Text='<%#Eval("TyLe_Giao") %>' Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Xem chi tiết">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnXemCTNS" runat="server" ImageUrl="~/images/detail.png" Width="24px"
                                                    AlternateText="Xem chi tiết" CommandArgument='<%# Eval("ID_CongDoan") %>' CommandName="ChiTietNhanSu" />
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
                                <asp:Button ID="btnSavePhanCong" runat="server" Text="Lưu lại" CssClass="btnSave" OnClick="btnSavePhanCong_Click" />
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
    <div class="modal_heigh fade modal-addThis modal-contactform in fontsize" id="modalNhanSuCongDoan" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="margin-bottom: 70vh;">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="text-align: left; font-weight: bold; font-family: Tahoma;">CHI TIẾT NHÂN SỰ CHO CÔNG ĐOẠN</p>
                        <button type="button" class="close" data-dismiss="modal" id="closeNhanSuCongDoan" runat="server" style="margin-top: -33px;">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gridChiTietNhanSuCongDoan" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
                                    Width="100%" BackColor="White" Style="margin-top: -0.2em;" ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                                    <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                                    <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                                    <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Họ tên nhân viên">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblHoTenNV" runat="server" Text='<%#Eval("HoTen") %>' Width="100%"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tỷ lệ giao">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTyLeGiaoNV" runat="server" Text='<%#Eval("TyLe_Giao") %>' Width="100%"></asp:Label>
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
                                <asp:Button ID="btnCloseNhanSuCongDoan" runat="server" Text="Đóng" CssClass="btnSave" OnClick="closeNhanSuCongDoan_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
