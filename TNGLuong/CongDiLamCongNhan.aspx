<%@ Page Language="C#" Title="Công đi làm theo tháng" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CongDiLamCongNhan.aspx.cs" Inherits="TNGLuong.CongDiLamCongNhan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%; text-align: center; font-weight: bold; font-size: 14px; padding-bottom: 15px;" colspan="2">CÔNG NHÂN XEM CÔNG ĐI LÀM</td>
            </tr>
            <tr>
                <td style="width: 35%;">Chọn tháng:</td>
                <td style="width: 65%;">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" Width="100%" AutoPostBack="True" format="MM/yyyy" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="margin-top: 10px; font-size: 9pt; font-weight: bold; color: red; width: 100%;">
                    <asp:Label ID="lbl" runat="server" Text="Danh sách chấm công" CssClass="margin-top" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridCongDiLamCongNhan" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
                        Width="100%" BackColor="White" Style="margin-top: -0.2em;" ShowHeaderWhenEmpty="True">
                        <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                        <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                        <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                        <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <Columns>
                               <asp:TemplateField HeaderText="Ngày">
                                <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNgay" runat="server" Text='<%#Eval("Ngay") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Giờ Vào">
                                <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblGioVao" runat="server" Text='<%#Eval("CS_GioVao") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Giờ Ra">
                                <HeaderStyle Width="30%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="30%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblGioRa" runat="server" Text='<%#Eval("CS_GioRa") %>' Width="100%"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Trạng thái">
                                <HeaderStyle Width="20%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Width="20%" HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTenCa" runat="server" Text='<%#Eval("TenCa") %>' Width="100%"></asp:Label>
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
                <td style="margin-top: 10px; font-size: 9pt; font-weight: bold; color: red;">
                    <asp:Label ID="lblTong" runat="server" Text="Tổng số công đi làm : "></asp:Label>
                    <span style="font-size: 9pt; font-weight: bold; color:black">
                          <asp:Label ID="lblTongSoCong" runat="server" ></asp:Label>
                    </span>
                </td>
             
            </tr>
        </table>
    </div>

</asp:Content>
