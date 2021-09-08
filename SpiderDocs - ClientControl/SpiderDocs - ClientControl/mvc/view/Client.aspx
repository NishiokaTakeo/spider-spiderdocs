<%@ Page Title="Clients" Language="C#" MasterPageFile="~/mvc/view/Site.master" AutoEventWireup="true"
    CodeBehind="Client.aspx.cs" Inherits="SpiderDocs_ClientControl._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style2
        {
            height: 17px;
        }
        .style3
        {
            width: 15px;
        }
        .style4
        {
            height: 20px;
            width: 15px;
        }
        .style5
        {
            height: 17px;
            width: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>


    
            CLIENTS
        

        
    

        <asp:GridView ID="GridView1" runat="server" Width="922px" 
            AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="sqlDataSource" 
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" ForeColor="Black" GridLines="Horizontal" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" AllowSorting="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" 
                    SortExpression="id" />
                <asp:BoundField DataField="client_name" HeaderText="Client" 
                    SortExpression="client_name" />
                <asp:BoundField DataField="date_activated" HeaderText="Activated at" 
                    SortExpression="date_activated" />
                <asp:CheckBoxField DataField="enable" HeaderText="Enable" 
                    SortExpression="enable" />
                <asp:BoundField DataField="product_key" HeaderText="Product key" 
                    SortExpression="product_key" />
                <asp:BoundField DataField="current_version" HeaderText="Current Version" 
                    SortExpression="current_version" />
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

         <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" 
        ActiveTabIndex="0" Width="919px" Height="258px">
             <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                 <HeaderTemplate>
                     Client Details
                 </HeaderTemplate>
                 <ContentTemplate>
                     
                     <table width="876" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="192">ID</td>
    <td width="13">&nbsp;</td>
    <td width="226">Client</td>
    <td width="8">&nbsp;</td>
    <td width="190">Product Key</td>
    <td class="style3">&nbsp;</td>
    <td width="193">&nbsp;</td>
  </tr>
  <tr>
    <td>
        <asp:TextBox ID="txtClientId" runat="server" CssClass="textEntry" Height="16px"  Width="186px" MaxLength="8"></asp:TextBox>
      </td>
    <td>&nbsp;</td>
    <td>
        <asp:TextBox ID="txtClientName" runat="server" CssClass="textEntry" 
            Height="16px" Width="186px"></asp:TextBox>
      </td>
    <td>&nbsp;</td>
    <td>
        <asp:TextBox ID="txtClientProductKey" runat="server" CssClass="textEntry" 
            Height="16px" MaxLength="9" Width="186px"></asp:TextBox>
      </td>
    <td class="style3">&nbsp;</td>
    <td>Enable<asp:CheckBox ID="ckActive" runat="server" />
      </td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td class="style3">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>Current Version</td>
    <td>&nbsp;</td>
    <td>Activated at</td>
    <td>&nbsp;</td>
    <td>Version availables</td>
    <td class="style3">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td class="style1">
        <asp:TextBox ID="txtClientCurVersion" runat="server" CssClass="textEntry" 
            Height="16px" MaxLength="5" Width="186px"></asp:TextBox>
      </td>
    <td class="style1"></td>
    <td class="style1">
        <asp:TextBox ID="txtDateActivade" runat="server" CssClass="textEntry" 
            Enabled="False" Height="16px" ReadOnly="True" Width="186px"></asp:TextBox>
      </td>
    <td class="style1"></td>
    <td class="style1">
        <ajaxToolkit:ComboBox ID="cboUpdateAvailables" runat="server" MaxLength="0" Width="160px" AppendDataBoundItems="true"
            DropDownStyle="DropDownList" Height="15px">
        </ajaxToolkit:ComboBox>
      </td>
    <td class="style4"></td>
    <td class="style1">
        &nbsp;</td>
  </tr>
  <tr>
    <td class="style2"></td>
    <td class="style2"></td>
    <td class="style2"></td>
    <td class="style2"></td>
    <td class="style2"></td>
    <td class="style5"></td>
    <td class="style2">&nbsp;</td>
  </tr>
  <tr>
    <td>
        <asp:Button ID="btnNew" runat="server" onclick="btnNew_Click" Text="New" />
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
      </td>
    <td>&nbsp;</td>
    <td colspan="5">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
      </td>
  </tr>
</table>


                 </ContentTemplate>
             </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                 <HeaderTemplate>
                     Update Checks
                 </HeaderTemplate>
                 <ContentTemplate>
                     <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                         BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                         CellPadding="4" DataKeyNames="id" DataSourceID="dtsUpdateCheck" 
                         ForeColor="Black" GridLines="Horizontal" Height="70px" Width="899px" 
                         AllowPaging="True" AllowSorting="True" PageSize="8">
                         <Columns>
                             <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" 
                                 ReadOnly="True" SortExpression="id" />
                             <asp:BoundField DataField="client_id" HeaderText="Client ID" 
                                 SortExpression="client_id" />
                             <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" />
                         </Columns>
                         <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                         <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" 
                             HorizontalAlign="Left" />
                         <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                         <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                         <SortedAscendingCellStyle BackColor="#F7F7F7" />
                         <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                         <SortedDescendingCellStyle BackColor="#E5E5E5" />
                         <SortedDescendingHeaderStyle BackColor="#242121" />
                     </asp:GridView>
                     <asp:SqlDataSource ID="dtsUpdateCheck" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:spiderdocs_conn %>" 
                         SelectCommand="SELECT [id], [client_id], [date] FROM [client_update_check] WHERE ([client_id] = @client_id) ORDER BY [date] DESC">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="GridView1" DefaultValue="0" Name="client_id" 
                                 PropertyName="SelectedValue" Type="String" />
                         </SelectParameters>
                     </asp:SqlDataSource>
                 </ContentTemplate>
             </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                 <HeaderTemplate>
                     Version Historic
                 </HeaderTemplate>
                 <ContentTemplate>
                     <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                         AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                         BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                         DataSourceID="dtsVersionHistoric" ForeColor="Black" GridLines="Horizontal" 
                         Height="70px" PageSize="8" Width="899px">
                         <Columns>
                             <asp:BoundField DataField="version" HeaderText="Version" 
                                 SortExpression="version" />
                             <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" 
                                 NullDisplayText="Pending..." />
                         </Columns>
                         <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                         <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" 
                             HorizontalAlign="Left" />
                         <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                         <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                         <SortedAscendingCellStyle BackColor="#F7F7F7" />
                         <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                         <SortedDescendingCellStyle BackColor="#E5E5E5" />
                         <SortedDescendingHeaderStyle BackColor="#242121" />
                     </asp:GridView>
                     <asp:SqlDataSource ID="dtsVersionHistoric" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:spiderdocs_conn %>" SelectCommand="select a.date,b.version
from client_update a
inner join updates b on a.update_id =b.id
WHERE (a.client_id = @client_id) ORDER BY  a.date DESC">
                         <SelectParameters>
                             <asp:ControlParameter ControlID="GridView1" DefaultValue="0" Name="client_id" 
                                 PropertyName="SelectedValue" Type="String" />
                         </SelectParameters>
                     </asp:SqlDataSource>
                     <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                 </ContentTemplate>
             </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>

         <br />
            <asp:SqlDataSource ID="sqlDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:spiderdocs_conn %>" 
            SelectCommand="SELECT [id], [client_name], [date_activated], [enable], [product_key], [current_version] FROM [client] ORDER BY [client_name]">
        </asp:SqlDataSource>





   

    

                    </ContentTemplate>
        </asp:UpdatePanel>


   
            </asp:Content>
