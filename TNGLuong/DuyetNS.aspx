<%@ Page Title="Duyệt NS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DuyetNS.aspx.cs" Inherits="TNGLuong.DuyetNS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;" align="center" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Chọn ngày: " Width="80px" CssClass="text_left"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" Width="80%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged" Text='<%#string.Format("{0:dd-MM-yyyy}",DateTime.Now) %>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" align="center" colspan="2">
                    <asp:Label ID="Label2" runat="server" Text="Nhân viên: " Width="80px" CssClass="text_left"></asp:Label>
                    <asp:DropDownList ID="ddlUser" runat="server" Width="80%" AutoPostBack="True" DataTextField="HoTen" DataValueField="MaNS_ID" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" CssClass="margin-top" OnDataBound="ddlUser_DataBound"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:100%;margin-bottom:10px;">
                    <div style="width:100%;height:25px;text-align:left;display:inline-block;padding-top:5px;padding-bottom:5px;">
                        <asp:Label ID="lblluongCB" runat="server" Text="Lương CB: 0 VNĐ" Font-Size="6.8pt"  Width="100%">
                        </asp:Label>
                    </div>
                    <asp:GridView ID="gridLuongNS" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowHeaderWhenEmpty="True" Font-Size="7pt">
                        <Columns>
                            <asp:BoundField DataField="Ngay" HeaderText="Ngày" DataFormatString="{0:dd-MM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="17%" />
                            <ItemStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="GioLamViec" HeaderText="Số giờ làm" >
                            <HeaderStyle HorizontalAlign="Center" Width="8px" />
                            <ItemStyle HorizontalAlign="Center" Width="8px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="LuongSP" HeaderText="Tiền lương SP" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="14%"/>
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VuotNangSuat" HeaderText="Tiền vượt NS" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="14%"/>
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LuongThemGio" HeaderText="Tiền thêm giờ" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="14%"/>
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TongTienLuong" HeaderText="Tổng (VNĐ)" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="14%"/>
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemStyle Font-Size="7pt" />
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
                <td style="width: 100%;margin-bottom:10px;">                    
                    <asp:GridView ID="gridNangSuatUser" runat="server" CssClass="gridDuyeNS" Width="100%" OnRowCreated="gridNangSuatUser_RowCreated" AllowPaging="true" PageSize="30"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gridNangSuatUser_RowDataBound" Font-Size="7pt">
                        <Columns>
                            <asp:TemplateField HeaderText="Công đoạn">
                                <HeaderStyle Width="60%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="60%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTenCongDoan" runat="server" Text='<%#Eval("TenCongDoan") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="T/T">
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPheDuyet" runat="server" Text='<%#Eval("PheDuyet") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Giá">
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDonGia" runat="server" Text='<%#Eval("DonGia","{0:#,0.##}") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lũy kế">
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblLuyKe" runat="server" Text='<%#Eval("LuyKe") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NS">
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblKH" runat="server" Text='<%#Eval("CongNhan") %>' Width="100%"></asp:Label>
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
                    <div align="center" style="padding-top: 5px;">
                        <asp:Button ID="btnPD" runat="server" CssClass="btnSave" Text="Duyệt" Width="100px" OnClick="btnPD_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="2">
                    <div style="margin-top: 10px; width: 100%; font-size: 10pt; font-weight: bold; color: red;">
                        <asp:Label ID="Label4" runat="server" Text="Lý do chưa duyệt:" CssClass="margin-top"></asp:Label>
                    </div>
                    <div style="width: 100%; font-size: 9pt; color: red; padding: 1px 15px;">
                        <asp:TextBox ID="txtLyDo" runat="server" Text="Tổ trưởng chưa phê duyệt." Width="100%" TextMode="MultiLine" Height="50px" BorderStyle="Solid" BorderColor="#000666" BorderWidth="1px"></asp:TextBox>
                    </div>
                    <div style="width: 100%;" align="center">
                        <asp:Button ID="btnHuyPD" runat="server" CssClass="btnSave" Text="Bỏ duyệt" Width="100px" OnClick="btnHuyPD_Click" />
                    </div>
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
