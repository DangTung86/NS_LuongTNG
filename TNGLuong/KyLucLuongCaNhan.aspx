<%@ Page Title="Kỷ lục lương cá nhân" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KyLucLuongCaNhan.aspx.cs" Inherits="TNGLuong.KyLucLuongCaNhan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma;">
        <p class="theP">KỶ LỤC LƯƠNG CÁ NHÂN</p>
        <p id="demo"></p>
        <div class="row">
            <div class="col-md-3" style="line-height: 2.2;">Xem theo</div>
            <div class="col-md-6">
                <asp:DropDownList ID="cmbKLCaNhan" CssClass="form-control" runat="server" DataTextField="TimKiem" DataValueField="IDTimKiem" AutoPostBack="true" OnSelectedIndexChanged="cmbKLCaNhan_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3"></div>
        </div>
        <div class="row">
            <asp:Label ID="lblTieuDe" runat="server" Text="KỶ LỤC 5 NGÀY LƯƠNG CAO NHẤT TỪ NĂM 2022-2023" Width="100%" Style="padding-top: 15px; text-align: center; font-weight: bold;"></asp:Label>
        </div>
        <div class="row" id="divChart" runat="server">
            <asp:Chart ID="ChartKLCaNhan" runat="server">
                <Series>
                    <asp:Series Name="SeriesKLCaNhan" ChartType="Column" XValueMember="TenCot" YValueMembers="TongTienLuong" IsValueShownAsLabel="true" LabelFormat="{0:0.##}" Font="Tahoma, 9.75pt"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartAreaKLCaNhan"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </div>
</asp:Content>
