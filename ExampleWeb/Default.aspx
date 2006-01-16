<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
		<h2>Trees </h2>
		<asp:GridView ID="TreeGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
			DataSourceID="TreeSource">
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
			</Columns>
		</asp:GridView>
		<br />
		<h2>Apples on tree </h2>
		<asp:GridView ID="ApplesGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
			DataSourceID="ApplesSource">
			<Columns>
				<asp:BoundField DataField="SeedCount" HeaderText="SeedCount" SortExpression="SeedCount" />
			</Columns>
		</asp:GridView>
		<br />
		<asp:ObjectDataSource ID="TreeSource" runat="server" SelectMethod="GetTrees" TypeName="Dao">
		</asp:ObjectDataSource>
		<asp:ObjectDataSource ID="ApplesSource" runat="server" SelectMethod="GetApples" TypeName="Dao">
			<SelectParameters>
				<asp:ControlParameter ControlID="TreeGrid" Name="treeId" PropertyName="SelectedValue"
					Type="Int32" />
			</SelectParameters>
		</asp:ObjectDataSource>
	</form>
</body>
</html>
