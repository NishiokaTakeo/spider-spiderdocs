<%@ Page Title="Updates" Language="C#" MasterPageFile="~/mvc/view/Site.Master" AutoEventWireup="true"
    CodeBehind="Update.aspx.cs" Inherits="SpiderDocs_ClientControl.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 17px;
        }
        .style2
        {
            width: 119px;
        }
        .style5
        {
            width: 119px;
            height: 25px;
        }
        .style7
        {
            height: 25px;
        }
        .style8
        {
            height: 34px;
        }
        .style10
        {
            height: 94px;
        }
        .style11
        {
            width: 291px;
        }
        .style12
        {
            height: 25px;
            width: 291px;
        }
        .style13
        {
            height: 88px;
            width: 291px;
        }
        .style14
        {
            height: 88px;
        }
    </style>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>


        Updates

        <asp:GridView ID="GridView1" runat="server" Width="922px" 
            AutoGenerateColumns="False" DataSourceID="dtsUp" 
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" ForeColor="Black" GridLines="Horizontal" 
    DataKeyNames="id" AllowPaging="True" AllowSorting="True" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="20">
            <Columns>
                <asp:CommandField ShowSelectButton="True">
                <ItemStyle Width="60px" />
                </asp:CommandField>
                <asp:BoundField DataField="id" HeaderText="Id" InsertVisible="False" 
                    ReadOnly="True" SortExpression="id" >
                <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="version" HeaderText="Version" 
                    SortExpression="version" />
                <asp:BoundField DataField="url_download" HeaderText="URL Update" 
                    SortExpression="url_download" />
                <asp:BoundField DataField="url_details" HeaderText="URL_details" 
                    SortExpression="url_details" Visible="False" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Left" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#FFD1C6" Font-Bold="False" ForeColor="#111111" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>

        <br />
        <br />

        Updates
        <hr/>
        <table width="876" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style2">
                    ID</td>
                <td width="13">
                    &nbsp;</td>
                <td class="style11">
                    Version</td>
                <td width="8">
                    &nbsp;</td>
                <td width="190">
                    URL</td>
                <td width="8">
                    &nbsp;</td>
                <td width="193">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:TextBox runat="server" MaxLength="8" CssClass="textEntry" Height="16px" 
                        Width="50px" ID="txtId" Enabled="False" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="style7">
                    </td>
                <td class="style12">
                    <asp:TextBox runat="server" CssClass="textEntry" Height="16px" Width="186px"
                        ID="txtVersion"></asp:TextBox>
                </td>
                <td class="style7">
                    </td>
                <td colspan="3" class="style7">
                    <asp:TextBox runat="server" CssClass="textEntry" Height="16px" Width="432px" 
                        ID="txtUrl">http://spiderdocs.spiderdevelopments.com.au:5321/Updates/setup_</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10" colspan="7">
                    <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload" runat="server" Height="63px" 
                        onuploadcomplete="AjaxFileUpload_UploadComplete" 
                        MaximumNumberOfFiles="3" />
                    </td>
            </tr>
            <tr>
                <td class="style14">
                    </td>
                <td class="style14">
                    </td>
                <td class="style13">
                    </td>
                <td class="style14">
                    </td>
                <td class="style14">
                    </td>
                <td class="style14">
                    </td>
                <td class="style14">
                    </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Button runat="server" Text="New" ID="btnNewUpdate" OnClick="btnNew_Click">
                    </asp:Button>
                    <asp:Button runat="server" Text="Save" ID="btnSaveUpdate" 
                        OnClick="btnSave_Click">
                    </asp:Button>
                </td>
                <td>
                    &nbsp;</td>
                <td colspan="5">
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />


         <asp:SqlDataSource ID="dtsUp" runat="server" 
    ConnectionString="<%$ ConnectionStrings:spiderdocs_conn %>" 
    SelectCommand="SELECT [id], [version], [url_download], [url_details] FROM [updates] ORDER BY [version]"></asp:SqlDataSource>

                        </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
