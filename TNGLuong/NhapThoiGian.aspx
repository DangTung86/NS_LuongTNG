<%@ Page Title="Nhập thời gian" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NhapThoiGian.aspx.cs" Inherits="TNGLuong.NhapThoiGian" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="w3-container" style="width: 100%;">
        <div class="w3-bar w3-black">
            <button id="btnnhap" runat="server" class="w3-bar-item w3-button tablink w3-red">Nhập thời gian chờ</button>
            <button id="btnbao" runat="server" class="w3-bar-item w3-button tablink">Đếm thời gian chờ</button>
        </div>

        <div id="divNhap" runat="server" class="w3-container w3-border city">
            <h2>Nhập thời gian chờ</h2>
            <p>London is the capital city of England.</p>
        </div>

        <div id="divBao" runat="server" class="w3-container w3-border city" style="display: none">
            <h2>Đếm thời gian chờ</h2>
            <p>Paris is the capital of France.</p>
        </div>
    </div>--%>
    <div style="width: 100%; font-family: Tahoma; font-size: 12px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 35%;">Chọn ngày:</td>
                <td style="width: 65%;">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtNgayThang" format="dd/MM/yyyy" Width="100%" AutoPostBack="True" TextMode="Date" OnTextChanged="txtDate_TextChanged" Style="margin-bottom: 10px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server" EnableScriptGlobalization="True"></cc1:ToolkitScriptManager>
                    <asp:GridView ID="gridNhapThoiGian" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" ShowFooter="True" Width="100%" OnRowDataBound="gridNhapThoiGian_RowDataBound" OnRowDeleting="gridNhapThoiGian_RowDeleting" DataKeyNames="ID_ThoiGianNgungViec">
                        <Columns>
                            <asp:BoundField DataField="STT" HeaderText="STT">
                                <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ten_LyDoNgungViec" HeaderText="Nguyên nhân">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="IdLyDo" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdLyDo" runat="server" Text='<%#Eval("ID_LyDoNgungViec") %>'></asp:Label>
                                    <asp:Label ID="lblXacNhan" runat="server" Text='<%#Eval("XacNhan") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bắt đầu">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%#Eval("ThoiGian_BatDau","{0:HH:mm}") %>' Width="100%" OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true" Style="text-align: center;" TextMode="Time"></asp:TextBox>
                                    <%--<cc1:MaskedEditExtender ID="startEditExtender" runat="server"
                                        TargetControlID="txtStartDate" AcceptAMPM="false" MaskType="Time"
                                        Mask="99:99" AutoComplete="False" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kết thúc">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%#Eval("ThoiGian_KetThuc","{0:HH:mm}") %>' Width="100%" OnTextChanged="txtEndDate_TextChanged" AutoPostBack="true" Style="text-align: center;" TextMode="Time"></asp:TextBox>
                                   <%-- <cc1:MaskedEditExtender ID="endEditExtender" runat="server"
                                        TargetControlID="txtEndDate" AcceptAMPM="false" MaskType="Time"
                                        Mask="99:99" AutoComplete="False" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Số phút">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Right" Width="60px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtThoiGian" runat="server" Text='<%#Eval("ThoiGian","{0:0.#}") %>' Width="100%" TextMode="Number" Style="text-align: right;" OnTextChanged="txtThoiGian_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ghi chú">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGhiChu" runat="server" Text='<%#Eval("GhiChu") %>' Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField Visible="false">
                                <HeaderStyle Width="20px" />
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                <FooterStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="cmdDelete" CommandName="Delete" CommandArgument='<%# Eval("ID_ThoiGianNgungViec")%>' runat="server" ImageUrl="~/images/cancel.png" CausesValidation="False" Width="20px"></asp:ImageButton>
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
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnXacNhan" runat="server" Text="Lưu" CssClass="btnSave" OnClick="btnXacNhan_Click" />
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
