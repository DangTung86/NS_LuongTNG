<%@ Page Title="Bảng tính lương ngày" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BangTinhLuongCN.aspx.cs" Inherits="TNGLuong.BangTinhLuongCN" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%;" class="fontsize">
        <table style="width: 100%;" class="fontsize">
            <tr>
                <td style="width: 25%; font-weight: bold; padding: 5px 5px;">Mã NS: </td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblMaNS" runat="server" Text="" Width="100%"></asp:Label>
                </td>
                <td style="width: 25%; font-weight: bold; padding: 5px 5px;">Đơn vị: </td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblDonVi" runat="server" Text="" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 25%; font-weight: bold; padding: 5px 5px;">Họ Tên: </td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblHoten" runat="server" Text="" Width="100%"></asp:Label>
                </td>
                <td style="width: 25%; font-weight: bold; padding: 5px 5px;">Bộ Phận: </td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblBoPhan" runat="server" Text="" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 25%; font-weight: bold; padding: 5px 5px;">Ngày: </td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" Width="100%" format="dd/MM/yyyy" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
                <td style="width: 25%;font-weight: bold; padding: 5px 5px;">Trạng Thái</td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblTrangThai" runat="server" Text="" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 25%; padding: 5px 5px;">Thời gian làm việc</td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblThoiGianLamViec" runat="server" Text="0" Width="100%"></asp:Label>
                </td>
                <td style="width: 25%; padding: 5px 5px;">Tiền lương cấp bậc</td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblLuongCapBac" runat="server" Text="0" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 25%; padding: 5px 5px;">Số giờ làm thêm</td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblSoGioLamThem" runat="server" Text="0" Width="100%"></asp:Label>
                </td>
                <td style="width: 25%; padding: 5px 5px;">Số giờ ngừng việc</td>
                <td style="width: 25%; padding: 5px 5px;">
                    <asp:Label ID="lblSoGioNgungViec" runat="server" Text="0" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4" class="tables">
                    <table style="width: 100%;border: 1px solid #666;border-collapse: collapse;">
                        <%if (Session["KhoaBL"] == null || Session["KhoaBL"].ToString().Equals(("false").ToUpper()))
                        {%>
                        <tr style="border: 1px solid #666;">
                            <td colspan="2" style="font-weight: bold;border: 1px solid #666;background-color:#006699;color:white;">A - Tiền lương ngày</td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">1. Tiền lương sản phẩm</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongSP" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">2. Tiền lương nhẩy khâu</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongNhayKhau" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">3. Tiền lương vượt giao khoán</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongVuotGK" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">4. Tiền lương thêm giờ</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongThemGio" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">5. Tiền lương ngừng việc</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="LuongNgungViec" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">6. Tiền lương thời gian (nếu có)</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="LuongThoiGian" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;font-weight: bold; text-align: center;border: 1px solid #666;background-color:beige;">Tổng cộng</td>
                            <td style="width: 50%;border: 1px solid #666;background-color:beige;font-weight: bold;">
                                <asp:Label ID="lblTongLuongNgay" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <% } %>
                        <tr>
                            <td colspan="2" style="font-weight: bold;border: 1px solid #666;background-color:#006699;color:white;">B - Tiền lương tháng Lũy kế đến ngày TH</td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">1. Tiền lương sản phẩm</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongSPLK" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">2. Tiền lương nhẩy khâu</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongNhayKhauLK" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <%if (Session["KhoaBL"] == null || Session["KhoaBL"].ToString().Equals(("false").ToUpper()))
                        {%>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">3. Tiền lương vượt giao khoán</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="LuongGiaoKhoanLK" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">4. Tiền lương thêm giờ</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongThemGioLK" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">5. Tiền lương ngừng việc</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongNgungViecLK" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">6. Tiền lương thời gian (nếu có)</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongThoiGianLK" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">7. Tiền chuyên cần</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblLuongChuyenCan" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">8. Tiền thưởng Thành tích tháng</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblThuongThanhTich" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;border: 1px solid #666;">9. Các loại phụ cấp (nếu có)</td>
                            <td style="width: 50%;border: 1px solid #666;">
                                <asp:Label ID="lblPhuCap" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; font-weight: bold; text-align: center;border: 1px solid #666;background-color:beige;">Tổng cộng</td>
                            <td style="width: 50%;border: 1px solid #666;background-color:beige;font-weight: bold;">
                                <asp:Label ID="lblTongLk" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <% } %>
                    </table>
                </td>
            </tr>
            <%if (Session["KhoaBL"] == null || Session["KhoaBL"].ToString().Equals(("false").ToUpper()))
                        {%>
            <tr>
                <td colspan="4" align="center" style="padding: 10px 5px;">
                    <asp:Button ID="btnChiTiet" runat="server" Text="Xem chi tiết" CssClass="btnSave" OnClick="btnChiTiet_Click" /></td>
            </tr>
            <% } %>
            <tr>
                <td colspan="4"></td>
            </tr>
        </table>

    </div>
    <div class="modal_heigh fade modal-addThis modal-contactform in fontsize" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="text-align: left;font-weight:bold;font-family:Tahoma;">CHI TIẾT LƯƠNG SẢN PHẨM NGÀY</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server" style="margin-top: -33px;">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <table style="width: 100%;">
                        <tr>
                            <td style="font-weight: bold; color: red;">
                                <asp:Label ID="Label2" runat="server" Text="I. Công đoạn giao kế hoạch"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridNangSuat" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCreated="gridNangSuat_RowCreated" OnRowDataBound="gridNangSuat_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="TenPhongban" HeaderText="Tổ">
                                            <HeaderStyle HorizontalAlign="Center" Width="6%"/>
                                            <ItemStyle HorizontalAlign="Center" Width="6%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TenCongDoan" HeaderText="Tên công đoạn">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ThoiGian" HeaderText="Thời gian công nghệ">
                                            <HeaderStyle HorizontalAlign="Left" Width="8%"/>
                                            <ItemStyle HorizontalAlign="Center" Width="8%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="KeHoach_NhanVien" DataFormatString="{0:#,0}" HeaderText="Số GK">
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="8%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DonGia" DataFormatString="{0:#,0.##}" HeaderText="Đơn giá">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ThucHien_NhanVien" DataFormatString="{0:#,0}" HeaderText="TH">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ThanhTien" DataFormatString="{0:#,0.##}" HeaderText="Thành tiền">
                                            <HeaderStyle HorizontalAlign="Center" Width="12%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="12%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HieuSuat" DataFormatString="{0:#,0.##}" HeaderText="Hiệu suất">
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="8%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TienLuong" DataFormatString="{0:#,0}" HeaderText="Tiền lương">
                                            <HeaderStyle HorizontalAlign="Center" Width="13%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="13%"/>
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
                                <asp:Label ID="Label3" runat="server" Text="II. Công đoạn nhảy khâu"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridNhayKhau" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCreated="gridNhayKhau_RowCreated" OnRowDataBound="gridNhayKhau_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="TenCongDoan" HeaderText="Tên công đoạn">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ThoiGian" HeaderText="Thời gian công nghệ">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DonGia" DataFormatString="{0:#,0.##}" HeaderText="Đơn giá">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ThucHien_NhanVien" DataFormatString="{0:#,0}" HeaderText="TH">
                                            <HeaderStyle HorizontalAlign="Center" Width="12%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="12%"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ThanhTien" DataFormatString="{0:#,0.##}" HeaderText="Thành tiền">
                                            <HeaderStyle HorizontalAlign="Center" Width="13%"/>
                                            <ItemStyle HorizontalAlign="Right" Width="13%"/>
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
