<%@ Page Title="Tổng hợp công đoạn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TongHopCongDoan.aspx.cs" Inherits="TNGLuong.TongHopCongDoan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma;" class="fontsize">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%; text-align: center; font-weight: bold; font-size: 14px; padding-bottom: 15px;" colspan="2">TỔNG HỢP CÔNG ĐOẠN</td>
            </tr>
            <tr>
                <%if (Session["KhoaBL"] != null && Session["KhoaBL"].ToString().Equals(("true").ToUpper()))
                        {%>
                <td style="width: 35%; padding-bottom: 15px">
                    <asp:Label ID="Label2" runat="server" Text="Chọn tháng:"></asp:Label>
                </td>
                <td style="width: 65%; padding-bottom: 15px">
                    <%--<asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" format="dd/MM/yyyy" Width="100%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlThang" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlThang_SelectedIndexChanged" DataTextField="TenThang" DataValueField="ThangID" CssClass="margin-top"></asp:DropDownList>
                </td>
                <% } else {%>
                <td style="width: 35%; padding-bottom: 15px">
                    <asp:Label ID="Label1" runat="server" Text="Chọn ngày:"></asp:Label>
                </td>
                <td style="width: 65%; padding-bottom: 15px">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" format="dd/MM/yyyy" Width="100%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                </td>
                <% }%>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblCD" runat="server" Text="I. Công đoạn giao kế hoạch" CssClass="margin-top" Width="100%" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:GridView ID="gridNangSuatToMay" runat="server" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False"
                        Width="100%" BackColor="White" Style="margin-top: -0.2em;" OnRowCreated="gridNangSuatToMay_RowCreated" OnRowDataBound="gridNangSuatToMay_RowDataBound">
                        <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
                        <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
                        <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
                        <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="TenPhongban" HeaderText="Tổ" SortExpression="TenPhongban">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TenCongDoan" HeaderText="Tên công đoạn" SortExpression="TenCongDoan">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TGCN" DataFormatString="{0:#,0.##}" HeaderText="TGCN" SortExpression="TGCN">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LuyKe" DataFormatString="{0:#,0.##}" HeaderText="Lũy kế" SortExpression="LuyKe">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NangSuat" DataFormatString="{0:#,0.##}" HeaderText="NS" SortExpression="NangSuat">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
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
    <div style="width: 100%; font-family: Tahoma; margin-top: 15px;" class="fontsize">
        <asp:Label ID="Label3" runat="server" Text="II. Công đoạn nhảy khâu" CssClass="margin-top" Width="100%" Font-Bold="True" ForeColor="Red"></asp:Label>
        <asp:GridView ID="gridNangSuatNhayKhau" runat="server" BorderColor="#CCCCCC" BorderStyle="None"
            BorderWidth="1px" AutoGenerateColumns="False" Width="100%" BackColor="White" OnRowCreated="gridNangSuatNhayKhau_RowCreated" OnRowDataBound="gridNangSuatNhayKhau_RowDataBound">
            <AlternatingRowStyle CssClass="GridStyle_AltRowStyle" />
            <HeaderStyle CssClass="GridStyle_HeaderStyle" BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <RowStyle CssClass="GridStyle_RowStyle" ForeColor="#000066" />
            <FooterStyle CssClass="GridStyle_FooterStyle" BackColor="White" ForeColor="#000066" />
            <PagerStyle CssClass="GridStyle_pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <Columns>
                <asp:BoundField DataField="TenPhongban" HeaderText="Tổ" SortExpression="TenPhongban">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TenCongDoan" HeaderText="Tên công đoạn" SortExpression="TenCongDoan">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TGCN" DataFormatString="{0:#,0.##}" HeaderText="TGCN" SortExpression="TGCN">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LuyKe" DataFormatString="{0:#,0.##}" HeaderText="Lũy kế" SortExpression="LuyKe">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="NangSuat" DataFormatString="{0:#,0.##}" HeaderText="NS" SortExpression="NangSuat">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>
</asp:Content>
