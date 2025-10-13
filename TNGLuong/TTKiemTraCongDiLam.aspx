<%@ Page Title="Kiểm tra công đi làm của công nhân" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TTKiemTraCongDiLam.aspx.cs" Inherits="TNGLuong.TTKiemTraCongDiLam" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;" class="fontsize">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;" align="center" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Chọn tháng: " Width="80px" CssClass="text_left"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" format="dd/MM/yyyy" Width="80%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; margin-bottom: 10px;">
                    <br />
                    <p style="font-family: Tahoma; color: red; font-weight: bold;" id="pnameCNCN" runat="server">Danh sách chấm công theo ngày</p>
                    <asp:GridView ID="gridChamCongHomNay" AutoGenerateColumns="False" Width="100%" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gridChamCongHomNay_RowDataBound">
                        <Columns>
                              <asp:TemplateField HeaderText="STT" SortExpression="STT">
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSTT" runat="server" Text='<%# Eval("STT") %>' Visible="true"></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mã NS" SortExpression="MaNS">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaNS_ID" runat="server" Text='<%# Eval("MaNS") %>' Visible="true"></asp:Label>
                                    <asp:Label ID="lblMaNS" runat="server" Text='<%# Eval("MaNS_ID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:BoundField DataField="HoTen" HeaderText="Họ tên">
                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CS_GioVao" HeaderText="Giờ Vào">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CS_GioRa" HeaderText="Giờ Ra">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TenCa" HeaderText="Trạng thái">
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Chọn lí do nghỉ">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"/>
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTenCa" runat="server" Width="100%"></asp:DropDownList>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Ngay" HeaderText="Ngày giờ">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"/>
                                <ItemStyle HorizontalAlign="Left" Width="20%"/>
                            </asp:BoundField>--%>
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
                <td colspan="2" style="width: 45%" align="center">
                    <asp:Button ID="btnCapNhatLyDo" runat="server" Text="Cập nhật" CssClass="btnSave" OnClick="btnCapNhatLyDo_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%; margin-bottom: 10px;">
                    <br />
                    <p style="font-family: Tahoma; color: red; font-weight: bold;" runat="server">Danh sách theo tháng</p>
                    <asp:ListView ID="lvUser" runat="server" OnItemDataBound="lvUser_ItemDataBound" ItemPlaceholderID="itemPlaceHolder">
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
                                            <asp:Label ID="lblHoTen" runat="server" Text='<%# Eval("HoTen") %>'></asp:Label>
                                            <asp:Label ID="lblMaNS_ID" runat="server" Text='<%# Eval("MaNS_ID") %>' Visible="false"></asp:Label>
                                            <%--<asp:Label ID="lblPD" runat="server" Text='<%# Eval("PheDuyet") %>' Visible="false"></asp:Label>--%>
                                        </span>
                                        <%--                                        <span style="float: right;">
                                            <asp:Label ID="lblTotal" runat="server" Text='T/Lg: 0'></asp:Label>
                                        </span>--%>
                                    </b>
                                </div>
                                <asp:GridView ID="gridCongLamViecCongNhan" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="fontsize" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="Ngay" HeaderText="Ngày">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CS_GioVao" HeaderText="Giờ Vào">
                                            <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CS_GioRa" HeaderText="Giờ Ra" >
                                            <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TenCa" HeaderText="Trạng thái" >
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
