<%@ Page Title="Lương NS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LuongNS.aspx.cs" Inherits="TNGLuong.LuongNS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%;">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <p class="pluong">BẢNG TÍNH LƯƠNG NGÀY</p>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 30px;" align="center" class="fontsize">
                    <asp:Label ID="Label3" runat="server" Text="Chọn tháng: " Width="100px" CssClass="text_left"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" Width="55%" AutoPostBack="True" TextMode="Month" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="font-family: Tahoma;font-size: 10px;">
                    <div style="width:100%;height:25px;text-align:left;display:inline-block;padding-top:5px;padding-bottom:5px;">
                        <asp:Label ID="lblluongCB" runat="server" Text="Lương CB: 0 VNĐ" CssClass="fontsize"  Width="40%">
                        </asp:Label>
                        <div style="width:60%;float:right;text-align: right;" class="fontsize">
                            <img width="20px" src="images/detail.png" /> : chi tiết - <img width="20px" src="images/refresh.png" /> : Tính lại lương
                        </div>
                    </div>
                    <asp:GridView ID="gridLuongNS" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="gridLuongNS_RowCommand" ShowHeaderWhenEmpty="True" CssClass="fontsize">
                        <Columns>
                            <asp:BoundField DataField="Ngay" HeaderText="Ngày" DataFormatString="{0:dd-MM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="17%" />
                            <ItemStyle HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="GioLamViec" HeaderText="Số giờ làm">
                            <HeaderStyle HorizontalAlign="Center" Width="8px" />
                            <ItemStyle HorizontalAlign="Center" Width="8px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="LuongSP" HeaderText="Tiền lương SP" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="10.5%"/>
                            <ItemStyle HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="VuotNangSuat" HeaderText="Tiền vượt NS" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="10.5%"/>
                            <ItemStyle HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="LuongThemGio" HeaderText="Tiền thêm giờ" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="10.5%"/>
                            <ItemStyle HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="LuongChoViec" HeaderText="Lương chờ việc" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="10.5%"/>
                            <ItemStyle HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="TongTienLuong" HeaderText="Tổng (VNĐ)" DataFormatString="{0:#,0}" >
                            <HeaderStyle HorizontalAlign="Center" Width="14%"/>
                            <ItemStyle HorizontalAlign="Right"/>
                            <ItemStyle Font-Size="7pt" />
                            </asp:BoundField>                            
                            <asp:TemplateField>
                                <HeaderStyle Width="22px" />
                                <ItemStyle HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:ImageButton ID="cmdDetail" CommandName="Detail" CommandArgument='<%# Eval("Ngay")%>' runat="server" ImageUrl="~/images/detail.png" CausesValidation="False" Width="20px" ToolTip="Chi tiết"></asp:ImageButton>
                                    <asp:ImageButton ID="cmdRefresh" CommandName="Refresh" CommandArgument='<%# Eval("Ngay")%>' runat="server" ImageUrl="~/images/Refresh.png" CausesValidation="False" Width="20px" ToolTip="Chi tiết"></asp:ImageButton>
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
        </table>
    </div>
    <div class="modal fade modal-addThis modal-contactform in" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div id="divThongBao" runat="server" class="modal-dialog modal-dialog-centered" style="display: none;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p class="pluong" style="text-align: left;font-size: 20px;">CHI TIẾT BẢNG TÍNH LƯƠNG NGÀY</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server" style="margin-top: -33px;">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">


                    <asp:GridView ID="girdDetailNS" runat="server" AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"  ShowHeaderWhenEmpty="True" Width="98%" CssClass="fontsize" OnRowCreated="girdDetailNS_RowCreated" OnRowDataBound="girdDetailNS_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="TenCongDoan" HeaderText="Công đoạn" >
                            <HeaderStyle HorizontalAlign="Center" Width="50%" />
                            </asp:BoundField>                            
                            <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" DataFormatString="{0:#,0}">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LuyKe" HeaderText="Lũy kế">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CongNhan" HeaderText="NS">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Tổng">
                                <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="10%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTOng" runat="server"  Width="100%"></asp:Label>
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
        </div>
    </div>
</asp:Content>
