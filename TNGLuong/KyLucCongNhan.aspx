<%@ Page Title="Kỷ lục công nhân" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KyLucCongNhan.aspx.cs" Inherits="TNGLuong.KyLucCongNhan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <p class="theP">KỶ LỤC LƯƠNG SP</p>
    <p id="demo"></p>
    <div class="panel with-nav-tabs panel-primary">
        <div class="panel-heading">
            <ul class="nav nav-tabs">
                <li>   
                    <asp:LinkButton ID="btnTuanNay" runat="server" CssClass="" OnClick="btnTuanNay_Click">Tuần này</asp:LinkButton>
                    <%--<a href="#tabTuanNay" data-toggle="tab" id="aTuanNay" runat="server" class="active" aria-expanded="true">Tuần này</a>--%>
                </li>
                <li>                
                    <asp:LinkButton ID="btnTuanTrc" runat="server" CssClass="" OnClick="btnTuanTrc_Click">Tuần trước</asp:LinkButton>
                    <%--<a href="#tabTuanTruoc" data-toggle="tab" id="aTuanTrc" runat="server" class="" aria-expanded="false">Tuần trước</a>--%>
                </li>
                <li>
                    <asp:LinkButton ID="btnThangNay" runat="server" CssClass="" OnClick="btnThangNay_Click">Tháng này</asp:LinkButton>
                    <%--<a href="#tabThangNay" data-toggle="tab" id="aThangNay" runat="server" class="" aria-expanded="false">Tháng này</a>--%>
                </li>
                <li>
                    <asp:LinkButton ID="btnThangTrc" runat="server" CssClass="" OnClick="btnThangTrc_Click">Tháng trước</asp:LinkButton>
                    <%--<a href="#tabThangTruoc" data-toggle="tab" id="aThangTrc" runat="server" class="" aria-expanded="false">Tháng trước</a>--%>
                </li>
            </ul>
        </div>
        <div class="panel-body">
            <div class="tab-content">
                <div class="tab-pane fade" id="tabTuanNay" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="border: 0px solid transparent;text-align:center;">
                                <asp:Label ID="lblXepHangTN" runat="server" Text="Xếp hạng: 0/0." Font-Bold="True" Font-Size="14pt" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <div class="row" style="padding-bottom: 5px;">
                                    <div class="col-md-2">
                                        Đơn vị/Bộ phận
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="cmbDonVi" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="TenDonVi" DataValueField="DonViID" OnSelectedIndexChanged="cmbDonVi_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <asp:GridView ID="gvTuanNay" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvTuanNay_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="SoTT" DataFormatString="{0:#}" HeaderText="Top">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HoTenTop" HeaderText="Đơn vị / Bộ phận">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="70%" />
                                            <ItemStyle Width="70%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TongTienLuong" DataFormatString="{0:#,#.##}" HeaderText="Tiền lương">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="TrangThai" SortExpression="TrangThai" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrangThai" runat="server" Text='<%#Eval("TrangThai") %>' Width="100%" Visible="false"></asp:Label>
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
                <div class="tab-pane fade" id="tabTuanTruoc"  runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="border: 0px solid transparent;text-align:center;">
                                <asp:Label ID="lblXepHangTC" runat="server" Text="Xếp hạng: 0/0." Font-Bold="True" Font-Size="14pt" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <div class="row" style="padding-bottom: 5px;">
                                    <div class="col-md-2">
                                        Đơn vị/Bộ phận
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="cmbDonViTuanTrc" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="TenDonVi" DataValueField="DonViID" OnSelectedIndexChanged="cmbDonViTuanTrc_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <asp:GridView ID="gvTuanTrc" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvTuanTrc_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="SoTT" DataFormatString="{0:#}" HeaderText="Top">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HoTenTop" HeaderText="Đơn vị / Bộ phận">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="70%" />
                                            <ItemStyle Width="70%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TongTienLuong" DataFormatString="{0:#,#.##}" HeaderText="Tiền lương">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="TrangThai" SortExpression="TrangThai" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrangThaiTrc" runat="server" Text='<%#Eval("TrangThai") %>' Width="100%" Visible="false"></asp:Label>
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
                <div class="tab-pane fade" id="tabThangNay"  runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="border: 0px solid transparent;text-align:center;">
                                <asp:Label ID="lblXepHangThgN" runat="server" Text="Xếp hạng: 0/0." Font-Bold="True" Font-Size="14pt" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <div class="row" style="padding-bottom: 5px;">
                                    <div class="col-md-2">
                                        Đơn vị/Bộ phận
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="cmbDonViThang" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="TenDonVi" DataValueField="DonViID" OnSelectedIndexChanged="cmbDonViThang_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <asp:GridView ID="gvThang" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvThang_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="SoTT" DataFormatString="{0:#}" HeaderText="Top">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HoTenTop" HeaderText="Đơn vị / Bộ phận">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="70%" />
                                            <ItemStyle Width="70%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TongTienLuong" DataFormatString="{0:#,#.##}" HeaderText="Tiền lương">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="TrangThai" SortExpression="TrangThai" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrangThaiThang" runat="server" Text='<%#Eval("TrangThai") %>' Width="100%" Visible="false"></asp:Label>
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
                <div class="tab-pane fade" id="tabThangTruoc"  runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td style="border: 0px solid transparent;text-align:center;">
                                <asp:Label ID="lblXepHangThgC" runat="server" Text="Xếp hạng: 0/0." Font-Bold="True" Font-Size="14pt" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <div class="row" style="padding-bottom: 5px;">
                                    <div class="col-md-2">
                                        Đơn vị/Bộ phận
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="cmbDonViThangTrc" CssClass="form-control" runat="server" AutoPostBack="True" DataTextField="TenDonVi" DataValueField="DonViID" OnSelectedIndexChanged="cmbDonViThangTrc_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 0px solid transparent;">
                                <asp:GridView ID="gvThangTrc" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvThangTrc_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="SoTT" DataFormatString="{0:#}" HeaderText="Top">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HoTenTop" HeaderText="Đơn vị / Bộ phận">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="70%" />
                                            <ItemStyle Width="70%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TongTienLuong" DataFormatString="{0:#,#.##}" HeaderText="Tiền lương">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="TrangThai" SortExpression="TrangThai" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrangThaiThangTrc" runat="server" Text='<%#Eval("TrangThai") %>' Width="100%" Visible="false"></asp:Label>
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
            </div>
        </div>
    </div>
</asp:Content>
