<%@ Page Title="Duyệt NS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DuyetNSCN.aspx.cs" Inherits="TNGLuong.DuyetNSCN" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;" class="fontsize">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;" align="center" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Chọn ngày: " Width="80px" CssClass="text_left"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" format="dd/MM/yyyy" Width="80%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; margin-bottom: 10px;">
                    <br />
                    <p style="font-family: Tahoma;color: red;font-weight: bold;" id="pnameCNCN" runat="server">Danh sách chưa nhập năng suất</p>
                    <asp:GridView ID="gridCongNhanChuaNhap" AutoGenerateColumns="False" Width="100%" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <Columns>
                            <asp:BoundField DataField="MaNS_ID" HeaderText="MaNS_ID" Visible="False" />
                            <asp:BoundField DataField="MaNS" HeaderText="Mã nhân sự">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HoTen" HeaderText="Họ tên">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Điện thoại">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                <ItemTemplate>
                                    <a class="addThis_item--icon" href='tel:<%#Eval("DienThoai")%>' rel="nofollow" aria-label="phone">
                                        <img src="images/phone.png"/>
                                    </a>
                                    <%--<asp:ImageButton ID="cmdDelete" CommandName="Delete" CommandArgument='<%# Eval("ID_ThoiGianNgungViec")%>' runat="server" ImageUrl="~/images/cancel.png" CausesValidation="False" Width="20px"></asp:ImageButton>--%>
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
                <td style="width: 100%; margin-bottom: 10px;">
                    <br />
                    <p style="font-family: Tahoma;color: red;font-weight: bold;" id="pnameCNDN" runat="server">Danh sách đã nhập năng suất</p>
                    <asp:ListView ID="lvUser" runat="server" ItemPlaceholderID="itemPlaceHolder" OnItemDataBound="lvUser_ItemDataBound">
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li style="padding-bottom: 10px;">
                                <div style="width: 100%; padding-left: 10px; background-color: #006699; font-size: 14px;">

                                    <b>
                                        <span>
                                            <asp:Label ID="lblHoTen" runat="server" Text='<%# Eval("HoTen") %>' style="color: #fff;"></asp:Label>
                                            <asp:Label ID="lblTyLeTH" runat="server" Text='<%# Eval("TyLeTHNgay") %>' style="color: red;float:right;padding-right: 10px;"></asp:Label>
                                            <asp:Label ID="lblMaNS_ID" runat="server" Text='<%# Eval("MaNS_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblPD" runat="server" Text='<%# Eval("PheDuyet") %>' Visible="false"></asp:Label>
                                        </span>
<%--                                        <span style="float: right;">
                                            <asp:Label ID="lblTotal" runat="server" Text='T/Lg: 0'></asp:Label>
                                        </span>--%>
                                    </b>
                                </div>
                                <asp:GridView ID="gridNangSuatUser" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gridNangSuatUser_RowDataBound" CssClass="fontsize" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="MaHang" HeaderText="Mã hàng">
                                            <HeaderStyle HorizontalAlign="Center" Width="16%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="TenCongDoan" HeaderText="Công đoạn">
                                            <HeaderStyle HorizontalAlign="Center" Width="54%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="DonGia" DataFormatString="{0:#,0.##}" HeaderText="Giá">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="HeSoK_DonGia" DataFormatString="{0:#,0.##}" HeaderText="Hệ số K">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CongNhan" HeaderText="NS">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="MaNS" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaNS" runat="server" Text='<%# Eval("MaNS_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
