<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThuongNam.aspx.cs" Inherits="TNGLuong.ThuongNam" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%;" class="fontsize">
        <h4 style="text-align: center;padding-bottom: 10px;">            
            <asp:Label ID="lblTieuDe" runat="server" Text="PHIẾU THANH TOÁN THƯỞNG NĂM 2020"></asp:Label>
        </h4>
        <h5 style="text-align: center;padding-bottom: 10px;">Đơn vị: <asp:Label ID="lblDonVi_ToMay" runat="server" Text=""></asp:Label></h5>
        <div style="margin:auto;width: 95%;">
            <div style="display: inline-flex; width: 100%;padding-bottom: 10px;">
                <div style="width: 20%;line-height: 1.7;">Chọn năm: </div>
                <div style="width: 80%;line-height: 1.7;"><asp:DropDownList ID="ddlNam" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlNam_SelectedIndexChanged" DataTextField="SoNam" DataValueField="SoNam"></asp:DropDownList></div>
            </div> 
            <div style="display: inline-flex; width: 100%;">
                <div style="width: 50%;line-height: 1.7;">Họ tên: <asp:Label ID="lblHoTen" runat="server" Text=""></asp:Label></div>
                <div style="width: 50%;line-height: 1.7;">Mã NS: <asp:Label ID="lblMaNS" runat="server" Text=""></asp:Label></div>
            </div>            
            <table style="width: 100%;border: 1px solid black;line-height: 1.7;">
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Tổng lương SP</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblTong_LuongSP" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Lương tạm ứng</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblLuong_Thang13" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Tổng thưởng ABC</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblTong_ThuongABC" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Thưởng tích lũy</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThuong_ABC" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Số tháng làm việc</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblSoThang_LamViec" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Thưởng LĐTT</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThuong_TienTien" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Số tháng XT</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblSoThang_XetThuong" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Thưởng CSTĐ</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThuong_CSTD" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Số tháng xếp hạng Khá</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblSoThang_LoaiA" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Thưởng tối thiểu</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThuong_ToiThieu" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Số tháng bù lương</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblSoThang_BuLuong" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Thưởng khác</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThuong_Khac" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;font-weight:bold;padding: 0px 5px;">Số tháng kỷ luật</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblSoThang_KyLuat" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;font-weight:bold;padding: 0px 5px;">Tổng cộng</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblTong_ThuongNam" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;font-weight:bold;padding: 0px 5px;">Tổng thu nhập(2)</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblTong_ThuNhap" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;font-weight:bold;padding: 0px 5px;">Thuế TNCN</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThueTNCN" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">Thu nhập BQ/tháng(3)</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThuNhapBQ" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;font-weight:bold;padding: 0px 5px;">THƯỞNG NĂM(1)</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblThucNhan" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">T. thưởng / TTN</td>
                    <td style="width: 25%; border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblTyLe_Thuong_TTN" runat="server" Text=""></asp:Label></td>
                    <td style="width: 25%;border: 1px solid black;padding: 0px 5px;">T. thưởng / TNBQ</td>
                    <td style="width: 25%;border: 1px solid black;text-align: right;padding: 0px 5px;"><asp:Label ID="lblTyLe_Thuong_TNBQ" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <div style="padding-top: 8px;">
                <p style="font-weight: bold;">Ghi chú:</p>
                <p>- Lương tạm tứng = Ʃ(Lương SP tháng 12 tháng)/12 x <asp:Label ID="lblHeSoK4" runat="server" Text=""></asp:Label></p>
                <p>- Thưởng tích lũy = Tổng thưởng thành tính tháng x <asp:Label ID="lblHeSoK1" runat="server" Text=""></asp:Label></p>
                <p>- Thưởng LĐTT (nếu có) =Lương tạm tứng x <asp:Label ID="lblHeSoK2" runat="server" Text=""></asp:Label></p>
                <p>- Thưởng CSTĐ (nếu có) =Lương tạm tứng x <asp:Label ID="lblHeSoK3" runat="server" Text=""></asp:Label></p>
                <p>- Người lao động mới vào làm việc tại công ty chưa có tiền thưởng tích lũy và tiền lương tạm ứng < 3 triệu đồng thì được công ty hỗ trợ tiền tết mỗi ngày làm việc tại công ty (từ ngày ký hợp đồng đến ngày nghỉ tết) là 30.000 đ/ngày. Mức tối thiểu là 500.000 đồng, mức tối đa là 3.000.000 đồng</p>
                <p>(Chỉ thưởng tối thiểu khi Tổng thưởng nhỏ hơn mức thưởng tối thiểu)</p>
                <p>(Các trường hợp sau đây không được xét thưởng: Đang bị kỷ luật, Hợp đồng thử việc-vụ việc, Tham gia ngừng việc tập thể)</p>
            </div>
            <div style="padding-bottom: 20px;padding-top: 20px;width:100%;">
                <img src="images/border_bottom.png" alt="IMG" width="100%">
            </div>
        </div>
    </div>
</asp:Content>
