<%@ Page Title="Báo thời gian" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThoiGianCho.aspx.cs" Inherits="TNGLuong.ThoiGianCho" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="true"></asp:ScriptManager>
        <asp:Timer ID="timer1" runat="server" OnTick="timer1_Tick" Interval="1000"></asp:Timer>
        <asp:UpdatePanel ID="updPnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr id="btn1" runat="server">
                        <td style="width: 25%;">
                            <asp:Button ID="btnBTP" runat="server" Text="Chờ BTP" Width="99%" CssClass="buttonCho" OnClick="btnBTP_Click" />
                        </td>
                        <td style="width: 25%;">
                            <asp:Button ID="btnCoDien" runat="server" Text="Cơ điện" Width="99%" CssClass="buttonCho" OnClick="btnCoDien_Click" />
                        </td>
                        <td style="width: 25%;">
                            <asp:Button ID="btnPhuLieu" runat="server" Text="Phụ liệu" Width="99%" CssClass="buttonCho" OnClick="btnPhuLieu_Click" />
                        </td>
                        <%--<td style="width: 25%;">
                            <asp:Button ID="btnKhauTrc" runat="server" Text="Khâu trước" Width="99%" CssClass="buttonCho" OnClick="btnKhauTrc_Click" />
                        </td>--%>
                    </tr>
                    <tr id="btn2" runat="server">
                        <td style="width: 75%;" colspan="2" align="center">
                            <asp:Button ID="btnHuongDan" runat="server" Text="Hướng dẫn/mẫu" Width="48%" Style="margin-right: 1px;" CssClass="buttonCho" OnClick="btnHuongDan_Click" />
                            <asp:Button ID="btnChatLuong" runat="server" Text="Lỗi chất lượng" Width="48%" Style="margin-right: 1px;" CssClass="buttonCho" OnClick="btnChatLuong_Click" />
                        </td>
                        <td style="width: 25%;">
                            <asp:Button ID="btnKhac" runat="server" Text="Khác" Width="99%" CssClass="buttonCho" OnClick="btnKhac_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-top:3px;" align="center" id="tdlbl" runat="server">
                            <div style="width:100%;">
                                <asp:Label ID="lblTieuDe" runat="server" Text="" Font-Bold="True" Font-Size="14pt" ></asp:Label>
                            </div>
                            <div class="rcorners1">
                                <asp:Label ID="lblDongHo" runat="server" Text="00:00" Font-Bold="True" Font-Size="40pt" ForeColor="White"></asp:Label>
                            </div>                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" style="padding-top:3px;height:50px;" id="tdbtn" runat="server">
                            <asp:Button ID="btnKetThuc" runat="server" Text="Kết thúc" CssClass="btnSave" OnClick="btnKetThuc_Click" Height="40px" Width="120px"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>    
</asp:Content>