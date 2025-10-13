<%@ Page Title="Đăng ký cơm ca" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DangKyComCa.aspx.cs" Inherits="TNGLuong.DangKyComCa" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; font-family: Tahoma;" class="fontsize">
        <p class="theP">Đăng ký cơm ca</p>
        <div class="row">
            <div class="row">
                <div class="col-md-2">
                        <asp:Label runat="server" CssClass="control-label">Năm: </asp:Label>
                    </div>
                <div class="col-md-10">
                    <asp:DropDownList ID="cmbNam" runat="server" Width="100%" OnSelectedIndexChanged="cmbNam_SelectedIndexChanged" AutoPostBack="True" CssClass="margin-top"></asp:DropDownList>
                </div>
           </div>
            <div class="row">
                <div class="col-md-2">
                        <asp:Label runat="server" CssClass="control-label">Tháng: </asp:Label>
                    </div>
                <div class="col-md-10">
                    <asp:DropDownList ID="cmbThang" runat="server" Width="100%" OnSelectedIndexChanged="cmbThang_SelectedIndexChanged" AutoPostBack="True" CssClass="margin-top"></asp:DropDownList>
                </div>
           </div>
        </div>
        <div class="row">
            <asp:Label ID="lblTongNgayDK" runat="server" Text="Số ngày đăng ký: 0 - Số ngày ko đăng ký: 0" Width="100%" Style="padding-left: 10px;padding-top:10px;padding-bottom:10px;" Font-Bold="True" Font-Size="9pt" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <asp:GridView ID="gvDangKy" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                BorderWidth="1px" Width="100%" OnRowCommand="gvDangKy_RowCommand" OnRowDataBound="gvDangKy_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="STT">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" Height="35px"/>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày">
                        <HeaderStyle Width="110px" HorizontalAlign="Center" />
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblweekday_id" runat="server" Text='<%#Eval("weekday_id") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblNgay" runat="server" Text='<%#Eval("Ngay","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:BoundField DataField="ThuTV" HeaderText="Thứ">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GhiChu" HeaderText="Nhận xét">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left"/>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Đăng ký">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsDangKy" runat="server" Checked='<%#Eval("AnCa") %>' OnCheckedChanged="chkIsDangKy_CheckedChanged" AutoPostBack="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnNhanXet" runat="server" Text="Nhận xét" CommandArgument='<%# Eval("Ngay") %>' CommandName="nhanxetCC" Width="100%" BackColor="#006699" BorderStyle="Solid" BorderWidth="1px" BorderColor="White" ForeColor="White" Font-Bold="True"></asp:LinkButton>
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
    <div class="modal fade modal-addThis modal-contactform in" id="addthismodalContact" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" style="display: none;">
        <div id="divThongBao" runat="server" class="modal-dialog modal-dialog-centered" style="display: none;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="divthongbao">
                        <p style="color: #039; font-family: Tahoma;">Thông báo</p>
                        <button type="button" class="close" data-dismiss="modal" id="btnclose" runat="server">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <asp:Label ID="lblMessenger" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade modal-addThis modal-succesform in" id="divNhanXet" runat="server" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="divthongbao" style="width: 96%;">
                        <p>
                            <asp:Label ID="lblNhanXet_DeXuat" runat="server" Text="Nhận xét và đưa ra ý kiển về bữa ăn" Style="color: #039; font-family: Tahoma;"></asp:Label>
                        </p>
                        <button type="button" class="close" data-dismiss="modal" id="btncloseNX" runat="server" style="margin-top: -1em;">×</button>
                    </div>
                </div>
                <div class="modal-body content_popupform">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label runat="server" CssClass="control-label">Ngày: </asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="dtpNgay" runat="server" Width="100%" CssClass="form-control" format="dd/MM/yyyy" TextMode="Date" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label runat="server" CssClass="control-label">Nhận xét: </asp:Label>
                        </div>
                        <div class="col-md-10">
                            <textarea ID="txtNhanXet" runat="server" class="form-control" style="height:150px;width:100%;"></textarea>
                        </div>
                    </div>
                    <div class="row" style="padding-top:10px;padding-bottom:10px;">
                        <asp:Button ID="btnSave" runat="server" Text="Lưu" CssClass="btnCenter" OnClick="btnSave_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
