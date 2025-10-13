<%@ Page Title="Tổng hợp lương tháng" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TongHopLuong.aspx.cs" Inherits="TNGLuong.TongHopLuong" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma;" class="fontsize">
        <table style="width: 100%;">
            <tr>
                <td style="width: 25%;"><strong>Chọn ngày</strong></td>
                <td style="width: 25%;"><strong>TL tháng</strong></td>
                <td style="width: 25%;"><strong>SL BL tháng</strong></td>
                <td style="width: 25%;"><strong>TL BL tháng</strong></td>                
            </tr>
            <tr>
                <td style="width: 25%;">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" format="dd/MM/yyyy" Width="96%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="lblTLThang" runat="server" Text="0" Width="100%"></asp:Label>
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="lblSLBLThang" runat="server" Text="0" Width="100%"></asp:Label>                    
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="lblTLBLThang" runat="server" Text="0" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="padding-top: 10px;">
                    <p></p>
                </td>
            </tr>
            <tr>
                <td style="width: 25%;"><strong>TT</strong></td>
                <td style="width: 25%;"><strong>TL ngày</strong></td>               
                <td style="width: 25%;"><strong>SL BL ngày</strong></td>
                 <td style="width: 25%;"><strong>TL BL ngày</strong></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTrangThai" runat="server" Text="" Width="100%" ForeColor="Red"></asp:Label></td>
                <td>
                    <asp:Label ID="lblTLNgay" runat="server" Text="0" Width="100%"></asp:Label></td>
                <td>
                    <asp:Label ID="lblSLBLNgay" runat="server" Text="0" Width="100%"></asp:Label>
                    </td>
                <td>
                    <asp:Label ID="lblTLBLNgay" runat="server" Text="0" Width="100%"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td colspan="4" style="padding-top: 10px;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="grdTongHopLuong" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="grdTongHopLuong_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table style="width: 100%;border-collapse: collapse;border-color:#999;border-style:solid;" border="1">
                                        <tr>
                                            <td class="w3-center" rowspan="2" style="color: #FFFFFF; background-color: #006699;width: 30px;"><strong>STT</strong></td>
                                            <td class="w3-center" rowspan="2" style="color: #FFFFFF; background-color: #006699;width: 27%"><strong>Họ tên</strong></td>
                                            <td class="w3-center" rowspan="2" style="color: #FFFFFF; background-color: #006699;width: 13%"><strong>Mã NS</strong></td>
                                            <td class="w3-center" colspan="2" style="color: #FFFFFF; background-color: #006699;height:24px;width: 30%"><strong>LK Tháng</strong></td>
                                            <td class="w3-center" colspan="2" style="color: #FFFFFF; background-color: #006699;height:24px;width: 30%"><strong>Ngày</strong></td>
                                            <td class="w3-center" rowspan="2" style="width: 30px; color: #FFFFFF; background-color: #006699;"><strong>Xem CT</strong></td>
                                        </tr>
                                        <tr>
                                            <td class="w3-center" style="color: #FFFFFF; background-color: #006699;height:24px;width: 15%"><strong>Tổng</strong></td>
                                            <td class="w3-center" style="color: #FFFFFF; background-color: #006699;height:24px;width: 15%"><strong>Bù</strong></td>
                                            <td class="w3-center" style="color: #FFFFFF; background-color: #006699;height:24px;width: 15%"><strong>Tổng</strong></td>
                                            <td class="w3-center" style="color: #FFFFFF; background-color: #006699;height:24px;width: 15%"><strong>Bù</strong></td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center" style="width: 30px;">
                                            <asp:Label ID="lblSTT" runat="server" Text='<%# Eval("STT") %>' Width="100%" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td style="width: 27%;text-align: left;padding-left:5px;">
                                            <asp:Label ID="lblHoten" runat="server" Text='<%# Eval("HoTen") %>' Width="100%" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td style="width: 13%">
                                            <asp:Label ID="lblMaNS" runat="server" Text='<%# Eval("MaNS") %>' Width="100%" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td style="width: 15%;text-align: right;padding-right:5px;">
                                            <asp:Label ID="lblTLThang" runat="server" Text='<%# Eval("TLThang","{0:#,0.##}") %>' Width="100%" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td style="width: 15%;text-align: right;padding-right:5px;">
                                            <asp:Label ID="lblBLThang" runat="server" Text='<%# Eval("BLThang","{0:#,0.##}") %>' Width="100%" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td style="width: 15%;text-align: right;padding-right:5px;">
                                            <asp:Label ID="lblTLNgay" runat="server" Text='<%# Eval("TLNgay","{0:#,0.##}") %>' Width="100%" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td style="width: 15%;text-align: right;padding-right:5px;">
                                            <asp:Label ID="lblBLNgay" runat="server" Text='<%# Eval("BLNgay","{0:#,0.##}") %>' Width="100%" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 30px;">
                                            <asp:ImageButton ID="imgbtnXemCT" runat="server" ImageUrl="~/images/detail.png" Width="24px"
                                                AlternateText="Xem chi tiết" CommandArgument='<%# Eval("MaNS_ID") %>' CommandName="ChiTiet" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div class="modal_heigh fade modal-addThis modal-contactform in fontsize" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" style="margin-bottom: 70vh;">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="text-align: left;font-weight:bold;font-family:Tahoma;">CHI TIẾT LƯƠNG VÀ NĂNG SUẤT CÁ NHÂN</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server" style="margin-top: -33px;">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <table style="width: 100%;">
                        <tr>
                            <td style="font-weight: bold; color: red;">
                                <asp:Label ID="lblChiTietBL" runat="server" Text="Chi tiết bảng lương tháng của cá nhân:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--Grid--%>
                                <asp:GridView ID="grdChiTietTongHop" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnRowDataBound="grdChiTietTongHop_RowDataBound" OnRowCreated="grdChiTietTongHop_RowCreated">
                                    <Columns>
                                        <asp:BoundField DataField="Ngay" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Ngày">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LuongSP" DataFormatString="{0:#,0.##}" HeaderText="Lương SP">
                                        <HeaderStyle HorizontalAlign="Center" Width="14%" />
                                        <ItemStyle HorizontalAlign="Right" Width="14%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NhayKhau" DataFormatString="{0:#,0.##}" HeaderText="Nhảy khâu">
                                        <HeaderStyle HorizontalAlign="Center" Width="14%" />
                                        <ItemStyle HorizontalAlign="Right" Width="14%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VuotNangSuat" DataFormatString="{0:#,0.##}" HeaderText="Thưởng VNS">
                                        <HeaderStyle HorizontalAlign="Center" Width="14%" />
                                        <ItemStyle HorizontalAlign="Right" Width="14%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LuongThemGio" DataFormatString="{0:#,0.##}" HeaderText="Thêm giờ">
                                        <HeaderStyle HorizontalAlign="Center" Width="14%" />
                                        <ItemStyle HorizontalAlign="Right" Width="14%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BuLuong" DataFormatString="{0:#,0.##}" HeaderText="Bù lương">
                                        <HeaderStyle HorizontalAlign="Center" Width="14%" />
                                        <ItemStyle HorizontalAlign="Right" Width="14%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TongTienLuong" DataFormatString="{0:#,0.##}" HeaderText="Tổng TL">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Right" Width="15%" />
                                        </asp:BoundField>
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
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: red;">
                                <asp:Label ID="lblChiTietNS" runat="server" Text="Chi tiết năng suất tháng của cá nhân:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--Grid--%>
                                <asp:GridView ID="grdChiTietNangSuat" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" OnRowDataBound="grdChiTietNangSuat_RowDataBound" OnRowCreated="grdChiTietNangSuat_RowCreated">
                                    <Columns>
                                        <asp:BoundField DataField="TenPhongban" HeaderText="Tổ">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"  />
                                        <ItemStyle HorizontalAlign="Center" Width="10%"  />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TenCongDoan" HeaderText="Tên công đoạn">
                                        <HeaderStyle HorizontalAlign="Center" Width="30%"  />
                                        <ItemStyle Width="30%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TGCN" DataFormatString="{0:#,0.##}" HeaderText="TGCN">
                                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LKKeHoach" DataFormatString="{0:#,0.##}" HeaderText="LK KH">
                                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LKThucHien" DataFormatString="{0:#,0.##}" HeaderText="LK TH">
                                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="KeHoach" DataFormatString="{0:#,0.##}" HeaderText="KH ngày">
                                        <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ThucHien" DataFormatString="{0:#,0.##}" HeaderText="TH ngày">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
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
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
